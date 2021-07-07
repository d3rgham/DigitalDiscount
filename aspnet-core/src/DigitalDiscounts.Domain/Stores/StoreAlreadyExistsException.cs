using Volo.Abp;

namespace DigitalDiscounts.Stores
{
    public class StoreAlreadyExistsException : BusinessException
    {
        public StoreAlreadyExistsException(string name) : base(DigitalDiscountsDomainErrorCodes.StoreAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
