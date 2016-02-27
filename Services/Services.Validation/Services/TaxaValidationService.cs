namespace ProcessingTools.Services.Validation
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;

    using Common.Models.Contracts;
    using Contracts;
    using Models;
    using Models.Contracts;
    using ProcessingTools.Bio.Taxonomy.ServiceClient.GlobalNamesResolver;
    using ProcessingTools.Contracts.Types;
    using ProcessingTools.Services.Cache.Contracts;
    using ProcessingTools.Services.Cache.Models;

    public class TaxaValidationService : ITaxaValidationService
    {
        private IValidationCacheService cacheService;

        public TaxaValidationService(IValidationCacheService cacheService)
        {
            if (cacheService == null)
            {
                throw new ArgumentNullException("cacheService");
            }

            this.cacheService = cacheService;
        }

        public async Task<IEnumerable<IValidationServiceModel<ITaxonName>>> Validate(params ITaxonName[] items)
        {
            var exceptions = new ConcurrentQueue<Exception>();
            var result = new ConcurrentQueue<IValidationServiceModel<ITaxonName>>();

            var itemsToCheck = this.ValidateItemsFromCache(items, result);

            string[] scientificNames = itemsToCheck.ToArray<string>();

            if (scientificNames.Length < 1)
            {
                // All requested items are already cached and their status is Valid.
                return result;
            }

            var resolver = new GlobalNamesResolverDataRequester();
            XmlDocument gnrXmlResponse = await resolver.SearchWithGlobalNamesResolverPost(scientificNames);

            gnrXmlResponse.SelectNodes("//datum")
                .Cast<XmlNode>()
                .AsParallel()
                .ForAll(datumNode =>
                {
                    try
                    {
                        string suppliedNameString = datumNode["supplied-name-string"].InnerText;

                        var validatedObject = new TaxonNameValidationServiceModel
                        {
                            ValidatedObject = new TaxonName
                            {
                                Name = suppliedNameString
                            },
                            ValidationException = null
                        };

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

                        this.CacheObtainedData(validatedObject);

                        result.Enqueue(validatedObject);
                    }
                    catch (Exception e)
                    {
                        exceptions.Enqueue(new Exception($"Error: Invalid content in response: {datumNode.InnerXml}", e));
                    }
                });

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }

            return result;
        }

        private IEnumerable<string> ValidateItemsFromCache(ITaxonName[] items, ConcurrentQueue<IValidationServiceModel<ITaxonName>> validatedItems)
        {
            var namesToCheck = new ConcurrentQueue<string>();
            items.Select(i => i.Name)
                .AsParallel()
                .ForAll(name =>
                {
                    var cachedItems = this.cacheService.All(name).Result.ToList();

                    int numberOfValidMatches = cachedItems
                        .Where(i => i.Status == ValidationStatus.Valid)
                        .Count();

                    int numberOfNonValidMatches = cachedItems
                        .Where(i => i.Status != ValidationStatus.Valid)
                        .Count();

                    if (numberOfNonValidMatches == 0 && numberOfValidMatches > 0)
                    {
                        var validatedObject = new TaxonNameValidationServiceModel
                        {
                            ValidatedObject = new TaxonName
                            {
                                Name = name
                            },
                            ValidationException = null,
                            ValidationStatus = ValidationStatus.Valid
                        };

                        validatedItems.Enqueue(validatedObject);
                    }
                    else
                    {
                        namesToCheck.Enqueue(name);
                    }
                });

            return namesToCheck;
        }

        private void CacheObtainedData(TaxonNameValidationServiceModel validatedObject)
        {
            this.cacheService.Add(
                validatedObject.ValidatedObject.Name,
                new ValidationCacheServiceModel
                {
                    Status = validatedObject.ValidationStatus,
                    LastUpdate = DateTime.Now
                }).Wait();
        }
    }
}
