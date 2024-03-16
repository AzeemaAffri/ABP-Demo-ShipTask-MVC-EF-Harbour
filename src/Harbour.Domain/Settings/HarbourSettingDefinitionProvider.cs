using Volo.Abp.Settings;

namespace Harbour.Settings;

public class HarbourSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(HarbourSettings.MySetting1));
    }
}
