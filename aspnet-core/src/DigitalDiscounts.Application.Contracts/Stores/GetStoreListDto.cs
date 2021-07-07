using Volo.Abp.Application.Dtos;

namespace DigitalDiscounts.Stores
{
    public class GetStoreListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
