using DigitalDiscounts.Licenses;
using DigitalDiscounts.Stores;
using AutoMapper;

namespace DigitalDiscounts
{
    public class DigitalDiscountsApplicationAutoMapperProfile : Profile
    {
        public DigitalDiscountsApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            CreateMap<Store, StoreDto>();
            CreateMap<Store, StoreLookupDto>();

            CreateMap<License, LicenseDto>();
            CreateMap<CreateUpdateLicenseDto, License>();
        }
    }
}
