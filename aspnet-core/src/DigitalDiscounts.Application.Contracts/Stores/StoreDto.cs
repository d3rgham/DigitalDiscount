using System;
using Volo.Abp.Application.Dtos;

namespace DigitalDiscounts.Stores
{
    public class StoreDto : EntityDto<Guid>
    {
        public string Name { get; set; }

    }
}
