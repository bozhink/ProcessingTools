namespace ProcessingTools.Bio.Taxonomy.Processors.Processors.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Extensions;
    using ProcessingTools.Processors;
    using ProcessingTools.Processors.Comparers.Bio.Taxonomy;
    using ProcessingTools.Processors.Contracts.Providers.Bio.Taxonomy;
    using ProcessingTools.Processors.Contracts.Models.Bio.Taxonomy.Parsers;
    using ProcessingTools.Processors.Contracts.Processors.Bio.Taxonomy.Parsers;
    using ProcessingTools.Processors.Models.Bio.Taxonomy.Parsers;

    public class LowerTaxaParser : ILowerTaxaParser
    {
        private const string SelectLowerTaxaWithInvalidChildNodesXPath = ".//tn[@type='lower'][count(*) != count(tn-part)]";
        private const string SelectLowerTaxaWithoutChildNodesXPath = ".//tn[@type='lower'][not(*)]";
        private const string TaxonNamePartElementFormatString = @"<tn-part type=""{0}"">{1}</tn-part>";
        private const string UncertaintyRankPairTaxonNameParts123FormatString = @"<tn-part type=""" + AttributeValues.UncertaintyRank + @""">$1</tn-part>$2<tn-part type=""{0}"">$3</tn-part>";

        private readonly IParseLowerTaxaStrategiesProvider strategiesProvider;
        private readonly ILogger logger;

        public LowerTaxaParser(IParseLowerTaxaStrategiesProvider strategiesProvider, ILogger logger)
        {
            if (strategiesProvider == null)
            {
                throw new ArgumentNullException(nameof(strategiesProvider));
            }

            this.strategiesProvider = strategiesProvider;
            this.logger = logger;
        }

        public async Task<object> Parse(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var strategies = this.strategiesProvider.Strategies
                .Where(s => s.ExecutionPriority > 0)
                .OrderBy(s => s.ExecutionPriority);

            foreach (var strategy in strategies)
            {
                await strategy.Parse(context);
            }

            ////this.AddTaxonNamePartsToTaxonNameElements(context);
            ////var knownRanks = this.BuildDictionaryOfKnownRanks(context);
            ////this.ResolveWithDictionaryOfKnownRanks(context, knownRanks);
            this.ParseLowerTaxaWithoutBasionym(context);
            this.ParseLowerTaxaWithBasionym(context);
            this.RemoveWrappingItalics(context);

            this.EnsureFormatting(context);
            this.EnsureUncertaintyRank(context);

            return true;
        }

        private void EnsureFormatting(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            string replace = context.InnerXml;

            // Parse question mark
            replace = Regex.Replace(
                replace,
                @"(?<=</tn-part>)\s*\?",
                @"<tn-part type=""uncertainty-rank"">?</tn-part>");

            // Parse hybrid sign
            replace = Regex.Replace(
                replace,
                @"(?<=</tn-part>\s*)×(?=\s*<tn-part)",
                @"<tn-part type=""hybrid-sign"">×</tn-part>");

            // Here we must return the dot after tn-part[@type='infraspecific-rank'] back into the tag
            replace = Regex.Replace(replace, @"</tn-part>\.", ".</tn-part>");

            // Clear multiple white spaces between ‘tn-part’-s
            replace = Regex.Replace(replace, @"(?<=</tn-part>)\s{2,}(?=<tn-part)", " ");

            context.InnerXml = replace;
        }

        private void EnsureUncertaintyRank(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            string xpath = XPathStrings.TaxonNamePartsOfLowerTaxonNames + $"[@{AttributeNames.Type}='{AttributeValues.InfraRank}']";

            context.SelectNodes(xpath)
                .Cast<XmlNode>()
                .AsParallel()
                .ForAll(node =>
                {
                    string value = node.InnerText.Trim(new char[] { ' ', '.' });
                    if (SpeciesPartsPrefixesResolver.UncertaintyPrefixes.Contains(value))
                    {
                        node.Attributes[AttributeNames.Type].InnerText = AttributeValues.UncertaintyRank;
                    }
                });
        }





        private string ParseLower(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return text;
            }

            string replace = text;

            replace = this.ParseTaxaFromBeginning(replace);

            return replace;
        }

        private string ParseTaxaFromBeginning(string text)
        {
            string replace = text;

            // Genus species subspecies
            replace = Regex.Replace(
                replace,
                @"\A([A-Z][a-z\.]+)([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)",
                @"<tn-part type=""genus"">$1</tn-part>$2<tn-part type=""species"">$3</tn-part>$4<tn-part type=""subspecies"">$5</tn-part>");

            // Genus species
            replace = Regex.Replace(
                replace,
                @"\A([A-Z][a-z\.]+)([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)",
                @"<tn-part type=""genus"">$1</tn-part>$2<tn-part type=""species"">$3</tn-part>");
            replace = Regex.Replace(
                replace,
                @"\A‘([A-Z][a-z\.]+)’([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)",
                @"‘<tn-part type=""genus"">$1</tn-part>’$2<tn-part type=""species"">$3</tn-part>");

            // Genus (Subgenus) species subspecies
            replace = Regex.Replace(
                replace,
                @"\A([A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+)([\s\?×]*)\(\s*([A-Z][a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]*?)\s*\)([\s\?×]*)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)",
                @"<tn-part type=""genus"">$1</tn-part>$2(<tn-part type=""subgenus"">$3</tn-part>$4)$5<tn-part type=""species"">$6</tn-part>$7<tn-part type=""subspecies"">$8</tn-part>");

            // Genus (superspecies) species subspecies
            replace = Regex.Replace(
                replace,
                @"\A([A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+)([\s\?×]*)\(\s*([a-z][a-zçäöüëïâěôûêî\.-]+)([\s\?×]*?)\s*\)([\s\?×]*)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)",
                @"<tn-part type=""genus"">$1</tn-part>$2(<tn-part type=""superspecies"">$3</tn-part>$4)$5<tn-part type=""species"">$6</tn-part>$7<tn-part type=""subspecies"">$8</tn-part>");

            // Genus (Subgenus) species
            replace = Regex.Replace(
                replace,
                @"\A([A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+)([\s\?×]*)\(\s*([A-Z][a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]*?)\s*\)([\s\?×]*)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)",
                @"<tn-part type=""genus"">$1</tn-part>$2(<tn-part type=""subgenus"">$3</tn-part>$4)$5<tn-part type=""species"">$6</tn-part>");

            // Genus (superspecies) species
            replace = Regex.Replace(
                replace,
                @"\A([A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+)([\s\?×]*)\(\s*([a-z][a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]*?)\s*\)([\s\?×]*)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)",
                @"<tn-part type=""genus"">$1</tn-part>$2(<tn-part type=""superspecies"">$3</tn-part>$4)$5<tn-part type=""species"">$6</tn-part>");

            // Genus (Subgenus)
            replace = Regex.Replace(
                replace,
                @"\A([A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+)([\s\?×]*)\(\s*([A-Z][a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]*?)\s*\)",
                @"<tn-part type=""genus"">$1</tn-part>$2(<tn-part type=""subgenus"">$3</tn-part>$4)");

            // Genus
            replace = Regex.Replace(
                replace,
                @"\A([A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+)",
                @"<tn-part type=""genus"">$1</tn-part>");

            // species
            replace = Regex.Replace(
                replace,
                @"\A([a-zçäöüëïâěôûêîæœ\.-]+)",
                @"<tn-part type=""species"">$1</tn-part>");
            return replace;
        }

        private void AddFullNameAttribute(XmlNode context)
        {
            var document = context.OwnerDocument();

            context.SelectNodes(XPathStrings.LowerTaxonNamePartWithNoFullNameAttribute)
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
            var document = context.OwnerDocument();

            context.SelectNodes(XPathStrings.LowerTaxonNameWithNoGenusTaxonNamePart)
                .Cast<XmlNode>()
                .AsParallel()
                .ForAll(lowerTaxon =>
                {
                    var ranks = lowerTaxon.SelectNodes(".//tn-part" + XPathStrings.TaxonNamePartOfNonAuxiliaryType + "/@type")
                        .Cast<XmlAttribute>()
                        .Select(a => a.InnerText)
                        .Distinct()
                        .Select(t => t.ToSpeciesPartType())
                        .Where(t => t != SpeciesPartType.Undefined)
                        .ToArray();

                    if (ranks.Any(r => r < SpeciesPartType.Superspecies))
                    {
                        // All ranks are super-specific, so no empty genus or species element should be added
                        return;
                    }

                    if (ranks.All(r => r > SpeciesPartType.Species))
                    {
                        // All ranks are infra-specific, so here an empty species element is needed
                        XmlElement speciesElement = document.CreateElement(ElementNames.TaxonNamePart);
                        speciesElement.SetAttribute(
                            AttributeNames.Type,
                            TaxonRankType.Species.MapTaxonRankTypeToTaxonRankString());
                        speciesElement.SetAttribute(
                            AttributeNames.FullName,
                            string.Empty);

                        lowerTaxon.PrependChild(speciesElement);
                    }

                    // Add empty genus element
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
                });
        }

        private void AddTaxonNamePartsToTaxonNameElements(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.SelectNodes(".//tn[@type='lower'][not(tn-part)]")
                .Cast<XmlNode>()
                .AsParallel()
                .ForAll(node =>
                {
                    var text = node.InnerXml
                        .RegexReplace("><", "> <")
                        .RegexReplace(@"<[^<>]+>", string.Empty);

                    var matchWordValues = Regex.Match(text, @"((?:[^\W\d]|\-)+)");
                    var matches = new HashSet<string>(matchWordValues.AsEnumerable());

                    string innerXmlReplacement = node.InnerXml;
                    foreach (var match in matches)
                    {
                        string pattern = @"(?<!<[^<>]+)\b(" + match + @"\b\.?)";
                        innerXmlReplacement = Regex.Replace(innerXmlReplacement, pattern, @"<tn-part type="""">$1</tn-part>");
                    }

                    node.InnerXml = innerXmlReplacement;
                });
        }

        private IDictionary<string, string> BuildDictionaryOfKnownRanks(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var items = context.SelectNodes(".//tn[@type='lower']//tn-part[normalize-space(@type)!='']")
                .Cast<XmlNode>()
                .Select(n => $"{n.InnerText}|{n.Attributes[AttributeNames.Type]?.InnerText}")
                .Distinct()
                .ToArray();

            var dictionary = new Dictionary<string, string>();

            foreach (var item in items)
            {
                var taxonRankPair = item.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (taxonRankPair.Length < 2)
                {
                    continue;
                }

                string taxonName = taxonRankPair[0];
                string taxonRank = taxonRankPair[1].ToLower();
                if (dictionary.ContainsKey(taxonName))
                {
                    if (dictionary[taxonName] != taxonRank)
                    {
                        // Taxon name has multiple ranks
                        dictionary.Remove(taxonName);
                    }
                }
                else
                {
                    dictionary.Add(taxonName, taxonRank);
                }
            }

            return dictionary;
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

                    replace = Regex.Replace(replace, "<genus>(.+?)</genus>", @"<tn-part type=""genus"">$1</tn-part>");
                    replace = Regex.Replace(replace, "<genus-authority>(.*?)</genus-authority>", @"<tn-part type=""authority"">$1</tn-part>");

                    replace = Regex.Replace(replace, "<basionym>(.*?)</basionym>", parseBasionym);
                    replace = Regex.Replace(replace, "<specific>(.*?)</specific>", parseSpecific);

                    replace = Regex.Replace(replace, @"<basionym-authority>(\s*)(\(.*?\))(\s*)(.*?)</basionym-authority>", @"$1<tn-part type=""basionym-authority"">$2</tn-part>$3<tn-part type=""authority"">$4</tn-part>");
                    replace = Regex.Replace(replace, "<basionym-authority>(.*?)</basionym-authority>", @"<tn-part type=""authority"">$1</tn-part>");
                    replace = Regex.Replace(replace, "<authority>(.*?)</authority>", @"<tn-part type=""authority"">$1</tn-part>");

                    for (Match m = Regex.Match(replace, @"<infraspecific-rank>[^<>]*</infraspecific-rank>\s*<infraspecific>[^<>]*</infraspecific>\s*\)?\s*<species>[^<>]*</species>(\s*<authority>[^<>]*</authority>)?"); m.Success; m = m.NextMatch())
                    {
                        string replace1 = m.Value;

                        char[] separators = new char[] { ' ', ',', '.' };
                        string infraSpecificRank = Regex.Replace(replace, "^.*?<infraspecific-rank>([^<>]*)</infraspecific-rank>.*$", "$1").Split(separators, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();

                        Console.WriteLine(infraSpecificRank);

                        string rank = rankResolver.Resolve(infraSpecificRank);

                        replace1 = Regex.Replace(replace1, "<infraspecific-rank>([^<>]*)</infraspecific-rank>", @"<tn-part type=""infraspecific-rank"">$1</tn-part>");
                        replace1 = Regex.Replace(replace1, "<infraspecific>([^<>]*)</infraspecific>", @"<tn-part type=""" + rank + @""">$1</tn-part>");
                        replace1 = Regex.Replace(replace1, @"<species>([a-zçäöüëïâěôûêîæœ\.-]+)</species>", @"<tn-part type=""species"">$1</tn-part>");
                        replace1 = Regex.Replace(replace1, @"<species>([a-zçäöüëïâěôûêîæœ\.-]+)\s+([a-z\s\.-]+)</species>", @"<tn-part type=""species"">$1</tn-part> <tn-part type=""subspecies"">$2</tn-part>");
                        rank = replace1.Contains(@"type=""subspecies""") ? "subspecies" : "species";
                        replace1 = Regex.Replace(replace1, "<authority>([^<>]*)</authority>", @"<tn-part type=""authority"">$1</tn-part>");
                        replace = Regex.Replace(replace, Regex.Escape(m.Value), replace1);
                    }

                    for (Match m = Regex.Match(replace, @"<infraspecific-rank>[^<>]*</infraspecific-rank>\s*<infraspecific>[^<>]*</infraspecific>(\s*<authority>[^<>]*</authority>)?"); m.Success; m = m.NextMatch())
                    {
                        string replace1 = m.Value;
                        string infraSpecificRank = Regex.Replace(Regex.Replace(replace, "^.*?<infraspecific-rank>([^<>]*)</infraspecific-rank>.*$", "$1"), "\\.", string.Empty);
                        string rank = rankResolver.Resolve(infraSpecificRank);
                        replace1 = Regex.Replace(replace1, "<infraspecific-rank>([^<>]*)</infraspecific-rank>", @"<tn-part type=""infraspecific-rank"">$1</tn-part>");
                        replace1 = Regex.Replace(replace1, "<infraspecific>([^<>]*)</infraspecific>", @"<tn-part type=""" + rank + @""">$1</tn-part>");
                        ////replace1 = Regex.Replace(replace1, "<authority>([^<>]*)</authority>", @"<tn-part type=""" + rank + @"-authority"">$1</tn-part>");
                        replace1 = Regex.Replace(replace1, "<authority>([^<>]*)</authority>", @"<tn-part type=""authority"">$1</tn-part>");
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

        private void ResolveWithDictionaryOfKnownRanks(XmlNode context, IDictionary<string, string> dictionary)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            context.SelectNodes(".//tn[@type='lower']//tn-part[normalize-space(@type)='']")
                .Cast<XmlNode>()
                .AsParallel()
                .ForAll(node =>
                {
                    string taxonName = node.InnerText;
                    string taxonRank = null;
                    dictionary.TryGetValue(taxonName, out taxonRank);
                    if (!string.IsNullOrEmpty(taxonRank))
                    {
                        node.SafeSetAttributeValue(AttributeNames.Type, taxonRank);
                    }
                });
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
