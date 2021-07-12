using System;
using Volo.Abp;
using JetBrains.Annotations;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Ardalis.GuardClauses;
using DigitalDiscounts.Stores;

namespace DigitalDiscounts.Licenses
{
    public class LicenseManager : DomainService
    {
        private readonly ILicenseRepository _licenseRepository;
        private readonly IStoreRepository _storeRepository;


        public LicenseManager(ILicenseRepository licenseRepository, IStoreRepository storeRepository)
        {
            _licenseRepository = licenseRepository;
            _storeRepository = storeRepository;
        }

        public async Task<License> CreateAsync(long number, LicenseStatus status, DateTime startDate, DateTime endDate, Guid storeId)
        {
            try
            {
                // Ardalis.GuardClauses to do some validations.
                Guard.Against.NegativeOrZero(number, nameof(number), "The license number cannot be 0 or negative");
                Guard.Against.NullOrEmpty(storeId, nameof(storeId), "This store is not exists");
                Guard.Against.OutOfRange(startDate, nameof(startDate), DateTime.Today, endDate, "The license dates are out of range");

                var existingLicense = await _licenseRepository.FindByNumberAsync(number);

                if (existingLicense != null)
                    throw new DuplicateLicenseException(number);

                return new License(GuidGenerator.Create(), number, status, startDate, endDate, storeId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> UpdateAsync([NotNull] long number, [NotNull] LicenseStatus status,
                                               [NotNull] DateTime startDate, [NotNull] DateTime endDate,
                                               [NotNull] Guid storeId, [NotNull] License license)
        {
            try
            {
                if (license.Number != number)
                    await ChangeNumberAsync(license, number);

                if (license.StoreId != storeId)
                    await ChangeStoreAsync(license, storeId);

                if (license.Status != status)
                    ChangeStatus(license, status);

                Guard.Against.OutOfRange(startDate, nameof(startDate), DateTime.Today, endDate, "The license dates are out of range");
                license.StartDate = startDate;
                license.EndDate = endDate;

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task ChangeNumberAsync(License license, long newNumber)
        {
            try
            {
                Check.NotNull(license, nameof(license));
                Guard.Against.NegativeOrZero(newNumber, nameof(newNumber), "The license number cannot be 0 or negative");
                var existingLicense = await _licenseRepository.FindByNumberAsync(newNumber);

                if (existingLicense != null && existingLicense.Id != license.Id)
                    throw new DuplicateLicenseException(newNumber);

                license.ChangeNumber(newNumber);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task ChangeStoreAsync(License license, Guid storeId)
        {
            try
            {
                Check.NotNull(license, nameof(license));
                Guard.Against.NullOrEmpty(storeId, nameof(storeId), "Store value requierd");

                var existingStore = await _storeRepository.FindAsync(storeId);
                Guard.Against.Null(existingStore, nameof(existingStore), "Store is not exist");

                license.ChangeStore(storeId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void ChangeStatus(License license, LicenseStatus status)
        {
            try
            {
                Check.NotNull(license, nameof(license));
                Guard.Against.NegativeOrZero((int)status, nameof(status), "Status value requierd");
                license.ChangeStatus(status);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
