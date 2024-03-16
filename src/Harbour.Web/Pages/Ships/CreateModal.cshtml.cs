using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Harbour.Ships;
using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Harbour.Web.Pages.Ships;

public class CreateModalModel : HarbourPageModel
{
    [BindProperty]
    public CreateShipViewModel Ship { get; set; }

    private readonly IShipAppService _shipAppService;

    public CreateModalModel(IShipAppService shipAppService)
    {
        _shipAppService = shipAppService;
    }

    public void OnGet()
    {
        Ship= new CreateShipViewModel();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateShipViewModel, CreateShipDto>(Ship);
        await _shipAppService.CreateAsync(dto);
        return NoContent();
    }

    public class CreateShipViewModel
    {
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
