using DigitalDiscounts.Permissions;
using DigitalDiscounts.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace DigitalDiscounts.Licenses
{
    //[Authorize(DigitalDiscountsPermissions.Licenses.Default)]
    public class LicenseAppService : DigitalDiscountsAppService, ILicenseAppService
    {
        private readonly ILicenseRepository _licenseRepository;
        private readonly LicenseManager _licenseManager;
        private readonly IStoreRepository _storeRepository;

        public LicenseAppService(ILicenseRepository licenseRepository, LicenseManager licenseManager, IStoreRepository storeRepository)
        {
            _licenseRepository = licenseRepository;
            _licenseManager = licenseManager;
            _storeRepository = storeRepository;
        }

        //...SERVICE METHODS WILL COME HERE...
        public async Task<LicenseDto> GetAsync(Guid id)
        {
            try
            {
                var licenseWithStore = await _licenseRepository.GetAsyncLicenseWithStore(id);
                var licenseDto = ObjectMapper.Map<License, LicenseDto>(licenseWithStore.LicenseObj);
                licenseDto.StoreName = licenseWithStore.StoreName;
                return licenseDto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<PagedResultDto<LicenseDto>> GetListAsync(GetLicenseListDto input)
        {
            try
            {
                if (input.Sorting.IsNullOrWhiteSpace())
                    input.Sorting = nameof(License.Number);

                var licensesWithStore = await _licenseRepository.GetListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, input.Filter);

                //Convert the query result to a list of LicenseDto objects
                List<LicenseDto> licensDtoList = new();

                foreach (var license in licensesWithStore)
                {
                    var licenseDto = ObjectMapper.Map<License, LicenseDto>(license.LicenseObj);
                    licenseDto.StoreName = license.StoreName;
                    licensDtoList.Add(licenseDto);
                }

                var totalcount = await _licenseRepository.GetCountAsync();

                return new PagedResultDto<LicenseDto>(totalcount, licensDtoList);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //[Authorize(DigitalDiscountsPermissions.Licenses.Create)]
        public async Task<LicenseDto> CreateAsync(CreateUpdateLicenseDto input)
        {
            try
            {
                var license = await _licenseManager.CreateAsync(input.Number, input.Status, input.StartDate, input.EndDate, input.StoreId);
                await _licenseRepository.InsertAsync(license);
                return ObjectMapper.Map<License, LicenseDto>(license);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //[Authorize(DigitalDiscountsPermissions.Stores.Edit)]
        public async Task<bool> UpdateAsync(Guid id, CreateUpdateLicenseDto input)
        {
            try
            {
                var license = await _licenseRepository.GetAsyncLicense(id);
                bool Isupdated = await _licenseManager.UpdateAsync(input.Number, input.Status, input.StartDate, input.EndDate, input.StoreId, license);

                if (Isupdated)
                    await _licenseRepository.UpdateAsync(license);

                return Isupdated;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //[Authorize(DigitalDiscountsPermissions.Stores.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                await _licenseRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ListResultDto<StoreLookupDto>> GetStoreLookupAsync()
        {
            try
            {
                var stores = await _storeRepository.GetListAsync();
                return new ListResultDto<StoreLookupDto>(ObjectMapper.Map<List<Store>, List<StoreLookupDto>>(stores));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
