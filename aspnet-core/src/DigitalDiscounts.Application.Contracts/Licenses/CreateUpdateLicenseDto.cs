using System;
using System.ComponentModel.DataAnnotations;

namespace DigitalDiscounts.Licenses
{
    public class CreateUpdateLicenseDto
    {
        [Required]
        public long Number { get; set; }

        [Required]
        public LicenseStatus Status { get; set; } = LicenseStatus.Undefined;

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; } = DateTime.Now;

        [Required]
        public Guid StoreId { get; set; }
    }
}
