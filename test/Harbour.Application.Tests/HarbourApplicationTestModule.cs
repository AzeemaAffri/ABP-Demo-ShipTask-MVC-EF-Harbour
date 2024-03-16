using Volo.Abp.Modularity;

namespace Harbour;

[DependsOn(
    typeof(HarbourApplicationModule),
    typeof(HarbourDomainTestModule)
)]
public class HarbourApplicationTestModule : AbpModule
{

}
