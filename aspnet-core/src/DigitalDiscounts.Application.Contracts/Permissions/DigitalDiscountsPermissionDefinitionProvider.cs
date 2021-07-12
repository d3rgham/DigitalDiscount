using DigitalDiscounts.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace DigitalDiscounts.Permissions
{
    public class DigitalDiscountsPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var digitalDiscountGroup = context.AddGroup(DigitalDiscountsPermissions.GroupName, L("Permission:DigitalDiscount"));

            var storesPermission = digitalDiscountGroup.AddPermission(DigitalDiscountsPermissions.Stores.Default, L("Permission:Stores"));
            storesPermission.AddChild(DigitalDiscountsPermissions.Stores.Create, L("Permission:Stores.Create"));
            storesPermission.AddChild(DigitalDiscountsPermissions.Stores.Edit, L("Permission:Stores.Edit"));
            storesPermission.AddChild(DigitalDiscountsPermissions.Stores.Delete, L("Permission:Stores.Delete"));

            var licensesPermission = digitalDiscountGroup.AddPermission(DigitalDiscountsPermissions.Licenses.Default, L("Permission:Licenses"));
            licensesPermission.AddChild(DigitalDiscountsPermissions.Licenses.Create, L("Permission:Licenses.Create"));
            licensesPermission.AddChild(DigitalDiscountsPermissions.Licenses.Edit, L("Permission:Licenses.Edit"));
            licensesPermission.AddChild(DigitalDiscountsPermissions.Licenses.Delete, L("Permission:Licenses.Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<DigitalDiscountsResource>(name);
        }
    }
}
