namespace ProcessingTools.Processors.Strategies.Bio.Taxonomy
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using Contracts.Strategies.Bio.Taxonomy;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Extensions;

    public class ParseLowerTaxaWithFullStringMatchStrategy : IParseLowerTaxaWithFullStringMatchStrategy
    {
        private const string GenusPattern = @"[A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+";
        private const string SubgenusPattern = @"[A-Z][a-zçäöüëïâěôûêîæœ\.-]+";
        private const string SpeciesPattern = @"[A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+";
        private const string SubspeciesPattern = SpeciesPattern;
        private const string SuperspeciesPattern = SpeciesPattern;

        private const string InternalSignsPattern = @"[\s\?×]*";
        private const string InternalSignsPatternStrict = @"[\s\?×]+";

        public int ExecutionPriority => 2;

        public async Task<object> Parse(XmlNode context)
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
                    node.InnerXml = this.ParseFullStringMatch(node.InnerXml);
                });

            return await Task.FromResult(true);
        }

        /// <summary>
        /// Parse taxa with full string match.
        /// </summary>
        /// <param name="text">Text string to be parsed.</param>
        /// <returns>Parsed text string.</returns>
        private string ParseFullStringMatch(string text)
        {
            string replace = text

                // Genus species subspecies
                .RegexReplace(
                    $"\\A({GenusPattern})({InternalSignsPatternStrict})({SpeciesPattern})({InternalSignsPatternStrict})({SubspeciesPattern})\\Z",
                    @"<tn-part type=""genus"">$1</tn-part>$2<tn-part type=""species"">$3</tn-part>$4<tn-part type=""subspecies"">$5</tn-part>")

                // Genus species
                .RegexReplace(
                    $"\\A({GenusPattern})({InternalSignsPatternStrict})({SpeciesPattern})\\Z",
                    @"<tn-part type=""genus"">$1</tn-part>$2<tn-part type=""species"">$3</tn-part>")
                .RegexReplace(
                    $"\\A‘({GenusPattern})’({InternalSignsPatternStrict})({SpeciesPattern})\\Z",
                    @"‘<tn-part type=""genus"">$1</tn-part>’$2<tn-part type=""species"">$3</tn-part>")

                // Genus (Subgenus) species subspecies
                .RegexReplace(
                    $"\\A({GenusPattern})({InternalSignsPattern})\\(\\s*({SubgenusPattern})({InternalSignsPattern}?)\\s*\\)({InternalSignsPattern})({SpeciesPattern})({InternalSignsPatternStrict})({SubspeciesPattern})\\Z",
                    @"<tn-part type=""genus"">$1</tn-part>$2(<tn-part type=""subgenus"">$3</tn-part>$4)$5<tn-part type=""species"">$6</tn-part>$7<tn-part type=""subspecies"">$8</tn-part>")

                // Genus (superspecies) species subspecies
                .RegexReplace(
                    $"\\A({GenusPattern})({InternalSignsPattern})\\(\\s*({SuperspeciesPattern})({InternalSignsPattern}?)\\s*\\)({InternalSignsPattern})({SpeciesPattern})({InternalSignsPatternStrict})({SubspeciesPattern})\\Z",
                    @"<tn-part type=""genus"">$1</tn-part>$2(<tn-part type=""superspecies"">$3</tn-part>$4)$5<tn-part type=""species"">$6</tn-part>$7<tn-part type=""subspecies"">$8</tn-part>")

                // Genus (Subgenus) species
                .RegexReplace(
                    $"\\A({GenusPattern})({InternalSignsPattern})\\(\\s*({SubgenusPattern})({InternalSignsPattern}?)\\s*\\)({InternalSignsPattern})({SpeciesPattern})\\Z",
                    @"<tn-part type=""genus"">$1</tn-part>$2(<tn-part type=""subgenus"">$3</tn-part>$4)$5<tn-part type=""species"">$6</tn-part>")

                // Genus (superspecies) species
                .RegexReplace(
                    $"\\A({GenusPattern})({InternalSignsPattern})\\(\\s*({SuperspeciesPattern})({InternalSignsPattern}?)\\s*\\)({InternalSignsPattern})({SpeciesPattern})\\Z",
                    @"<tn-part type=""genus"">$1</tn-part>$2(<tn-part type=""superspecies"">$3</tn-part>$4)$5<tn-part type=""species"">$6</tn-part>")

                // Genus (Subgenus)
                .RegexReplace(
                    $"\\A({GenusPattern})({InternalSignsPattern})\\(\\s*({SubgenusPattern})({InternalSignsPattern}?)\\s*\\)\\Z",
                    @"<tn-part type=""genus"">$1</tn-part>$2(<tn-part type=""subgenus"">$3</tn-part>$4)")

                // Genus
                .RegexReplace(
                    $"\\A({GenusPattern})\\Z",
                    @"<tn-part type=""genus"">$1</tn-part>")

                // species subspecies
                .RegexReplace(
                    $"\\A({SpeciesPattern})\\s+({SubspeciesPattern})\\Z",
                    @"<tn-part type=""species"">$1</tn-part> <tn-part type=""subspecies"">$2</tn-part>")

                // species × species
                .RegexReplace(
                    $"\\A({SpeciesPattern})({InternalSignsPatternStrict})({SubspeciesPattern})\\Z",
                    @"<tn-part type=""species"">$1</tn-part>$2<tn-part type=""species"">$3</tn-part>")

                // species
                .RegexReplace(
                    $"\\A({SpeciesPattern})\\Z",
                    @"<tn-part type=""species"">$1</tn-part>");

            return replace;
        }
    }
}
