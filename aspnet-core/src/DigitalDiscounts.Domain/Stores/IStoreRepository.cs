using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DigitalDiscounts.Stores
{
    public interface IStoreRepository : IRepository<Store, Guid>
    {
        Task<Store> FindByNameAsync(string name);

        Task<List<Store>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null);
    }
}
