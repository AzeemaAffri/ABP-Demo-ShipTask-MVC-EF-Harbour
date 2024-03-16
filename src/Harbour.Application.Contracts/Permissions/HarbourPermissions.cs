namespace Harbour.Permissions;

public static class HarbourPermissions
{
    public const string GroupName = "Harbour";

    public static class Fleets
    {
        public const string Default = GroupName + ".Fleets";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    // *** ADDED a NEW NESTED CLASS ***
    public static class Ships
    {
        public const string Default = GroupName + ".Ships";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
}
