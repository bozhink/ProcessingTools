namespace ProcessingTools.Services.Validation.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Abstractions;
    using Contracts.Models;
    using Contracts.Services;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Services.Cache.Contracts.Services.Validation;

    public class UrlValidationService : AbstractValidationService<string>, IUrlValidationService
    {
        private const int MaximalNumberOfItemsToSendAtOnce = 10;

        public UrlValidationService(IValidationCacheService cacheService)
            : base(cacheService)
        {
        }

        protected override Func<string, string> GetPermalink => item => item;

        protected override async Task<IEnumerable<IValidationServiceModel<string>>> Validate(IEnumerable<string> items) => await Task.Run(() =>
        {
            if (items == null)
            {
                return null;
            }

            var result = items.Distinct().Select(this.MapToResponseModel).ToList();

            int numberOfItemsToCheck = result.Count();
            for (int i = 0; i < numberOfItemsToCheck + MaximalNumberOfItemsToSendAtOnce; i += MaximalNumberOfItemsToSendAtOnce)
            {
                IValidationServiceModel<string>[] itemsToSend = null;

                try
                {
                    itemsToSend = result.Skip(i)?.Take(MaximalNumberOfItemsToSendAtOnce)?.ToArray();
                }
                catch
                {
                    continue;
                }

                if (itemsToSend?.Length < 1)
                {
                    continue;
                }

                try
                {
                    itemsToSend.AsParallel().ForAll(item => this.MakeRequest(item).Wait());
                }
                catch
                {
                    // Skip
                }
            }

            return result;
        });

        private async Task MakeRequest(IValidationServiceModel<string> item)
        {
            try
            {
                var validatedObject = item.ValidatedObject;
                if (string.IsNullOrWhiteSpace(validatedObject))
                {
                    throw new ApplicationException($"URL address is null or whitespace: '{validatedObject}'.");
                }

                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(validatedObject);
                    item.ValidationStatus = this.MapHttpStatusCodeToValidationStatus(response.StatusCode);
                }
            }
            catch (Exception e)
            {
                item.ValidationStatus = ValidationStatus.Undefined;
                item.ValidationException = e;
            }
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
