using System;
using System.ComponentModel.DataAnnotations;

namespace DigitalDiscounts.Stores
{
    public class UpdateStoreDto
    {
        [Required]
        [StringLength(StoreConsts.MaxNameLength)]
        public string Name { get; set; }
    }
}
