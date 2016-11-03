namespace ProcessingTools.Services.Validation.Factories
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Models;
    using Models.Contracts;

    using ProcessingTools.Enumerations;
    using ProcessingTools.Extensions.Linq;
    using ProcessingTools.Services.Cache.Contracts.Validation;
    using ProcessingTools.Services.Cache.Models.Validation;

    public abstract class ValidationServiceFactory<TValidatedObject, TItemToCheck> : IValidationService<TValidatedObject>
    {
        private readonly IValidationCacheService cacheService;

        public ValidationServiceFactory(IValidationCacheService cacheService)
        {
            if (cacheService == null)
            {
                throw new ArgumentNullException(nameof(cacheService));
            }

            this.cacheService = cacheService;
        }

        protected IValidationCacheService CacheService => this.cacheService;

        protected bool CacheServiceIsUsable { get; private set; }

        protected abstract Func<TItemToCheck, string> GetContextKey { get; }

        protected abstract Func<TValidatedObject, TItemToCheck> GetItemToCheck { get; }

        protected abstract Func<TItemToCheck, TValidatedObject> GetValidatedObject { get; }

        protected Func<TItemToCheck, IValidationServiceModel<TValidatedObject>> GetValidationServiceModel => item => new ValidationServiceModel<TValidatedObject>
        {
            ValidatedObject = this.GetValidatedObject.Invoke(item),
            ValidationException = null,
            ValidationStatus = ValidationStatus.Valid
        };

        public async Task<IEnumerable<IValidationServiceModel<TValidatedObject>>> Validate(params TValidatedObject[] items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items), "Items to validate should not be null.");
            }

            if (items.Length < 1)
            {
                throw new ApplicationException("Number of items to validate should be greater than zero.");
            }

            var validatedItems = new ConcurrentQueue<IValidationServiceModel<TValidatedObject>>();
            var itemsToCheck = new ConcurrentQueue<TItemToCheck>();

            try
            {
                await this.ValidateItemsFromCache(items, validatedItems, itemsToCheck);
                this.CacheServiceIsUsable = true;
            }
            catch
            {
                itemsToCheck = new ConcurrentQueue<TItemToCheck>(items.Select(this.GetItemToCheck));
                this.CacheServiceIsUsable = false;
            }

            if (itemsToCheck.Count() < 1)
            {
                // All requested items are already cached and their status is Valid.
                return validatedItems;
            }

            await this.Validate(itemsToCheck.ToArray(), validatedItems);

            return validatedItems.ToList();
        }

        protected async Task CacheObtainedData(IValidationServiceModel<TValidatedObject> validatedObject)
        {
            if (this.CacheServiceIsUsable)
            {
                string context = this.GetContextKey.Invoke(this.GetItemToCheck.Invoke(validatedObject.ValidatedObject));

                try
                {
                    await this.CacheService.Add(
                        context,
                        new ValidationCacheServiceModel
                        {
                            Status = validatedObject.ValidationStatus,
                            LastUpdate = DateTime.Now
                        });
                }
                catch
                {
                    this.CacheServiceIsUsable = false;
                }
            }
        }

        protected abstract Task Validate(TItemToCheck[] items, ConcurrentQueue<IValidationServiceModel<TValidatedObject>> validatedItems);

        private Task ValidateItemsFromCache(
            TValidatedObject[] items,
            ConcurrentQueue<IValidationServiceModel<TValidatedObject>> validatedItems,
            ConcurrentQueue<TItemToCheck> itemsToCheck)
        {
            Task[] tasks = items.Select(this.GetItemToCheck)
                .Select(item => this.ValidateSingleItem(item, validatedItems, itemsToCheck))
                .ToArray();

            return Task.WhenAll(tasks);
        }

        private async Task ValidateSingleItem(
            TItemToCheck item,
            ConcurrentQueue<IValidationServiceModel<TValidatedObject>> validatedItems,
            ConcurrentQueue<TItemToCheck> itemsToCheck)
        {
            string context = this.GetContextKey.Invoke(item);
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ApplicationException("Cache context should be valid string.");
            }

            try
            {
                var lastCachedItem = await this.cacheService.Get(context);

                if (lastCachedItem == null || lastCachedItem.Status != ValidationStatus.Valid)
                {
                    itemsToCheck.Enqueue(item);
                }
                else
                {
                    var validatedObject = this.GetValidationServiceModel.Invoke(item);
                    validatedItems.Enqueue(validatedObject);
                }
            }
            catch
            {
                itemsToCheck.Enqueue(item);
            }
        }
    }
}