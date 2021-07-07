using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DigitalDiscounts.Data;
using Volo.Abp.DependencyInjection;

namespace DigitalDiscounts.EntityFrameworkCore
{
    public class EntityFrameworkCoreDigitalDiscountsDbSchemaMigrator
        : IDigitalDiscountsDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreDigitalDiscountsDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the DigitalDiscountsMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<DigitalDiscountsMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}