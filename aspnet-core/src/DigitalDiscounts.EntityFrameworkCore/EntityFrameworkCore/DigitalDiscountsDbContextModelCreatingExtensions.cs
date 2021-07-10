using Volo.Abp;
using DigitalDiscounts.Stores;
using DigitalDiscounts.Licenses;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

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

            builder.Entity<Store>(b =>
            {
                b.ToTable(DigitalDiscountsConsts.DbTablePrefix + "Stores", DigitalDiscountsConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).IsRequired().HasMaxLength(StoreConsts.MaxNameLength);
                b.HasIndex(x => x.Name);
            });

            builder.Entity<License>(b =>
            {
                b.ToTable(DigitalDiscountsConsts.DbTablePrefix + "Licenses", DigitalDiscountsConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Number).IsRequired();

                // ADD THE MAPPING FOR THE RELATION
                b.HasOne<Store>().WithMany().HasForeignKey(x => x.StoreId).IsRequired();
            });
        }
    }
}