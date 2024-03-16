using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Harbour.Ships;
using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Harbour.Web.Pages.Ships;

public class EditModalModel : HarbourPageModel
{
    [BindProperty]
    public EditShipViewModel Ship { get; set; }

    private readonly IShipAppService _shipAppService;

    public EditModalModel(IShipAppService shipAppService)
    {
        _shipAppService = shipAppService;
    }

    public async Task OnGetAsync(Guid id)
    {
        var shipDto = await _shipAppService.GetAsync(id);
        Ship = ObjectMapper.Map<ShipDto, EditShipViewModel>(shipDto);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _shipAppService.UpdateAsync(
            Ship.Id,
            ObjectMapper.Map<EditShipViewModel, UpdateShipDto>(Ship)
        );

        return NoContent();
    }

    public class EditShipViewModel
    {
        [HiddenInput]
        public Guid Id { get; set; }

        [Required]
        [StringLength(ShipConsts.MaxNameLength)]
        public string Name { get; set; } = string.Empty;

        [Required]

        public string Type { get; set; }

        [Required]

        public int YearBuilt { get; set; }


        [TextArea]
        public int PassengerCapacity { get; set; }
    }
}

