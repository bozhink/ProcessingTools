// <copyright file="AbstractValidationService{T}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Abstractions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Constants;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Contracts.Models.Validation;
    using ProcessingTools.Contracts.Services.Cache;
    using ProcessingTools.Contracts.Services.Validation;
    using ProcessingTools.Services.Models.Cache;
    using ProcessingTools.Services.Models.Validation;

    /// <summary>
    /// Abstract generic validation service.
    /// </summary>
    /// <typeparam name="T">Type of the validated object.</typeparam>
    public abstract class AbstractValidationService<T> : IValidationService<T>
    {
        private readonly IValidationCacheService cacheService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractValidationService{T}"/> class.
        /// </summary>
        /// <param name="cacheService">Validation cache service.</param>
        protected AbstractValidationService(IValidationCacheService cacheService)
        {
            this.cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
            this.CacheServiceIsUsable = true;
        }

        /// <summary>
        /// Gets permalink.
        /// </summary>
        protected abstract Func<T, string> GetPermalink { get; }

        /// <summary>
        /// Gets mapping function for validated object to validation model.
        /// </summary>
        protected virtual Func<T, IValidationModel<T>> MapToResponseModel => item => new ValidationServiceModel<T>
        {
            ValidatedObject = item,
            ValidationException = null,
            ValidationStatus = ValidationStatus.Undefined
        };

        private bool CacheServiceIsUsable { get; set; }

        /// <inheritdoc/>
        public async Task<IValidationModel<T>[]> ValidateAsync(params T[] items)
        {
            if (items == null || items.Length < 1)
            {
                return null;
            }

            var result = items.Select(this.MapToResponseModel).ToArray();

            await this.ValidateWithCacheAsync(result).ConfigureAwait(false);

            var itemsToCheck = this.GetItemsToCheck(result);

            var validatedItems = await this.ValidateAsync(itemsToCheck.Select(i => i.ValidatedObject)).ConfigureAwait(false);
            validatedItems.ToList().ForEach(validatedItem =>
            {
                string permalink = this.GetPermalink(validatedItem.ValidatedObject);
                foreach (var item in result.Where(i => this.GetPermalink(i.ValidatedObject) == permalink))
                {
                    item.ValidationStatus = validatedItem.ValidationStatus;
                    item.ValidationException = validatedItem.ValidationException;
                }

                this.AddItemToCacheAsync(validatedItem).Wait(CachingConstants.WaitAddDataToCacheTimeoutMilliseconds);
            });

            return result;
        }

        /// <summary>
        /// Validate items with appropriate logic.
        /// </summary>
        /// <param name="items">Items to be validated.</param>
        /// <returns>Task of validated items.</returns>
        protected abstract Task<IValidationModel<T>[]> ValidateAsync(IEnumerable<T> items);

        private async Task AddItemToCacheAsync(IValidationModel<T> item)
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

                await this.cacheService.AddAsync(permalink, model).ConfigureAwait(false);
            }
            catch
            {
                this.CacheServiceIsUsable = false;
            }
        }

        private IEnumerable<IValidationModel<T>> GetItemsToCheck(IEnumerable<IValidationModel<T>> items)
        {
            return items.Where(i => i.ValidationStatus != ValidationStatus.Valid);
        }

        private async Task<ValidationStatus> ValidateSingleItemFromCacheAsync(T item)
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

            var cachedItem = await this.cacheService.GetAsync(permalink).ConfigureAwait(false);
            if (cachedItem == null)
            {
                return DefaultStatus;
            }

            return cachedItem.Status;
        }

        private async Task<object> ValidateWithCacheAsync(IEnumerable<IValidationModel<T>> items)
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
                    var status = await this.ValidateSingleItemFromCacheAsync(item.ValidatedObject).ConfigureAwait(false);
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

            await Task.WhenAll(tasks).ConfigureAwait(false);

            return true;
        }
    }
}
