namespace ProcessingTools.Bio.Taxonomy.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Bio.Taxonomy.Types;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;

    // TODO: Consolidate different extension namespace-s
    public static class TaxonomyExtensions
    {
        public static XmlElement CreateTaxonNameXmlElement(this IDocument document, TaxonType type)
        {
            XmlElement tn = document.XmlDocument.CreateElement(ElementNames.TaxonName);
            tn.SetAttribute(AttributeNames.Type, type.ToString().ToLower());
            return tn;
        }

        public static IEnumerable<string> ExtractUniqueNonParsedHigherTaxa(this XmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            var taxaNames = node.SelectNodes(".//tn[@type='higher'][not(tn-part)]")
                .Cast<XmlNode>()
                .Select(c => c.InnerText);

            var result = new HashSet<string>(taxaNames);

            return result;
        }

        public static Task PrintNonParsedTaxa(this XmlDocument xmlDocument, ILogger logger)
        {
            return Task.Run(() =>
            {
                var uniqueHigherTaxaList = xmlDocument.ExtractUniqueNonParsedHigherTaxa()
                    .Distinct()
                    .OrderBy(s => s)
                    .ToList();

                if (uniqueHigherTaxaList.Count > 0)
                {
                    logger?.Log("\nNon-parsed taxa: {0}\n", string.Join("\n\t", uniqueHigherTaxaList));
                }
            });
        }
    }
}
