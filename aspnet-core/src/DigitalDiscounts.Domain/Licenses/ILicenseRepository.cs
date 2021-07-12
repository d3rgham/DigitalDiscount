using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.Domain.Repositories;
using DigitalDiscounts.Licenses;
using System.Linq;

namespace DigitalDiscounts.Licenses
{
    public interface ILicenseRepository : IRepository<License, Guid> 
    {
        Task<License> GetAsyncLicense(Guid id);
        Task<License> FindByNumberAsync(long number);
        Task <dynamic> GetAsyncLicenseWithStore(Guid id);
        Task<dynamic> GetListAsync(int skipCount, int maxResultCount, string sorting, long filter);
        Task<dynamic> GetListAsync(int skipCount, int maxResultCount, string sorting, int filter);

    }
}
