using DigitalDiscounts.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace DigitalDiscounts.Permissions
{
    public class DigitalDiscountsPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(DigitalDiscountsPermissions.GroupName);
            var digitalDiscountGroup = context.AddGroup(DigitalDiscountsPermissions.GroupName, L("Permission:DigitalDiscount"));

            //Define your own permissions here. Example:
            //myGroup.AddPermission(DigitalDiscountsPermissions.MyPermission1, L("Permission:MyPermission1"));

            var storesPermission = digitalDiscountGroup.AddPermission(DigitalDiscountsPermissions.Stores.Default, L("Permission:Stores"));
            storesPermission.AddChild(DigitalDiscountsPermissions.Stores.Create, L("Permission:Stores.Create"));
            storesPermission.AddChild(DigitalDiscountsPermissions.Stores.Edit, L("Permission:Stores.Edit"));
            storesPermission.AddChild(DigitalDiscountsPermissions.Stores.Delete, L("Permission:Stores.Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<DigitalDiscountsResource>(name);
        }
    }
}
