using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DigitalDiscounts.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using DigitalDiscounts.Stores;
using Volo.Abp.Domain.Entities;



namespace DigitalDiscounts.Licenses
{
    public class EfCoreLicenseRepository : EfCoreRepository<DigitalDiscountsDbContext, License, Guid>, ILicenseRepository
    {
        private readonly IStoreRepository _storeRepository;

        public EfCoreLicenseRepository(IDbContextProvider<DigitalDiscountsDbContext> dbContextProvider, IStoreRepository storeRepository) : base(dbContextProvider)
        {
            _storeRepository = storeRepository;
        }

        public async Task<License> GetAsyncLicense(Guid id)
        {
            try
            {
                var dbSet = await GetDbSetAsync();
                return await dbSet.FirstOrDefaultAsync(license => license.Id == id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<dynamic> GetAsyncLicenseWithStore(Guid id)
        {
            try
            {
                var queryable = await GetQueryableAsync();

                //var licenseStatus = Enum.GetValues(typeof(LicenseStatus)).Cast<LicenseStatus>().Select(s => new { Value = (int)s, Name = s.ToString() }).ToList();

                //Prepare a query to join licenses and stores
                var query = from license in queryable
                            join store in _storeRepository on license.StoreId equals store.Id 
                            //join s in licenseStatus on (int)license.Status equals s.Value
                            where license.Id == id
                            select new LicenseWithStore { LicenseObj = license, StoreName = store.Name };

                //Execute the query and get the license with store
                LicenseWithStore queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);

                if (queryResult == null)
                    throw new EntityNotFoundException(typeof(License), id);

                return queryResult;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<dynamic> GetListAsync(int skipCount, int maxResultCount, string sorting, long filter = 0)
        {
            try
            {
                //Get the IQueryable<License> from the repository
                var queryable = await GetQueryableAsync();

                //Prepare a query to join licenses and stores
                var query = from license in queryable
                            join store in _storeRepository on license.StoreId equals store.Id
                            select new LicenseWithStore { LicenseObj = license, StoreName = store.Name };

                //Paging & sorting
                query = query.WhereIf(!filter.Equals(0), l => l.LicenseObj.Number.Equals(filter)).
                    OrderBy(NormalizeSorting(sorting)).Skip(skipCount).Take(maxResultCount);

                //Execute the query and get a list
                var queryResult = await AsyncExecuter.ToListAsync(query);

                return queryResult;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<dynamic> GetListAsync(int skipCount, int maxResultCount, string sorting, int filter = 0)
        {
            try
            {
                //Get the IQueryable<License> from the repository
                var queryable = await GetQueryableAsync();

                //Prepare a query to join licenses and stores
                var query = from license in queryable
                            join store in _storeRepository on license.StoreId equals store.Id
                            select new LicenseWithStore { LicenseObj = license, StoreName = store.Name };

                //Paging & sorting
                query = query.Where(l => ((int)l.LicenseObj.Status).Equals(filter)).
                    OrderBy(NormalizeSorting(sorting)).Skip(skipCount).Take(maxResultCount);

                //Execute the query and get a list
                var queryResult = await AsyncExecuter.ToListAsync(query);

                return queryResult;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<License> FindByNumberAsync(long number)
        {
            try
            {
                var dbSet = await GetDbSetAsync();
                return await dbSet.FirstOrDefaultAsync(license => license.Number == number);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static string NormalizeSorting(string sorting)
        {
            try
            {
                if (sorting.IsNullOrEmpty())
                    return $"LicenseObj.{nameof(License.Number)}";

                if (sorting.Contains("licenseNumber", StringComparison.OrdinalIgnoreCase))
                    return sorting.Replace("licenseNumber", "license.Number", StringComparison.OrdinalIgnoreCase);

                if (sorting.Contains("licenseStatus", StringComparison.OrdinalIgnoreCase))
                    return sorting.Replace("licenseStatus", "license.Status", StringComparison.OrdinalIgnoreCase);

                if (sorting.Contains("licenseStartDate", StringComparison.OrdinalIgnoreCase))
                    return sorting.Replace("licenseStartDate", "license.StartDate", StringComparison.OrdinalIgnoreCase);

                if (sorting.Contains("licenseEndDate", StringComparison.OrdinalIgnoreCase))
                    return sorting.Replace("licenseEndDate", "license.EndDate", StringComparison.OrdinalIgnoreCase);

                if (sorting.Contains("licenseEndDate", StringComparison.OrdinalIgnoreCase))
                    return sorting.Replace("licenseEndDate", "license.EndDate", StringComparison.OrdinalIgnoreCase);

                return $"LicenseObj.{sorting}";
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> GetCountAsync()
        {
            try
            {
                return await GetCountAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
