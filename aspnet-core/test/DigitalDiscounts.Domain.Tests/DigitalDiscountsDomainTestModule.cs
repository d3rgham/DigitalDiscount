using DigitalDiscounts.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace DigitalDiscounts
{
    [DependsOn(
        typeof(DigitalDiscountsEntityFrameworkCoreTestModule)
        )]
    public class DigitalDiscountsDomainTestModule : AbpModule
    {

    }
}