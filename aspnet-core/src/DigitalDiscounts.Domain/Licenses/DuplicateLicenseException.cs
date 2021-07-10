using Volo.Abp;

namespace DigitalDiscounts.Licenses
{
    public class DuplicateLicenseException : BusinessException
    {
        public DuplicateLicenseException(long number) : base(DigitalDiscountsDomainErrorCodes.DuplicateLicense)
        {
            WithData("number", number);
        }
    }
}
