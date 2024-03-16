using System;
using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;

namespace Harbour.Ships;

public abstract class ShipAppService_Tests<TStartupModule> : HarbourApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly IShipAppService _shipAppService;

    protected ShipAppService_Tests()
    {
        _shipAppService = GetRequiredService<IShipAppService>();
    }

    [Fact]
    public async Task Should_Get_All_Ships_Without_Any_Filter()
    {
        var result = await _shipAppService.GetListAsync(new GetShipListDto());

        result.TotalCount.ShouldBeGreaterThanOrEqualTo(2);
        result.Items.ShouldContain(ship => ship.Name == "Fishing Vessel");
        result.Items.ShouldContain(ship => ship.Name == "Offshore Vessel");
    }

    [Fact]
    public async Task Should_Get_Filtered_Ships()
    {
        var result = await _shipAppService.GetListAsync(
            new GetShipListDto { Filter = "Fishing" });

        result.TotalCount.ShouldBeGreaterThanOrEqualTo(1);
        result.Items.ShouldContain(ship => ship.Name == "Fishing Vessel");
        result.Items.ShouldNotContain(ship => ship.Name == "Offshore Vessel");
    }

    [Fact]
    public async Task Should_Create_A_New_Ship()
    {
        var shipDto = await _shipAppService.CreateAsync(
            new CreateShipDto
            {
                Name = "Passenger Ships",
                Type = "Ferries ",
                YearBuilt = 2009,
                PassengerCapacity = 100
            }
        );

        shipDto.Id.ShouldNotBe(Guid.Empty);
        shipDto.Name.ShouldBe("Passenger Ships");
    }

    [Fact]
    public async Task Should_Not_Allow_To_Create_Duplicate_Ship()
    {
        await Assert.ThrowsAsync<ShipAlreadyExistsException>(async () =>
        {
            await _shipAppService.CreateAsync(
                new CreateShipDto
                {
                    Name = "Fishing Vessel",
                    Type = "Offshore Vessel",
                    YearBuilt = 2000,
                    PassengerCapacity = 40
                }
            );
        });
    }

    //TODO: Test other methods...
}
