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
    using Common.Types;
    using Contracts;
    using Models;
    using Models.Contracts;
    using ProcessingTools.Bio.Taxonomy.ServiceClient.GlobalNamesResolver;

    public class TaxaValidationService : ITaxaValidationService
    {
        public async Task<IEnumerable<IValidationServiceModel<ITaxonName>>> Validate(IEnumerable<ITaxonName> items)
        {
            var notFoundNames = new HashSet<string>();
            string[] scientificNames = items.Select(i => i.Name).ToArray<string>();

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
                            result.Enqueue(new TaxonNameValidationServiceModel
                            {
                                ValidatedObject = new TaxonName
                                {
                                    Name = suppliedNameString
                                },
                                ValidationException = null,
                                ValidationStatus = ValidationStatus.Invalid
                            });
                        }
                        else
                        {
                            result.Enqueue(new TaxonNameValidationServiceModel
                            {
                                ValidatedObject = new TaxonName
                                {
                                    Name = suppliedNameString
                                },
                                ValidationException = null,
                                ValidationStatus = ValidationStatus.Valid
                            });
                        }
                    }
                    catch
                    {
                        throw new Exception($"Error: Invalid content in response: {datumNode.InnerXml}");
                    }
                });

            return result;
        }
    }
}
