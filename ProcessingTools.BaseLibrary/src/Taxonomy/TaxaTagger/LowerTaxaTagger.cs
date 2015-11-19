namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml;
    using Configurator;
    using Contracts;

    public class LowerTaxaTagger : TaxaTagger
    {
        // private const string LowerTaxaXPathTemplate = "//i[{0}]|//italic[{0}]|//Italic[{0}]";
        private const string LowerTaxaXPathTemplate = "//p[{0}]|//td[{0}]|//th[{0}]|//li[{0}]|//article-title[{0}]|//title[{0}]|//label[{0}]|//ref[{0}]|//kwd[{0}]|//tp:nomenclature-citation[{0}]|//*[@object_id='95'][{0}]|//value[../@id!='244'][../@id!='434'][../@id!='433'][../@id!='432'][../@id!='431'][../@id!='430'][../@id!='429'][../@id!='428'][../@id!='427'][../@id!='426'][../@id!='425'][../@id!='424'][../@id!='423'][../@id!='422'][../@id!='421'][../@id!='420'][../@id!='419'][../@id!='417'][../@id!='48'][{0}]";

        private readonly TagContent lowerTaxaTag = new TagContent("tn", @" type=""lower""");

        private ILogger logger;

        public LowerTaxaTagger(string xml, IStringDataList whiteList, IStringDataList blackList, ILogger logger)
            : base(xml, whiteList, blackList)
        {
            this.logger = logger;
        }

        public LowerTaxaTagger(Config config, string xml, IStringDataList whiteList, IStringDataList blackList, ILogger logger)
            : base(config, xml, whiteList, blackList)
        {
            this.logger = logger;
        }

        public LowerTaxaTagger(IBase baseObject, IStringDataList whiteList, IStringDataList blackList, ILogger logger)
            : base(baseObject, whiteList, blackList)
        {
            this.logger = logger;
        }

        public override void Tag()
        {
            try
            {
                var plausibleLowerTaxa = new HashSet<string>(this.XmlDocument.SelectNodes("//i|//italic|//Italic")
                    .Cast<XmlNode>()
                    .Select(x => x.InnerText)
                    .Where(this.IsMatchingLowerTaxaFormat));

                plausibleLowerTaxa = new HashSet<string>(this.ClearFakeTaxaNames(plausibleLowerTaxa));

                // Tag all direct matches
                plausibleLowerTaxa
                    .OrderByDescending(t => t.Length)
                    .TagContentInDocument(this.lowerTaxaTag, LowerTaxaXPathTemplate, this.XmlDocument, true, true, this.logger);

                // Get all word parts of the plausible lower taxa
                Regex matchWord = new Regex(@"\b[^\W\d]+\b\.?");
                var plausibleLowerTaxaWordTemplates = new HashSet<string>();
                foreach (string taxon in plausibleLowerTaxa)
                {
                    for (Match m = matchWord.Match(taxon); m.Success; m = m.NextMatch())
                    {
                        plausibleLowerTaxaWordTemplates.Add(m.Value);
                    }
                }

                // TODO: bottleneck
                // Tag all word parts of the plausible lower taxa
                plausibleLowerTaxaWordTemplates
                    .OrderByDescending(t => t.Length)
                    .TagContentInDocument(this.lowerTaxaTag, LowerTaxaXPathTemplate, this.XmlDocument, true, true, this.logger);

                // TODO: move to format
                this.Xml = Regex.Replace(
                    this.Xml,
                    @"‘<i>(<tn type=""lower""[^>]*>)([A-Z][a-z\.×]+)(</tn>)(?:</i>)?’\s*(?:<i>)?([a-z\.×-]+)</i>",
                    "$1‘$2’ $4$3");

                // this.AdvancedTagLowerTaxa("//*[i]");
                this.Xml = this.TagInfraspecificTaxa(this.Xml);
            }
            catch
            {
                throw;
            }
        }

        public override void Tag(IXPathProvider xpathProvider)
        {
            this.Tag();
        }

        // TODO: XPath-s correction needed
        private void AdvancedTagLowerTaxa(string xpath)
        {
            foreach (XmlNode node in this.XmlDocument.SelectNodes(xpath, this.NamespaceManager))
            {
                this.TagInfraspecificTaxa(node);
            }

            if ((this.Config.ArticleSchemaType == SchemaType.System) && !this.Config.TagWholeDocument)
            {
                foreach (XmlNode node in this.XmlDocument.SelectNodes("//value[.//tn[@type='lower']]", this.NamespaceManager))
                {
                    this.TagInfraspecificTaxa(node);
                }
            }
        }

        private void TagInfraspecificTaxa(XmlNode node)
        {
            System.Console.WriteLine(node.InnerText);
            System.Console.WriteLine();

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
                const string Subpattern = @"(?![,\.])(?!\s+and\b)(?!\s+as\b)(?!\s+to\b)\s*([^<>\(\)\[\]]{0,40}?)\s*(\(\s*)?((?i)\b(?:subgen(?:us)?|subg|sg|(?:sub)?ser|trib|(?:sub)?sect(?:ion)?)\b\.?)\s*(?:<i>)?(?:<tn type=""lower""[^>]*>)?([A-Za-z][A-Za-z\.-]+(?:\s+[a-z\s\.-]+){0,3})(?:</tn>)?(?:</i>)?(\s*\))?";

                {
                    const string InfraspecificPattern = @"<i><tn type=""lower""[^>]*>([A-Za-z][A-Za-z\.-]+)</tn></i>" + Subpattern;
                    Regex re = new Regex(InfraspecificPattern);

                    for (Match m = re.Match(result); m.Success; m = m.NextMatch())
                    {
                        result = re.Replace(
                            result,
                            @"<tn type=""lower""><genus>$1</genus> <genus-authority>$2</genus-authority> $3<infraspecific-rank>$4</infraspecific-rank> <infraspecific>$5</infraspecific></tn>$6");
                    }

                    result = Regex.Replace(result, @"(?<=\(\s*<infraspecific[^\)]*?)(</tn>)(\s*\))", "$2$1"); // Move closing bracket in tn if it is outside
                }

                {
                    const string InfraspecificPattern = @"(?<=</infraspecific>\s*\)?)</tn>" + Subpattern;
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

                    result = Regex.Replace(result, @"(?<=\(\s*<infraspecific[^\)]*?)(</tn>)(\s*\))", "$2$1"); // Move closing bracket in tn if it is outside
                }
            }

            // <i><tn>A. herbacea</tn></i> Walter var. <i>herbacea</i>
            // <i>Lespedeza hirta</i> (L.) Hornem. var. <i>curtissii</i>
            const string InfraspecificRankSubpattern = @"(?i)(?:\b(?:ab?|sp|var|subvar|subsp|subspec|subspecies|ssp|race|fo?|forma?|st|r|sf|cf|gr|nr|near|sp\.\s*near|n\.?\s*sp|aff|prope|(?:sub)?sect)\b\.?|×|\?)";
            const string InfraspecificRankNamePairSubpattern = @"\s*(" + InfraspecificRankSubpattern + @")\s*(?:<i>|<i [^>]*>)(?:<tn type=""lower""[^>]*>)?([a-z][a-z-]+)(?:</tn>)?</i>";

            {
                {
                    const string InfraspecificPattern = @"(?:<i>|<i [^>]*>)<tn type=""lower""[^>]*>([^<>]*?)</tn></i>(?![,\.])\s*((?:[^<>\(\)\[\]]{0,3}?\([^<>\(\)\[\]]{0,30}?\)[^<>\(\)\[\]]{0,30}?|[^<>\(\)\[\]]{0,30}?)?)" + InfraspecificRankNamePairSubpattern;
                    Regex re = new Regex(InfraspecificPattern);

                    System.Console.WriteLine(re.Match(result).Length);

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
                      Regex.IsMatch(textToCheck, @"\A(?<genus>[‘“]?[A-Z]+[’”]?)\Z");

            return result;
        }
    }
}