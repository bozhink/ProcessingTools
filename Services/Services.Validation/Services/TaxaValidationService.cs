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

        public async Task<IEnumerable<IValidationServiceModel<ITaxonName>>> Validate(IEnumerable<ITaxonName> items)
        {
            string[] scientificNames = items.Select(i => i.Name).ToArray<string>();

            var exceptions = new ConcurrentQueue<Exception>();
            var result = new ConcurrentQueue<IValidationServiceModel<ITaxonName>>();

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

                        // Cache obtained data.
                        this.cacheService.Add(
                            validatedObject.ValidatedObject.Name,
                            new ValidationCacheServiceModel
                            {
                                Status = validatedObject.ValidationStatus,
                                LastUpdate = DateTime.Now
                            }).Wait();

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
    }
}
