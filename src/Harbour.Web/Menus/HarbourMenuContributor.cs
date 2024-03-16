using System.Threading.Tasks;
using Harbour.Localization;
using Harbour.MultiTenancy;
using Harbour.Permissions;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace Harbour.Web.Menus;

public class HarbourMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<HarbourResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                HarbourMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);
        context.Menu.AddItem(
        new ApplicationMenuItem(
            "Harbour",
            l["Menu:Harbour"],
            icon: "fas fa-ship"
        ).AddItem(
            new ApplicationMenuItem(
                "Harbour.Fleetss",
                l["Menu:Fleets"],
                url: "/Fleets"
            ).RequirePermissions(HarbourPermissions.Fleets.Default) // Check the permission!
        ).AddItem( // ADDED THE NEW "SHIPS" MENU ITEM UNDER THE "HARBOUR" MENU
        new ApplicationMenuItem(
            "Harbour.Ships",
            l["Menu:Ships"],
            url: "/Ships"
        ).RequirePermissions(HarbourPermissions.Ships.Default)
    )
    );



        return Task.CompletedTask;
    }
}
