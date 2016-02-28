namespace ProcessingTools.Services.Validation.Factories
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ProcessingTools.Contracts.Types;
    using ProcessingTools.Services.Cache.Contracts;
    using ProcessingTools.Services.Cache.Models;
    using ProcessingTools.Services.Common.Contracts;
    using ProcessingTools.Services.Common.Models;
    using ProcessingTools.Services.Common.Models.Contracts;

    public abstract class ValidationServiceFactory<TValidatedObject, TItemToCheck> : IValidationService<TValidatedObject>
    {
        private readonly IValidationCacheService cacheService;

        public ValidationServiceFactory(IValidationCacheService cacheService)
        {
            if (cacheService == null)
            {
                throw new ArgumentNullException("cacheService");
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
                throw new ArgumentNullException("items", "Items to validate should not be null.");
            }

            if (items.Length < 1)
            {
                throw new ApplicationException("Number of items to validate should be greater tham zero.");
            }

            var result = new ConcurrentQueue<IValidationServiceModel<TValidatedObject>>();

            TItemToCheck[] itemsToCheck;
            try
            {
                itemsToCheck = await this.ValidateItemsFromCache(items, result);
                this.CacheServiceIsUsable = true;
            }
            catch
            {
                itemsToCheck = items.Select(this.GetItemToCheck).ToArray();
                this.CacheServiceIsUsable = false;
            }

            if (itemsToCheck.Count() < 1)
            {
                // All requested items are already cached and their status is Valid.
                return result;
            }

            await this.Validate(itemsToCheck, result);

            return result;
        }

        protected async Task CacheObtainedData(IValidationServiceModel<TValidatedObject> validatedObject)
        {
            if (this.CacheServiceIsUsable)
            {
                string context = this.GetContextKey.Invoke(this.GetItemToCheck.Invoke(validatedObject.ValidatedObject));
                await this.CacheService.Add(
                    context,
                    new ValidationCacheServiceModel
                    {
                        Status = validatedObject.ValidationStatus,
                        LastUpdate = DateTime.Now
                    });
            }
        }

        protected abstract Task Validate(TItemToCheck[] items, ConcurrentQueue<IValidationServiceModel<TValidatedObject>> output);

        private Task<TItemToCheck[]> ValidateItemsFromCache(TValidatedObject[] items, ConcurrentQueue<IValidationServiceModel<TValidatedObject>> validatedItems)
        {
            return Task.Run(() =>
            {
                var itemsToCheck = new ConcurrentQueue<TItemToCheck>();
                items.Select(this.GetItemToCheck)
                    .AsParallel()
                    .ForAll(item =>
                    {
                        string context = this.GetContextKey.Invoke(item);
                        var cachedItems = this.cacheService.All(context).Result.ToList();

                        int numberOfValidMatches = cachedItems
                            .Where(i => i.Status == ValidationStatus.Valid)
                            .Count();

                        int numberOfNonValidMatches = cachedItems
                            .Where(i => i.Status != ValidationStatus.Valid)
                            .Count();

                        if (numberOfNonValidMatches == 0 && numberOfValidMatches > 0)
                        {
                            var validatedObject = this.GetValidationServiceModel.Invoke(item);
                            validatedItems.Enqueue(validatedObject);
                        }
                        else
                        {
                            itemsToCheck.Enqueue(item);
                        }
                    });

                return itemsToCheck.ToArray<TItemToCheck>();
            });
        }
    }
}