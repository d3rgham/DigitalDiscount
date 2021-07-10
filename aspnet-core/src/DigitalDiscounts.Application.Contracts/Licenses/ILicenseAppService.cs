using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace DigitalDiscounts.Licenses
{
    public interface ILicenseAppService : IApplicationService
    {
        Task<LicenseDto> GetAsync(Guid id);

        //Task<PagedResultDto<LicenseDto>> GetListAsync(GetLicenseListDto input);

        Task<LicenseDto> CreateAsync(CreateUpdateLicenseDto input);

        Task<bool> UpdateAsync(Guid id, CreateUpdateLicenseDto input);

        Task DeleteAsync(Guid id);

        Task<ListResultDto<StoreLookupDto>> GetStoreLookupAsync();
    }
}
