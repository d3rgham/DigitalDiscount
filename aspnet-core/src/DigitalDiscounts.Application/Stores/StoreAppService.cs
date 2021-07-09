using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;
using Volo.Abp.Domain.Repositories;
using DigitalDiscounts.Permissions;
using Microsoft.AspNetCore.Authorization;

namespace DigitalDiscounts.Stores
{
    [Authorize(DigitalDiscountsPermissions.Stores.Default)]
    public class StoreAppService : DigitalDiscountsAppService, IStoreAppService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly StoreManager _storeManager;

        public StoreAppService(IStoreRepository storeRepository, StoreManager storeManager)
        {
            _storeRepository = storeRepository;
            _storeManager = storeManager;
        }

        //...SERVICE METHODS WILL COME HERE...
        public async Task<StoreDto> GetAsync(Guid id)
        {
            var store = await _storeRepository.GetAsync(id);
            return ObjectMapper.Map<Store, StoreDto>(store);
        }

        public async Task<PagedResultDto<StoreDto>> GetListAsync(GetStoreListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Store.Name);
            }

            var stores = await _storeRepository.GetListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, input.Filter);

            var totalCount = input.Filter == null ? await _storeRepository.CountAsync()
                : await _storeRepository.CountAsync(store => store.Name.Contains(input.Filter));

            return new PagedResultDto<StoreDto>(totalCount, ObjectMapper.Map<List<Store>, List<StoreDto>>(stores));
        }

        [Authorize(DigitalDiscountsPermissions.Stores.Create)]
        public async Task<StoreDto> CreateAsync(CreateStoreDto input)
        {
            var store = await _storeManager.CreateAsync(input.Name);

            await _storeRepository.InsertAsync(store);

            return ObjectMapper.Map<Store, StoreDto>(store);
        }

        [Authorize(DigitalDiscountsPermissions.Stores.Edit)]
        public async Task UpdateAsync(Guid id, UpdateStoreDto input)
        {
            var store = await _storeRepository.GetAsync(id);

            if (store.Name != input.Name)
            {
                await _storeManager.ChangeNameAsync(store, input.Name);
            }

            await _storeRepository.UpdateAsync(store);
        }

        [Authorize(DigitalDiscountsPermissions.Stores.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _storeRepository.DeleteAsync(id);
        }
    }
}

