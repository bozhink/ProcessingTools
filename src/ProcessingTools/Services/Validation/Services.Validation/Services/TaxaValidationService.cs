namespace ProcessingTools.Services.Validation.Services
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using Abstractions;
    using Contracts.Models;
    using Contracts.Services;
    using ProcessingTools.Bio.Taxonomy.ServiceClient.GlobalNamesResolver.Contracts;
    using ProcessingTools.Constants.Uri;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Services.Cache.Contracts.Services.Validation;

    public class TaxaValidationService : AbstractValidationService<string>, ITaxaValidationService
    {
        private const int MaximalNumberOfItemsToSendAtOnce = 100;
        private readonly IGlobalNamesResolverDataRequester requester;

        public TaxaValidationService(IValidationCacheService cacheService, IGlobalNamesResolverDataRequester requester)
            : base(cacheService)
        {
            if (requester == null)
            {
                throw new ArgumentNullException(nameof(requester));
            }

            this.requester = requester;
        }

        protected override Func<string, string> GetPermalink => item => string.Format(
            "{0}:{1}",
            PermalinkPrefixes.ValidationCacheTaxonName,
            item.Trim().RegexReplace(@"\W+", "_").ToLower());

        protected override async Task<IEnumerable<IValidationServiceModel<string>>> Validate(IEnumerable<string> items)
        {
            if (items == null)
            {
                return null;
            }

            var result = new ConcurrentQueue<IValidationServiceModel<string>>();

            var itemsToCheck = new HashSet<string>(items);
            await this.ProcessItemsToCheck(result, itemsToCheck);

            return result;
        }

        private void ProcessDatumNode(XmlNode datumNode, ConcurrentQueue<IValidationServiceModel<string>> result)
        {
            string suppliedNameString = datumNode["supplied-name-string"]?.InnerText;
            var validatedObject = this.MapToResponseModel(suppliedNameString);

            try
            {
                IEnumerable<string> nameParts = Regex.Replace(suppliedNameString, @"\W+", " ")
                    .ToLower()
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

        private async Task ProcessItemsToCheck(ConcurrentQueue<IValidationServiceModel<string>> result, HashSet<string> items)
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
                    XmlDocument gnrXmlResponse = await this.requester.SearchWithGlobalNamesResolverPost(itemsToSend);

                    this.SaveResponseToTempDirectory(gnrXmlResponse);

                    gnrXmlResponse.SelectNodes("//datum")
                        .Cast<XmlNode>()
                        .AsParallel()
                        .ForAll(datumNode => this.ProcessDatumNode(datumNode, result));
                }
                catch
                {
                }
            }
        }

        private void SaveResponseToTempDirectory(XmlDocument gnrXmlResponse)
        {
            try
            {
                gnrXmlResponse.Save($"{System.IO.Path.GetTempPath()}/gnr-{Guid.NewGuid().ToString()}.xml");
            }
            catch
            {
            }
        }
    }
}
