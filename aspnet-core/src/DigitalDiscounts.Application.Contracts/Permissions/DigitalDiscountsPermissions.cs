namespace DigitalDiscounts.Permissions
{
    public static class DigitalDiscountsPermissions
    {
        public const string GroupName = "DigitalDiscounts";

        public static class Stores
        {
            public const string Default = GroupName + ".Stores";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }

        public static class Licenses
        {
            public const string Default = GroupName + ".Licenses";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }
    }
}

