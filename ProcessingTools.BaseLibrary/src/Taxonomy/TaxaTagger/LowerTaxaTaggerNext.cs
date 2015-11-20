namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml;
    using Configurator;
    using Contracts;

    public class LowerTaxaTaggerNext : TaxaTagger
    {
        private const int NumberOfSequentalInfraRanks = 5;

        private const string InfragenericSubpattern = @"(?i)\b(?:subgen(?:us)?|subg|ser|trib|(?:sub)?sect(?:ion)?)\b\.?";
        private const string InfraspecificSubpattern = @"(?i)(?:\b(?:ab?|sp|var|subvar|subsp|subspec|subspecies|ssp|race|fo?|forma?|st|r|sf|cf|gr|nr|near|sp\.\s*near|n\.?\s*sp|aff|prope|(?:sub)?sect)\b\.?|×|\?)";

        private const string LowerTaxaXPathTemplate = "//i[{0}]|//italic[{0}]|//Italic[{0}]";

        private readonly TagContent lowerTaxaTag = new TagContent("tn", @" type=""lower""");

        private ILogger logger;

        public LowerTaxaTaggerNext(string xml, IStringDataList whiteList, IStringDataList blackList, ILogger logger)
            : base(xml, whiteList, blackList)
        {
            this.logger = logger;
        }

        public LowerTaxaTaggerNext(Config config, string xml, IStringDataList whiteList, IStringDataList blackList, ILogger logger)
            : base(config, xml, whiteList, blackList)
        {
            this.logger = logger;
        }

        public LowerTaxaTaggerNext(IBase baseObject, IStringDataList whiteList, IStringDataList blackList, ILogger logger)
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
                    @"‘(?:<i>)?<tn type=""lower""[^>]*>([A-Z][a-z\.×]+)</tn>(?:</i>)?’\s*(?:<i>)?([a-z][a-z\.×-]+)(?:</i>)?",
                    @"<tn type=""lower"">‘$1’ $2</tn>");

                {
                    string replace = this.Xml;

                    replace = this.TagInfraspecificTaxa(replace);

                    // Remove italics around tn[@type="lower"]
                    replace = Regex.Replace(replace, @"<i>(<tn type=""lower""[^>]*>.*?</tn>)</i>", "$1");

                    replace = this.CombineSequentalSpaceSeparatedInfraRanks(replace, InfraspecificSubpattern);
                    replace = this.CombineSequentalSpaceSeparatedInfraRanks(replace, InfragenericSubpattern);

                    this.XmlDocument.SafeReplaceInnerXml(replace, this.logger);
                }
            }
            catch
            {
                throw;
            }
        }

        private string CombineSequentalSpaceSeparatedInfraRanks(string content, string infraSubpattern)
        {
            string result = content;

            var matchSequentalTaxaTags = new Regex(@"<tn type=""lower""[^>]*>([^<>]*)</tn>\s*<tn type=""lower""[^>]*>(" + infraSubpattern + @"[^<>]*)</tn>");
            const string SequentalTaxaTagsReplacement = @"<tn type=""lower"">$1 $2</tn>";

            var matchBracketsSequentalTaxaTags = new Regex(@"<tn type=""lower""[^>]*>([^<>]*)</tn>\s*\(\s*<tn type=""lower""[^>]*>(" + infraSubpattern + @"[^<>]*)</tn>\s*\)");
            const string BracketsSequentalTaxaTagsReplacement = @"<tn type=""lower"">$1 ($2)</tn>";

            for (int i = 0; i < NumberOfSequentalInfraRanks; ++i)
            {
                result = matchSequentalTaxaTags.Replace(result, SequentalTaxaTagsReplacement);
                result = matchBracketsSequentalTaxaTags.Replace(result, BracketsSequentalTaxaTagsReplacement);
            }

            return result;
        }

        private string TagInfraspecificTaxa(string content)
        {
            string result = content;

            result = this.ProcessSensuStrictoOrLato(result);
            result = this.ProcessSubgenericTaxa(result);
            result = this.ProcessSubspecificTaxa(result);
            result = this.ProcessAuthorities(result);

            return result;
        }

        private string ProcessAuthorities(string content)
        {
            string result = content;

            // TODO
            /*
            (?<=</tn-part>)</tn>\s*\(\s*([^\W\d](?:[\w\.,-]+|\s+|&[A-Za-z0-9#]+;)+)\s*\)
             (<tn-part type="basionym-authority">$1</tn-part>)</tn>

            </tn>\s*([^\W\d](?:[\w\.-]+|\s+|&[A-Za-z0-9#]+;)+?)
            </tn>\s*((?:\u[\w\.-]+|\s+?|&[A-Za-z0-9#]+;)+\l)\b
            </tn>\s*((?:\u[\w\.-]+|\s+?|&[A-Za-z0-9#]+;)+\l\b\.?)
            </tn>\s*((?:\u[\w\s\.-]+|&[A-Za-z0-9#]+;)+\l\b\.??)
            </tn>\s*((?:\u[\w\s\.-]+|&[A-Za-z0-9#]+;)+\l\b\.??)(?=[^\.\w])

             <tn-part type="authority">$1</tn-part></tn>
            */

            return result;
        }

        private string ProcessSubspecificTaxa(string content)
        {
            // <i><tn>A. herbacea</tn></i> Walter var. <i>herbacea</i>
            // <i>Lespedeza hirta</i> (L.) Hornem. var. <i>curtissii</i>
            string result = content;

            const string InfraspecificPattern = @"(" + InfraspecificSubpattern + @")\s*<i>(?:<tn type=""lower""[^>]*>)?([a-z-]+)(?:</tn>)?</i>";
            result = Regex.Replace(result, InfraspecificPattern, @"<tn type=""lower"">$1 $2</tn>");

            return result;
        }

        private string ProcessSubgenericTaxa(string replace)
        {
            string result = replace;

            const string InfragenericPattern = @"(" + InfragenericSubpattern + @")\s*(?:<i>)?(?:<tn type=""lower""[^>]*>)?([A-Z][A-Za-z\.-]+(?:\s+[a-z\s\.-]+){0,3})(?:</tn>)?(?:</i>)?";
            result = Regex.Replace(result, InfragenericPattern, @"<tn type=""lower"">$1 $2</tn>");

            return result;
        }

        private string ProcessSensuStrictoOrLato(string content)
        {
            const string SensuPattern = @"(?:\(\s*)?(?i)(?:\bsensu\b\s*[a-z]*|s\.?\s*[ls]\.?|s\.?\s*str\.?)(?:\s*\))?";

            // Neoserica (s. l.) abnormoides, Neoserica (sensu lato) abnormis
            const string InfraspecificPattern = @"<i><tn type=""lower""[^>]*>([^<>]*?)</tn></i>\s*(" + SensuPattern + @")\s*<i>(?:<tn type=""lower""[^>]*>)?([a-z\s-]+)(?:</tn>)?</i>";
            Regex re = new Regex(InfraspecificPattern);

            var result = re.Replace(
                content,
                @"<tn type=""lower""><basionym>$1</basionym> <sensu>$2</sensu> <specific>$3</specific></tn>");

            return result;
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