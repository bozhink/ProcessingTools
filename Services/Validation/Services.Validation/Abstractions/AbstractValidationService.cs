namespace ProcessingTools.Services.Validation.Abstractions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts.Models;
    using Contracts.Services;
    using Models;
    using ProcessingTools.Common.Collections;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Services.Cache.Contracts.Services.Validation;
    using ProcessingTools.Services.Cache.Models.Validation;

    public abstract class AbstractValidationService<T, TItemToCheck> : IValidationService<T>
    {
        private readonly IValidationCacheService cacheService;

        public AbstractValidationService(IValidationCacheService cacheService)
        {
            if (cacheService == null)
            {
                throw new ArgumentNullException(nameof(cacheService));
            }

            this.cacheService = cacheService;
            this.CacheServiceIsUsable = true;
        }

        protected bool CacheServiceIsUsable { get; private set; }

        protected abstract Func<TItemToCheck, string> GetContextKey { get; }

        protected abstract Func<T, TItemToCheck> GetItemToCheck { get; }

        protected abstract Func<TItemToCheck, T> GetValidatedObject { get; }

        protected Func<TItemToCheck, IValidationServiceModel<T>> MapToValidationServiceModel => item => new ValidationServiceModel<T>
        {
            ValidatedObject = this.GetValidatedObject(item),
            ValidationException = null,
            ValidationStatus = ValidationStatus.Valid
        };

        public async Task<IEnumerable<IValidationServiceModel<T>>> Validate(params T[] items)
        {
            var validatedItems = new ConcurrentQueueCollection<IValidationServiceModel<T>>();

            if (items == null || items.Length < 1)
            {
                return validatedItems;
            }

            var itemsToCheck = await this.TryValidationWithCache(items, validatedItems);
            if (itemsToCheck.Count() > 0)
            {
                await this.Validate(itemsToCheck, validatedItems);
            }

            return validatedItems.ToArray();
        }

        protected virtual async Task AddItemToCache(IValidationServiceModel<T> item)
        {
            if (!this.CacheServiceIsUsable)
            {
                return;
            }

            if (item == null)
            {
                return;
            }

            string contextKey = this.GetContextKey(this.GetItemToCheck(item.ValidatedObject));
            if (string.IsNullOrWhiteSpace(contextKey))
            {
                return;
            }

            try
            {
                var model = new ValidationCacheServiceModel
                {
                    Status = item.ValidationStatus
                };

                await this.cacheService.Add(contextKey, model);
            }
            catch
            {
                this.CacheServiceIsUsable = false;
            }
        }

        protected abstract Task Validate(IEnumerable<TItemToCheck> items, ICollection<IValidationServiceModel<T>> validatedItems);

        private async Task<IEnumerable<TItemToCheck>> TryValidationWithCache(
            IEnumerable<T> items,
            ICollection<IValidationServiceModel<T>> validatedItems)
        {
            var itemsToCheck = new ConcurrentQueueCollection<TItemToCheck>();
            try
            {
                await this.ValidateItemsFromCache(items, validatedItems, itemsToCheck);
                this.CacheServiceIsUsable = true;
            }
            catch
            {
                itemsToCheck = new ConcurrentQueueCollection<TItemToCheck>(items.Select(this.GetItemToCheck));
                this.CacheServiceIsUsable = false;
            }

            return itemsToCheck.ToArray();
        }

        private async Task ValidateItemsFromCache(
            IEnumerable<T> items,
            ICollection<IValidationServiceModel<T>> validatedItems,
            ICollection<TItemToCheck> itemsToCheck)
        {
            if (!this.CacheServiceIsUsable)
            {
                return;
            }

            if (items == null)
            {
                return;
            }

            var tasks = items.Select(this.GetItemToCheck)
                .Select(item => this.ValidateSingleItem(item, validatedItems, itemsToCheck))
                .ToArray();

            await Task.WhenAll(tasks);
        }

        private async Task ValidateSingleItem(
            TItemToCheck item,
            ICollection<IValidationServiceModel<T>> validatedItems,
            ICollection<TItemToCheck> itemsToCheck)
        {
            if (!this.CacheServiceIsUsable)
            {
                return;
            }

            string contextKey = this.GetContextKey(item);
            if (string.IsNullOrWhiteSpace(contextKey))
            {
                return;
            }

            var cachedItem = await this.cacheService.Get(contextKey);
            if (cachedItem != null && cachedItem.Status == ValidationStatus.Valid)
            {
                var model = this.MapToValidationServiceModel(item);
                validatedItems.Add(model);
                return;
            }

            itemsToCheck.Add(item);
        }
    }
}
