namespace ProcessingTools.Processors.Strategies.Bio.Taxonomy
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts.Strategies.Bio.Taxonomy;
    using ProcessingTools.Extensions;
    using ProcessingTools.Processors.Common.Bio.Taxonomy;

    public class ParseLowerTaxaWithByPartsMatchStrategy : IParseLowerTaxaWithByPartsMatchStrategy
    {
        private const string InfraRankPairTaxonNameParts123FormatString = @"<tn-part type=""" + AttributeValues.InfraRank + @""">$1</tn-part>$2<tn-part type=""{0}"">$3</tn-part>";

        private const string InfraTaxonNamePattern = @"([A-Za-zçäöüëïâěôûêîæœ\.-]+)";
        private const string InfraPatternSuffix = @"(\.\s*|\s+)" + InfraTaxonNamePattern;

        public int ExecutionPriority => 200;

        public async Task<object> ParseAsync(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.SelectNodes(XPathStrings.LowerTaxonNameWithNoTaxonNamePart)
                .Cast<XmlNode>()
                .AsParallel()
                .ForAll(node =>
                {
                    node.InnerXml = this.ParseDifferentPartsOfTaxonomicNames(node.InnerXml);
                });

            return await Task.FromResult(true).ConfigureAwait(false);
        }

        /// <summary>
        /// Parse different parts of the taxonomic name.
        /// </summary>
        /// <param name="text">Text string to be parsed.</param>
        /// <returns>Parsed text string.</returns>
        private string ParseDifferentPartsOfTaxonomicNames(string text)
        {
            var patternRanks = SpeciesPartsPrefixesResolver.NonAmbiguousSpeciesPartsRanks.GroupBy(p => p.Value)
                .Select(g => new
                {
                    Rank = g.Key,
                    Pattern = @"\b((?i)" + string.Join("|", g.Select(p => Regex.Escape(p.Key)).ToArray()) + @")\b"
                });

            string replace = text;
            foreach (var patternRank in patternRanks)
            {
                replace = replace.RegexReplace(
                    patternRank.Pattern + InfraPatternSuffix,
                    string.Format(InfraRankPairTaxonNameParts123FormatString, patternRank.Rank.ToRankString()));
            }

            return replace;
        }
    }
}
