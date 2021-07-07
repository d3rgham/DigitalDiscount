using System.Threading.Tasks;

namespace DigitalDiscounts.Data
{
    public interface IDigitalDiscountsDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
