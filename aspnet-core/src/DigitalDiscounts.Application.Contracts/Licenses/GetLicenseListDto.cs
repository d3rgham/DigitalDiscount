using Volo.Abp.Application.Dtos;

namespace DigitalDiscounts.Licenses
{
    public class GetLicenseListDto : PagedAndSortedResultRequestDto
    {
        public long Filter { get; set; }
    }
}
