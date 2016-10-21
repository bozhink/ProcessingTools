/*
institutional_code -> @description
specimen_code -> @institutionalCode
<institutional_code description="Australian National Insect Collection, CSIRO, Canberra City, Australia" attribute1="http://grbio.org/institution/queensland-museum">ANIC</institutional_code>
*/

namespace ProcessingTools.BaseLibrary
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml;

    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts;
    using ProcessingTools.DocumentProvider;
    using ProcessingTools.Extensions;
    using ProcessingTools.Xml.Cache;
    using ProcessingTools.Xml.Extensions;
    using ProcessingTools.Xml.Providers;
    using ProcessingTools.Xml.Transformers;
    using System.Threading.Tasks;

    public class Codes : TaxPubDocument
    {
        private const string SpecimenCodeTagName = "specimen_code";
        private const string InstitutionTagName = "institution";
        private const string InstitutionalCodeTagName = "institutional_code";

        private readonly XmlElement specimenCodeTagModel;

        /*
         * ANSP 22529
         * B 10 0154930
         * GRA0002851-0
         * IBSS FEB RAS 7787
         * IBSS FEB RAS 7787
         * IPMB-C 13.00009
         * ISEA 001.4045, 001.4047, 001.4058
         * MHNG 7904
         * MNCN 15.05/60013H
         * MNCN 15.05/60013P
         * MNCN 15.05/7180
         * MNCN 15.05/7477P
         * MNCN15.05/60147H
         * MNCN15.05/60148H
         * MNHN #AR 13335
         * MNHN-IU-2013-9128
         * NHMUK 1991027
         * NHMW 32008
         * NMBE 534197/1
         * NMBE 534197/1
         * NMBE 534197/1; ZIN RAS 1
         * NMBE 534361/2
         * NMNZ M–075248/1
         * NMW.Z.2015.013.1a
         * NSMT Mo-76704j
         * NSMT-Mo-76703
         * RMNH.CRUS.D.2604
         * RMNH.CRUS.D.56397
         * S-G-1519
         * S08-12075
         * Z-000004443
         * ZIN RAS 1
         * ZMUC-BIV-30
         * ZUPV/EHU 188
         */

        private string[] codePrefixes = new string[]
        {
            @"ALP",
            @"AMNH",
            @"ANSP",
            @"Bau",
            @"BM",
            @"BMNH",
            @"BR",
            @"CASENT",
            @"CM",
            @"CMNML",
            @"COI",
            @"DZSJRP",
            @"FLAS",
            @"FMNH",
            @"HBG",
            @"HEID",
            @"IBSS FEB RAS",
            @"IPMB-C",
            @"IRIPP Iso\-",
            @"ISEA",
            @"LACMENT",
            @"LDM",
            @"LISC",
            @"LISU",
            @"MA",
            @"MHNG",
            @"MN",
            @"MNCN",
            @"MNHN",
            @"NHM",
            @"NHMUK",
            @"NHMW",
            @"NMBE",
            @"OSUC",
            @"PCGMK",
            @"PR",
            @"PRE",
            @"PTBG",
            @"RMNH.CRUS.D.",
            @"SMF",
            @"UFES",
            @"USNM",
            @"WAG",
            @"ZFMK",
            @"ZIN",
            @"ZIN RAS",
            @"ZMB",
            @"ZMMSU",
            @"ZMUC-BIV-",
            @"ZUPV/EHU",
            @"ZUTC Iso\.",
            @"ZUTC",
        };

        private readonly ILogger logger;

        public Codes(string xml, ILogger logger)
            : base(xml)
        {
            this.logger = logger;
            this.specimenCodeTagModel = this.XmlDocument.CreateElement(SpecimenCodeTagName);
        }

        public async Task TagSpecimenCodes()
        {
            const string CodePattern = @"\b[A-Z0-9](\s?[\.:\\\/–—−-]?\s?[A-Z0-9]\s?)+[A-Z0-9]\b";

            var potentialSpecimenCodes = this.ExtractPotentialSpecimenCodes(CodePattern);

            this.logger?.Log("\n\n" + potentialSpecimenCodes.Count() + " code words in article\n");
            foreach (string word in potentialSpecimenCodes)
            {
                this.logger?.Log(word);
            }

            this.logger?.Log("\n\nPlausible specimen codes\n\n");

            var plausibleSpecimenCodes = this.GetPlausibleSpecimenCodesBasedOnInstitutionalCodes(potentialSpecimenCodes);

            await this.ReplaceSpecimenCodesInXml(XPathConstants.SelectContentNodesXPathTemplate, plausibleSpecimenCodes, this.specimenCodeTagModel);
            await this.GuessSequentalSpecimenCodes(this.specimenCodeTagModel, XPathConstants.SelectContentNodesXPathTemplate);
        }

        public IEnumerable<ISpecimenCode> GetPrefixNumericCodes()
        {
            var prefixNumericSpecimenCodes = new List<ISpecimenCode>();
            string textContent = this.XmlDocument.GetTextContent();

            for (int i = 0, length = this.codePrefixes.Length; i < length; ++i)
            {
                string prefix = this.codePrefixes[i];
                Regex prefixNumericCodes = new Regex(@"(?i)\b(?:" + prefix + @")\W{0,3}(?:\d+(?:[\/\.]\d+)+|\d+)[a-z]?\b");
                if (prefixNumericCodes.Match(textContent).Success)
                {
                    prefixNumericSpecimenCodes.AddRange(
                        textContent
                            .GetMatches(prefixNumericCodes)
                            .Select(s => new SpecimenCode(prefix, "prefix-numeric", s)));
                }
            }

            if (prefixNumericSpecimenCodes.Count > 0)
            {
                prefixNumericSpecimenCodes = prefixNumericSpecimenCodes.Distinct().ToList();
            }

            return prefixNumericSpecimenCodes;
        }

        /// <summary>
        /// Gets all matches of Janzen specimen codes in the text of the XmlDocument.
        /// </summary>
        /// <returns>ICollection of found different Janzen specimen codes.</returns>
        /// <example>
        /// Janzen codes:
        /// yy-SRNP-xxxxxx
        /// DHJPARxxxxxxx
        /// </example>
        public List<ISpecimenCode> GetJanzenCodes()
        {
            var janzenSpecimenCodes = new List<ISpecimenCode>();

            Regex srnpCodes = new Regex(@"(?i)\b\w{1,3}\W{1,3}SRNP\W{1,3}\w{1,8}\b");
            janzenSpecimenCodes.AddRange(
                this.XmlDocument.InnerText.GetMatches(srnpCodes)
                    .Select(a => new SpecimenCode("SRNP", "Janzen", a))
                    .ToList());

            Regex dhjparCodes = new Regex(@"(?i)\bDHJPAR\w{1,8}\b");
            janzenSpecimenCodes.AddRange(
                this.XmlDocument.InnerText.GetMatches(dhjparCodes)
                    .Select(a => new SpecimenCode("DHJPAR", "Janzen", a))
                    .ToList());

            if (janzenSpecimenCodes.Count > 0)
            {
                janzenSpecimenCodes = janzenSpecimenCodes.Distinct().ToList();
            }

            return janzenSpecimenCodes;
        }

        public async Task TagKnownSpecimenCodes()
        {
            var knownSpecimenCodes = new List<ISpecimenCode>();

            knownSpecimenCodes.AddRange(this.GetJanzenCodes());
            knownSpecimenCodes.AddRange(this.GetPrefixNumericCodes());

            knownSpecimenCodes = knownSpecimenCodes.Distinct().ToList();

            await this.ReplaceSpecimenCodesInXml(XPathConstants.SelectContentNodesXPathTemplate, knownSpecimenCodes, this.specimenCodeTagModel);
            await this.GuessSequentalPrefixNumericSpecimenCodes(XPathConstants.SelectContentNodesXPathTemplate, this.specimenCodeTagModel);
        }

        private void SetAttributesOfSequentalSpecimenCodes(XmlNode node, string tagName)
        {
            this.logger?.Log("\n{0}", node.OuterXml);

            if (node.NextSibling != null)
            {
                string nodeFullString = node.Attributes["full-string"].InnerText;
                int nodeFullStringLenght = nodeFullString.Length;

                for (XmlNode next = node.NextSibling; next != null; next = next.NextSibling)
                {
                    if (next.Name == tagName)
                    {
                        if (next.Attributes.Count > 0)
                        {
                            break;
                        }

                        string nextInnerText = next.InnerText;
                        int nextInnerTextLength = nextInnerText.Length;

                        foreach (XmlAttribute attribute in node.Attributes)
                        {
                            XmlAttribute attr = (XmlAttribute)attribute.CloneNode(true);
                            if (attr.Name == "full-string")
                            {
                                if (nextInnerTextLength >= nodeFullStringLenght)
                                {
                                    // TODO
                                    attr.InnerText = nextInnerText;
                                }
                                else
                                {
                                    attr.InnerText = nodeFullString.Substring(0, nodeFullStringLenght - nextInnerTextLength) + nextInnerText;
                                }
                            }

                            next.Attributes.Append(attr);
                        }

                        this.logger?.Log(next.OuterXml);
                    }
                }
            }
        }

        private IEnumerable<string> ExtractPotentialSpecimenCodes(string codePattern)
        {
            XmlDocument cleanedXmlDocument = new XmlDocument();
            cleanedXmlDocument.PreserveWhitespace = true;
            cleanedXmlDocument.LoadXml(this.Xml);
            cleanedXmlDocument.InnerXml = Regex.Replace(
                cleanedXmlDocument.InnerXml,
                @"(?<=</xref>)\s*:\s*" + codePattern,
                string.Empty);

            // TODO: DI
            var transformer = new XslTransformer(new CodesRemoveNonCodeNodesXslTransformProvider(new XslTransformCache()));

            // TODO: async, DI
            cleanedXmlDocument.LoadXml(transformer.Transform(cleanedXmlDocument).Result);

            Regex matchCodePattern = new Regex(codePattern);
            return cleanedXmlDocument.InnerText.GetMatches(matchCodePattern);
        }

        /// <summary>
        /// Gets all plausible specimen codes which contains a used in the article institutional code.
        /// </summary>
        /// <param name="potentialSpecimenCodes">The list of potential specimen codes.</param>
        /// <returns>Filtered list of plausible specimen codes.</returns>
        private IEnumerable<ISpecimenCode> GetPlausibleSpecimenCodesBasedOnInstitutionalCodes(IEnumerable<string> potentialSpecimenCodes)
        {
            var result = new List<ISpecimenCode>();

            var institutionalCodesXmlNodes = this.XmlDocument.SelectNodes(".//institutional_code", this.NamespaceManager);

            var institutionalCodes = institutionalCodesXmlNodes.GetStringListOfUniqueXmlNodeContent();
            foreach (string institutionalCode in institutionalCodes)
            {
                /*
                 * Here we believe that institutional codes are not specimen codes.
                 */
                string pattern = @"\A" + institutionalCode + @"(\s?[\.:\\\/–—−-]?\s?[A-Z0-9]\s?)+[A-Z0-9]\b";
                Regex matchPattern = new Regex(pattern);
                var codeWordsList = potentialSpecimenCodes.Where(word => matchPattern.IsMatch(word))
                    .Select(word => new SpecimenCode(institutionalCode, "plausible", word))
                    .ToList();

                if (codeWordsList != null && codeWordsList.Count > 0)
                {
                    result.AddRange(codeWordsList);
                }
            }

            return new HashSet<ISpecimenCode>(result);
        }

        private async Task ReplaceSpecimenCodesInXml(string xpathTemplate, IEnumerable<ISpecimenCode> specimenCodes, XmlElement tagModel)
        {
            foreach (var specimenCode in specimenCodes)
            {
                var codeElement = (XmlElement)tagModel.CloneNode(true);
                codeElement.SetAttribute("prefix", specimenCode.Prefix);
                codeElement.SetAttribute("type", specimenCode.Type);

                await specimenCode.Code.TagContentInDocument(codeElement, xpathTemplate, this, true, false, this.logger);
            }

            /*
             * Here we might have nested <specimen_code> which probably is due to mistaken codes.
             */
            {
                string nestedSpecimenCodesXpath = string.Format("//{0}[{0}]", tagModel.Name);
                foreach (XmlNode nestedSpecimenCodesNode in this.XmlDocument.SelectNodes(nestedSpecimenCodesXpath, this.NamespaceManager))
                {
                    this.logger?.Log("WARNING: Nested specimen codes: " + nestedSpecimenCodesNode.InnerXml);
                }
            }
        }

        private async Task GuessSequentalPrefixNumericSpecimenCodes(string xpathTemplate, XmlElement tagModel)
        {
            //// <specimenCode full-string="UQIC 221451"><institutionalCode attribute1="http://grbio.org/institution/university-queensland-insect-collection">UQIC</institutionalCode> 221451</specimenCode>, 221452, 221447, 221448, 221450, 221454, 221456
            //// <specimenCode full-string="UQIC 221451">.*?</specimenCode>, 221452, 221447, 221448, 221450, 221454, 221456
            //// <specimenCode full-string="UQIC 221451">.*?</specimenCode>, <specimenCode full-string="221452">221452</specimenCode>, 221447, 221448, 221450, 221454, 221456
            //// <specimenCode full-string="UQIC 221451">.*?</specimenCode>, <specimenCode full-string="UQIC 221452">221452</specimenCode>, 221447, 221448, 221450, 221454, 221456

            string closeTag = $"</{tagModel.Name}>";
            string guessNextCodePattern = @"(?<=" + closeTag + @"(?:\W{1,5}|(?:\W*?<!--[\w\s]*?-->)+\W*))((?:\d[\/\.]?(?:<[^>]*>)*){1,20}(?:(?i)[a-z]\b)?)";
            Regex guessNextCode = new Regex(guessNextCodePattern);

            await this.GuessNextSpecimenCodesByRegex(xpathTemplate, tagModel, guessNextCode, ".//*[@prefix][@type='prefix-numeric']");
        }

        /// <summary>
        /// Tags next specimen codes when we have some tagged ones.
        /// </summary>
        /// <param name="tagModel">The tag model.</param>
        /// <param name="xpathTemplate">XPath string template of the type "//node-to-search-in[{0}]".</param>
        private async Task GuessSequentalSpecimenCodes(XmlElement tagModel, string xpathTemplate)
        {
            //// <specimenCode full-string="UQIC 221451"><institutionalCode attribute1="http://grbio.org/institution/university-queensland-insect-collection">UQIC</institutionalCode> 221451</specimenCode>, 221452, 221447, 221448, 221450, 221454, 221456
            //// <specimenCode full-string="UQIC 221451">.*?</specimenCode>, 221452, 221447, 221448, 221450, 221454, 221456
            //// <specimenCode full-string="UQIC 221451">.*?</specimenCode>, <specimenCode full-string="221452">221452</specimenCode>, 221447, 221448, 221450, 221454, 221456
            //// <specimenCode full-string="UQIC 221451">.*?</specimenCode>, <specimenCode full-string="UQIC 221452">221452</specimenCode>, 221447, 221448, 221450, 221454, 221456

            string closeTag = $"</{tagModel.Name}>";
            string guessNextCodePattern = "(?<=" + closeTag + @"\W{1,3})(\b[A-Z0-9](?:<[^>]*>)*(?:\s?[\.:\\\/–—−-]?\s?[A-Z0-9]\s?(?:<[^>]*>)*){1,20}[A-Z0-9]\b)";
            Regex guessNextCode = new Regex(guessNextCodePattern);

            await this.GuessNextSpecimenCodesByRegex(xpathTemplate, tagModel, guessNextCode, string.Format("//{0}[count(@*)!=0]", tagModel.Name));
        }

        private async Task GuessNextSpecimenCodesByRegex(string xpathTemplate, XmlElement tagModel, Regex guessNextCode, string xpathToSelectSpecimenCodeTags)
        {
            XmlNode replacementNode = tagModel.CloneNode(true);
            replacementNode.InnerText = "$1";
            string replacement = replacementNode.OuterXml;

            string xpath = string.Format(xpathTemplate, tagModel.Name);
            foreach (XmlNode node in this.XmlDocument.SelectNodes(xpath, this.NamespaceManager))
            {
                string nodeInnerXml = node.InnerXml;
                while (guessNextCode.Match(nodeInnerXml).Success)
                {
                    nodeInnerXml = guessNextCode.Replace(nodeInnerXml, replacement);
                }

                await node.SafeReplaceInnerXml(nodeInnerXml, this.logger);

                if (xpathToSelectSpecimenCodeTags != null && xpathToSelectSpecimenCodeTags.Length > 0)
                {
                    foreach (XmlNode specimenCodeNode in node.SelectNodes(xpathToSelectSpecimenCodeTags, this.NamespaceManager))
                    {
                        this.SetAttributesOfSequentalSpecimenCodes(specimenCodeNode, tagModel.Name);
                    }
                }
            }
        }
    }
}
