using DigitalDiscounts.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace DigitalDiscounts.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class DigitalDiscountsController : AbpController
    {
        protected DigitalDiscountsController()
        {
            LocalizationResource = typeof(DigitalDiscountsResource);
        }
    }
}