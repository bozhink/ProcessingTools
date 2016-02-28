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
    using Factories;
    using Models;
    using Models.Contracts;

    using ProcessingTools.Bio.Taxonomy.ServiceClient.GlobalNamesResolver;
    using ProcessingTools.Contracts.Types;
    using ProcessingTools.Services.Cache.Contracts;

    public class TaxaValidationService : ValidationServiceFactory<ITaxonName, string>, ITaxaValidationService
    {
        public TaxaValidationService(IValidationCacheService cacheService)
            : base(cacheService)
        {
        }

        protected override Func<string, string> GetContextKey => item => item;

        protected override Func<ITaxonName, string> GetItemToCheck => item => item.Name;

        protected override Func<string, ITaxonName> GetValidatedObject => item => new TaxonName
        {
            Name = item
        };

        protected override async Task Validate(string[] itemsToCheck, ConcurrentQueue<IValidationServiceModel<ITaxonName>> output)
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

                        this.CacheObtainedData(validatedObject).Wait();

                        output.Enqueue(validatedObject);
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
    }
}
