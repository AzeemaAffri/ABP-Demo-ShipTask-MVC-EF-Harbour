using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Harbour.Fleets;

public interface IFleetAppService :
    ICrudAppService< //Defines CRUD methods
        FleetDto, //Used to show fleets
        Guid, //Primary key of the fleet entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateFleetDto> //Used to create/update a fleet
{
    // ADD the NEW METHOD
    Task<ListResultDto<ShipLookupDto>> GetShipLookupAsync();
}
