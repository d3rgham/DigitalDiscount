using System;
using System.Collections.Generic;
using System.Text;
using DigitalDiscounts.Localization;
using Volo.Abp.Application.Services;

namespace DigitalDiscounts
{
    /* Inherit your application services from this class.
     */
    public abstract class DigitalDiscountsAppService : ApplicationService
    {
        protected DigitalDiscountsAppService()
        {
            LocalizationResource = typeof(DigitalDiscountsResource);
        }
    }
}
