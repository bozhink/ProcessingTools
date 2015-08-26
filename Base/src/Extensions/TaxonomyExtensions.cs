namespace ProcessingTools.Base
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml;

    public static class TaxonomyExtensions
    {
        public static string RemoveTaxonNamePartTags(this string content)
        {
            string result = Regex.Replace(content, @"(?<=full-name=""([^<>""]+)""[^>]*>)[^<>]*(?=</)", "$1");
            return Regex.Replace(result, "</?tn-part[^>]*>|</?tp:taxon-name-part[^>]*>", string.Empty);
        }

        public static void RemoveTaxonNamePartTags(this XmlDocument xmlDocument)
        {
            foreach (XmlNode taxonName in xmlDocument.SelectNodes("//tn[name(..)!='tp:nomenclature']|//tp:taxon-name[name(..)!='tp:nomenclature']", Config.TaxPubNamespceManager(xmlDocument)))
            {
                taxonName.InnerXml = taxonName.InnerXml.RemoveTaxonNamePartTags();
            }
        }

        public static List<string> ExtractUniqueHigherTaxa(this XmlDocument xmlDocument)
        {
            XmlNamespaceManager xmlNamespaceManager = Config.TaxPubNamespceManager(xmlDocument);
            XmlNodeList nodeList = xmlDocument.SelectNodes("//tn[@type='higher'][not(tn-part)]", xmlNamespaceManager);
            return nodeList.Cast<XmlNode>().Select(c => c.InnerXml).Distinct().ToList();
        }

        public static string GetRemplacementStringForTaxonNamePartRank(this string rank, bool taxPub = false)
        {
            string prefix, suffix;
            if (taxPub)
            {
                prefix = "<tp:taxon-name-part taxon-name-part-type=\"";
                suffix = "\">$1</tp:taxon-name-part>";
            }
            else
            {
                prefix = "<tn-part type=\"";
                suffix = "\">$1</tn-part>";
            }

            return prefix + rank + suffix;
        }

        public static void PrintNonParsedTaxa(this XmlDocument xmlDocument)
        {
            List<string> uniqueHigherTaxaList = xmlDocument.ExtractUniqueHigherTaxa();
            if (uniqueHigherTaxaList.Count > 0)
            {
                Alert.Log("\nNon-parsed taxa:");
                foreach (string taxon in uniqueHigherTaxaList)
                {
                    Alert.Log("\t" + taxon);
                }

                Alert.Log();
            }
        }
    }
}
