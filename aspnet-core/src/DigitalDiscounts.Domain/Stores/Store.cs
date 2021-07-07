using System;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace DigitalDiscounts.Stores
{
    public class Store : FullAuditedAggregateRoot<Guid> //BaseEntity
    {
        public string Name { get; private set; }


        private Store()
        {
            /* This constructor is for deserialization / ORM purpose */
        }

        internal Store(Guid id, [NotNull] string name) : base(id)
        {
            SetName(name);
        }

        internal Store ChangeName([NotNull] string name)
        {
            SetName(name);
            return this;
        }

        private void SetName([NotNull] string name)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(name), maxLength: StoreConsts.MaxNameLength);
        }
    }
}
