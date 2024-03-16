using Harbour.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Harbour.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class HarbourPageModel : AbpPageModel
{
    protected HarbourPageModel()
    {
        LocalizationResourceType = typeof(HarbourResource);
    }
}
