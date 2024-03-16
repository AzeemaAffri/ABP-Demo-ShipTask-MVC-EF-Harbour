using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Harbour.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Harbour.Ships;

public class EfCoreShipRepository
    : EfCoreRepository<HarbourDbContext, Ship, Guid>,
        IShipRepository
{
    public EfCoreShipRepository(
        IDbContextProvider<HarbourDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<Ship> FindByNameAsync(string name)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(ship => ship.Name == name);
    }

    public async Task<List<Ship>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                ship => ship.Name.Contains(filter)
                )
            .OrderBy(sorting)
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }
}
