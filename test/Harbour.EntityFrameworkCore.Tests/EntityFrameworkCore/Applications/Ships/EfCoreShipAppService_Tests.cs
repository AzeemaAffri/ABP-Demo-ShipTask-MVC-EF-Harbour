using Harbour.Ships;
using Xunit;

namespace Harbour.EntityFrameworkCore.Applications.Ships;

[Collection(HarbourTestConsts.CollectionDefinitionName)]
public class EfCoreShipAppService_Tests : ShipAppService_Tests<HarbourEntityFrameworkCoreTestModule>
{

}
