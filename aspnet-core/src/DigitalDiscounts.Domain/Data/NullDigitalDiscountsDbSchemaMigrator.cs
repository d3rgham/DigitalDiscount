using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace DigitalDiscounts.Data
{
    /* This is used if database provider does't define
     * IDigitalDiscountsDbSchemaMigrator implementation.
     */
    public class NullDigitalDiscountsDbSchemaMigrator : IDigitalDiscountsDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}