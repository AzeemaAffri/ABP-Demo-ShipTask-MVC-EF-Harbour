using AutoMapper;
using Harbour.Fleets;
using Harbour.Ships;

namespace Harbour.Web;

public class HarbourWebAutoMapperProfile : Profile
{
    public HarbourWebAutoMapperProfile()
    {
        CreateMap<FleetDto, CreateUpdateFleetDto>();

        CreateMap<Pages.Ships.CreateModalModel.CreateShipViewModel,
                    CreateShipDto>();

        CreateMap<ShipDto, Pages.Ships.EditModalModel.EditShipViewModel>();
        CreateMap<Pages.Ships.EditModalModel.EditShipViewModel,
                    UpdateShipDto>();
        CreateMap<Pages.Fleets.CreateModalModel.CreateFleetViewModel, CreateUpdateFleetDto>();
        CreateMap<FleetDto, Pages.Fleets.EditModalModel.EditFleetViewModel>();
        CreateMap<Pages.Fleets.EditModalModel.EditFleetViewModel, CreateUpdateFleetDto>();

    }
}
