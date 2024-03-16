
using System;
using System.Threading.Tasks;
using Harbour.Ships;
using Harbour.Fleets;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using System.Reflection;

namespace Harbour;

public class HarbourDataSeederContributor
    : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<Fleet, Guid> _fleetRepository;
    private readonly IShipRepository _shipRepository;
    private readonly ShipManager _shipManager;

    public HarbourDataSeederContributor(
        IRepository<Fleet, Guid> fleetRepository,
        IShipRepository shipRepository,
        ShipManager shipManager)
    {
        _fleetRepository = fleetRepository;
        _shipRepository = shipRepository;
        _shipManager = shipManager;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _fleetRepository.GetCountAsync() > 0)
        {
            return;
        }

        var fishing = await _shipRepository.InsertAsync(
            await _shipManager.CreateAsync(
                      "Fishing vessel ",
                      "commercial ",
                      2000,
                       40

            )
        );

        var offshore = await _shipRepository.InsertAsync(
            await _shipManager.CreateAsync(
                    "Offshore vessel ",
                    "Diving",
                    2003,
                    10

            )
        );

        await _fleetRepository.InsertAsync(
            new Fleet
            {
                ShipId = fishing.Id, // SET THE SHIP
                Name = "Container",
                HarbourShip = HarbourShips.Panamax

            },
            autoSave: true
        );

        await _fleetRepository.InsertAsync(
            new Fleet
            {
                ShipId = offshore.Id, // SET THE SHIP
                Name = " Bulk Carrier",
                HarbourShip = HarbourShips.Lakers

            },
            autoSave: true
        );
    }
}
