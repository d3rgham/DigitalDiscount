using System;
using System.ComponentModel.DataAnnotations;

namespace DigitalDiscounts.Stores
{
    public class CreateStoreDto
    {
        [Required]
        [StringLength(StoreConsts.MaxNameLength)]
        public string Name { get; set; }
    }
}
