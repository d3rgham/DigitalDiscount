using System;
using System.Threading.Tasks;
using DigitalDiscounts.Stores;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;
using DigitalDiscounts.Permissions;
using Serilog;
using Microsoft.AspNetCore.Authorization;

namespace DigitalDiscounts.Licenses
{
    [Authorize(DigitalDiscountsPermissions.Licenses.Default)]
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
                Log.Information("Getting a license with ID: " + id);

                var licenseWithStore = await _licenseRepository.GetAsyncLicenseWithStore(id);
                var licenseDto = ObjectMapper.Map<License, LicenseDto>(licenseWithStore.LicenseObj);
                licenseDto.StoreName = licenseWithStore.StoreName;
                return licenseDto;
            }
            catch (Exception ex)
            {
                Log.Error("Error occured while getting a license with ID: " + id);
                throw;
            }
        }

        public async Task<PagedResultDto<LicenseDto>> GetListAsync(GetLicenseListDto input)
        {
            try
            {
                Log.Information("Getting a list of licenses");

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
                Log.Error("Error while getting a list of licenses");
                throw;
            }
        }

        public async Task<PagedResultDto<LicenseDto>> GetListByStatusAsync(GetLicenseListDto input)
        {
            try
            {
                Log.Information("Getting a list of licenses by status");

                if (input.Sorting.IsNullOrWhiteSpace())
                    input.Sorting = nameof(License.Number);

                var licensesWithStore = await _licenseRepository.GetListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, (int)input.Filter);

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
                Log.Error("Error while getting a list of licenses by status");
                throw;
            }
        }

        [Authorize(DigitalDiscountsPermissions.Licenses.Create)]
        public async Task<LicenseDto> CreateAsync(CreateUpdateLicenseDto input)
        {
            try
            {
                Log.Information("Creating a new licenses");
                var license = await _licenseManager.CreateAsync(input.Number, input.Status, input.StartDate, input.EndDate, input.StoreId);
                await _licenseRepository.InsertAsync(license);
                return ObjectMapper.Map<License, LicenseDto>(license);
            }
            catch (Exception ex)
            {
                Log.Error("Error while creating a new licenses");
                throw;
            }
        }

        [Authorize(DigitalDiscountsPermissions.Licenses.Edit)]
        public async Task<bool> UpdateAsync(Guid id, CreateUpdateLicenseDto input)
        {
            try
            {
                Log.Information("Updating an existing licenses");
                var license = await _licenseRepository.GetAsyncLicense(id);
                bool Isupdated = await _licenseManager.UpdateAsync(input.Number, input.Status, input.StartDate, input.EndDate, input.StoreId, license);

                if (Isupdated)
                    await _licenseRepository.UpdateAsync(license);

                return Isupdated;
            }
            catch (Exception ex)
            {
                Log.Error("Error while updating an existing licenses");
                throw;
            }
        }

        [Authorize(DigitalDiscountsPermissions.Licenses.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                Log.Information("Deleting a license with ID: " + id);
                await _licenseRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Log.Error("Error occured while deleting a license with ID: " + id);
                throw;
            }
        }

        public async Task<ListResultDto<StoreLookupDto>> GetStoreLookupAsync()
        {
            try
            {
                Log.Information("Getting stores Lookup");
                var stores = await _storeRepository.GetListAsync();
                return new ListResultDto<StoreLookupDto>(ObjectMapper.Map<List<Store>, List<StoreLookupDto>>(stores));
            }
            catch (Exception ex)
            {
                Log.Error("Error while getting stores Lookup");
                throw;
            }
        }
    }
}
