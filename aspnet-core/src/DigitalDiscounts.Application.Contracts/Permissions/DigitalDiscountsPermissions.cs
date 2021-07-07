namespace DigitalDiscounts.Permissions
{
    public static class DigitalDiscountsPermissions
    {
        public const string GroupName = "DigitalDiscounts";

        //Add your own permission names. Example:
        //public const string MyPermission1 = GroupName + ".MyPermission1";
        public static class Stores
        {
            public const string Default = GroupName + ".Stores";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }
    }
}