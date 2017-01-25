namespace ProcessingTools.Services.Validation.Services
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Abstractions;
    using Comparers;
    using Contracts.Models;
    using Contracts.Services;
    using ProcessingTools.Constants;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Services.Cache.Contracts.Services.Validation;

    public class UrlValidationService : AbstractValidationService<IUrl, IUrl>, IUrlValidationService
    {
        public UrlValidationService(IValidationCacheService cacheService)
            : base(cacheService)
        {
        }

        protected override Func<IUrl, string> GetContextKey => item => item.FullAddress;

        protected override Func<IUrl, IUrl> GetItemToCheck => item => item;

        protected override Func<IUrl, IUrl> GetValidatedObject => item => item;

        protected override Task Validate(IEnumerable<IUrl> items, ICollection<IValidationServiceModel<IUrl>> validatedItems)
        {
            return Task.Run(() =>
            {
                if (items == null)
                {
                    return;
                }

                if (validatedItems == null)
                {
                    return;
                }

                var comparer = new UrlEqualityComparer();
                var exceptions = new ConcurrentQueue<Exception>();

                items.Distinct(comparer)
                    .GroupBy(i => i.BaseAddress)
                    .AsParallel()
                    .ForAll(group =>
                    {
                        // For different BaseAddress-es requests are parallel,
                        // for equal - sequential.
                        foreach (var item in group)
                        {
                            try
                            {
                                var validationResult = this.MakeRequest(item).Result;
                                this.AddItemToCache(validationResult).Wait(CachingConstants.WaitAddDataToCacheTimeoutMilliseconds);
                                validatedItems.Add(validationResult);
                            }
                            catch (Exception e)
                            {
                                exceptions.Enqueue(e);
                            }
                        }
                    });

                if (exceptions.Count > 0)
                {
                    throw new AggregateException(exceptions);
                }
            });
        }

        private async Task<IValidationServiceModel<IUrl>> MakeRequest(IUrl item)
        {
            var result = this.MapToValidationServiceModel(item);

            try
            {
                if (string.IsNullOrWhiteSpace(item.Address))
                {
                    throw new ApplicationException($"URL address is null or whitespace: '{item.BaseAddress}//{item.Address}'.");
                }

                using (var client = new HttpClient())
                {
                    if (!string.IsNullOrWhiteSpace(item.BaseAddress))
                    {
                        client.BaseAddress = new Uri(item.BaseAddress);
                    }

                    var response = await client.GetAsync(item.Address);

                    result.ValidationStatus = this.MapHttpStatusCodeToValidationStatus(response.StatusCode);
                }
            }
            catch (Exception e)
            {
                result.ValidationStatus = ValidationStatus.Undefined;
                result.ValidationException = e;
            }

            return result;
        }

        private ValidationStatus MapHttpStatusCodeToValidationStatus(HttpStatusCode statusCode)
        {
            ValidationStatus validationStatus;

            if (statusCode == HttpStatusCode.OK)
            {
                validationStatus = ValidationStatus.Valid;
            }
            else if ((int)statusCode < 400)
            {
                validationStatus = ValidationStatus.Undefined;
            }
            else
            {
                validationStatus = ValidationStatus.Invalid;
            }

            return validationStatus;
        }
    }
}
