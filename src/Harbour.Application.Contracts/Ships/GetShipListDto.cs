using Volo.Abp.Application.Dtos;

namespace Harbour.Ships;

public class GetShipListDto : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
}
