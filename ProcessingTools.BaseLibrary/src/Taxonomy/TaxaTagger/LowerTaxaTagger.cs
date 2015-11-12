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
        private const string LowerTaxaXPathTemplate = "//i[{0}]|//italic[{0}]|//Italic[{0}]";

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

                // Add all parts of plausibleLowerTaxa to the plausibleLowerTaxa hashset
                Regex matchWord = new Regex(@"\b[^\W\d]+\b\.?");
                
                // TODO: bottleneck
                foreach (string item in new HashSet<string>(plausibleLowerTaxa))
                {
                    for (Match m = matchWord.Match(item); m.Success; m = m.NextMatch())
                    {
                        plausibleLowerTaxa.Add(m.Value);
                    }
                }

                plausibleLowerTaxa.TagContentInDocument(this.lowerTaxaTag, LowerTaxaXPathTemplate, this.XmlDocument, true, true, this.logger);

                // TODO: move to format
                this.Xml = Regex.Replace(
                    this.Xml,
                    @"‘<i>(<tn type=""lower"">)([A-Z][a-z\.×]+)(</tn>)(?:</i>)?’\s*(?:<i>)?([a-z\.×-]+)</i>",
                    "$1‘$2’ $4$3");

                this.AdvancedTagLowerTaxa("//*[i]");
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
            string replace = node.InnerXml;

            const string SensuPattern = @"(?:\(\s*)?(?i)(?:\bsensu\b\s*[a-z]*|s\.?\s*[ls]\.?|s\.?\s*str\.?)(?:\s*\))?";

            {
                // Neoserica (s. l.) abnormoides, Neoserica (sensu lato) abnormis
                const string InfraspecificPattern = @"<i><tn type=""lower""[^>]*>([^<>]*?)</tn></i>\s*(" + SensuPattern + @")\s*<i>(?:<tn type=""lower""[^>]*>)?([a-z\s-]+)(?:</tn>)?</i>";
                Regex re = new Regex(InfraspecificPattern);

                replace = re.Replace(
                    replace,
                    @"<tn type=""lower""><basionym>$1</basionym> <sensu>$2</sensu> <specific>$3</specific></tn>");
            }

            // Genus subgen(us)?. Subgenus sect(ion)?. Section subsect(ion)?. Subsection
            {
                const string Subpattern = @"(?![,\.])(?!\s+and\b)(?!\s+as\b)(?!\s+to\b)\s*([^<>\(\)\[\]]{0,40}?)\s*(\(\s*)?((?i)\b(?:subgen(?:us)?|subg|ser|trib|(?:sub)?sect(?:ion)?)\b\.?)\s*(?:<i>)?(?:<tn type=""lower"">)?([A-Za-z\.-]+(?:\s+[a-z\s\.-]+){0,3})(?:</tn>)?(?:</i>)?(\s*\))?";

                {
                    const string InfraspecificPattern = @"<i><tn type=""lower""[^>]*>([A-Za-z\.-]+)</tn></i>" + Subpattern;
                    Regex re = new Regex(InfraspecificPattern);

                    for (Match m = re.Match(replace); m.Success; m = m.NextMatch())
                    {
                        replace = re.Replace(
                            replace,
                            @"<tn type=""lower""><genus>$1</genus> <genus-authority>$2</genus-authority> $3<infraspecific-rank>$4</infraspecific-rank> <infraspecific>$5</infraspecific></tn>$6");
                    }

                    replace = Regex.Replace(replace, @"(?<=\(\s*<infraspecific[^\)]*?)(</tn>)(\s*\))", "$2$1"); // Move closing bracket in tn if it is outside
                }

                {
                    const string InfraspecificPattern = @"(?<=</infraspecific>\s*\)?)</tn>" + Subpattern;
                    Regex re = new Regex(InfraspecificPattern);

                    for (int i = 0; i < 3; i++)
                    {
                        for (Match m = re.Match(replace); m.Success; m = m.NextMatch())
                        {
                            replace = re.Replace(
                                replace,
                                " <authority>$1</authority> $2<infraspecific-rank>$3</infraspecific-rank> <infraspecific>$4</infraspecific></tn>$5");
                        }
                    }

                    replace = Regex.Replace(replace, @"(?<=\(\s*<infraspecific[^\)]*?)(</tn>)(\s*\))", "$2$1"); // Move closing bracket in tn if it is outside
                }
            }

            // <i><tn>A. herbacea</tn></i> Walter var. <i>herbacea</i>
            // <i>Lespedeza hirta</i> (L.) Hornem. var. <i>curtissii</i>
            const string InfraSpecificSubpattern = @"(?i)(?:\b(?:ab?|sp|var|subvar|subsp|subspec|subspecies|ssp|race|fo?|forma?|st|r|sf|cf|gr|nr|near|sp\.\s*near|n\.?\s*sp|aff|prope|(?:sub)?sect)\b\.?|×|\?)";

            {
                {
                    const string InfraspecificPattern = @"<i><tn type=""lower""[^>]*>([^<>]*?)</tn></i>(?![,\.])\s*((?:[^<>\(\)\[\]]{0,3}?\([^<>\(\)\[\]]{0,30}?\)[^<>\(\)\[\]]{0,30}?|[^<>\(\)\[\]]{0,30}?)?)\s*(" + InfraSpecificSubpattern + @")\s*<i>(?:<tn type=""lower""[^>]*>)?([a-z-]+)(?:</tn>)?</i>";
                    Regex re = new Regex(InfraspecificPattern);

                    for (Match m = re.Match(replace); m.Success; m = m.NextMatch())
                    {
                        replace = re.Replace(
                            replace,
                            @"<tn type=""lower""><basionym>$1</basionym> <basionym-authority>$2</basionym-authority> <infraspecific-rank>$3</infraspecific-rank> <infraspecific>$4</infraspecific></tn>");
                    }

                    replace = Regex.Replace(
                        replace,
                        @"(?<=</infraspecific>\s*\)?)</tn>\s*<i>(?:<tn type=""lower""[^>]*>)?([A-Za-z\.\s-]+)(?:</tn>)?</i>",
                        " <species>$1</species></tn>");
                }

                {
                    const string InfraspecificPattern = @"(?<=(?:</infraspecific>|</species>)\s*\)?)</tn>\s*([^<>]{0,100}?)\s*(" + InfraSpecificSubpattern + @")\s*<i>(?:<tn type=""lower""[^>]*>)?([a-z-]+)(?:</tn>)?</i>";
                    Regex re = new Regex(InfraspecificPattern);

                    for (int i = 0; i < 4; i++)
                    {
                        for (Match m = re.Match(replace); m.Success; m = m.NextMatch())
                        {
                            replace = re.Replace(
                                replace,
                                " <authority>$1</authority> <infraspecific-rank>$2</infraspecific-rank> <infraspecific>$3</infraspecific></tn>");
                        }
                    }
                }
            }

            // Here we must extract species+subspecies in <infraspecific/>, which comes from tagging of subgenera and [sub]sections
            replace = Regex.Replace(replace, @"<infraspecific>([A-Za-z\.-]+)\s+([a-z\s\.-]+)</infraspecific>", "<infraspecific>$1</infraspecific> <species>$2</species>");

            replace = Regex.Replace(replace, " <([a-z-]+)?authority></([a-z-]+)?authority>", string.Empty);

            node.SafeReplaceInnerXml(replace, this.logger);
        }

        private bool IsMatchingLowerTaxaFormat(string textToCheck)
        {
            bool result = false;

            result |= Regex.IsMatch(textToCheck, @"\A[A-Z][a-z\.×]+(\-[A-Z][a-z\.×]+)?\s*[a-z\.×-]*\Z") ||
                      Regex.IsMatch(textToCheck, @"\A[A-Z][a-z\.×]+(\-[A-Z][a-z\.×]+)?\s*[a-z\.×-]+\s*[a-z×-]+\Z") ||
                      Regex.IsMatch(textToCheck, @"\A[A-Z][a-z\.×]+(\-[A-Z][a-z\.×]+)?\s*\(\s*[A-Za-z][a-z\.×]+\s*\)\s*[a-z\.×-]*\Z") ||
                      Regex.IsMatch(textToCheck, @"\A[A-Z][a-z\.×]+(\-[A-Z][a-z\.×]+)?\s*\(\s*[A-Za-z][a-z\.×]+\s*\)\s*[a-z\.×-]+\s*[a-z×-]+\Z") ||
                      Regex.IsMatch(textToCheck, @"\A[A-Z][a-z\.×]+(\-[A-Z][a-z\.×]+)?\s*\(\s*[A-Za-z][a-z\.×]+\s*\)\s*[a-z\.×-]+\s*[a-z×-]+\Z");

            return result;
        }
    }
}