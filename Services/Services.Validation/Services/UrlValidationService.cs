namespace ProcessingTools.Services.Validation
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Contracts;
    using Models;
    using Models.Contracts;

    using ProcessingTools.Contracts.Types;
    using ProcessingTools.Services.Cache.Contracts;
    using ProcessingTools.Services.Cache.Models;
    using ProcessingTools.Services.Common.Models.Contracts;

    public class UrlValidationService : IUrlValidationService
    {
        private IValidationCacheService cacheService;

        public UrlValidationService(IValidationCacheService cacheService)
        {
            if (cacheService == null)
            {
                throw new ArgumentNullException("cacheService");
            }

            this.cacheService = cacheService;
        }

        public Task<IEnumerable<IValidationServiceModel<IUrl>>> Validate(params IUrl[] items)
        {
            return Task.Run(() =>
            {
                var result = new ConcurrentQueue<IValidationServiceModel<IUrl>>();

                var itemsToCheck = this.ValidateItemsFromCache(items, result);

                if (itemsToCheck.Count() < 1)
                {
                    // All requested items are already cached and their status is Valid.
                    return result;
                }

                itemsToCheck.GroupBy(i => i.BaseAddress)
                    .AsParallel()
                    .ForAll(group =>
                    {
                        // For different BaseAddress-es requests are parallel,
                        // for equal - sequental.
                        foreach (var item in group)
                        {
                            var validationResult = MakeRequest(item).Result;
                            this.CacheObtainedData(validationResult);
                            result.Enqueue(validationResult);
                        }
                    });

                return result.AsEnumerable();
            });
        }

        private static async Task<UrlValidationServiceModel> MakeRequest(IUrl item)
        {
            var validationResult = new UrlValidationServiceModel
            {
                ValidatedObject = item,
                ValidationStatus = ValidationStatus.Undefined,
                ValidationException = null
            };

            try
            {
                if (string.IsNullOrWhiteSpace(item.Address))
                {
                    throw new ApplicationException($"Url address is null or whitespace: '{item.BaseAddress}//{item.Address}'.");
                }

                using (HttpClient client = new HttpClient())
                {
                    if (!string.IsNullOrWhiteSpace(item.BaseAddress))
                    {
                        client.BaseAddress = new Uri(item.BaseAddress);
                    }

                    var response = await client.GetAsync(item.Address);

                    var statusCode = response.StatusCode;

                    if (statusCode == HttpStatusCode.OK)
                    {
                        validationResult.ValidationStatus = ValidationStatus.Valid;
                    }
                    else if ((int)statusCode < 400)
                    {
                        validationResult.ValidationStatus = ValidationStatus.Undefined;
                    }
                    else
                    {
                        validationResult.ValidationStatus = ValidationStatus.Invalid;
                    }
                }
            }
            catch (Exception e)
            {
                validationResult.ValidationStatus = ValidationStatus.Undefined;
                validationResult.ValidationException = e;
            }

            return validationResult;
        }

        private IEnumerable<IUrl> ValidateItemsFromCache(IUrl[] items, ConcurrentQueue<IValidationServiceModel<IUrl>> validatedItems)
        {
            var itemsToCheck = new ConcurrentQueue<IUrl>();
            items.AsParallel()
                .ForAll(item =>
                {
                    var cachedItems = this.cacheService.All(item.FullAddress).Result.ToList();

                    int numberOfValidMatches = cachedItems
                        .Where(i => i.Status == ValidationStatus.Valid)
                        .Count();

                    int numberOfNonValidMatches = cachedItems
                        .Where(i => i.Status != ValidationStatus.Valid)
                        .Count();

                    if (numberOfNonValidMatches == 0 && numberOfValidMatches > 0)
                    {
                        var validatedObject = new UrlValidationServiceModel
                        {
                            ValidatedObject = new UrlModel
                            {
                                Address = item.Address,
                                BaseAddress = item.BaseAddress
                            },
                            ValidationException = null,
                            ValidationStatus = ValidationStatus.Valid
                        };

                        validatedItems.Enqueue(validatedObject);
                    }
                    else
                    {
                        itemsToCheck.Enqueue(item);
                    }
                });

            return itemsToCheck;
        }

        private void CacheObtainedData(UrlValidationServiceModel validatedObject)
        {
            this.cacheService.Add(
                validatedObject.ValidatedObject.FullAddress,
                new ValidationCacheServiceModel
                {
                    Status = validatedObject.ValidationStatus,
                    LastUpdate = DateTime.Now
                }).Wait();
        }
    }
}
