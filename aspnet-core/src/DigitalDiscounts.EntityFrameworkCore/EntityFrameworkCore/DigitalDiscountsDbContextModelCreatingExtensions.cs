using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace DigitalDiscounts.EntityFrameworkCore
{
    public static class DigitalDiscountsDbContextModelCreatingExtensions
    {
        public static void ConfigureDigitalDiscounts(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(DigitalDiscountsConsts.DbTablePrefix + "YourEntities", DigitalDiscountsConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});
        }
    }
}