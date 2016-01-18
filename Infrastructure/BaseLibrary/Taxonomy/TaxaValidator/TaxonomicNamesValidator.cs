namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;

    using Bio.Taxonomy.ServiceClient.GlobalNamesResolver;
    using Configurator;
    using ProcessingTools.Contracts;

    public class TaxonomicNamesValidator : Base, IValidator
    {
        private ILogger logger;

        public TaxonomicNamesValidator(string xml, ILogger logger)
            : base(xml)
        {
            this.logger = logger;
        }

        public TaxonomicNamesValidator(Config config, string xml, ILogger logger)
            : base(config, xml)
        {
            this.logger = logger;
        }

        public async Task Validate()
        {
            var notFoundNames = new HashSet<string>();
            string[] scientificNames = this.XmlDocument.ExtractTaxa(true).ToArray<string>();

            var resolver = new GlobalNamesResolverDataRequester();
            XmlDocument gnrXmlResponse = await resolver.SearchWithGlobalNamesResolverPost(scientificNames);

            foreach (XmlNode datumNode in gnrXmlResponse.SelectNodes("//datum"))
            {
                try
                {
                    string suppliedNameString = datumNode["supplied-name-string"].InnerText;

                    IEnumerable<string> nameParts = Regex.Replace(suppliedNameString, @"\W+", " ").ToLower().Split(' ').Select(s => s.Trim());

                    const string XPathPartPrefix = "[contains(translate(string(.), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'), '";
                    const string XPathPartSuffix = "')]";

                    string xpathPart = XPathPartPrefix + string.Join(XPathPartSuffix + XPathPartPrefix, nameParts) + XPathPartSuffix;

                    XmlNodeList taxaMatches = datumNode.SelectNodes(".//results/result/*" + xpathPart);
                    if (taxaMatches.Count < 1)
                    {
                        notFoundNames.Add(suppliedNameString);
                    }
                }
                catch
                {
                    this.logger?.Log("Error: Invalid content in response: {0}", datumNode.InnerXml);
                }
            }

            this.logger?.Log("Not found taxa names:\n|\t{0}\n", string.Join("\n|\t", notFoundNames));

            try
            {
                gnrXmlResponse.Save(this.Config.GnrOutputFileName);
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
            }
        }
    }
}