using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DigitalDiscounts.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;

namespace DigitalDiscounts.Stores
{
    public class EfCoreStoreRepository : EfCoreRepository<DigitalDiscountsDbContext, Store, Guid>, IStoreRepository
    {
        public EfCoreStoreRepository(IDbContextProvider<DigitalDiscountsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Store> FindByNameAsync(string name)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(store => store.Name == name);
        }

        public async Task<List<Store>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
        {
            var dbSet = await GetDbSetAsync();

            return await dbSet
                .WhereIf(!filter.IsNullOrWhiteSpace(), store => store.Name.Contains(filter)).OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
