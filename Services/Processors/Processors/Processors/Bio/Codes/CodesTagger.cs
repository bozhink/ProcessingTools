/*
institutional_code -> @description
specimen_code -> @institutionalCode
<institutional_code description="Australian National Insect Collection, CSIRO, Canberra City, Australia" attribute1="http://grbio.org/institution/queensland-museum">ANIC</institutional_code>
*/

namespace ProcessingTools.Processors.Processors.Bio.Codes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using Contracts.Factories.Bio;
    using Contracts.Models.Bio.Codes;
    using Contracts.Processors.Bio.Codes;
    using Models.Bio.Codes;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Extensions;
    using ProcessingTools.Harvesters.Contracts.Harvesters.Content;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;
    using ProcessingTools.Layout.Processors.Models.Taggers;
    using ProcessingTools.Xml.Extensions;

    public class CodesTagger : ICodesTagger
    {
        private const string InstitutionalCodeTagName = "institutional_code";
        private const string InstitutionTagName = "institution";
        private const string SpecimenCodeTagName = "specimen_code";

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

        private readonly ICodesTransformersFactory transformerFactory;
        private readonly ITextContentHarvester contentHarvester;
        private readonly IContentTagger contentTagger;
        private readonly ILogger logger;

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

        public CodesTagger(
            ICodesTransformersFactory transformerFactory,
            ITextContentHarvester contentHarvester,
            IContentTagger contentTagger,
            ILogger logger)
        {
            if (transformerFactory == null)
            {
                throw new ArgumentNullException(nameof(transformerFactory));
            }

            if (contentHarvester == null)
            {
                throw new ArgumentNullException(nameof(contentHarvester));
            }

            if (contentTagger == null)
            {
                throw new ArgumentNullException(nameof(contentTagger));
            }

            this.transformerFactory = transformerFactory;
            this.contentHarvester = contentHarvester;
            this.contentTagger = contentTagger;
            this.logger = logger;
        }

        public async Task TagKnownSpecimenCodes(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var knownSpecimenCodes = new List<ISpecimenCode>();

            knownSpecimenCodes.AddRange(this.GetJanzenCodes(document));
            knownSpecimenCodes.AddRange(await this.GetPrefixNumericCodes(document));

            knownSpecimenCodes = knownSpecimenCodes.Distinct().ToList();

            var tagModel = this.GetTagModel(document);

            await this.ReplaceSpecimenCodesInXml(document, XPathStrings.ContentNodes, knownSpecimenCodes, tagModel);
            await this.GuessSequentalPrefixNumericSpecimenCodes(document, XPathStrings.ContentNodes, tagModel);
        }

        public async Task TagSpecimenCodes(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            const string CodePattern = @"\b[A-Z0-9](\s?[\.:\\\/–—−-]?\s?[A-Z0-9]\s?)+[A-Z0-9]\b";

            var potentialSpecimenCodes = await this.ExtractPotentialSpecimenCodes(document, CodePattern);

            this.logger?.Log("\n\n" + potentialSpecimenCodes.Count() + " code words in article\n");
            foreach (string word in potentialSpecimenCodes)
            {
                this.logger?.Log(word);
            }

            this.logger?.Log("\n\nPlausible specimen codes\n\n");

            var plausibleSpecimenCodes = this.GetPlausibleSpecimenCodesBasedOnInstitutionalCodes(document, potentialSpecimenCodes);

            var tagModel = this.GetTagModel(document);

            await this.ReplaceSpecimenCodesInXml(document, XPathStrings.ContentNodes, plausibleSpecimenCodes, tagModel);
            await this.GuessSequentalSpecimenCodes(document, tagModel, XPathStrings.ContentNodes);
        }

        private async Task<IEnumerable<string>> ExtractPotentialSpecimenCodes(IDocument document, string codePattern)
        {
            XmlDocument cleanedXmlDocument = new XmlDocument();
            cleanedXmlDocument.PreserveWhitespace = true;
            cleanedXmlDocument.LoadXml(document.Xml);
            cleanedXmlDocument.InnerXml = Regex.Replace(
                cleanedXmlDocument.InnerXml,
                @"(?<=</xref>)\s*:\s*" + codePattern,
                string.Empty);

            var cleanedContent = await this.transformerFactory
                .GetCodesRemoveNonCodeNodesTransformer()
                .Transform(cleanedXmlDocument);

            cleanedXmlDocument.LoadXml(cleanedContent);

            Regex matchCodePattern = new Regex(codePattern);
            return cleanedXmlDocument.InnerText.GetMatches(matchCodePattern);
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
        private IEnumerable<ISpecimenCode> GetJanzenCodes(IDocument document)
        {
            var janzenSpecimenCodes = new List<ISpecimenCode>();

            Regex srnpCodes = new Regex(@"(?i)\b\w{1,3}\W{1,3}SRNP\W{1,3}\w{1,8}\b");
            janzenSpecimenCodes.AddRange(
                document.XmlDocument.InnerText.GetMatches(srnpCodes)
                    .Select(a => new SpecimenCode("SRNP", "Janzen", a))
                    .ToList());

            Regex dhjparCodes = new Regex(@"(?i)\bDHJPAR\w{1,8}\b");
            janzenSpecimenCodes.AddRange(
                document.XmlDocument.InnerText.GetMatches(dhjparCodes)
                    .Select(a => new SpecimenCode("DHJPAR", "Janzen", a))
                    .ToList());

            if (janzenSpecimenCodes.Count > 0)
            {
                janzenSpecimenCodes = janzenSpecimenCodes.Distinct().ToList();
            }

            return janzenSpecimenCodes;
        }

        /// <summary>
        /// Gets all plausible specimen codes which contains a used in the article institutional code.
        /// </summary>
        /// <param name="potentialSpecimenCodes">The list of potential specimen codes.</param>
        /// <returns>Filtered list of plausible specimen codes.</returns>
        private IEnumerable<ISpecimenCode> GetPlausibleSpecimenCodesBasedOnInstitutionalCodes(IDocument document, IEnumerable<string> potentialSpecimenCodes)
        {
            var result = new List<ISpecimenCode>();

            var institutionalCodes = document.SelectNodes(".//institutional_code")
                .Select(c => c.InnerText)
                .Distinct()
                .ToList();

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

        private async Task<IEnumerable<ISpecimenCode>> GetPrefixNumericCodes(IDocument document)
        {
            var prefixNumericSpecimenCodes = new List<ISpecimenCode>();
            string textContent = await this.contentHarvester.Harvest(document.XmlDocument.DocumentElement);

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

        private XmlElement GetTagModel(IDocument document) => document.XmlDocument.CreateElement(SpecimenCodeTagName);

        private async Task GuessNextSpecimenCodesByRegex(IDocument document, string xpathTemplate, XmlElement tagModel, Regex guessNextCode, string xpathToSelectSpecimenCodeTags)
        {
            XmlNode replacementNode = tagModel.CloneNode(true);
            replacementNode.InnerText = "$1";
            string replacement = replacementNode.OuterXml;

            string xpath = string.Format(xpathTemplate, tagModel.Name);
            foreach (XmlNode node in document.SelectNodes(xpath))
            {
                string nodeInnerXml = node.InnerXml;
                while (guessNextCode.Match(nodeInnerXml).Success)
                {
                    nodeInnerXml = guessNextCode.Replace(nodeInnerXml, replacement);
                }

                await node.SafeReplaceInnerXml(nodeInnerXml, this.logger);

                if (xpathToSelectSpecimenCodeTags != null && xpathToSelectSpecimenCodeTags.Length > 0)
                {
                    foreach (XmlNode specimenCodeNode in node.SelectNodes(xpathToSelectSpecimenCodeTags, document.NamespaceManager))
                    {
                        this.SetAttributesOfSequentalSpecimenCodes(specimenCodeNode, tagModel.Name);
                    }
                }
            }
        }

        private async Task GuessSequentalPrefixNumericSpecimenCodes(IDocument document, string xpathTemplate, XmlElement tagModel)
        {
            //// <specimenCode full-string="UQIC 221451"><institutionalCode attribute1="http://grbio.org/institution/university-queensland-insect-collection">UQIC</institutionalCode> 221451</specimenCode>, 221452, 221447, 221448, 221450, 221454, 221456
            //// <specimenCode full-string="UQIC 221451">.*?</specimenCode>, 221452, 221447, 221448, 221450, 221454, 221456
            //// <specimenCode full-string="UQIC 221451">.*?</specimenCode>, <specimenCode full-string="221452">221452</specimenCode>, 221447, 221448, 221450, 221454, 221456
            //// <specimenCode full-string="UQIC 221451">.*?</specimenCode>, <specimenCode full-string="UQIC 221452">221452</specimenCode>, 221447, 221448, 221450, 221454, 221456

            string closeTag = $"</{tagModel.Name}>";
            string guessNextCodePattern = @"(?<=" + closeTag + @"(?:\W{1,5}|(?:\W*?<!--[\w\s]*?-->)+\W*))((?:\d[\/\.]?(?:<[^>]*>)*){1,20}(?:(?i)[a-z]\b)?)";
            Regex guessNextCode = new Regex(guessNextCodePattern);

            await this.GuessNextSpecimenCodesByRegex(document, xpathTemplate, tagModel, guessNextCode, ".//*[@prefix][@type='prefix-numeric']");
        }

        /// <summary>
        /// Tags next specimen codes when we have some tagged ones.
        /// </summary>
        /// <param name="tagModel">The tag model.</param>
        /// <param name="xpathTemplate">XPath string template of the type "//node-to-search-in[{0}]".</param>
        private async Task GuessSequentalSpecimenCodes(IDocument document, XmlElement tagModel, string xpathTemplate)
        {
            //// <specimenCode full-string="UQIC 221451"><institutionalCode attribute1="http://grbio.org/institution/university-queensland-insect-collection">UQIC</institutionalCode> 221451</specimenCode>, 221452, 221447, 221448, 221450, 221454, 221456
            //// <specimenCode full-string="UQIC 221451">.*?</specimenCode>, 221452, 221447, 221448, 221450, 221454, 221456
            //// <specimenCode full-string="UQIC 221451">.*?</specimenCode>, <specimenCode full-string="221452">221452</specimenCode>, 221447, 221448, 221450, 221454, 221456
            //// <specimenCode full-string="UQIC 221451">.*?</specimenCode>, <specimenCode full-string="UQIC 221452">221452</specimenCode>, 221447, 221448, 221450, 221454, 221456

            string closeTag = $"</{tagModel.Name}>";
            string guessNextCodePattern = "(?<=" + closeTag + @"\W{1,3})(\b[A-Z0-9](?:<[^>]*>)*(?:\s?[\.:\\\/–—−-]?\s?[A-Z0-9]\s?(?:<[^>]*>)*){1,20}[A-Z0-9]\b)";
            Regex guessNextCode = new Regex(guessNextCodePattern);

            await this.GuessNextSpecimenCodesByRegex(document, xpathTemplate, tagModel, guessNextCode, string.Format("//{0}[count(@*)!=0]", tagModel.Name));
        }

        private async Task ReplaceSpecimenCodesInXml(IDocument document, string xpathTemplate, IEnumerable<ISpecimenCode> specimenCodes, XmlElement tagModel)
        {
            foreach (var specimenCode in specimenCodes)
            {
                var codeElement = (XmlElement)tagModel.CloneNode(true);
                codeElement.SetAttribute("prefix", specimenCode.Prefix);
                codeElement.SetAttribute("type", specimenCode.Type);

                var settings = new ContentTaggerSettings
                {
                    CaseSensitive = true,
                    MinimalTextSelect = false
                };

                await this.contentTagger.TagContentInDocument(specimenCode.Code, codeElement, xpathTemplate, document, settings);
            }

            /*
             * Here we might have nested <specimen_code> which probably is due to mistaken codes.
             */
            {
                string nestedSpecimenCodesXpath = string.Format(".//{0}[{0}]", tagModel.Name);
                foreach (XmlNode nestedSpecimenCodesNode in document.SelectNodes(nestedSpecimenCodesXpath))
                {
                    this.logger?.Log("WARNING: Nested specimen codes: " + nestedSpecimenCodesNode.InnerXml);
                }
            }
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
    }
}
