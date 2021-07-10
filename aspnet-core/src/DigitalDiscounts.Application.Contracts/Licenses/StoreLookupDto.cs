using System;
using Volo.Abp.Application.Dtos;

namespace DigitalDiscounts.Licenses
{
    public class StoreLookupDto : EntityDto<Guid>
    {
        public string Name { get; set; }
    }
}
