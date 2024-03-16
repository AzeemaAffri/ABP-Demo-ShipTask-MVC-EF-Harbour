using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Harbour.Ships;
using Harbour.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Harbour.Fleets;

[Authorize(HarbourPermissions.Fleets.Default)]
public class FleetAppService :
    CrudAppService<
        Fleet, //The Fleet entity
        FleetDto, //Used to show fleets
        Guid, //Primary key of the fleet entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateFleetDto>, //Used to create/update a fleet
    IFleetAppService //implement the IFleetAppService
{
    private readonly IShipRepository _shipRepository;

    public FleetAppService(
        IRepository<Fleet, Guid> repository,
        IShipRepository shipRepository)
        : base(repository)
    {
        _shipRepository = shipRepository;
        GetPolicyName = HarbourPermissions.Fleets.Default;
        GetListPolicyName = HarbourPermissions.Fleets.Default;
        CreatePolicyName = HarbourPermissions.Fleets.Create;
        UpdatePolicyName = HarbourPermissions.Fleets.Edit;
        DeletePolicyName = HarbourPermissions.Fleets.Delete;
    }

    public override async Task<FleetDto> GetAsync(Guid id)
    {
        //Get the IQueryable<Fleet> from the repository
        var queryable = await Repository.GetQueryableAsync();

        //Prepare a query to join fleets and ships
        var query = from fleet in queryable
                    join ship in await _shipRepository.GetQueryableAsync() on fleet.ShipId equals ship.Id
                    where fleet.Id == id
                    select new { fleet, ship };

        //Execute the query and get the fleet with ship
        var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
        if (queryResult == null)
        {
            throw new EntityNotFoundException(typeof(Fleet), id);
        }

        var fleetDto = ObjectMapper.Map<Fleet, FleetDto>(queryResult.fleet);
        fleetDto.ShipName = queryResult.ship.Name;
        return fleetDto;
    }

    public override async Task<PagedResultDto<FleetDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        //Get the IQueryable<Fleet> from the repository
        var queryable = await Repository.GetQueryableAsync();

        //Prepare a query to join fleets and ships
        var query = from fleet in queryable
                    join ship in await _shipRepository.GetQueryableAsync() on fleet.ShipId equals ship.Id
                    select new { fleet, ship };

        //Paging
        query = query
            .OrderBy(NormalizeSorting(input.Sorting))
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        //Execute the query and get a list
        var queryResult = await AsyncExecuter.ToListAsync(query);

        //Convert the query result to a list of FleetDto objects
        var fleetDtos = queryResult.Select(x =>
        {
            var fleetDto = ObjectMapper.Map<Fleet, FleetDto>(x.fleet);
            fleetDto.ShipName = x.ship.Name;
            return fleetDto;
        }).ToList();

        //Get the total count with another query
        var totalCount = await Repository.GetCountAsync();

        return new PagedResultDto<FleetDto>(
            totalCount,
            fleetDtos
        );
    }

    public async Task<ListResultDto<ShipLookupDto>> GetShipLookupAsync()
    {
        var ships = await _shipRepository.GetListAsync();

        return new ListResultDto<ShipLookupDto>(
            ObjectMapper.Map<List<Ship>, List<ShipLookupDto>>(ships)
        );
    }

    private static string NormalizeSorting(string sorting)
    {
        if (sorting.IsNullOrEmpty())
        {
            return $"fleet.{nameof(Fleet.Name)}";
        }

        if (sorting.Contains("shipName", StringComparison.OrdinalIgnoreCase))
        {
            return sorting.Replace(
                "shipName",
                "ship.Name",
                StringComparison.OrdinalIgnoreCase
            );
        }

        return $"fleet.{sorting}";
    }
}
