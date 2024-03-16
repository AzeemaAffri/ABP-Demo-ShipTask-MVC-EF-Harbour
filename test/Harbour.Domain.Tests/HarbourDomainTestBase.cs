using Volo.Abp.Modularity;

namespace Harbour;

/* Inherit from this class for your domain layer tests. */
public abstract class HarbourDomainTestBase<TStartupModule> : HarbourTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
