namespace ProcessingTools.Base.Taxonomy
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml;

    public class TaxonomicNamesValidator : Base, IValidator
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
                        ////Alert.Log(suppliedNameString);

                        IEnumerable<string> nameParts = Regex.Replace(suppliedNameString, @"\W+", " ").ToLower().Split(' ').Select(s => s.Trim());

                        const string XPathPartPrefix = "[contains(translate(string(.), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'), '";
                        const string XPathPartSuffix = "')]";

                        string xpathPart = XPathPartPrefix + string.Join(XPathPartSuffix + XPathPartPrefix, nameParts) + XPathPartSuffix;

                        XmlNodeList taxaMatches = datumNode.SelectNodes(".//results/result/*" + xpathPart);
                        if (taxaMatches.Count < 1)
                        {
                            notFoundNames.Add(suppliedNameString);
                        }
                        else
                        {
                            ////foreach (XmlNode match in taxaMatches)
                            ////{
                            ////    Alert.Log("|->\t{0}\n", match.InnerText);
                            ////}
                        }
                    }
                    catch
                    {
                        Alert.Log("Error: Invalid content in response: {0}", datumNode.InnerXml);
                    }
                }

                Alert.Log("Not found taxa names:\n|\t{0}\n", string.Join("\n|\t", notFoundNames));

                // TODO
                gnrXmlResponse.Save(@"C:\temp\gnr-response.xml");
            }
            catch
            {
                throw;
            }
        }
    }
}