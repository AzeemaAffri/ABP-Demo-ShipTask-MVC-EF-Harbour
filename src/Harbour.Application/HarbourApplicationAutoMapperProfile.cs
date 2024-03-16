using Harbour.Fleets;
using AutoMapper;
using Harbour.Ships;

namespace Harbour;

public class HarbourApplicationAutoMapperProfile : Profile
{
    public HarbourApplicationAutoMapperProfile()
    {
        CreateMap<Fleet, FleetDto>();
        CreateMap<CreateUpdateFleetDto, Fleet>();
        CreateMap<Ship, ShipDto>();
        CreateMap<Ship, ShipLookupDto>();


    }
}
