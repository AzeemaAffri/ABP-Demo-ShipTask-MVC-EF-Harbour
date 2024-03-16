using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Harbour.Data;

/* This is used if database provider does't define
 * IHarbourDbSchemaMigrator implementation.
 */
public class NullHarbourDbSchemaMigrator : IHarbourDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
