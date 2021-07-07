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

            //Define your own permissions here. Example:
            //myGroup.AddPermission(DigitalDiscountsPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<DigitalDiscountsResource>(name);
        }
    }
}
