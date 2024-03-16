using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Harbour.Data;
using Volo.Abp.DependencyInjection;

namespace Harbour.EntityFrameworkCore;

public class EntityFrameworkCoreHarbourDbSchemaMigrator
    : IHarbourDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreHarbourDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the HarbourDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<HarbourDbContext>()
            .Database
            .MigrateAsync();
    }
}
