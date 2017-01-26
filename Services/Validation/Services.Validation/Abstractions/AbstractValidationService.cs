namespace ProcessingTools.Services.Validation.Abstractions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts.Models;
    using Contracts.Services;
    using Models;
    using ProcessingTools.Constants;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Services.Cache.Contracts.Services.Validation;
    using ProcessingTools.Services.Cache.Models.Validation;

    public abstract class AbstractValidationService<T> : IValidationService<T>
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

        protected abstract Func<T, string> GetPermalink { get; }

        protected virtual Func<T, IValidationServiceModel<T>> MapToResponseModel => item => new ValidationServiceModel<T>
        {
            ValidatedObject = item,
            ValidationException = null,
            ValidationStatus = ValidationStatus.Undefined
        };

        private bool CacheServiceIsUsable { get; set; }

        public async Task<IEnumerable<IValidationServiceModel<T>>> Validate(params T[] items)
        {
            if (items == null || items.Length < 1)
            {
                return null;
            }

            var result = items.Select(this.MapToResponseModel).ToArray();

            await this.ValidateWithCache(result);

            var itemsToCheck = this.GetItemsToCheck(result);

            var validatedItems = await this.Validate(itemsToCheck.Select(i => i.ValidatedObject));
            validatedItems.ToList().ForEach(validatedItem =>
            {
                string permalink = this.GetPermalink(validatedItem.ValidatedObject);
                foreach (var item in result.Where(i => this.GetPermalink(i.ValidatedObject) == permalink))
                {
                    item.ValidationStatus = validatedItem.ValidationStatus;
                    item.ValidationException = validatedItem.ValidationException;
                }

                this.AddItemToCache(validatedItem).Wait(CachingConstants.WaitAddDataToCacheTimeoutMilliseconds);
            });

            return result;
        }

        protected abstract Task<IEnumerable<IValidationServiceModel<T>>> Validate(IEnumerable<T> items);

        private async Task AddItemToCache(IValidationServiceModel<T> item)
        {
            if (!this.CacheServiceIsUsable)
            {
                return;
            }

            if (item == null)
            {
                return;
            }

            string permalink = this.GetPermalink(item.ValidatedObject);
            if (string.IsNullOrWhiteSpace(permalink))
            {
                return;
            }

            try
            {
                var model = new ValidationCacheServiceModel
                {
                    Status = item.ValidationStatus
                };

                await this.cacheService.Add(permalink, model);
            }
            catch (Exception e)
            {
                this.CacheServiceIsUsable = false;
            }
        }

        private IEnumerable<IValidationServiceModel<T>> GetItemsToCheck(IEnumerable<IValidationServiceModel<T>> items)
        {
            return items.Where(i => i.ValidationStatus != ValidationStatus.Valid);
        }

        private async Task<ValidationStatus> ValidateSingleItemFromCache(T item)
        {
            const ValidationStatus DefaultStatus = ValidationStatus.Undefined;

            if (!this.CacheServiceIsUsable)
            {
                return DefaultStatus;
            }

            string permalink = this.GetPermalink(item);
            if (string.IsNullOrWhiteSpace(permalink))
            {
                return DefaultStatus;
            }

            var cachedItem = await this.cacheService.Get(permalink);
            if (cachedItem == null)
            {
                return DefaultStatus;
            }

            return cachedItem.Status;
        }

        private async Task<object> ValidateWithCache(IEnumerable<IValidationServiceModel<T>> items)
        {
            if (!this.CacheServiceIsUsable || items == null)
            {
                return false;
            }

            var itemsToCheck = this.GetItemsToCheck(items);

            var tasks = itemsToCheck.Select(async (item) =>
            {
                try
                {
                    var status = await this.ValidateSingleItemFromCache(item.ValidatedObject);
                    item.ValidationStatus = status;
                }
                catch (Exception e)
                {
                    item.ValidationStatus = ValidationStatus.Undefined;
                    item.ValidationException = e;
                    this.CacheServiceIsUsable = false;
                }
            })
            .ToArray();

            await Task.WhenAll(tasks);

            return true;
        }
    }
}
