namespace ProcessingTools.Bio.Taxonomy.Processors.Taggers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;

    using Abstractions.Taggers;
    using Contracts.Taggers;

    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Bio.Taxonomy.Types;
    using ProcessingTools.Contracts;
    using ProcessingTools.Extensions;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;
    using ProcessingTools.Layout.Processors.Models.Taggers;
    using ProcessingTools.Xml.Extensions;

    public class LowerTaxaTagger : TaxaTagger, ILowerTaxaTagger
    {
        private const string SensuSubpattern = @"(?:\(\s*)?(?i)(?:\bsensu\b\s*[a-z]*|s\.?\s*[ls]\.?|s\.?\s*str\.?)(?:\s*\))?";
        private const string InfragenericRankSubpattern = @"(?i)\b(?:subgen(?:us)?|subg|sg|(?:sub)?ser|trib|(?:super)?(?:sub)?sec[ct]?(?:ion)?)\b\.?";
        private const string InfraspecificRankSubpattern = @"(?i)(?:\b(?:ab?|mod|sp|var|subvar|subsp|sbsp|subspec|subspecies|ssp|race|rassa|(?:sub)?f[ao]?|(?:sub)?forma?|st|r|sf|cf|gr|n\.?\s*sp|nr|(?:sp(?:\.\s*|\s+))?(?:near|aff)|prope|(?:super)?(?:sub)?sec[ct]?(?:ion)?)\b\.?(?:\s*[γβɑ])?(?:\s*\bn(?:ova?)?\b\.?)?|×|\?)";

        private const string StructureXPathTemplate = ".//p|.//title|.//article-meta/title-group|.//label|.//license-p|.//li|.//th|.//td|.//mixed-citation|.//element-citation|.//nlm-citation|.//tp:nomenclature-citation";

        private const string LowerTaxaXPath = ".//p|.//td|.//th|.//li|.//article-title|.//title|.//label|.//ref|.//kwd|.//tp:nomenclature-citation|.//*[@object_id='95']|.//*[@object_id='90']|.//value[../@id!='244'][../@id!='434'][../@id!='433'][../@id!='432'][../@id!='431'][../@id!='430'][../@id!='429'][../@id!='428'][../@id!='427'][../@id!='426'][../@id!='425'][../@id!='424'][../@id!='423'][../@id!='422'][../@id!='421'][../@id!='420'][../@id!='419'][../@id!='417'][../@id!='48']";

        private const string ItalicXPath = ".//i[not(tn)]|.//italic[not(tn)]|.//Italic[not(tn)]";

        private readonly IContentTagger contentTagger;
        private readonly ILogger logger;

        public LowerTaxaTagger(
            IBiotaxonomicBlackListIterableDataService service,
            IContentTagger contentTagger,
            ILogger logger)
            : base(service)
        {
            if (contentTagger == null)
            {
                throw new ArgumentNullException(nameof(contentTagger));
            }

            this.contentTagger = contentTagger;
            this.logger = logger;
        }

        public override async Task<object> Tag(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            await this.MainTag(document);

            await this.DeepTag(document);

            return true;
        }

        private async Task MainTag(IDocument document)
        {
            var knownLowerTaxaNames = this.GetKnownLowerTaxa(document);

            var plausibleLowerTaxa = new HashSet<string>(this.GetPlausibleLowerTaxa(document).Concat(knownLowerTaxaNames));

            plausibleLowerTaxa = new HashSet<string>((await this.ClearFakeTaxaNames(document, plausibleLowerTaxa))
                .Select(name => name.ToLower()));

            this.TagDirectTaxonomicMatches(document, plausibleLowerTaxa);

            // TODO: move to format
            document.Xml = Regex.Replace(
                document.Xml,
                @"‘<i>(<tn type=""lower""[^>]*>)([A-Z][a-z\.×]+)(</tn>)(?:</i>)?’\s*(?:<i>)?([a-z\.×-]+)</i>",
                "$1‘$2’ $4$3");

            this.AdvancedTagLowerTaxa(document, string.Format(StructureXPathTemplate, "count(.//tn[@type='lower']) != 0"));
            //// this.Xml = this.TagInfraspecificTaxa(this.Xml);
        }

        private void TagDirectTaxonomicMatches(IDocument document, IEnumerable<string> taxonomicNames)
        {
            var comparer = StringComparer.InvariantCultureIgnoreCase;

            // Tag all direct matches
            document.SelectNodes(ItalicXPath)
                .AsParallel()
                .ForAll(node =>
                {
                    if (taxonomicNames.Contains(node.InnerText, comparer))
                    {
                        var tn = this.CreateNewTaxonNameXmlElement(document, TaxonType.Lower);
                        tn.InnerXml = node.InnerXml
                            .RegexReplace(@"\A([A-Z][A-Za-z]{0,2}\.)([a-z])", "$1 $2");
                        node.InnerXml = tn.OuterXml;
                    }
                });
        }

        private IEnumerable<string> GetPlausibleLowerTaxa(IDocument document)
        {
            var result = document.SelectNodes(ItalicXPath)
                .Select(x => x.InnerText)
                .Where(this.IsMatchingLowerTaxaFormat)
                .ToArray();

            return result;
        }

        private IEnumerable<string> GetKnownLowerTaxa(IDocument document)
        {
            var result = document.SelectNodes(".//tn[@type='lower']")
                .Select(x => x.InnerText)
                .ToArray();

            return result;
        }

        // TODO: XPath-s correction needed
        private void AdvancedTagLowerTaxa(IDocument document, string xpath)
        {
            document.SelectNodes(xpath)
                .AsParallel()
                .ForAll(this.TagInfrarankTaxaSync);

            document.SelectNodes(".//value[.//tn[@type='lower']]")
                .AsParallel()
                .ForAll(this.TagInfrarankTaxaSync);
        }

        private void TagInfrarankTaxaSync(XmlNode node)
        {
            this.TagInfrarankTaxa(node).Wait();
        }

        private async Task TagInfrarankTaxa(XmlNode node)
        {
            await this.TagSensu(node);

            await this.TagBindedInfragenericTaxa(node);

            await this.TagBareInfragenericTaxa(node);

            await this.TagBindedInfraspecificTaxa(node);

            await this.TagBareInfraspecificTaxa(node);

            await this.TagInfraspecicTaxaFinalize(node);
        }

        private async Task TagInfraspecicTaxaFinalize(XmlNode node)
        {
            // Here we must extract species+subspecies in <infraspecific/>, which comes from tagging of subgenera and [sub]sections
            string replace = node.InnerXml;
            replace = Regex.Replace(replace, @"<infraspecific>([A-Za-z][A-Za-z\.-]+)\s+([a-z][a-z\s\.-]+)</infraspecific>", "<infraspecific>$1</infraspecific> <species>$2</species>");

            replace = Regex.Replace(replace, @" (?:<(?:[a-z-]+)?authority></(?:[a-z-]+)?authority>|<(?:[a-z-]+)?authority\s*/>)", string.Empty);

            await node.SafeReplaceInnerXml(replace, this.logger);
        }

        // Neoserica (s. l.) abnormoides, Neoserica (sensu lato) abnormis
        private async Task TagSensu(XmlNode node)
        {
            const string InfraspecificPattern = @"<i><tn type=""lower""[^>]*>([^<>]*?)</tn></i>\s*(" + SensuSubpattern + @")\s*<i>(?:<tn type=""lower""[^>]*>)?([a-z][a-z\s-]+)(?:</tn>)?</i>";
            Regex re = new Regex(InfraspecificPattern);

            string result = node.InnerXml;
            result = re.Replace(
                result,
                @"<tn type=""lower""><basionym>$1</basionym> <sensu>$2</sensu> <specific>$3</specific></tn>");

            await node.SafeReplaceInnerXml(result, this.logger);
        }

        // Genus subgen(us)?. Subgenus sect(ion)?. Section subsect(ion)?. Subsection
        private async Task TagBindedInfragenericTaxa(XmlNode node)
        {
            const string Subpattern = @"(?!\s*[,\.:])(?!\s+and\b)(?!\s+w?as\b)(?!\s+from\b)(?!\s+w?remains\b)(?!\s+to\b)\s*([^<>\(\)\[\]:\+\\\/]{0,40}?)\s*(\(\s*)?(" + InfragenericRankSubpattern + @")\s*(?:<i>)?(?:<tn type=""lower""[^>]*>)?([A-Za-z][A-Za-z\.-]+(?:\s+[a-z\.-]+){0,3})(?:</tn>)?(?:</i>)?(\s*\))?";

            string result = node.InnerXml;

            {
                const string InfraspecificPattern = @"<i><tn type=""lower""[^>]*>([A-Za-z][A-Za-z\.-]+)</tn></i>" + Subpattern;

                for (Match m = Regex.Match(result, InfraspecificPattern); m.Success; m = m.NextMatch())
                {
                    result = Regex.Replace(
                        result,
                        InfraspecificPattern,
                        @"<tn type=""lower""><genus>$1</genus> <authority>$2</authority> $3<infraspecific-rank>$4</infraspecific-rank> <infraspecific>$5</infraspecific></tn>$6");
                }

                // Move closing bracket in tn if it is outside
                result = Regex.Replace(result, @"(?<=\(\s*<infraspecific[^\)]*?)(</tn>)(\s*\))", "$2$1");
            }

            {
                const string InfraspecificPattern = @"(?<=</infraspecific>[\s\)]*)</tn>" + Subpattern;
                for (int i = 0; i < 8; i++)
                {
                    for (Match m = Regex.Match(result, InfraspecificPattern); m.Success; m = m.NextMatch())
                    {
                        result = Regex.Replace(
                            result,
                            InfraspecificPattern,
                            " <authority>$1</authority> $2<infraspecific-rank>$3</infraspecific-rank> <infraspecific>$4</infraspecific></tn>$5");
                    }
                }

                // Move closing bracket in tn if it is outside
                result = Regex.Replace(result, @"(?<=\(\s*<infraspecific[^\)]*?)(</tn>)(\s*\))", "$2$1");
            }

            await node.SafeReplaceInnerXml(result, this.logger);
        }

        // <i><tn>A. herbacea</tn></i> Walter var. <i>herbacea</i>
        // <i>Lespedeza hirta</i> (L.) Hornem. var. <i>curtissii</i>
        // <i>Melitaea phoebe</i> Knoch rassa <i>occitanica</i> Staudinger 2-gen. <i>francescoi</i>
        // <i>Melitaea phoebe</i> Knoch sbsp. n. <i>canellina</i> Stauder, 1922
        // <i>Melitaea phoebe</i> mod. <i>nimbula</i> Higgins, 1941
        private async Task TagBindedInfraspecificTaxa(XmlNode node)
        {
            const string InfraspecificRankNamePairSubpattern = @"\s*(" + InfraspecificRankSubpattern + @")\s*(?:<i>|<i [^>]*>)(?:<tn type=""lower""[^>]*>)?([a-z][a-z-]+)(?:</tn>)?</i>";

            string result = node.InnerXml;

            {
                const string InfraspecificPattern = @"(?:<i>|<i [^>]*>)<tn type=""lower""[^>]*>([^<>]*?)</tn></i>(?![,\.])\s*((?:[^<>\(\)\[\]:\+]{0,3}?\([^<>\(\)\[\]:\+\\\/]{0,30}?\)[^<>\(\)\[\]:\+]{0,30}?|[^<>\(\)\[\]:\+]{0,30}?)?)" + InfraspecificRankNamePairSubpattern;
                Regex re = new Regex(InfraspecificPattern);

                for (Match m = re.Match(result); m.Success; m = m.NextMatch())
                {
                    result = re.Replace(
                        result,
                        @"<tn type=""lower""><basionym>$1</basionym> <basionym-authority>$2</basionym-authority> <infraspecific-rank>$3</infraspecific-rank> <infraspecific>$4</infraspecific></tn>");
                }

                result = Regex.Replace(
                    result,
                    @"(?<=</infraspecific>\s*\)?)</tn>\s*(?:<i>|<i [^>]*>)(?:<tn type=""lower""[^>]*>)?([A-Za-z][A-Za-z\.\s-]+)(?:</tn>)?</i>",
                    " <species>$1</species></tn>");
            }

            {
                const string InfraspecificPattern = @"(?<=(?:</infraspecific>|</species>)\s*\)?)</tn>\s*([^<>]{0,100}?)" + InfraspecificRankNamePairSubpattern;
                Regex re = new Regex(InfraspecificPattern);

                for (int i = 0; i < 4; i++)
                {
                    for (Match m = re.Match(result); m.Success; m = m.NextMatch())
                    {
                        result = re.Replace(
                            result,
                            " <authority>$1</authority> <infraspecific-rank>$2</infraspecific-rank> <infraspecific>$3</infraspecific></tn>");
                    }
                }
            }

            await node.SafeReplaceInnerXml(result, this.logger);
        }

        // Tag bare infraspecific citations in text
        // var. <italic>schischkinii</italic>
        private async Task TagBareInfraspecificTaxa(XmlNode node)
        {
            const string BareInfraspecificPattern = @"(" + InfraspecificRankSubpattern + @")\s*<i>([A-Za-z][A-Za-z\.\-]+)</i>";
            Regex re = new Regex(BareInfraspecificPattern);

            string result = node.InnerXml;

            if (re.IsMatch(result))
            {
                result = re.Replace(
                    result,
                    @"<tn type=""lower""><infraspecific-rank>$1</infraspecific-rank> <infraspecific>$2</infraspecific></tn>");
            }

            await node.SafeReplaceInnerXml(result, this.logger);
        }

        // Tag bare infraspecific citations in text
        // section <italic>Stenodiptera</italic>
        private async Task TagBareInfragenericTaxa(XmlNode node)
        {
            const string BareInfragenericPattern = @"(" + InfragenericRankSubpattern + @")\s*<i>(?:<tn type=""lower"">)?([A-Za-z][A-Za-z\.\-]+)(?:</tn>)?</i>";
            Regex re = new Regex(BareInfragenericPattern);

            string result = node.InnerXml;

            if (re.IsMatch(result))
            {
                result = re.Replace(
                    result,
                    @"<tn type=""lower""><infraspecific-rank>$1</infraspecific-rank> <infraspecific>$2</infraspecific></tn>");
            }

            await node.SafeReplaceInnerXml(result, this.logger);
        }

        private bool IsMatchingLowerTaxaFormat(string textToCheck)
        {
            bool result = false;

            result |= Regex.IsMatch(textToCheck, @"\A(?<genus>[‘“]?[A-Z][a-z\.×]+[’”]?[‘“]?(?:\-[A-Z][a-z\.×]+)?[’”]?)\s*(?<species>[a-z\.×-]*)\Z") ||
                      Regex.IsMatch(textToCheck, @"\A(?<genus>[‘“]?[A-Z][a-z\.×]+[’”]?[‘“]?(?:\-[A-Z][a-z\.×]+)?[’”]?)\s*(?<species>[a-z\.×-]+)\s*(?<subspecies>[a-z×-]+)\Z") ||
                      Regex.IsMatch(textToCheck, @"\A(?<genus>[‘“]?[A-Z][a-z\.×]+[’”]?[‘“]?(?:\-[A-Z][a-z\.×]+)?[’”]?)\s*\(\s*(?<subgenus>[A-Za-z][a-z\.×]+)\s*\)\s*(?<species>[a-z\.×-]*)\Z") ||
                      Regex.IsMatch(textToCheck, @"\A(?<genus>[‘“]?[A-Z][a-z\.×]+[’”]?[‘“]?(?:\-[A-Z][a-z\.×]+)?[’”]?)\s*\(\s*(?<subgenus>[A-Za-z][a-z\.×]+)\s*\)\s*(?<species>[a-z\.×-]+)\s*(?<subspecies>[a-z×-]+)\Z") ||
                      Regex.IsMatch(textToCheck, @"\A(?<genus>[‘“]?[A-Z]{2,}[’”]?)\Z");

            result &= !textToCheck.Contains("s.n.") && !textToCheck.Contains(" coll.");

            return result;
        }

        private async Task DeepTag(IDocument document)
        {
            var knownLowerTaxaNamesXml = new HashSet<string>(document.SelectNodes(".//tn[@type='lower']").Select(x => x.InnerXml));

            ////// string clearUselessTaxonNamePartsSubpattern = string.Join(
            //////    "|",
            //////    SpeciesPartsPrefixesResolver
            //////        .SpeciesPartsRanks
            //////        .Keys
            //////        .Select(k => $"\\b{Regex.Escape(k)}\\b\\."));

            // TODO: This algorithm must be refined: generate smaller pattern strings from the original.
            var taxa = knownLowerTaxaNamesXml
                .Select(t => t.RegexReplace(@"<(sensu)[^>/]*>.*?</\1>|<((?:basionym-)?authority)[^>/]*>.*?</\2>|<(infraspecific-rank)[^>/]*>.*?</\3>|\bcf\b\.|\bvar\b\.", string.Empty))
                .Select(t => t.RegexReplace(@"<[^>]*>", string.Empty).RegexReplace(@"[\s\d\?]+\-?", " "))
                .Select(t => t.RegexReplace(@"[^\w\.]+", " "))
                .Distinct()
                .ToList();

            taxa.AddRange(document.SelectNodes(".//treatment//species_name/fields | .//checklist_taxon/fields")
                .Select(this.GetSystemTaxonNameString)
                .Where(t => !string.IsNullOrWhiteSpace(t))
                .Distinct()
                .ToArray());

            foreach (string taxon in new HashSet<string>(taxa))
            {
                taxa.AddRange(
                    taxon.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                        .Where(s => !string.IsNullOrWhiteSpace(s) && s.Length > 2));
            }

            var orderedTaxaParts = new HashSet<string>(taxa).OrderByDescending(t => t.Length);

            var tagModel = this.CreateNewTaxonNameXmlElement(document, TaxonType.Lower);
            foreach (var item in orderedTaxaParts)
            {
                try
                {
                    var settings = new ContentTaggerSettings
                    {
                        CaseSensitive = true,
                        MinimalTextSelect = true
                    };

                    await this.contentTagger.TagContentInDocument(item, tagModel, LowerTaxaXPath, document, settings);
                }
                catch (Exception e)
                {
                    this.logger.Log(e, "‘{0}’", item);
                }
            }
        }

        private string GetSystemTaxonNameString(XmlNode node)
        {
            const string SpeciesFormatString = " {0}";
            var specificTaxonNamePartsRanks = new string[]
            {
                "species",
                "subspecies",
                "variety",
                "form"
            };

            var stringBuilder = new StringBuilder();

            string genus = this.SelectTaxonNamePartTextValue(node, nameof(genus));
            if (!string.IsNullOrEmpty(genus))
            {
                stringBuilder.Append(genus);
            }

            string subgenus = this.SelectTaxonNamePartTextValue(node, nameof(subgenus));
            if (!string.IsNullOrEmpty(subgenus))
            {
                stringBuilder.AppendFormat(string.IsNullOrEmpty(genus) ? SpeciesFormatString : " ({0})", subgenus);
            }

            foreach (var elementName in specificTaxonNamePartsRanks)
            {
                this.AppendSpecificTaxonNamePartsToStringBuilder(node, SpeciesFormatString, elementName, stringBuilder);
            }

            string taxonNameFullString = stringBuilder.ToString().Trim();

            this.logger?.Log("{0} -> {1}", nameof(this.GetSystemTaxonNameString), taxonNameFullString);

            return taxonNameFullString;
        }

        private void AppendSpecificTaxonNamePartsToStringBuilder(XmlNode node, string speciesFormatString, string taxonNamePartElementName, StringBuilder stringBuilder)
        {
            string taxonNamePartTextValue = this.SelectTaxonNamePartTextValue(node, taxonNamePartElementName);
            if (!string.IsNullOrEmpty(taxonNamePartTextValue))
            {
                stringBuilder.AppendFormat(speciesFormatString, taxonNamePartTextValue);
            }
        }

        private string SelectTaxonNamePartTextValue(XmlNode node, string taxonNamePartElementName)
        {
            string textValue = node.SelectSingleNode(taxonNamePartElementName)?.InnerText ?? string.Empty;
            return Regex.Replace(textValue, @"\s+", string.Empty);
        }
    }
}
