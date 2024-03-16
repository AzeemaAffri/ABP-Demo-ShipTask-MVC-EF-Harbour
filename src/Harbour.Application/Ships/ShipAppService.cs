using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harbour.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Harbour.Ships;

[Authorize(HarbourPermissions.Ships.Default)]
public class ShipAppService : HarbourAppService, IShipAppService
{
    private readonly IShipRepository _shipRepository;
    private readonly ShipManager _shipManager;

    public ShipAppService(
        IShipRepository shipRepository,
        ShipManager shipManager)
    {
        _shipRepository = shipRepository;
        _shipManager = shipManager;
    }
    public async Task<ShipDto> GetAsync(Guid id)
    {
        var ship = await _shipRepository.GetAsync(id);
        return ObjectMapper.Map<Ship, ShipDto>(ship);
    }
    public async Task<PagedResultDto<ShipDto>> GetListAsync(GetShipListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Ship.Name);
        }

        var ships = await _shipRepository.GetListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting,
            input.Filter
        );

        var totalCount = input.Filter == null
            ? await _shipRepository.CountAsync()
            : await _shipRepository.CountAsync(
                ship=> ship.Name.Contains(input.Filter));

        return new PagedResultDto<ShipDto>(
            totalCount,
            ObjectMapper.Map<List<Ship>, List<ShipDto>>(ships)
        );
    }
    [Authorize(HarbourPermissions.Ships.Create)]
    public async Task<ShipDto> CreateAsync(CreateShipDto input)
    {
        var ship = await _shipManager.CreateAsync(
            input.Name,
            input.Type,
            input.YearBuilt,
            input.PassengerCapacity
        );

        await _shipRepository.InsertAsync(ship);

        return ObjectMapper.Map<Ship, ShipDto>(ship);
    }
    [Authorize(HarbourPermissions.Ships.Edit)]
    public async Task UpdateAsync(Guid id, UpdateShipDto input)
    {
        var ship = await _shipRepository.GetAsync(id);

        if (ship.Name != input.Name)
        {
            await _shipManager.ChangeNameAsync(ship, input.Name);
        }

        ship.Type = input.Type;
        ship.YearBuilt = input.YearBuilt;
        ship.PassengerCapacity = input.PassengerCapacity;

        await _shipRepository.UpdateAsync(ship);
    }
    [Authorize(HarbourPermissions.Ships.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _shipRepository.DeleteAsync(id);
    }




    //...SERVICE METHODS WILL COME HERE...
}
