namespace ProcessingTools.Services.Validation
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Comparers;
    using Constants;
    using Contracts;
    using Factories;
    using Models;

    using ProcessingTools.Contracts.Types;
    using ProcessingTools.Services.Cache.Contracts;
    using ProcessingTools.Services.Common.Models.Contracts;

    public class UrlValidationService : ValidationServiceFactory<UrlServiceModel, UrlServiceModel>, IUrlValidationService
    {
        public UrlValidationService(IValidationCacheService cacheService)
            : base(cacheService)
        {
        }

        protected override Func<UrlServiceModel, string> GetContextKey => item => item.FullAddress;

        protected override Func<UrlServiceModel, UrlServiceModel> GetItemToCheck => item => item;

        protected override Func<UrlServiceModel, UrlServiceModel> GetValidatedObject => item => item;

        protected override Task Validate(UrlServiceModel[] items, ConcurrentQueue<IValidationServiceModel<UrlServiceModel>> validatedItems)
        {
            return Task.Run(() =>
            {
                var comparer = new UrlEqualityComparer();
                var exceptions = new ConcurrentQueue<Exception>();

                items.Distinct(comparer)
                    .GroupBy(i => i.BaseAddress)
                    .AsParallel()
                    .ForAll(group =>
                    {
                        // For different BaseAddress-es requests are parallel,
                        // for equal - sequental.
                        foreach (var item in group)
                        {
                            try
                            {
                                var validationResult = this.MakeRequest(item).Result;
                                this.CacheObtainedData(validationResult).Wait(CachingConstants.WaitAddDataToCacheTimeoutMilliseconds);
                                validatedItems.Enqueue(validationResult);
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

        private async Task<IValidationServiceModel<UrlServiceModel>> MakeRequest(UrlServiceModel item)
        {
            var validationResult = this.GetValidationServiceModel(item);

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
    }
}
