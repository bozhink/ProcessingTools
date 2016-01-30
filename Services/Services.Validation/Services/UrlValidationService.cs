namespace ProcessingTools.Services.Validation.Services
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

    using ProcessingTools.Services.Common.Models.Contracts;
    using ProcessingTools.Services.Common.Types;

    public class UrlValidationService : IUrlValidationService
    {
        public Task<IEnumerable<IValidationServiceModel<IUrl>>> Validate(IEnumerable<IUrl> items)
        {
            return Task.Run(() =>
            {
                var result = new ConcurrentQueue<IValidationServiceModel<IUrl>>();

                items.GroupBy(i => i.BaseAddress)
                    .AsParallel()
                    .ForAll(group =>
                    {
                        // For different BaseAddress-es requests are parallel,
                        // for equal - sequental.
                        foreach (var item in group)
                        {
                            var validationResult = MakeRequest(item).Result;
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
    }
}
