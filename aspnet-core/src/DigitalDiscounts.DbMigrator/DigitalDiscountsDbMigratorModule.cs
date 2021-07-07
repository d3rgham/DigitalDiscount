using DigitalDiscounts.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace DigitalDiscounts.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(DigitalDiscountsEntityFrameworkCoreDbMigrationsModule),
        typeof(DigitalDiscountsApplicationContractsModule)
        )]
    public class DigitalDiscountsDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
