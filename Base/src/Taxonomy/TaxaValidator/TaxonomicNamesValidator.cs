namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml;

    public class TaxonomicNamesValidator : Base, IBaseValidator
    {
        public TaxonomicNamesValidator(string xml)
            : base(xml)
        {
        }

        public TaxonomicNamesValidator(Config config, string xml)
            : base(config, xml)
        {
        }

        public TaxonomicNamesValidator(IBase baseObject)
            : base(baseObject)
        {
        }

        public void Validate()
        {
            string[] scientificNames = this.XmlDocument.ExtractTaxa(true).ToArray<string>();
            try
            {
                XmlDocument gnrXmlResponse = Net.SearchWithGlobalNamesResolverPost(scientificNames);

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
                        Alert.Log("Error: Invalid content in response: {0}", datumNode.InnerXml);
                    }
                }

                Alert.Log("Not found taxa names:\n|\t{0}\n", string.Join("\n|\t", notFoundNames));

                try
                {
                    gnrXmlResponse.Save(this.Config.GnrOutputFileName);
                }
                catch (Exception e)
                {
                    Alert.RaiseExceptionForMethod(e, 0);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}