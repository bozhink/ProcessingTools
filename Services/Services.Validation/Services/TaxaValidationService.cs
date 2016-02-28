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
            if (items == null)
            {
                throw new ArgumentNullException("items", "Items to validate should not be null.");
            }

            if (items.Length < 1)
            {
                throw new ApplicationException("Number of items to validate should be greater tham zero.");
            }

            var result = new ConcurrentQueue<IValidationServiceModel<ITaxonName>>();

            bool cacheServiceIsUsable;
            string[] itemsToCheck;
            try
            {
                itemsToCheck = this.ValidateItemsFromCache(items, result);
                cacheServiceIsUsable = true;
            }
            catch
            {
                itemsToCheck = items.Select(i => i.Name).ToArray();
                cacheServiceIsUsable = false;
            }

            if (itemsToCheck.Count() < 1)
            {
                // All requested items are already cached and their status is Valid.
                return result;
            }

            await Validate(result, cacheServiceIsUsable, itemsToCheck);

            return result;
        }

        private async Task Validate(ConcurrentQueue<IValidationServiceModel<ITaxonName>> result, bool cacheServiceIsUsable, string[] itemsToCheck)
        {
            var exceptions = new ConcurrentQueue<Exception>();

            var resolver = new GlobalNamesResolverDataRequester();
            XmlDocument gnrXmlResponse = await resolver.SearchWithGlobalNamesResolverPost(itemsToCheck);

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

                        if (cacheServiceIsUsable)
                        {
                            this.CacheObtainedData(validatedObject);
                        }

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
        }

        private string[] ValidateItemsFromCache(ITaxonName[] items, ConcurrentQueue<IValidationServiceModel<ITaxonName>> validatedItems)
        {
            var itemsToCheck = new ConcurrentQueue<string>();
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
                        itemsToCheck.Enqueue(name);
                    }
                });

            return itemsToCheck.ToArray<string>();
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
