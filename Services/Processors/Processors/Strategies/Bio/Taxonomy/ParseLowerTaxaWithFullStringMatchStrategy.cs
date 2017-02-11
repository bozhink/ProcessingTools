namespace ProcessingTools.Processors.Strategies.Bio.Taxonomy
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using Contracts.Strategies.Bio.Taxonomy;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Extensions;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class ParseLowerTaxaWithFullStringMatchStrategy : IParseLowerTaxaWithFullStringMatchStrategy
    {
        private const string GenusPattern = @"[A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+";
        private const string SubgenusPattern = @"[A-Z][a-zçäöüëïâěôûêîæœ\.-]+";
        private const string SpeciesPattern = @"[A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+";
        private const string InfraspecificPattern = SpeciesPattern;
        private const string SuperspeciesPattern = SpeciesPattern;
        private const string SpeciesPatternStrict = @"[a-zçäöüëïâěôûêîæœ\.-]+";
        private const string InfraspecificPatternStrict = SpeciesPatternStrict;

        private const string InternalSignsPattern = @"[\s\?×]*";
        private const string InternalSignsPatternStrict = @"[\s\?×]+";

        private readonly string[,] replaces = new string[,]
        {
            // Genus species subspecies
            {
                $"\\A({GenusPattern})({InternalSignsPatternStrict})({SpeciesPattern})({InternalSignsPatternStrict})({InfraspecificPattern})\\Z",
                @"<tn-part type=""genus"">$1</tn-part>$2<tn-part type=""species"">$3</tn-part>$4<tn-part type=""subspecies"">$5</tn-part>"
            },

            // Genus species
            {
                $"\\A({GenusPattern})({InternalSignsPatternStrict})({SpeciesPattern})\\Z",
                @"<tn-part type=""genus"">$1</tn-part>$2<tn-part type=""species"">$3</tn-part>"
            },
            {
                $"\\A‘({GenusPattern})’({InternalSignsPatternStrict})({SpeciesPattern})\\Z",
                @"‘<tn-part type=""genus"">$1</tn-part>’$2<tn-part type=""species"">$3</tn-part>"
            },

            // Genus (Subgenus) species subspecies
            {
                $"\\A({GenusPattern})({InternalSignsPattern})\\(\\s*({SubgenusPattern})({InternalSignsPattern}?)\\s*\\)({InternalSignsPattern})({SpeciesPattern})({InternalSignsPatternStrict})({InfraspecificPattern})\\Z",
                @"<tn-part type=""genus"">$1</tn-part>$2(<tn-part type=""subgenus"">$3</tn-part>$4)$5<tn-part type=""species"">$6</tn-part>$7<tn-part type=""subspecies"">$8</tn-part>"
            },

            // Genus (superspecies) species subspecies
            {
                    $"\\A({GenusPattern})({InternalSignsPattern})\\(\\s*({SuperspeciesPattern})({InternalSignsPattern}?)\\s*\\)({InternalSignsPattern})({SpeciesPattern})({InternalSignsPatternStrict})({InfraspecificPattern})\\Z",
                @"<tn-part type=""genus"">$1</tn-part>$2(<tn-part type=""superspecies"">$3</tn-part>$4)$5<tn-part type=""species"">$6</tn-part>$7<tn-part type=""subspecies"">$8</tn-part>"
            },

            // Genus (Subgenus) species
            {
                $"\\A({GenusPattern})({InternalSignsPattern})\\(\\s*({SubgenusPattern})({InternalSignsPattern}?)\\s*\\)({InternalSignsPattern})({SpeciesPattern})\\Z",
                @"<tn-part type=""genus"">$1</tn-part>$2(<tn-part type=""subgenus"">$3</tn-part>$4)$5<tn-part type=""species"">$6</tn-part>"
            },

            // Genus (superspecies) species
            {
                $"\\A({GenusPattern})({InternalSignsPattern})\\(\\s*({SuperspeciesPattern})({InternalSignsPattern}?)\\s*\\)({InternalSignsPattern})({SpeciesPattern})\\Z",
                @"<tn-part type=""genus"">$1</tn-part>$2(<tn-part type=""superspecies"">$3</tn-part>$4)$5<tn-part type=""species"">$6</tn-part>"
            },

            // Genus (Subgenus)
            {
                $"\\A({GenusPattern})({InternalSignsPattern})\\(\\s*({SubgenusPattern})({InternalSignsPattern}?)\\s*\\)\\Z",
                @"<tn-part type=""genus"">$1</tn-part>$2(<tn-part type=""subgenus"">$3</tn-part>$4)"
            },

            // Genus
            {
                $"\\A({GenusPattern})\\Z",
                @"<tn-part type=""genus"">$1</tn-part>"
            },

            // species subspecies
            {
                $"\\A({SpeciesPatternStrict})\\s+({InfraspecificPatternStrict})\\Z",
                @"<tn-part type=""species"">$1</tn-part> <tn-part type=""subspecies"">$2</tn-part>"
            },


            // species × species
            {
                $"\\A({SpeciesPatternStrict})({InternalSignsPatternStrict})({SpeciesPatternStrict})\\Z",
                @"<tn-part type=""species"">$1</tn-part>$2<tn-part type=""species"">$3</tn-part>"
            },

            // species
            {
                $"\\A({SpeciesPatternStrict})\\Z",
                @"<tn-part type=""species"">$1</tn-part>"
            },

            {
                @"\A([a-zçäöüëïâěôûêîæœ\.-]+)\s+([Ss]sp|[Ss]ubsp)(\.\s*|\s+)([A-Za-zçäöüëïâěôûêî-]+)\Z",
                @"<tn-part type=""species"">$1</tn-part> <tn-part type=""infraspecific-rank"">$2</tn-part>$3<tn-part type=""subspecies"">$4</tn-part>"
            },

            {
                @"\A([a-zçäöüëïâěôûêîæœ\.-]+)\s+([Vv]ar|[Vv]|[Vv]ariety)(\.\s*|\s+)([A-Za-zçäöüëïâěôûêî-]+)\Z",
                @"<tn-part type=""species"">$1</tn-part> <tn-part type=""infraspecific-rank"">$2</tn-part>$3<tn-part type=""variety"">$4</tn-part>"
            },

            {
                @"\A([a-zçäöüëïâěôûêîæœ\.-]+)\s+([Aa]b)(\.\s*|\s+)([A-Za-zçäöüëïâěôûêî-]+)\Z",
                @"<tn-part type=""species"">$1</tn-part> <tn-part type=""infraspecific-rank"">$2</tn-part>$3<tn-part type=""aberration"">$4</tn-part>"
            },

            {
                @"\A([a-zçäöüëïâěôûêîæœ\.-]+)\s+([Ff]|[Ff]orma?)(\.\s*|\s+)([A-Za-zçäöüëïâěôûêî-]+)\Z",
                @"<tn-part type=""species"">$1</tn-part> <tn-part type=""infraspecific-rank"">$2</tn-part>$3<tn-part type=""form"">$4</tn-part>"
            },

            {
                @"\A([a-zçäöüëïâěôûêîæœ\.-]+)\s+([Ss]f|[Ss]ubforma?)(\.\s*|\s+)([A-Za-zçäöüëïâěôûêî-]+)\Z",
                @"<tn-part type=""species"">$1</tn-part> <tn-part type=""infraspecific-rank"">$2</tn-part>$3<tn-part type=""subform"">$4</tn-part>"
            }
        };

        public int ExecutionPriority => 300;

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
            int length = this.replaces.GetLength(0);
            for (int i = 0; i < length; ++i)
            {
                var re = new Regex(replaces[i, 0]);
                if (re.IsMatch(text))
                {
                    return re.Replace(text, replaces[i, 1]);
                }
            }

            return text;
        }
    }
}
