using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Harbour.Ships;

public interface IShipRepository : IRepository<Ship, Guid>
{
    Task<Ship> FindByNameAsync(string name);

    Task<List<Ship>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null
    );
}
