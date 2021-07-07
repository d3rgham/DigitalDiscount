using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DigitalDiscounts.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class DigitalDiscountsMigrationsDbContextFactory : IDesignTimeDbContextFactory<DigitalDiscountsMigrationsDbContext>
    {
        public DigitalDiscountsMigrationsDbContext CreateDbContext(string[] args)
        {
            DigitalDiscountsEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<DigitalDiscountsMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new DigitalDiscountsMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../DigitalDiscounts.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
