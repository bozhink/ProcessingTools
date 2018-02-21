// <copyright file="ParseLowerTaxaWithByPartsMatchStrategy.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Bio.Taxonomy.Strategies
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts.Strategies.Bio.Taxonomy;
    using ProcessingTools.Extensions;

    /// <summary>
    /// Parse lower taxa with by-parts-match strategy.
    /// </summary>
    public class ParseLowerTaxaWithByPartsMatchStrategy : IParseLowerTaxaWithByPartsMatchStrategy
    {
        private const string InfraRankPairTaxonNameParts123FormatString = @"<tn-part type=""" + AttributeValues.InfraRank + @""">$1</tn-part>$2<tn-part type=""{0}"">$3</tn-part>";

        private const string InfraTaxonNamePattern = @"([A-Za-zçäöüëïâěôûêîæœ\.-]+)";
        private const string InfraPatternSuffix = @"(\.\s*|\s+)" + InfraTaxonNamePattern;

        /// <inheritdoc/>
        public int ExecutionPriority => 200;

        /// <inheritdoc/>
        public Task<object> ParseAsync(XmlNode context)
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

            return Task.FromResult<object>(true);
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
