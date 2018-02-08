// <copyright file="UrlValidationService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Models.Contracts.Validation;
    using ProcessingTools.Services.Abstractions.Validation;
    using ProcessingTools.Services.Contracts.Cache;
    using ProcessingTools.Services.Contracts.Validation;

    /// <summary>
    /// URL validation service.
    /// </summary>
    public class UrlValidationService : AbstractValidationService<string>, IUrlValidationService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UrlValidationService"/> class.
        /// </summary>
        /// <param name="cacheService">Validation cache service.</param>
        public UrlValidationService(IValidationCacheService cacheService)
            : base(cacheService)
        {
        }

        /// <inheritdoc/>
        protected override Func<string, string> GetPermalink => item => item;

        /// <inheritdoc/>
        protected override async Task<IValidationModel<string>[]> ValidateAsync(IEnumerable<string> items)
        {
            if (items == null || !items.Any())
            {
                return null;
            }

            var data = items.Where(i => !string.IsNullOrWhiteSpace(i))
                .Distinct()
                .Select(this.MapToResponseModel)
                .ToArray();

            foreach (var item in data)
            {
                await this.MakeRequestAsync(item).ConfigureAwait(false);
            }

            return data;
        }

        private async Task MakeRequestAsync(IValidationModel<string> item)
        {
            try
            {
                var validatedObject = item.ValidatedObject;
                if (string.IsNullOrWhiteSpace(validatedObject))
                {
                    throw new UriFormatException($"URL address is null or whitespace: '{validatedObject}'.");
                }

                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(validatedObject).ConfigureAwait(false);
                    item.ValidationStatus = this.MapHttpStatusCodeToValidationStatus(response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                item.ValidationStatus = ValidationStatus.Undefined;
                item.ValidationException = ex;
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
