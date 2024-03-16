using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Harbour.Ships;

public interface IShipAppService : IApplicationService
{
    Task<ShipDto> GetAsync(Guid id);

    Task<PagedResultDto<ShipDto>> GetListAsync(GetShipListDto input);

    Task<ShipDto> CreateAsync(CreateShipDto input);

    Task UpdateAsync(Guid id, UpdateShipDto input);

    Task DeleteAsync(Guid id);
}
