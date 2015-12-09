namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml;
    using Configurator;
    using Contracts;
    using Contracts.Log;
    using System;
    using System.Xml.Linq;
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
                var knownLowerTaxaNames = new HashSet<string>(this.XmlDocument.SelectNodes("//tn[@type='lower']")
                    .Cast<XmlNode>()
                    .Select(x => x.InnerText));

                var plausibleLowerTaxa = new HashSet<string>(this.XmlDocument.SelectNodes("//i|//italic|//Italic")
                    .Cast<XmlNode>()
                    .Select(x => x.InnerText)
                    .Where(this.IsMatchingLowerTaxaFormat));

                knownLowerTaxaNames
                    .ToList()
                    .ForEach(t => plausibleLowerTaxa.Add(t));

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
                      Regex.IsMatch(textToCheck, @"\A(?<genus>[‘“]?[A-Z]{2,}[’”]?)\Z");

            return result;
        }

        public class TaxaTagger : TaggerBase
        {
            public const string HigherTaxaMatchPattern = "\\b([A-Z](?i)[a-z]*(morphae?|mida|toda|ideae|oida|genea|formes|ales|lifera|ieae|indeae|eriae|idea|aceae|oidea|oidae|inae|ini|ina|anae|ineae|acea|oideae|mycota|mycotina|mycetes|mycetidae|phyta|phytina|opsida|phyceae|idae|phycidae|ptera|poda|phaga|itae|odea|alia|ntia|osauria))\\b";
            private const string HigherTaxaXPathTemplate = "//p[{0}]|//td[{0}]|//th[{0}]|//li[{0}]|//article-title[{0}]|//title[{0}]|//label[{0}]|//ref[{0}]|//kwd[{0}]|//tp:nomenclature-citation[{0}]|//value[../@id!='244'][../@id!='434'][../@id!='433'][../@id!='432'][../@id!='431'][../@id!='430'][../@id!='429'][../@id!='428'][../@id!='427'][../@id!='426'][../@id!='425'][../@id!='424'][../@id!='423'][../@id!='422'][../@id!='421'][../@id!='420'][../@id!='419'][../@id!='417'][../@id!='48'][{0}]";
            private const string HigherTaxaReplacePattern = "<tn type=\"higher\">$1</tn>";
            private const string LowerRaxaReplacePattern = "<tn type=\"lower\">$1</tn>";
            private const string SelectTreatmentGeneraXPathString = "//tp:taxon-treatment[string(tp:treatment-meta/kwd-group/kwd/named-content[@content-type='order'])='ORDO' or string(tp:treatment-meta/kwd-group/kwd/named-content[@content-type='family'])='FAMILIA']/tp:nomenclature/tn/tn-part[@type='genus']";
            private const string TreatmentMetaReplaceXPathTemplate = "//tp:taxon-treatment[string(tp:nomenclature/tn/tn-part[@type='genus'])='{0}']/tp:treatment-meta/kwd-group/kwd/named-content[@content-type='{1}']";

            public TaxaTagger(string xml)
                : base(xml)
            {
            }

            public TaxaTagger(Config config, string xml)
                : base(config, xml)
            {
            }

            public TaxaTagger(IBase baseObject)
                : base(baseObject)
            {
            }

            public static string TagItalics(string nodeXml)
            {
                string result = nodeXml;

                // Genus (Subgenus)? species subspecies?
                result = Regex.Replace(result, @"(?<=<i>)([A-Z][a-z\.×]+(\-[A-Z][a-z\.×]+)?\s*[a-z\.×-]*)(?=</i>)", LowerRaxaReplacePattern);
                result = Regex.Replace(result, @"(?<=<i>)([A-Z][a-z\.×]+(\-[A-Z][a-z\.×]+)?\s*[a-z\.×-]+\s*[a-z×-]+)(?=</i>)", LowerRaxaReplacePattern);
                result = Regex.Replace(result, @"(?<=<i>)([A-Z][a-z\.×]+(\-[A-Z][a-z\.×]+)?\s*\(\s*[A-Za-z][a-z\.×]+\s*\)\s*[a-z\.×-]*)(?=</i>)", LowerRaxaReplacePattern);
                result = Regex.Replace(result, @"(?<=<i>)([A-Z][a-z\.×]+(\-[A-Z][a-z\.×]+)?\s*\(\s*[A-Za-z][a-z\.×]+\s*\)\s*[a-z\.×-]+\s*[a-z×-]+)(?=</i>)", LowerRaxaReplacePattern);
                result = Regex.Replace(result, @"(?<=<i>)([A-Z\.-]{3,30})(?=</i>)", LowerRaxaReplacePattern);

                result = Regex.Replace(result, @"‘<i>(<tn type=""lower"">)([A-Z][a-z\.×]+)(</tn>)</i>’\s*<i>([a-z\.×-]+)</i>", "$1‘$2’ $4$3");

                result = TagInfraspecificTaxa(result);

                return result;
            }

            public static string TagInfraspecificTaxa(string nodeXml)
            {
                string replace = nodeXml;
                string infraspecificPattern;

                // Neoserica (s. l.) abnormoides, Neoserica (sensu lato) abnormis
                infraspecificPattern = @"<i><tn type=""lower"">([^<>]*?)</tn></i>\s*((?:\(\s*)?(?i)(?:\bsensu\b\s*[a-z]*|s\.?\s*[ls]\.?|s\.?\s*str\.?)(?:\s*\))?)\s*<i>([a-z\s-]+)</i>";
                replace = Regex.Replace(
                    replace,
                    infraspecificPattern,
                    @"<tn type=""lower""><basionym>$1</basionym> <sensu>$2</sensu> <specific>$3</specific></tn>");

                // Genus subgen(us)?. Subgenus sect(ion)?. Section subsect(ion)?. Subsection
                {
                    const string Subpattern = @"(?![,\.])(?!\s+and\b)(?!\s+as\b)(?!\s+to\b)\s*([^<>\(\)\[\]]{0,40}?)\s*(\(\s*)?((?i)\b(?:subgen(?:us)?|subg|ser|(?:sub)?sect(?:ion)?)\b\.?)\s*(?:<i>)?(?:<tn type=""lower"">)?([A-Za-z\.-]+(?:\s+[a-z\s\.-]+){0,3})(?:</tn>)?(?:</i>)?(\s*\))?";

                    infraspecificPattern = @"<i><tn type=""lower"">([A-Za-z\.-]+)</tn></i>" + Subpattern;
                    for (Match m = Regex.Match(replace, infraspecificPattern); m.Success; m = m.NextMatch())
                    {
                        replace = Regex.Replace(
                            replace,
                            infraspecificPattern,
                            @"<tn type=""lower""><genus>$1</genus> <genus-authority>$2</genus-authority> $3<infraspecific-rank>$4</infraspecific-rank> <infraspecific>$5</infraspecific></tn>$6");
                    }

                    replace = Regex.Replace(replace, @"(?<=\(\s*<infraspecific[^\)]*?)(</tn>)(\s*\))", "$2$1"); // Move closing bracket in tn if it is outside

                    infraspecificPattern = @"(?<=</infraspecific>\s*\)?)</tn>" + Subpattern;
                    for (int i = 0; i < 3; i++)
                    {
                        for (Match m = Regex.Match(replace, infraspecificPattern); m.Success; m = m.NextMatch())
                        {
                            replace = Regex.Replace(
                                replace,
                                infraspecificPattern,
                                " <authority>$1</authority> $2<infraspecific-rank>$3</infraspecific-rank> <infraspecific>$4</infraspecific></tn>$5");
                        }
                    }

                    replace = Regex.Replace(replace, @"(?<=\(\s*<infraspecific[^\)]*?)(</tn>)(\s*\))", "$2$1"); // Move closing bracket in tn if it is outside
                }

                // <i><tn>A. herbacea</tn></i> Walter var. <i>herbacea</i>
                // <i>Lespedeza hirta</i> (L.) Hornem. var. <i>curtissii</i>
                {
                    infraspecificPattern = @"<i><tn type=""lower"">([^<>]*?)</tn></i>(?![,\.])\s*((?:[^<>\(\)\[\]]{0,3}?\([^<>\(\)\[\]]{0,30}?\)[^<>\(\)\[\]]{0,30}?|[^<>\(\)\[\]]{0,30}?)?)\s*((?i)(?:\b(?:ab?|sp|var|subvar|subvar|subsp|subspecies|ssp|f|forma?|st|r|sf|cf|nr|near|sp\. near|aff|prope|(?:sub)?sect)\b\.?)|×|\?)\s*<i>([a-z-]+)</i>";
                    for (Match m = Regex.Match(replace, infraspecificPattern); m.Success; m = m.NextMatch())
                    {
                        replace = Regex.Replace(
                            replace,
                            infraspecificPattern,
                            @"<tn type=""lower""><basionym>$1</basionym> <basionym-authority>$2</basionym-authority> <infraspecific-rank>$3</infraspecific-rank> <infraspecific>$4</infraspecific></tn>");
                    }

                    replace = Regex.Replace(
                        replace,
                        @"(?<=</infraspecific>\s*\)?)</tn>\s*<i>([A-Za-z\.\s-]+)</i>",
                        " <species>$1</species></tn>");

                    infraspecificPattern = @"(?<=(?:</infraspecific>|</species>)\s*\)?)</tn>\s*([^<>]{0,100}?)\s*((?i)(?:\b(?:ab?|n?\.?\s*sp|var|subvar|subsp|subspecies|ssp|subspec|f|fo|forma?|st|r|sf|cf|nr|near|aff|prope|(?:sub)?sect)\b\.?)|×|\?)\s*<i>([a-z-]+)</i>";
                    for (int i = 0; i < 4; i++)
                    {
                        for (Match m = Regex.Match(replace, infraspecificPattern); m.Success; m = m.NextMatch())
                        {
                            replace = Regex.Replace(
                                replace,
                                infraspecificPattern,
                                " <authority>$1</authority> <infraspecific-rank>$2</infraspecific-rank> <infraspecific>$3</infraspecific></tn>");
                        }
                    }
                }

                // Here we must extract species+subspecies in <infraspecific/>, which comes from tagging of subgenera and [sub]sections
                replace = Regex.Replace(replace, @"<infraspecific>([A-Za-z\.-]+)\s+([a-z\s\.-]+)</infraspecific>", "<infraspecific>$1</infraspecific> <species>$2</species>");

                replace = Regex.Replace(replace, " <([a-z-]+)?authority></([a-z-]+)?authority>", string.Empty);

                return replace;
            }

            public void TagLowerTaxa(bool tagBasionym = false)
            {
                string xpath = string.Empty;
                if (this.Config.ArticleSchemaType == SchemaType.Nlm)
                {
                    if (this.Config.TagWholeDocument)
                    {
                        xpath = "//*[i]";
                    }
                    else
                    {
                        xpath = "//p[.//i]|//ref[.//i]|//kwd[.//i]|//article-title[.//i]|//li[.//i]|//th[.//i]|//td[.//i]|//title[.//i]|//label[.//i]|//tp:nomenclature-citation[.//i]";
                    }
                }
                else
                {
                    if (this.Config.TagWholeDocument)
                    {
                        xpath = "//*[i]";
                    }
                    else
                    {
                        xpath = "//p[.//i]|//li[.//i]|//td[.//i]|//th[.//i]";
                    }
                }

                /*
                 * The following piece of code will be executed twice: once for lower-level-content-holding tags, and next for all value tags (System)
                 */
                try
                {
                    foreach (XmlNode node in this.XmlDocument.SelectNodes(xpath, this.NamespaceManager))
                    {
                        node.InnerXml = TaxaTagger.TagItalics(node.InnerXml);
                    }

                    if (this.Config.ArticleSchemaType != SchemaType.Nlm && !this.Config.TagWholeDocument)
                    {
                        foreach (XmlNode node in this.XmlDocument.SelectNodes("//value[.//i]", this.NamespaceManager))
                        {
                            node.InnerXml = TaxaTagger.TagItalics(node.InnerXml);
                        }
                    }

                    this.RemoveTaxaInWrongPlaces();

                    if (tagBasionym)
                    {
                        foreach (XmlNode node in this.XmlDocument.SelectNodes(xpath, this.NamespaceManager))
                        {
                            node.InnerXml = TaxaTagger.TagInfraspecificTaxa(node.InnerXml);
                        }

                        if (this.Config.ArticleSchemaType != SchemaType.Nlm && !this.Config.TagWholeDocument)
                        {
                            foreach (XmlNode node in this.XmlDocument.SelectNodes("//value[.//tn[@type='lower']]", this.NamespaceManager))
                            {
                                node.InnerXml = TaxaTagger.TagInfraspecificTaxa(node.InnerXml);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    ////Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0, "Tag taxa.");
                }
            }

            private void RemoveFalseTaxaOfPersonNames()
            {
                try
                {
                    List<string> firstWordTaxaList = this.GetFirstWordOfTaxaNames();

                    char[] charsToSplit = new char[] { ' ', ',', ';' };
                    List<string> personNameParts = this.XmlDocument.SelectNodes("//surname[string-length(normalize-space(.)) > 2]|//given-names[string-length(normalize-space(.)) > 2]")
                        .Cast<XmlNode>().Select(s => s.InnerText).Distinct().ToList();

                    foreach (string taxon in firstWordTaxaList)
                    {
                        if (taxon.IndexOf('.') < 0)
                        {
                            Regex matchTaxonInName = new Regex("(?i)\\b" + Regex.Escape(taxon) + "\\b");
                            IEnumerable<string> queryResult = from item in personNameParts
                                                              where matchTaxonInName.Match(item).Success
                                                              select matchTaxonInName.Match(item).Value;

                            foreach (string item in queryResult)
                            {
                                if (item.IndexOf('.') < 0)
                                {
                                    this.XmlDocument.InnerXml = Regex.Replace(this.XmlDocument.InnerXml, "<tn [^>]*>((?i)" + item + "(\\s+.*?)?(\\.?))</tn>", "$1");
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    ////Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0);
                }
            }

            private void RemoveTaxaInWrongPlaces()
            {
                string xpath = "tn[.//tn] | a[.//tn] | ext-link[.//tn] | xref[.//tn] | article/front/notes/sec[.//tn] | tp:treatment-meta/kwd-group/kwd/named-content[.//tn] | *[@object_id='82'][.//tn] | *[@id='41'][.//tn] | *[@id='236' or @id='436' or @id='435' or @id='418' or @id='49' or @id='417' or @id='48' or @id='434' or @id='433' or @id='432' or @id='431' or @id='430' or @id='429' or @id='428' or @id='427' or @id='426' or @id='425' or @id='424' or @id='423' or @id='422' or @id='421' or @id='420' or @id='419' or @id='475' or @id='414']/value[.//tn]";
                Regex matchTaxonTag = new Regex(@"</?tn[^>]*>");
                foreach (XmlNode node in this.XmlDocument.SelectNodes(xpath, this.NamespaceManager))
                {
                    node.InnerXml = matchTaxonTag.Replace(node.InnerXml, string.Empty);
                }
            }

            private XElement GetBlackList()
            {
                return XElement.Load(this.Config.BlackListXmlFilePath);
            }

            private void ApplyBlackList()
            {
                try
                {
                    List<string> firstWordTaxaList = this.GetFirstWordOfTaxaNames();

                    XElement blackList = this.GetBlackList();

                    string xml = this.XmlDocument.InnerXml;
                    foreach (string taxon in firstWordTaxaList)
                    {
                        IEnumerable<string> queryResult = from item in blackList.Elements()
                                                          where Regex.Match(taxon, "(?i)" + item.Value).Success
                                                          select item.Value;
                        foreach (string item in queryResult)
                        {
                            xml = Regex.Replace(xml, "<tn [^>]*>((?i)" + item + "(\\s+.*?)?(\\.?))</tn>", "$1");
                        }
                    }

                    this.XmlDocument.InnerXml = xml;
                }
                catch (Exception e)
                {
                    ////Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0, "Apply black list.");
                }
            }

            private List<string> GetFirstWordOfTaxaNames()
            {
                List<string> firstWordTaxaList = this.XmlDocument.GetStringListOfUniqueXmlNodes("//tn", this.NamespaceManager)
                    .Cast<string>().Select(c => Regex.Match(c, @"\w+\.|\w+\b").Value).Distinct().ToList();
                return firstWordTaxaList;
            }
        }
    }
}