namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;

    using Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Configurator;
    using ProcessingTools.Contracts;
    using ProcessingTools.Infrastructure.Extensions;

    public class LowerTaxaTagger : TaxaTagger
    {
        // private const string LowerTaxaXPathTemplate = "//i[{0}]|//italic[{0}]|//Italic[{0}]";
        private const string LowerTaxaXPathTemplate = "//p[{0}]|//td[{0}]|//th[{0}]|//li[{0}]|//article-title[{0}]|//title[{0}]|//label[{0}]|//ref[{0}]|//kwd[{0}]|//tp:nomenclature-citation[{0}]|//*[@object_id='95'][{0}]|//*[@object_id='90'][{0}]|//value[../@id!='244'][../@id!='434'][../@id!='433'][../@id!='432'][../@id!='431'][../@id!='430'][../@id!='429'][../@id!='428'][../@id!='427'][../@id!='426'][../@id!='425'][../@id!='424'][../@id!='423'][../@id!='422'][../@id!='421'][../@id!='420'][../@id!='419'][../@id!='417'][../@id!='48'][{0}]";

        private const string ItalicXPath = "//i[not(tn)]|//italic[not(tn)]|//Italic[not(tn)]";

        private ILogger logger;

        public LowerTaxaTagger(Config config, string xml, ITaxonomicListDataService<string> blackList, ILogger logger)
            : base(config, xml, blackList)
        {
            this.logger = logger;
        }

        public override Task Tag()
        {
            return Task.Run(() =>
            {
                var knownLowerTaxaNames = new HashSet<string>(this.XmlDocument.SelectNodes("//tn[@type='lower']")
                    .Cast<XmlNode>()
                    .Select(x => x.InnerText));

                var plausibleLowerTaxa = new HashSet<string>(this.XmlDocument.SelectNodes(ItalicXPath)
                    .Cast<XmlNode>()
                    .Select(x => x.InnerText)
                    .Where(this.IsMatchingLowerTaxaFormat));

                foreach (string taxon in knownLowerTaxaNames)
                {
                    plausibleLowerTaxa.Add(taxon);
                }

                plausibleLowerTaxa = new HashSet<string>(this.ClearFakeTaxaNames(plausibleLowerTaxa)
                    .Select(name => name.ToLower()));

                var comparer = StringComparer.InvariantCultureIgnoreCase;

                // Tag all direct matches
                this.XmlDocument.SelectNodes(ItalicXPath)
                    .Cast<XmlNode>()
                    .AsParallel()
                    .ForAll(node =>
                    {
                        if (plausibleLowerTaxa.Contains(node.InnerText, comparer))
                        {
                            XmlElement tn = node.OwnerDocument.CreateElement("tn");
                            tn.SetAttribute("type", "lower");
                            tn.InnerXml = node.InnerXml;
                            node.InnerXml = tn.OuterXml;
                        }
                    });

                // TODO: move to format
                this.Xml = Regex.Replace(
                    this.Xml,
                    @"‘<i>(<tn type=""lower""[^>]*>)([A-Z][a-z\.×]+)(</tn>)(?:</i>)?’\s*(?:<i>)?([a-z\.×-]+)</i>",
                    "$1‘$2’ $4$3");

                const string StructureXPathTemplate = "//p[{0}]|//license-p[{0}]|//li[{0}]|//th[{0}]|//td[{0}]|//mixed-citation[{0}]|//element-citation[{0}]|//nlm-citation[{0}]|//tp:nomenclature-citation[{0}]";

                this.AdvancedTagLowerTaxa(string.Format(StructureXPathTemplate, "count(.//tn[@type='lower']) != 0"));
                ////this.Xml = this.TagInfraspecificTaxa(this.Xml);

                this.DeepTag();
            });
        }

        // TODO: XPath-s correction needed
        private void AdvancedTagLowerTaxa(string xpath)
        {
            this.XmlDocument.SelectNodes(xpath, this.NamespaceManager)
                .Cast<XmlNode>()
                .AsParallel()
                .ForAll(this.TagInfraspecificTaxa);

            ////if ((this.Config.ArticleSchemaType == SchemaType.System) && !this.Config.TagWholeDocument)
            ////{
            ////    this.XmlDocument.SelectNodes("//value[.//tn[@type='lower']]", this.NamespaceManager)
            ////        .Cast<XmlNode>()
            ////        .AsParallel()
            ////        .ForAll(this.TagInfraspecificTaxa);
            ////}
        }

        private void TagInfraspecificTaxa(XmlNode node)
        {
            string replace = this.TagInfraspecificTaxa(node.InnerXml);
            node.SafeReplaceInnerXml(replace, this.logger);
        }

        private string TagInfraspecificTaxa(string content)
        {
            string result = content;

            const string SensuPattern = @"(?:\(\s*)?(?i)(?:\bsensu\b\s*[a-z]*|s\.?\s*[ls]\.?|s\.?\s*str\.?)(?:\s*\))?";
            {
                // Neoserica (s. l.) abnormoides, Neoserica (sensu lato) abnormis
                const string InfraspecificPattern = @"<i><tn type=""lower""[^>]*>([^<>]*?)</tn></i>\s*(" + SensuPattern + @")\s*<i>(?:<tn type=""lower""[^>]*>)?([a-z][a-z\s-]+)(?:</tn>)?</i>";
                Regex re = new Regex(InfraspecificPattern);

                result = re.Replace(
                    result,
                    @"<tn type=""lower""><basionym>$1</basionym> <sensu>$2</sensu> <specific>$3</specific></tn>");
            }

            // Genus subgen(us)?. Subgenus sect(ion)?. Section subsect(ion)?. Subsection
            {
                const string Subpattern = @"(?![,\.])(?!\s+and\b)(?!\s+w?as\b)(?!\s+from\b)(?!\s+w?remains\b)(?!\s+to\b)\s*([^<>\(\)\[\]:\+]{0,40}?)\s*(\(\s*)?((?i)\b(?:subgen(?:us)?|subg|sg|(?:sub)?ser|trib|(?:sub)?sect(?:ion)?)\b\.?)\s*(?:<i>)?(?:<tn type=""lower""[^>]*>)?([A-Za-z][A-Za-z\.-]+(?:\s+[a-z\s\.-]+){0,3})(?:</tn>)?(?:</i>)?(\s*\))?";

                {
                    const string InfraspecificPattern = @"<i><tn type=""lower""[^>]*>([A-Za-z][A-Za-z\.-]+)</tn></i>" + Subpattern;
                    Regex re = new Regex(InfraspecificPattern);

                    for (Match m = re.Match(result); m.Success; m = m.NextMatch())
                    {
                        result = re.Replace(
                            result,
                            @"<tn type=""lower""><genus>$1</genus> <authority>$2</authority> $3<infraspecific-rank>$4</infraspecific-rank> <infraspecific>$5</infraspecific></tn>$6");
                    }

                    // Move closing bracket in tn if it is outside
                    result = Regex.Replace(result, @"(?<=\(\s*<infraspecific[^\)]*?)(</tn>)(\s*\))", "$2$1");
                }

                {
                    const string InfraspecificPattern = @"(?<=</infraspecific>[\s\)]*)</tn>" + Subpattern;
                    Regex re = new Regex(InfraspecificPattern);

                    for (int i = 0; i < 3; i++)
                    {
                        for (Match m = re.Match(result); m.Success; m = m.NextMatch())
                        {
                            result = re.Replace(
                                result,
                                " <authority>$1</authority> $2<infraspecific-rank>$3</infraspecific-rank> <infraspecific>$4</infraspecific></tn>$5");
                        }
                    }

                    // Move closing bracket in tn if it is outside
                    result = Regex.Replace(result, @"(?<=\(\s*<infraspecific[^\)]*?)(</tn>)(\s*\))", "$2$1");
                }
            }

            // <i><tn>A. herbacea</tn></i> Walter var. <i>herbacea</i>
            // <i>Lespedeza hirta</i> (L.) Hornem. var. <i>curtissii</i>
            const string InfraspecificRankSubpattern = @"(?i)(?:\b(?:ab?|sp|var|subvar|subsp|subspec|subspecies|ssp|race|fo?|forma?|st|r|sf|cf|gr|n\.?\s*sp|nr|(?:sp(?:\.\s*|\s+))?(?:near|aff)|prope|(?:sub)?sect)\b\.?|×|\?)";
            const string InfraspecificRankNamePairSubpattern = @"\s*(" + InfraspecificRankSubpattern + @")\s*(?:<i>|<i [^>]*>)(?:<tn type=""lower""[^>]*>)?([a-z][a-z-]+)(?:</tn>)?</i>";

            {
                {
                    const string InfraspecificPattern = @"(?:<i>|<i [^>]*>)<tn type=""lower""[^>]*>([^<>]*?)</tn></i>(?![,\.])\s*((?:[^<>\(\)\[\]:\+]{0,3}?\([^<>\(\)\[\]:\+]{0,30}?\)[^<>\(\)\[\]:\+]{0,30}?|[^<>\(\)\[\]:\+]{0,30}?)?)" + InfraspecificRankNamePairSubpattern;
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
            }

            // Here we must extract species+subspecies in <infraspecific/>, which comes from tagging of subgenera and [sub]sections
            result = Regex.Replace(result, @"<infraspecific>([A-Za-z][A-Za-z\.-]+)\s+([a-z][a-z\s\.-]+)</infraspecific>", "<infraspecific>$1</infraspecific> <species>$2</species>");

            result = Regex.Replace(result, @" (?:<(?:[a-z-]+)?authority></(?:[a-z-]+)?authority>|<(?:[a-z-]+)?authority\s*/>)", string.Empty);

            return result;
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

        private void DeepTag()
        {
            var knownLowerTaxaNamesXml = new HashSet<string>(this.XmlDocument.SelectNodes("//tn[@type='lower']")
                .Cast<XmlNode>()
                .Select(x => x.InnerXml));

            //////string clearUselessTaxonNamePartsSubpattern = string.Join(
            //////    "|",
            //////    SpeciesPartsPrefixesResolver
            //////        .SpeciesPartsRanks
            //////        .Keys
            //////        .Select(k => $"\\b{Regex.Escape(k)}\\b\\."));

            // TODO: This algorithm must be refined: generate smaller pattern strings from the original.
            var taxa = knownLowerTaxaNamesXml
                .Select(t => t.RegexReplace(@"<(sensu)[^>/]*>.*?</\1>|<((?:basionym-)?authority)[^>/]*>.*?</\2>|<(infraspecific-rank)[^>/]*>.*?</\3>|\bcf\b\.|\bvar\b\.", string.Empty))
                .Select(t => t.RegexReplace(@"<[^>]*>", string.Empty))
                .Select(t => t.RegexReplace(@"[^\w\.]+", " "))
                .ToList();

            foreach (string taxon in new HashSet<string>(taxa))
            {
                taxa.AddRange(taxon.Split(' ')
                    .Where(s => !string.IsNullOrWhiteSpace(s) && s.Length > 2));
            }

            var orderedTaxaParts = new HashSet<string>(taxa).OrderByDescending(t => t.Length);

            XmlElement lowerTaxaTagModel = this.XmlDocument.CreateElement("tn");
            lowerTaxaTagModel.SetAttribute("type", "lower");

            foreach (var item in orderedTaxaParts)
            {
                try
                {
                    item.TagContentInDocument(
                        lowerTaxaTagModel,
                        LowerTaxaXPathTemplate,
                        this.NamespaceManager,
                        this.XmlDocument,
                        true,
                        true,
                        this.logger)
                        .Wait();
                }
                catch (Exception e)
                {
                    this.logger.Log(e, "‘{0}’", item);
                }
            }
        }
    }
}