using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace DigitalDiscounts.EntityFrameworkCore
{
    [DependsOn(
        typeof(DigitalDiscountsEntityFrameworkCoreModule)
        )]
    public class DigitalDiscountsEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<DigitalDiscountsMigrationsDbContext>();
        }
    }
}
