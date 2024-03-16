using System;
using System.Linq;
using System.Threading.Tasks;
using Harbour.Ships;
using Shouldly;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;
using Xunit;

namespace Harbour.Fleets;

public abstract class FleetAppService_Tests<TStartupModule> : HarbourApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly IFleetAppService _fleetAppService;
    private readonly IShipAppService _shipAppService;

    protected FleetAppService_Tests()
    {
        _fleetAppService = GetRequiredService<IFleetAppService>();
        _shipAppService = GetRequiredService<IShipAppService>();
    }

    [Fact]
    public async Task Should_Get_List_Of_Fleets()
    {
        //Act
        var result = await _fleetAppService.GetListAsync(
            new PagedAndSortedResultRequestDto()
        );

        //Assert
        result.TotalCount.ShouldBeGreaterThan(0);
        result.Items.ShouldContain(b => b.Name == "Container" &&
                                        b.ShipName == "Fishing vessel ");
    }

    [Fact]
    public async Task Should_Create_A_Valid_Fleet()
    {
        var ships = await _shipAppService.GetListAsync(new GetShipListDto());
        var firstShip = ships.Items.First();

        //Act
        var result = await _fleetAppService.CreateAsync(
            new CreateUpdateFleetDto
            {
                ShipId = firstShip.Id,
                Name = "New test fleet 42",
                HarbourShip = HarbourShips.Panamax

            }
        );

        //Assert
        result.Id.ShouldNotBe(Guid.Empty);
        result.Name.ShouldBe("New test fleet 42");
    }

    [Fact]
    public async Task Should_Not_Create_A_Fleet_Without_Name()
    {
        var exception = await Assert.ThrowsAsync<AbpValidationException>(async () =>
        {
            await _fleetAppService.CreateAsync(
                new CreateUpdateFleetDto
                {
                    Name = "",
                    HarbourShip = HarbourShips.Panamax

                }
            );
        });

        exception.ValidationErrors
            .ShouldContain(err => err.MemberNames.Any(m => m == "Name"));
    }
}
