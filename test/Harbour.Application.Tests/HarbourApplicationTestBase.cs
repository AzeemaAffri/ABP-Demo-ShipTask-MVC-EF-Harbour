using Volo.Abp.Modularity;

namespace Harbour;

public abstract class HarbourApplicationTestBase<TStartupModule> : HarbourTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
