using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace DigitalDiscounts
{
    [Dependency(ReplaceServices = true)]
    public class DigitalDiscountsBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "DigitalDiscounts";
    }
}
