using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Harbour.Pages;

public class Index_Tests : HarbourWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
