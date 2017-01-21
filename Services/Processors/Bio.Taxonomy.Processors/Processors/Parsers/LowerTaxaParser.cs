namespace ProcessingTools.Bio.Taxonomy.Processors.Processors.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Bio.Taxonomy.Extensions;
    using ProcessingTools.Bio.Taxonomy.Processors.Comparers;
    using ProcessingTools.Bio.Taxonomy.Processors.Contracts.Parsers;
    using ProcessingTools.Bio.Taxonomy.Processors.Models.Parsers;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Extensions;
    using ProcessingTools.Xml.Extensions;

    public class LowerTaxaParser : ILowerTaxaParser
    {
        private const string InfraRank = "infraspecific-rank";
        private const string InfraRankPairTaxonNameParts123FormatString = @"<tn-part type=""" + InfraRank + @""">$1</tn-part>$2<tn-part type=""{0}"">$3</tn-part>";
        private const string SelectLowerTaxaWithInvalidChildNodesXPath = ".//tn[@type='lower'][count(*) != count(tn-part)]";
        private const string SelectLowerTaxaWithoutChildNodesXPath = ".//tn[@type='lower'][not(*)]";
        private const string TaxonNamePartElementFormatString = @"<tn-part type=""{0}"">{1}</tn-part>";
        private const string UncertaintyRank = "uncertainty-rank";
        private const string UncertaintyRankPairTaxonNameParts123FormatString = @"<tn-part type=""" + UncertaintyRank + @""">$1</tn-part>$2<tn-part type=""{0}"">$3</tn-part>";
        private readonly ILogger logger;

        public LowerTaxaParser(ILogger logger)
        {
            this.logger = logger;
        }

        public Task<object> Parse(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return Task.Run(() =>
            {
                return this.ParseSync(context);
            });
        }

        private static string GetInfraRankTaxonNameParts123ReplaceString(SpeciesPartType type)
        {
            return string.Format(InfraRankPairTaxonNameParts123FormatString, type.ToString().ToLower());
        }

        private static string GetUncertaintyRankTaxonNameParts123ReplaceString(SpeciesPartType type)
        {
            return string.Format(UncertaintyRankPairTaxonNameParts123FormatString, type.ToString().ToLower());
        }

        /// <summary>
        /// Parse different parts of the taxonomic name.
        /// </summary>
        /// <param name="text">Text string to be parsed.</param>
        /// <returns>Parsed text string.</returns>
        private static string ParseDifferentPartsOfTaxonomicNames(string text)
        {
            const string InfraTaxonNamePattern = @"([A-Za-zçäöüëïâěôûêîæœ\.-]+)";
            const string InfraPatternSuffix = @"(\.\s*|\s+)" + InfraTaxonNamePattern;

            // TODO: add other infrageneric ranks
            string replace = text
                .RegexReplace(
                    @"\b((?i)sect?|section)\b" + InfraPatternSuffix,
                    GetInfraRankTaxonNameParts123ReplaceString(SpeciesPartType.Section))

                .RegexReplace(
                    @"\b([Ss]ubvar)\b" + InfraPatternSuffix,
                    GetInfraRankTaxonNameParts123ReplaceString(SpeciesPartType.Subvariety))

                .RegexReplace(
                    @"\b([Vv]ar|[Vv]ariety|v)\b" + InfraPatternSuffix,
                    GetInfraRankTaxonNameParts123ReplaceString(SpeciesPartType.Variety))

                .RegexReplace(
                    @"\b([Ff]orma?|f)\b" + InfraPatternSuffix,
                    GetInfraRankTaxonNameParts123ReplaceString(SpeciesPartType.Form))

                .RegexReplace(
                    @"\b(sf|[Ss]ubforma?)\b" + InfraPatternSuffix,
                    GetInfraRankTaxonNameParts123ReplaceString(SpeciesPartType.Subform))

                .RegexReplace(
                    @"\b([Aa]b)\b" + InfraPatternSuffix,
                    GetInfraRankTaxonNameParts123ReplaceString(SpeciesPartType.Aberration))

                .RegexReplace(
                    @"\b([Ss]ubsp|[Ss]sp)\b" + InfraPatternSuffix,
                    GetInfraRankTaxonNameParts123ReplaceString(SpeciesPartType.Subspecies))

                .RegexReplace(
                    @"\b([Aa]ff|[Cc]f|[Nn]r|[Ss]p|[Ss]p\.?\s*ne?a?r|[Ss]p\s+ne?a?r)\b" + InfraPatternSuffix,
                    GetUncertaintyRankTaxonNameParts123ReplaceString(SpeciesPartType.Species))

                .RegexReplace(
                    @"\b([Ss]ubgen|[Ss]ubgenus|[Ss]g)\b" + InfraPatternSuffix,
                    GetInfraRankTaxonNameParts123ReplaceString(SpeciesPartType.Subgenus))

                .RegexReplace(
                    @"\b([Ss]ect|[Ss]ection)\b" + InfraPatternSuffix,
                    GetInfraRankTaxonNameParts123ReplaceString(SpeciesPartType.Section))

                .RegexReplace(
                    @"\b([Ss]ubsect|[Ss]ubsection)\b" + InfraPatternSuffix,
                    GetInfraRankTaxonNameParts123ReplaceString(SpeciesPartType.Subsection));

            return replace;
        }

        /// <summary>
        /// Parse taxa with full string match.
        /// </summary>
        /// <param name="text">Text string to be parsed.</param>
        /// <returns>Parsed text string.</returns>
        private static string ParseFullStringMatch(string text)
        {
            const string GenusPattern = @"[A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+";
            ////const string SubgenusPattern = @"[A-Z][a-zçäöüëïâěôûêîæœ\.-]+";
            const string SpeciesPattern = @"[A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+";
            const string SubspeciesPattern = @"[A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+";

            const string InternalSignsPattern = @"[\s\?×]+";

            string replace = text;

            // Genus species subspecies
            replace = Regex.Replace(
                replace,
                $"\\A({GenusPattern})({InternalSignsPattern})({SpeciesPattern})({InternalSignsPattern})({SubspeciesPattern})\\Z",
                "<tn-part type=\"genus\">$1</tn-part>$2<tn-part type=\"species\">$3</tn-part>$4<tn-part type=\"subspecies\">$5</tn-part>");

            // Genus species
            replace = Regex.Replace(
                replace,
                $"\\A({GenusPattern})({InternalSignsPattern})({SpeciesPattern})\\Z",
                "<tn-part type=\"genus\">$1</tn-part>$2<tn-part type=\"species\">$3</tn-part>");
            replace = Regex.Replace(
                replace,
                $"\\A‘({GenusPattern})’({InternalSignsPattern})({SpeciesPattern})\\Z",
                "‘<tn-part type=\"genus\">$1</tn-part>’$2<tn-part type=\"species\">$3</tn-part>");

            // Genus (Subgenus) species subspecies
            replace = Regex.Replace(
                replace,
                @"\A([A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+)([\s\?×]*)\(\s*([A-Z][a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]*?)\s*\)([\s\?×]*)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)\Z",
                "<tn-part type=\"genus\">$1</tn-part>$2(<tn-part type=\"subgenus\">$3</tn-part>$4)$5<tn-part type=\"species\">$6</tn-part>$7<tn-part type=\"subspecies\">$8</tn-part>");

            // Genus (superspecies) species subspecies
            replace = Regex.Replace(
                replace,
                @"\A([A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+)([\s\?×]*)\(\s*([a-z][a-zçäöüëïâěôûêî\.-]+)([\s\?×]*?)\s*\)([\s\?×]*)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)\Z",
                "<tn-part type=\"genus\">$1</tn-part>$2(<tn-part type=\"superspecies\">$3</tn-part>$4)$5<tn-part type=\"species\">$6</tn-part>$7<tn-part type=\"subspecies\">$8</tn-part>");

            // Genus (Subgenus) species
            replace = Regex.Replace(
                replace,
                @"\A([A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+)([\s\?×]*)\(\s*([A-Z][a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]*?)\s*\)([\s\?×]*)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)\Z",
                "<tn-part type=\"genus\">$1</tn-part>$2(<tn-part type=\"subgenus\">$3</tn-part>$4)$5<tn-part type=\"species\">$6</tn-part>");

            // Genus (superspecies) species
            replace = Regex.Replace(
                replace,
                @"\A([A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+)([\s\?×]*)\(\s*([a-z][a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]*?)\s*\)([\s\?×]*)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)\Z",
                "<tn-part type=\"genus\">$1</tn-part>$2(<tn-part type=\"superspecies\">$3</tn-part>$4)$5<tn-part type=\"species\">$6</tn-part>");

            // Genus (Subgenus)
            replace = Regex.Replace(
                replace,
                @"\A([A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+)([\s\?×]*)\(\s*([A-Z][a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]*?)\s*\)\Z",
                "<tn-part type=\"genus\">$1</tn-part>$2(<tn-part type=\"subgenus\">$3</tn-part>$4)");

            // Genus
            replace = Regex.Replace(
                replace,
                @"\A([A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+)\Z",
                "<tn-part type=\"genus\">$1</tn-part>");

            // species
            replace = Regex.Replace(
                replace,
                @"\A([a-zçäöüëïâěôûêîæœ\.-]+)\Z",
                @"<tn-part type=""species"">$1</tn-part>");
            return replace;
        }

        private static string ParseLower(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return text;
            }

            string replace = text;

            replace = ParseFullStringMatch(replace);

            replace = ParseDifferentPartsOfTaxonomicNames(replace);

            replace = ParseTaxaFromBeginning(replace);

            replace = ParseWholeString(replace);

            // Parse question mark
            replace = Regex.Replace(replace, @"(?<=</tn-part>)\s*\?", "<tn-part type=\"uncertainty-rank\">?</tn-part>");

            // Parse hybrid sign
            replace = Regex.Replace(replace, @"(?<=</tn-part>\s*)×(?=\s*<tn-part)", "<tn-part type=\"hybrid-sign\">×</tn-part>");

            // Here we must return the dot after tn-part[@type='infraspecific-rank'] back into the tag
            replace = Regex.Replace(replace, @"</tn-part>\.", ".</tn-part>");

            // Clear multiple white spaces between ‘tn-part’-s
            replace = Regex.Replace(replace, @"(?<=</tn-part>)\s{2,}(?=<tn-part)", " ");

            return replace;
        }

        private static string ParseTaxaFromBeginning(string text)
        {
            string replace = text;

            // Genus species subspecies
            replace = Regex.Replace(
                replace,
                @"\A([A-Z][a-z\.]+)([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)",
                "<tn-part type=\"genus\">$1</tn-part>$2<tn-part type=\"species\">$3</tn-part>$4<tn-part type=\"subspecies\">$5</tn-part>");

            // Genus species
            replace = Regex.Replace(
                replace,
                @"\A([A-Z][a-z\.]+)([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)",
                "<tn-part type=\"genus\">$1</tn-part>$2<tn-part type=\"species\">$3</tn-part>");
            replace = Regex.Replace(
                replace,
                @"\A‘([A-Z][a-z\.]+)’([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)",
                "‘<tn-part type=\"genus\">$1</tn-part>’$2<tn-part type=\"species\">$3</tn-part>");

            // Genus (Subgenus) species subspecies
            replace = Regex.Replace(
                replace,
                @"\A([A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+)([\s\?×]*)\(\s*([A-Z][a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]*?)\s*\)([\s\?×]*)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)",
                "<tn-part type=\"genus\">$1</tn-part>$2(<tn-part type=\"subgenus\">$3</tn-part>$4)$5<tn-part type=\"species\">$6</tn-part>$7<tn-part type=\"subspecies\">$8</tn-part>");

            // Genus (superspecies) species subspecies
            replace = Regex.Replace(
                replace,
                @"\A([A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+)([\s\?×]*)\(\s*([a-z][a-zçäöüëïâěôûêî\.-]+)([\s\?×]*?)\s*\)([\s\?×]*)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)",
                "<tn-part type=\"genus\">$1</tn-part>$2(<tn-part type=\"superspecies\">$3</tn-part>$4)$5<tn-part type=\"species\">$6</tn-part>$7<tn-part type=\"subspecies\">$8</tn-part>");

            // Genus (Subgenus) species
            replace = Regex.Replace(
                replace,
                @"\A([A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+)([\s\?×]*)\(\s*([A-Z][a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]*?)\s*\)([\s\?×]*)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)",
                "<tn-part type=\"genus\">$1</tn-part>$2(<tn-part type=\"subgenus\">$3</tn-part>$4)$5<tn-part type=\"species\">$6</tn-part>");

            // Genus (superspecies) species
            replace = Regex.Replace(
                replace,
                @"\A([A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+)([\s\?×]*)\(\s*([a-z][a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]*?)\s*\)([\s\?×]*)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)",
                "<tn-part type=\"genus\">$1</tn-part>$2(<tn-part type=\"superspecies\">$3</tn-part>$4)$5<tn-part type=\"species\">$6</tn-part>");

            // Genus (Subgenus)
            replace = Regex.Replace(
                replace,
                @"\A([A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+)([\s\?×]*)\(\s*([A-Z][a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]*?)\s*\)",
                "<tn-part type=\"genus\">$1</tn-part>$2(<tn-part type=\"subgenus\">$3</tn-part>$4)");

            // Genus
            replace = Regex.Replace(
                replace,
                @"\A([A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+)",
                "<tn-part type=\"genus\">$1</tn-part>");

            // species
            replace = Regex.Replace(
                replace,
                @"\A([a-zçäöüëïâěôûêîæœ\.-]+)",
                @"<tn-part type=""species"">$1</tn-part>");
            return replace;
        }

        private static string ParseWholeString(string text)
        {
            string replace = text;

            replace = Regex.Replace(
                replace,
                @"\A([a-zçäöüëïâěôûêîæœ\.-]+)\s+([Ss]sp|[Ss]ubsp)(\.\s*|\s+)([A-Za-zçäöüëïâěôûêî-]+)\Z",
                "<tn-part type=\"species\">$1</tn-part> <tn-part type=\"infraspecific-rank\">$2</tn-part>$3<tn-part type=\"subspecies\">$4</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\A([a-zçäöüëïâěôûêîæœ\.-]+)\s+([Vv]ar|[Vv]|[Vv]ariety)(\.\s*|\s+)([A-Za-zçäöüëïâěôûêî-]+)\Z",
                "<tn-part type=\"species\">$1</tn-part> <tn-part type=\"infraspecific-rank\">$2</tn-part>$3<tn-part type=\"variety\">$4</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\A([a-zçäöüëïâěôûêîæœ\.-]+)\s+([Aa]b)(\.\s*|\s+)([A-Za-zçäöüëïâěôûêî-]+)\Z",
                "<tn-part type=\"species\">$1</tn-part> <tn-part type=\"infraspecific-rank\">$2</tn-part>$3<tn-part type=\"aberration\">$4</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\A([a-zçäöüëïâěôûêîæœ\.-]+)\s+([Ff]|[Ff]orma?)(\.\s*|\s+)([A-Za-zçäöüëïâěôûêî-]+)\Z",
                "<tn-part type=\"species\">$1</tn-part> <tn-part type=\"infraspecific-rank\">$2</tn-part>$3<tn-part type=\"form\">$4</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\A([a-zçäöüëïâěôûêîæœ\.-]+)\s+([Ss]f|[Ss]ubforma?)(\.\s*|\s+)([A-Za-zçäöüëïâěôûêî-]+)\Z",
                "<tn-part type=\"species\">$1</tn-part> <tn-part type=\"infraspecific-rank\">$2</tn-part>$3<tn-part type=\"subform\">$4</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\A([a-zçäöüëïâěôûêîæœ\.-]+)\s+([a-zçäöüëïâěôûêî-]+)\Z",
                "<tn-part type=\"species\">$1</tn-part> <tn-part type=\"subspecies\">$2</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\A([a-zçäöüëïâěôûêîæœ\.-]+)\Z",
                "<tn-part type=\"species\">$1</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\A([A-Z\.-]+)\Z",
                "<tn-part type=\"genus\">$1</tn-part>");
            return replace;
        }

        private void AddFullNameAttribute(XmlNode context)
        {
            const string XPath = ".//tn[@type='lower']/tn-part[not(@full-name)][@type!='sensu'][@type!='hybrid-sign'][@type!='uncertainty-rank'][@type!='infraspecific-rank'][@type!='authority'][@type!='basionym-authority']";

            var document = context.OwnerDocument();

            context.SelectNodes(XPath)
                .Cast<XmlNode>()
                .AsParallel()
                .ForAll(lowerTaxonNamePart =>
                {
                    XmlAttribute fullNameAttribute = document.CreateAttribute(AttributeNames.FullName);

                    string lowerTaxonNamePartText = lowerTaxonNamePart.InnerText.Trim();
                    if (string.IsNullOrWhiteSpace(lowerTaxonNamePartText) || lowerTaxonNamePartText.Contains('.'))
                    {
                        fullNameAttribute.InnerText = string.Empty;
                    }
                    else
                    {
                        fullNameAttribute.InnerText = lowerTaxonNamePartText;
                    }

                    lowerTaxonNamePart.Attributes.Append(fullNameAttribute);
                });
        }

        private void AddMissingEmptyTagsInTaxonName(XmlNode context)
        {
            const string XPath = ".//tn[@type='lower'][not(count(tn-part)=1 and tn-part/@type='subgenus')][count(tn-part[@type='genus'])=0 or (count(tn-part[@type='species'])=0 and count(tn-part[@type!='genus'][@type!='subgenus'][@type!='section'][@type!='subsection'])!=0)]";

            var document = context.OwnerDocument();

            context.SelectNodes(XPath)
                .Cast<XmlNode>()
                .AsParallel()
                .ForAll(lowerTaxon =>
                {
                    var genusNode = lowerTaxon.SelectSingleNode(".//tn-part[@type='genus']");
                    if (genusNode == null)
                    {
                        var speciesNode = lowerTaxon.SelectSingleNode(".//tn-part[@type='species']");
                        if (speciesNode == null)
                        {
                            XmlElement speciesElement = document.CreateElement(ElementNames.TaxonNamePart);
                            speciesElement.SetAttribute(
                                AttributeNames.Type,
                                TaxonRankType.Species.MapTaxonRankTypeToTaxonRankString());
                            speciesElement.SetAttribute(
                                AttributeNames.FullName,
                                string.Empty);

                            lowerTaxon.PrependChild(speciesElement);
                        }

                        // Add genus tag
                        {
                            XmlElement genusElement = document.CreateElement(ElementNames.TaxonNamePart);
                            genusElement.SetAttribute(
                                AttributeNames.Type,
                                TaxonRankType.Genus.MapTaxonRankTypeToTaxonRankString());
                            genusElement.SetAttribute(
                                AttributeNames.FullName,
                                string.Empty);

                            lowerTaxon.PrependChild(genusElement);
                        }
                    }
                });
        }

        private void ParseLowerTaxaWithBasionym(XmlNode context)
        {
            try
            {
                var rankResolver = new SpeciesPartsPrefixesResolver();

                foreach (XmlNode lowerTaxon in context.SelectNodes(SelectLowerTaxaWithInvalidChildNodesXPath))
                {
                    string replace = Regex.Replace(lowerTaxon.InnerXml, "</?i>", string.Empty);

                    // TODO: DOM
                    string parseBasionym = Regex.Replace(replace, "^.*?<basionym>(.*?)</basionym>.*$", "$1");
                    parseBasionym = ParseLower(parseBasionym);

                    string parseSpecific = Regex.Replace(replace, "^.*?<specific>(.*?)</specific>.*$", "$1");
                    parseSpecific = ParseLower(parseSpecific);

                    replace = Regex.Replace(replace, "<genus>(.+?)</genus>", "<tn-part type=\"genus\">$1</tn-part>");
                    replace = Regex.Replace(replace, "<genus-authority>(.*?)</genus-authority>", "<tn-part type=\"authority\">$1</tn-part>");

                    replace = Regex.Replace(replace, "<basionym>(.*?)</basionym>", parseBasionym);
                    replace = Regex.Replace(replace, "<specific>(.*?)</specific>", parseSpecific);

                    replace = Regex.Replace(replace, @"<basionym-authority>(\s*)(\(.*?\))(\s*)(.*?)</basionym-authority>", "$1<tn-part type=\"basionym-authority\">$2</tn-part>$3<tn-part type=\"authority\">$4</tn-part>");
                    replace = Regex.Replace(replace, "<basionym-authority>(.*?)</basionym-authority>", "<tn-part type=\"authority\">$1</tn-part>");
                    replace = Regex.Replace(replace, "<authority>(.*?)</authority>", "<tn-part type=\"authority\">$1</tn-part>");

                    for (Match m = Regex.Match(replace, @"<infraspecific-rank>[^<>]*</infraspecific-rank>\s*<infraspecific>[^<>]*</infraspecific>\s*\)?\s*<species>[^<>]*</species>(\s*<authority>[^<>]*</authority>)?"); m.Success; m = m.NextMatch())
                    {
                        string replace1 = m.Value;

                        char[] separators = new char[] { ' ', ',', '.' };
                        string infraSpecificRank = Regex.Replace(replace, "^.*?<infraspecific-rank>([^<>]*)</infraspecific-rank>.*$", "$1").Split(separators, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();

                        Console.WriteLine(infraSpecificRank);

                        string rank = rankResolver.Resolve(infraSpecificRank);

                        replace1 = Regex.Replace(replace1, "<infraspecific-rank>([^<>]*)</infraspecific-rank>", "<tn-part type=\"infraspecific-rank\">$1</tn-part>");
                        replace1 = Regex.Replace(replace1, "<infraspecific>([^<>]*)</infraspecific>", "<tn-part type=\"" + rank + "\">$1</tn-part>");
                        replace1 = Regex.Replace(replace1, @"<species>([a-zçäöüëïâěôûêîæœ\.-]+)</species>", "<tn-part type=\"species\">$1</tn-part>");
                        replace1 = Regex.Replace(replace1, @"<species>([a-zçäöüëïâěôûêîæœ\.-]+)\s+([a-z\s\.-]+)</species>", "<tn-part type=\"species\">$1</tn-part> <tn-part type=\"subspecies\">$2</tn-part>");
                        rank = replace1.Contains("type=\"subspecies\"") ? "subspecies" : "species";
                        replace1 = Regex.Replace(replace1, "<authority>([^<>]*)</authority>", "<tn-part type=\"authority\">$1</tn-part>");
                        replace = Regex.Replace(replace, Regex.Escape(m.Value), replace1);
                    }

                    for (Match m = Regex.Match(replace, @"<infraspecific-rank>[^<>]*</infraspecific-rank>\s*<infraspecific>[^<>]*</infraspecific>(\s*<authority>[^<>]*</authority>)?"); m.Success; m = m.NextMatch())
                    {
                        string replace1 = m.Value;
                        string infraSpecificRank = Regex.Replace(Regex.Replace(replace, "^.*?<infraspecific-rank>([^<>]*)</infraspecific-rank>.*$", "$1"), "\\.", string.Empty);
                        string rank = rankResolver.Resolve(infraSpecificRank);
                        replace1 = Regex.Replace(replace1, "<infraspecific-rank>([^<>]*)</infraspecific-rank>", "<tn-part type=\"infraspecific-rank\">$1</tn-part>");
                        replace1 = Regex.Replace(replace1, "<infraspecific>([^<>]*)</infraspecific>", "<tn-part type=\"" + rank + "\">$1</tn-part>");
                        ////replace1 = Regex.Replace(replace1, "<authority>([^<>]*)</authority>", "<tn-part type=\"" + rank + "-authority\">$1</tn-part>");
                        replace1 = Regex.Replace(replace1, "<authority>([^<>]*)</authority>", "<tn-part type=\"authority\">$1</tn-part>");
                        replace = Regex.Replace(replace, Regex.Escape(m.Value), replace1);
                    }

                    replace = Regex.Replace(replace, @"<sensu>(.*?)</sensu>", @"<tn-part type=""sensu"">$1</tn-part>");
                    replace = Regex.Replace(replace, @"<tn-part type=""infraspecific-rank"">×</tn-part>", @"<tn-part type=""hybrid-sign"">×</tn-part>");
                    replace = Regex.Replace(replace, @"<tn-part type=""infraspecific-rank"">\?</tn-part>", @"<tn-part type=""uncertainty-rank"">?</tn-part>");
                    replace = Regex.Replace(replace, @"<tn-part type=""infraspecific-rank"">((?i)(afn|aff|prope|cf|nr|near|sp\. near)\.?)</tn-part>", @"<tn-part type=""uncertainty-rank"">$1</tn-part>");

                    lowerTaxon.InnerXml = replace;
                }

                this.AddFullNameAttribute(context);

                this.RegularizeRankOfSingleWordTaxonName(context);

                this.AddMissingEmptyTagsInTaxonName(context);
            }
            catch (Exception e)
            {
                this.logger?.Log(e, "Parse lower taxa with basionym.");
            }
        }

        private void ParseLowerTaxaWithoutBasionym(XmlNode context)
        {
            try
            {
                context.SelectNodes(SelectLowerTaxaWithoutChildNodesXPath)
                    .Cast<XmlNode>()
                    .AsParallel()
                    .ForAll(lowerTaxon =>
                    {
                        lowerTaxon.InnerXml = ParseLower(lowerTaxon.InnerXml);
                    });
            }
            catch (Exception e)
            {
                this.logger?.Log(e, "Parse lower taxa without basionym.");
            }
        }

        private object ParseSync(XmlNode context)
        {
            this.ParseLowerTaxaWithoutBasionym(context);
            this.ParseLowerTaxaWithBasionym(context);
            this.RemoveWrappingItalics(context);

            return true;
        }

        private void RegularizeRankOfSingleWordTaxonName(XmlNode context)
        {
            const string SingleWordTaxonNameXPathFormat = ".//tn[@type='lower'][count(tn-part) = 1][{0}]/{0}";

            const string TaxonNamePartWithValidContentXPath = "tn-part[normalize-space(.)!=''][not(@full-name)][@type]";
            string nonSingleWordTaxonNamePartsXPath = string.Format(".//tn[@type='lower'][count({0}) > 1]/{0}", TaxonNamePartWithValidContentXPath);

            var listOfNonSingleWordTaxonNameParts = context.SelectNodes(nonSingleWordTaxonNamePartsXPath)
                .Cast<XmlNode>()
                .Select(node => new TaxonNamePart(node))
                .Distinct(new TaxonNamePartContentEqualityComparer())
                .ToList();

            // Process single-word-taxon-names tagged with type genus.
            this.UpdateSingleWordTaxonNamePartOfTypeRanks(context, string.Format(SingleWordTaxonNameXPathFormat, XPathStrings.TaxonNamePartOfTypeGenus), listOfNonSingleWordTaxonNameParts);

            // Process single-word-taxon-names tagged with type species.
            this.UpdateSingleWordTaxonNamePartOfTypeRanks(context, string.Format(SingleWordTaxonNameXPathFormat, XPathStrings.TaxonNamePartOfTypeSpecies), listOfNonSingleWordTaxonNameParts);
        }

        private void RemoveWrappingItalics(XmlNode context)
        {
            // Remove wrapping i around tn[tn-part[@type='subgenus']]
            context.InnerXml = Regex.Replace(
                context.InnerXml,
                @"<i>(<tn(\s*>|\s[^<>]*>)<tn-part type=""genus""[^<>]*>[^<>]*</tn-part>\s*\(<tn-part type=""(subgenus|superspecies)""[^<>]*>.*?</tn>)</i>",
                "$1");
        }

        private void UpdateSingleWordTaxonNamePartOfTypeRanks(XmlNode context, string xpath, IEnumerable<ITaxonNamePart> listOfNonSingleWordTaxonNameParts)
        {
            var document = context.OwnerDocument();

            context.SelectNodes(xpath)
                .Cast<XmlNode>()
                .AsParallel()
                .ForAll(node =>
                {
                    var taxonNamePart = new TaxonNamePart(node);
                    var matches = listOfNonSingleWordTaxonNameParts.Where(t => t.FullName == taxonNamePart.FullName).ToList();
                    if (matches.Count == 1)
                    {
                        var match = matches.First();
                        if (match.Rank != taxonNamePart.Rank)
                        {
                            XmlAttribute rankAttribute = node.Attributes[AttributeNames.Type];
                            if (rankAttribute == null)
                            {
                                rankAttribute = document.CreateAttribute(AttributeNames.Type);
                                node.Attributes.Append(rankAttribute);
                            }

                            rankAttribute.InnerText = match.Rank.ToString().ToLower();

                            // TODO: remove this line
                            this.logger?.Log("\t {1} --> {0}", match.Rank, node.InnerText);
                        }
                    }
                });
        }
    }
}
