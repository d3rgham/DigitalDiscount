using System;
using Volo.Abp.Application.Dtos;

namespace DigitalDiscounts.Licenses
{
    public class LicenseDto : EntityDto<Guid>
    {
        public long Number { get; set; }
        public LicenseStatus Status { get;  set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid StoreId { get; set; }
        public string StoreName { get; set; }
    }
}
