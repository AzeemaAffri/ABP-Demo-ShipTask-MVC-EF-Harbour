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

public class CreateModalModel : HarbourPageModel
{
    [BindProperty]
    public CreateFleetViewModel Fleet { get; set; }

    public List<SelectListItem> Ships { get; set; }

    private readonly IFleetAppService _fleetAppService;

    public CreateModalModel(
        IFleetAppService fleetAppService)
    {
        _fleetAppService = fleetAppService;
    }

    public async Task OnGetAsync()
    {
        Fleet = new CreateFleetViewModel();

        var shipLookup = await _fleetAppService.GetShipLookupAsync();
        Ships = shipLookup.Items
            .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            .ToList();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _fleetAppService.CreateAsync(
            ObjectMapper.Map<CreateFleetViewModel, CreateUpdateFleetDto>(Fleet)
            );
        return NoContent();
    }

    public class CreateFleetViewModel
    {
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
