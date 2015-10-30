namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml;
    using Configurator;
    using Contracts;
    using Services.GlobalNamesResolver;

    public class TaxonomicNamesValidator : Base, IBaseValidator
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

        public TaxonomicNamesValidator(IBase baseObject, ILogger logger)
            : base(baseObject)
        {
            this.logger = logger;
        }

        public void Validate()
        {
            string[] scientificNames = this.XmlDocument.ExtractTaxa(true).ToArray<string>();
            try
            {
                XmlDocument gnrXmlResponse = GlobalNamesResolverDataRequester.SearchWithGlobalNamesResolverPost(scientificNames).Result;

                List<string> notFoundNames = new List<string>();

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
            catch
            {
                throw;
            }
        }
    }
}