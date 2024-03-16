using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Harbour.Web;

[Dependency(ReplaceServices = true)]
public class HarbourBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Harbour";
}
