using Harbour.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Harbour.Permissions;

public class HarbourPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var harbourGroup = context.AddGroup(HarbourPermissions.GroupName, L("Permission:Harbour"));

        var fleetsPermission = harbourGroup.AddPermission(HarbourPermissions.Fleets.Default, L("Permission:Fleets"));
        fleetsPermission.AddChild(HarbourPermissions.Fleets.Create, L("Permission:Fleets.Create"));
        fleetsPermission.AddChild(HarbourPermissions.Fleets.Edit, L("Permission:Fleets.Edit"));
        fleetsPermission.AddChild(HarbourPermissions.Fleets.Delete, L("Permission:Fleets.Delete"));
        var shipsPermission = harbourGroup.AddPermission(
    HarbourPermissions.Ships.Default, L("Permission:ships"));
        shipsPermission.AddChild(
            HarbourPermissions.Ships.Create, L("Permission:Ships.Create"));
        shipsPermission.AddChild(
            HarbourPermissions.Ships.Edit, L("Permission:Ships.Edit"));
        shipsPermission.AddChild(
            HarbourPermissions.Ships.Delete, L("Permission:Ships.Delete"));

    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<HarbourResource>(name);
    }
}
