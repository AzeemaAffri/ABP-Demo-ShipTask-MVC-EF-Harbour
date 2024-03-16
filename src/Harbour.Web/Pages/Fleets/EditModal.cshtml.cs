using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Harbour.Fleets;
using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Harbour.Web.Pages.Fleets;

public class EditModalModel : HarbourPageModel
{
    [BindProperty]
    public EditFleetViewModel Fleet { get; set; }

    public List<SelectListItem> Ships { get; set; }

    private readonly IFleetAppService _fleetAppService;

    public EditModalModel(IFleetAppService fleetAppService)
    {
        _fleetAppService = fleetAppService;
    }

    public async Task OnGetAsync(Guid id)
    {
        var fleetDto = await _fleetAppService.GetAsync(id);
        Fleet = ObjectMapper.Map<FleetDto, EditFleetViewModel>(fleetDto);

        var shipLookup = await _fleetAppService.GetShipLookupAsync();
        Ships = shipLookup.Items
            .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            .ToList();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _fleetAppService.UpdateAsync(
            Fleet.Id,
            ObjectMapper.Map<EditFleetViewModel, CreateUpdateFleetDto>(Fleet)
        );

        return NoContent();
    }

    public class EditFleetViewModel
    {
        [HiddenInput]
        public Guid Id { get; set; }

        [SelectItems(nameof(Ships))]
        [DisplayName("Ship")]
        public Guid ShipId { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public HarbourShips HarbourShip { get; set; } = HarbourShips.Suezmax;

      
    }
}
