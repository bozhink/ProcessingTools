namespace ProcessingTools.Harvesters.Harvesters.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using Contracts.Harvesters.Bio.Taxonomy;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Extensions;
    using ProcessingTools.Extensions.Linq;

    public class LowerTaxaHarvester : ILowerTaxaHarvester
    {
        private Func<XmlNode, string> GetXmlNodeStringValue => t => t.InnerXml.Replace("?", string.Empty)
            .RegexReplace(@"<(object-id)[^>]*(?:/>|[\s\S]*?</\1>)", string.Empty)
            .RegexReplace(@"</[^>]*>(?=[^\s\)\]])(?!\Z)", " ")
            .RegexReplace(@"<[^>]+>", string.Empty)
            .RegexReplace(@"\s+", " ")
            .Trim();

        public async Task<IEnumerable<string>> Harvest(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var result = await this.GetKnownLowerTaxa(context).ToArrayAsync();
            return result;
        }

        private IEnumerable<string> GetKnownLowerTaxa(XmlNode context)
        {
            var result = context.SelectNodes(XPathStrings.LowerTaxonNames)
                .Cast<XmlNode>()
                .Select(this.GetXmlNodeStringValue)
                .Distinct();

            return result;
        }
    }
}
