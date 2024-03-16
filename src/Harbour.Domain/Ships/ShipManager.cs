using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Harbour.Ships;

public class ShipManager : DomainService
{
    private readonly IShipRepository _shipRepository;

    public ShipManager(IShipRepository shipRepository)
    {
        _shipRepository = shipRepository;
    }

    public async Task<Ship> CreateAsync(
        string name,
        string type,
        int yearBuilt,
        int passengerCapacity)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));

        var existingShip = await _shipRepository.FindByNameAsync(name);
        if (existingShip != null)
        {
            throw new ShipAlreadyExistsException(name);
        }

        return new Ship(
            GuidGenerator.Create(),
            name,
            type,
            yearBuilt,
         passengerCapacity

        );
    }

    public async Task ChangeNameAsync(
        Ship ship,
        string newName)
    {
        Check.NotNull(ship, nameof(ship));
        Check.NotNullOrWhiteSpace(newName, nameof(newName));

        var existingShip= await _shipRepository.FindByNameAsync(newName);
        if (existingShip != null && existingShip.Id != ship.Id)
        {
            throw new ShipAlreadyExistsException(newName);
        }

        ship.ChangeName(newName);
    }
}
