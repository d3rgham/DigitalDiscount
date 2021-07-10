using System;
using Volo.Abp;
using JetBrains.Annotations;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace DigitalDiscounts.Licenses
{
    public class License : FullAuditedAggregateRoot<Guid> //BaseEntity
    {
        public long Number { get; private set; }
        public LicenseStatus Status { get; private set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid StoreId { get; private set; }

        private License()
        {
            /* This constructor is for deserialization / ORM purpose */
        }

        internal License(Guid id, [NotNull] long number, [NotNull] LicenseStatus status,
            [NotNull] DateTime startDate, [NotNull] DateTime endDate, Guid storeId) : base(id)
        {
            SetNumber(number);
            SetStatus(status);
            StartDate = startDate;
            EndDate = endDate;
            SetStoreId(storeId);
        }

        internal License ChangeNumber(long number)
        {
            SetNumber(number);
            return this;
        }

        internal License ChangeStatus(LicenseStatus status)
        {
            SetStatus(status);
            return this;
        }

        internal License ChangeStore(Guid storeId)
        {
            SetStoreId(storeId);
            return this;
        }

        private void SetNumber(long number)
        {
            Number = Check.NotNull<long>(number, nameof(number));
        }

        private void SetStatus(LicenseStatus status)
        {
            Status = Check.NotNull<LicenseStatus>(status, nameof(status));
        }

        private void SetStoreId(Guid storeId)
        {
            StoreId = Check.NotNull<Guid>(storeId, nameof(storeId));
        }
    }
}
