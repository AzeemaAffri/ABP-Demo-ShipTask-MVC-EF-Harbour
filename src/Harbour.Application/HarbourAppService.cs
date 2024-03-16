using System;
using System.Collections.Generic;
using System.Text;
using Harbour.Localization;
using Volo.Abp.Application.Services;

namespace Harbour;

/* Inherit your application services from this class.
 */
public abstract class HarbourAppService : ApplicationService
{
    protected HarbourAppService()
    {
        LocalizationResource = typeof(HarbourResource);
    }
}
