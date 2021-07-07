using Volo.Abp.Modularity;

namespace DigitalDiscounts
{
    [DependsOn(
        typeof(DigitalDiscountsApplicationModule),
        typeof(DigitalDiscountsDomainTestModule)
        )]
    public class DigitalDiscountsApplicationTestModule : AbpModule
    {

    }
}