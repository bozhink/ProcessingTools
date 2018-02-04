namespace ProcessingTools.Processors.Processors.Bio.Taxonomy.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Models.Contracts.Processors.Bio.Taxonomy;
    using ProcessingTools.Contracts.Processors.Processors.Bio.Taxonomy.Parsers;
    using ProcessingTools.Contracts.Strategies.Bio.Taxonomy;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Extensions;
    using ProcessingTools.Processors.Common.Bio.Taxonomy;
    using ProcessingTools.Processors.Models.Bio.Taxonomy;

    public class LowerTaxaParser : ILowerTaxaParser
    {
        private const string SelectLowerTaxaWithInvalidChildNodesXPath = ".//tn[@type='lower'][count(*) != count(tn-part)]";
        private const string SelectLowerTaxaWithoutChildNodesXPath = ".//tn[@type='lower'][not(*)]";
        private const string TaxonNamePartElementFormatString = @"<tn-part type=""{0}"">{1}</tn-part>";

        private readonly IParseLowerTaxaStrategiesProvider strategiesProvider;

        public LowerTaxaParser(IParseLowerTaxaStrategiesProvider strategiesProvider)
        {
            this.strategiesProvider = strategiesProvider ?? throw new ArgumentNullException(nameof(strategiesProvider));
        }

        public async Task<object> ParseAsync(XmlNode context)
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
                try
                {
                    await strategy.ParseAsync(context).ConfigureAwait(false);
                }
                catch
                {
                    // Skip
                }
            }

            this.AddFullNameAttribute(context);
            this.RegularizeRankOfSingleWordTaxonName(context);
            this.AddMissingEmptyTagsInTaxonName(context);
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
                    string value = node.InnerText.Trim(new[] { ' ', '.' });
                    if (SpeciesPartsPrefixesResolver.UncertaintyPrefixes.Contains(value))
                    {
                        node.Attributes[AttributeNames.Type].InnerText = AttributeValues.UncertaintyRank;
                    }
                    else if (value == "×")
                    {
                        node.Attributes[AttributeNames.Type].InnerText = AttributeValues.HybridSign;
                    }
                });
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
                var taxonRankPair = item.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (taxonRankPair.Length < 2)
                {
                    continue;
                }

                string taxonName = taxonRankPair[0];
                string taxonRank = taxonRankPair[1].ToLowerInvariant();
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
                    dictionary.TryGetValue(taxonName, out string taxonRank);
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

                            rankAttribute.InnerText = match.Rank.ToRankString();
                        }
                    }
                });
        }
    }
}
