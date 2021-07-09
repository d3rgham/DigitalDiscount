using System;
using Volo.Abp;
using JetBrains.Annotations;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace DigitalDiscounts.Stores
{
    public class StoreManager : DomainService
    {
        private readonly IStoreRepository _storeRepository;

        public StoreManager(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public async Task<Store> CreateAsync([NotNull] string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existingStore = await _storeRepository.FindByNameAsync(name);

            if (existingStore != null)
            {
                throw new StoreAlreadyExistsException(name);
            }

            return new Store(GuidGenerator.Create(), name);
        }

        public async Task ChangeNameAsync([NotNull] Store store, [NotNull] string newName)
        {
            Check.NotNull(store, nameof(store));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existingStore = await _storeRepository.FindByNameAsync(newName);
            if (existingStore != null && existingStore.Id != store.Id)
            {
                throw new StoreAlreadyExistsException(newName);
            }

            store.ChangeName(newName);
        }
    }
}
