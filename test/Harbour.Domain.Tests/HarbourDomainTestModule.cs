using Volo.Abp.Modularity;

namespace Harbour;

[DependsOn(
    typeof(HarbourDomainModule),
    typeof(HarbourTestBaseModule)
)]
public class HarbourDomainTestModule : AbpModule
{

}
