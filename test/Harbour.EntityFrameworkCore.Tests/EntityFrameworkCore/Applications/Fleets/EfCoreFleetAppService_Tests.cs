using Harbour.Fleets;
using Xunit;

namespace Harbour.EntityFrameworkCore.Applications.Fleets;

[Collection(HarbourTestConsts.CollectionDefinitionName)]
public class EfCoreFleetAppService_Tests : FleetAppService_Tests<HarbourEntityFrameworkCoreTestModule>
{

}
