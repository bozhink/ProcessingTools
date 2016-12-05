namespace ProcessingTools.Services.Validation.Services
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Bio.Taxonomy.ServiceClient.GlobalNamesResolver.Contracts;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Services.Cache.Contracts.Validation;
    using ProcessingTools.Services.Validation.Constants;
    using ProcessingTools.Services.Validation.Contracts;
    using ProcessingTools.Services.Validation.Factories;
    using ProcessingTools.Services.Validation.Models;
    using ProcessingTools.Services.Validation.Models.Contracts;

    public class TaxaValidationService : ValidationServiceFactory<TaxonNameServiceModel, string>, ITaxaValidationService
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

        protected override Func<string, string> GetContextKey => item => item;

        protected override Func<TaxonNameServiceModel, string> GetItemToCheck => item => item.Name;

        protected override Func<string, TaxonNameServiceModel> GetValidatedObject => item => new TaxonNameServiceModel
        {
            Name = item
        };

        protected override async Task Validate(string[] items, ConcurrentQueue<IValidationServiceModel<TaxonNameServiceModel>> validatedItems)
        {
            var exceptions = new ConcurrentQueue<Exception>();

            int numberOfItems = items.Length;

            for (int i = 0; i < numberOfItems + MaximalNumberOfItemsToSendAtOnce; i += MaximalNumberOfItemsToSendAtOnce)
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

                if (itemsToSend?.Length > 0)
                {
                    XmlDocument gnrXmlResponse = await this.requester.SearchWithGlobalNamesResolverPost(itemsToSend);

                    try
                    {
                        gnrXmlResponse.Save($"{System.IO.Path.GetTempPath()}/gnr-{Guid.NewGuid().ToString()}.xml");
                    }
                    catch
                    {
                    }

                    gnrXmlResponse.SelectNodes("//datum")
                        .Cast<XmlNode>()
                        .AsParallel()
                        .ForAll(datumNode =>
                        {
                            try
                            {
                                string suppliedNameString = datumNode["supplied-name-string"].InnerText;

                                var validatedObject = this.GetValidationServiceModel.Invoke(suppliedNameString);

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

                                this.CacheObtainedData(validatedObject).Wait(CachingConstants.WaitAddDataToCacheTimeoutMilliseconds);

                                validatedItems.Enqueue(validatedObject);
                            }
                            catch (Exception e)
                            {
                                exceptions.Enqueue(new Exception($"Error: Invalid content in response: {datumNode.InnerXml}", e));
                            }
                        });
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
