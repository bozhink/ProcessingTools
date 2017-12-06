// <copyright file="TaxaValidationService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Validation
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts.Clients.Bio.Taxonomy;
    using ProcessingTools.Constants.Uri;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Extensions;
    using ProcessingTools.Contracts.Models.Validation;
    using ProcessingTools.Services.Abstractions;
    using ProcessingTools.Contracts.Services.Cache;
    using ProcessingTools.Contracts.Services.Validation;

    /// <summary>
    /// Validation service for taxon names.
    /// </summary>
    public class TaxaValidationService : AbstractValidationService<string>, ITaxaValidationService
    {
        private const int MaximalNumberOfItemsToSendAtOnce = 100;
        private readonly IGlobalNamesResolverDataRequester requester;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxaValidationService"/> class.
        /// </summary>
        /// <param name="cacheService">Validation cache service.</param>
        /// <param name="requester">Data requester</param>
        public TaxaValidationService(IValidationCacheService cacheService, IGlobalNamesResolverDataRequester requester)
            : base(cacheService)
        {
            this.requester = requester ?? throw new ArgumentNullException(nameof(requester));
        }

        /// <inheritdoc/>
        protected override Func<string, string> GetPermalink => item => string.Format(
            "{0}:{1}",
            PermalinkPrefixes.ValidationCacheTaxonName,
            item.Trim().Replace(@"\W+", "_", true).ToUpperInvariant());

        /// <inheritdoc/>
        protected override async Task<IValidationModel<string>[]> ValidateAsync(IEnumerable<string> items)
        {
            if (items == null)
            {
                return null;
            }

            var result = new ConcurrentQueue<IValidationModel<string>>();

            var itemsToCheck = new HashSet<string>(items.Where(i => !string.IsNullOrWhiteSpace(i)));
            await this.ProcessItemsToCheckAsync(result, itemsToCheck).ConfigureAwait(false);

            return result.ToArray();
        }

        private void ProcessDatumNode(XmlNode datumNode, ConcurrentQueue<IValidationModel<string>> result)
        {
            string suppliedNameString = datumNode["supplied-name-string"]?.InnerText;
            var validatedObject = this.MapToResponseModel(suppliedNameString);

            try
            {
                IEnumerable<string> nameParts = Regex.Replace(suppliedNameString, @"\W+", " ")
                    .ToLowerInvariant()
                    .Split(' ')
                    .Select(s => s.Trim());

                const string XPathPartPrefix = "[contains(translate(string(.), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'), '";
                const string XPathPartSuffix = "')]";

                string xpathPart = XPathPartPrefix + string.Join(XPathPartSuffix + XPathPartPrefix, nameParts) + XPathPartSuffix;

                XmlNodeList taxaMatches = datumNode.SelectNodes(".//results/result/*" + xpathPart);
                if (taxaMatches.Count < 1)
                {
                    validatedObject.ValidationStatus = ValidationStatus.Invalid;
                }
                else
                {
                    validatedObject.ValidationStatus = ValidationStatus.Valid;
                }
            }
            catch (Exception e)
            {
                validatedObject.ValidationStatus = ValidationStatus.Undefined;
                validatedObject.ValidationException = e;
            }

            result.Enqueue(validatedObject);
        }

        private async Task ProcessItemsToCheckAsync(ConcurrentQueue<IValidationModel<string>> result, IEnumerable<string> items)
        {
            int numberOfItemsToCheck = items.Count();
            for (int i = 0; i < numberOfItemsToCheck + MaximalNumberOfItemsToSendAtOnce; i += MaximalNumberOfItemsToSendAtOnce)
            {
                string[] itemsToSend = null;

                try
                {
                    itemsToSend = items.Skip(i)?.Take(MaximalNumberOfItemsToSendAtOnce)?.ToArray();
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
                    XmlDocument gnrXmlResponse = await this.requester.SearchWithGlobalNamesResolverPost(itemsToSend, null).ConfigureAwait(false);

                    this.SaveResponseToTempDirectory(gnrXmlResponse);

                    gnrXmlResponse.SelectNodes("//datum")
                        .Cast<XmlNode>()
                        .AsParallel()
                        .ForAll(datumNode => this.ProcessDatumNode(datumNode, result));
                }
                catch
                {
                    // Skip
                }
            }
        }

        private void SaveResponseToTempDirectory(XmlDocument gnrXmlResponse)
        {
            try
            {
                gnrXmlResponse.Save($"{System.IO.Path.GetTempPath()}/gnr-{DateTime.UtcNow.Ticks}-{Guid.NewGuid().ToString()}.xml");
            }
            catch
            {
                // Skip
            }
        }
    }
}
