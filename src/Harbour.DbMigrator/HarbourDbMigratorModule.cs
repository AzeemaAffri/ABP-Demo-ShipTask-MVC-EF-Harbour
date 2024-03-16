using Harbour.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Harbour.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(HarbourEntityFrameworkCoreModule),
    typeof(HarbourApplicationContractsModule)
    )]
public class HarbourDbMigratorModule : AbpModule
{
}
