using Xunit;

namespace Harbour.EntityFrameworkCore;

[CollectionDefinition(HarbourTestConsts.CollectionDefinitionName)]
public class HarbourEntityFrameworkCoreCollection : ICollectionFixture<HarbourEntityFrameworkCoreFixture>
{

}
