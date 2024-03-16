using Harbour.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Harbour.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class HarbourController : AbpControllerBase
{
    protected HarbourController()
    {
        LocalizationResource = typeof(HarbourResource);
    }
}
