﻿// <copyright file="CodesTagger.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

/*
institutional_code -> @description
specimen_code -> @institutionalCode
<institutional_code description="Australian National Insect Collection, CSIRO, Canberra City, Australia" attribute1="http://grbio.org/institution/queensland-museum">ANIC</institutional_code>
*/

namespace ProcessingTools.Services.Bio.Codes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Common.Constants.Schema;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Bio.Codes;
    using ProcessingTools.Contracts.Services.Content;
    using ProcessingTools.Contracts.Services.Models.Bio.Codes;
    using ProcessingTools.Extensions;
    using ProcessingTools.Services.Models.Bio.Codes;
    using ProcessingTools.Services.Models.Content;

    /// <summary>
    /// Codes tagger.
    /// </summary>
    public class CodesTagger : ICodesTagger
    {
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

        private readonly ICodesTransformerFactory transformerFactory;
        private readonly ITextContentHarvester contentHarvester;
        private readonly IContentTagger contentTagger;
        private readonly ILogger logger;

        private readonly string[] codePrefixes = new[]
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

        /// <summary>
        /// Initializes a new instance of the <see cref="CodesTagger"/> class.
        /// </summary>
        /// <param name="transformerFactory">Instance of <see cref="ICodesTransformerFactory"/>.</param>
        /// <param name="contentHarvester">Instance of <see cref="ITextContentHarvester"/>.</param>
        /// <param name="contentTagger">Instance of <see cref="IContentTagger"/>.</param>
        /// <param name="logger">Logger.</param>
        public CodesTagger(
            ICodesTransformerFactory transformerFactory,
            ITextContentHarvester contentHarvester,
            IContentTagger contentTagger,
            ILogger<CodesTagger> logger)
        {
            this.transformerFactory = transformerFactory ?? throw new ArgumentNullException(nameof(transformerFactory));
            this.contentHarvester = contentHarvester ?? throw new ArgumentNullException(nameof(contentHarvester));
            this.contentTagger = contentTagger ?? throw new ArgumentNullException(nameof(contentTagger));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public async Task TagKnownSpecimenCodesAsync(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var knownSpecimenCodes = new List<ISpecimenCode>();

            knownSpecimenCodes.AddRange(this.GetJanzenCodes(document));
            knownSpecimenCodes.AddRange(await this.GetPrefixNumericCodes(document).ConfigureAwait(false));

            knownSpecimenCodes = knownSpecimenCodes.Distinct().ToList();

            var tagModel = this.GetTagModel(document);

            await this.ReplaceSpecimenCodesInXml(document, XPathStrings.ContentNodes, knownSpecimenCodes, tagModel).ConfigureAwait(false);
            await this.GuessSequentalPrefixNumericSpecimenCodes(document, XPathStrings.ContentNodes, tagModel).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task TagSpecimenCodesAsync(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            const string CodePattern = @"\b[A-Z0-9](\s?[\.:\\\/–—−-]?\s?[A-Z0-9]\s?)+[A-Z0-9]\b";

            var potentialSpecimenCodes = await this.ExtractPotentialSpecimenCodes(document, CodePattern).ConfigureAwait(false);

            this.logger.LogDebug("\n\n" + potentialSpecimenCodes.Count() + " code words in article\n");
            foreach (string word in potentialSpecimenCodes)
            {
                this.logger.LogDebug(message: word);
            }

            this.logger.LogDebug(message: "\n\nPlausible specimen codes\n\n");

            var plausibleSpecimenCodes = this.GetPlausibleSpecimenCodesBasedOnInstitutionalCodes(document, potentialSpecimenCodes);

            var tagModel = this.GetTagModel(document);

            await this.ReplaceSpecimenCodesInXml(document, XPathStrings.ContentNodes, plausibleSpecimenCodes, tagModel).ConfigureAwait(false);
            await this.GuessSequentalSpecimenCodes(document, tagModel, XPathStrings.ContentNodes).ConfigureAwait(false);
        }

        private async Task<IEnumerable<string>> ExtractPotentialSpecimenCodes(IDocument document, string codePattern)
        {
            XmlDocument cleanedXmlDocument = new XmlDocument
            {
                PreserveWhitespace = true,
            };

            cleanedXmlDocument.LoadXml(document.Xml);
            cleanedXmlDocument.InnerXml = Regex.Replace(
                cleanedXmlDocument.InnerXml,
                @"(?<=</xref>)\s*:\s*" + codePattern,
                string.Empty);

            var cleanedContent = await this.transformerFactory
                .GetCodesRemoveNonCodeNodesTransformer()
                .TransformAsync(cleanedXmlDocument)
                .ConfigureAwait(false);

            cleanedXmlDocument.LoadXml(cleanedContent);

            Regex matchCodePattern = new Regex(codePattern);
            return cleanedXmlDocument.InnerText.GetMatches(matchCodePattern);
        }

        /// <summary>
        /// Gets all matches of Janzen specimen codes in the text of the XmlDocument.
        /// </summary>
        /// <param name="document">Context document.</param>
        /// <returns>ICollection of found different Janzen specimen codes.</returns>
        /// <example>
        /// Janzen codes:
        /// ```
        /// yy-SRNP-xxxxxx
        /// DHJPARxxxxxxx
        /// ```.
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
        /// <param name="document">Document to be processed.</param>
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
            string textContent = await this.contentHarvester.HarvestAsync(document.XmlDocument.DocumentElement).ConfigureAwait(false);

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

        private async Task GuessNextSpecimenCodesByRegex(IDocument document, string xpathTemplate, XmlNode tagModel, Regex guessNextCode, string xpathToSelectSpecimenCodeTags)
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

                await node.SafeReplaceInnerXmlAsync(nodeInnerXml).ConfigureAwait(false);

                if (xpathToSelectSpecimenCodeTags != null && xpathToSelectSpecimenCodeTags.Length > 0)
                {
                    foreach (XmlNode specimenCodeNode in node.SelectNodes(xpathToSelectSpecimenCodeTags, document.NamespaceManager))
                    {
                        this.SetAttributesOfSequentalSpecimenCodes(specimenCodeNode, tagModel.Name);
                    }
                }
            }
        }

        private async Task GuessSequentalPrefixNumericSpecimenCodes(IDocument document, string xpathTemplate, XmlNode tagModel)
        {
            //// <specimenCode full-string="UQIC 221451"><institutionalCode attribute1="http://grbio.org/institution/university-queensland-insect-collection">UQIC</institutionalCode> 221451</specimenCode>, 221452, 221447, 221448, 221450, 221454, 221456
            //// <specimenCode full-string="UQIC 221451">.*?</specimenCode>, 221452, 221447, 221448, 221450, 221454, 221456
            //// <specimenCode full-string="UQIC 221451">.*?</specimenCode>, <specimenCode full-string="221452">221452</specimenCode>, 221447, 221448, 221450, 221454, 221456
            //// <specimenCode full-string="UQIC 221451">.*?</specimenCode>, <specimenCode full-string="UQIC 221452">221452</specimenCode>, 221447, 221448, 221450, 221454, 221456

            string closeTag = $"</{tagModel.Name}>";
            string guessNextCodePattern = @"(?<=" + closeTag + @"(?:\W{1,5}|(?:\W*?<!--[\w\s]*?-->)+\W*))((?:\d[\/\.]?(?:<[^>]*>)*){1,20}(?:(?i)[a-z]\b)?)";
            Regex guessNextCode = new Regex(guessNextCodePattern);

            await this.GuessNextSpecimenCodesByRegex(document, xpathTemplate, tagModel, guessNextCode, ".//*[@prefix][@type='prefix-numeric']").ConfigureAwait(false);
        }

        /// <summary>
        /// Tags next specimen codes when we have some tagged ones.
        /// </summary>
        /// <param name="document">Document to be processed.</param>
        /// <param name="tagModel">The tag model.</param>
        /// <param name="xpathTemplate">XPath string template of the type "//node-to-search-in[{0}]".</param>
        /// <returns>Task.</returns>
        private async Task GuessSequentalSpecimenCodes(IDocument document, XmlNode tagModel, string xpathTemplate)
        {
            //// <specimenCode full-string="UQIC 221451"><institutionalCode attribute1="http://grbio.org/institution/university-queensland-insect-collection">UQIC</institutionalCode> 221451</specimenCode>, 221452, 221447, 221448, 221450, 221454, 221456
            //// <specimenCode full-string="UQIC 221451">.*?</specimenCode>, 221452, 221447, 221448, 221450, 221454, 221456
            //// <specimenCode full-string="UQIC 221451">.*?</specimenCode>, <specimenCode full-string="221452">221452</specimenCode>, 221447, 221448, 221450, 221454, 221456
            //// <specimenCode full-string="UQIC 221451">.*?</specimenCode>, <specimenCode full-string="UQIC 221452">221452</specimenCode>, 221447, 221448, 221450, 221454, 221456

            string closeTag = $"</{tagModel.Name}>";
            string guessNextCodePattern = "(?<=" + closeTag + @"\W{1,3})(\b[A-Z0-9](?:<[^>]*>)*(?:\s?[\.:\\\/–—−-]?\s?[A-Z0-9]\s?(?:<[^>]*>)*){1,20}[A-Z0-9]\b)";
            Regex guessNextCode = new Regex(guessNextCodePattern);

            await this.GuessNextSpecimenCodesByRegex(document, xpathTemplate, tagModel, guessNextCode, string.Format("//{0}[count(@*)!=0]", tagModel.Name)).ConfigureAwait(false);
        }

        private async Task ReplaceSpecimenCodesInXml(IDocument document, string xpathTemplate, IEnumerable<ISpecimenCode> specimenCodes, XmlNode tagModel)
        {
            foreach (var specimenCode in specimenCodes)
            {
                var codeElement = (XmlElement)tagModel.CloneNode(true);
                codeElement.SetAttribute("prefix", specimenCode.Prefix);
                codeElement.SetAttribute("type", specimenCode.Type);

                var settings = new ContentTaggerSettings
                {
                    CaseSensitive = true,
                    MinimalTextSelect = false,
                };

                await this.contentTagger.TagContentInDocumentAsync(specimenCode.Code, codeElement, xpathTemplate, document, settings).ConfigureAwait(false);
            }

            /*
             * Here we might have nested <specimen_code> which probably is due to mistaken codes.
             */
            {
                string nestedSpecimenCodesXpath = string.Format(".//{0}[{0}]", tagModel.Name);
                foreach (XmlNode nestedSpecimenCodesNode in document.SelectNodes(nestedSpecimenCodesXpath))
                {
                    this.logger.LogWarning("WARNING: Nested specimen codes: " + nestedSpecimenCodesNode.InnerXml);
                }
            }
        }

        private void SetAttributesOfSequentalSpecimenCodes(XmlNode node, string tagName)
        {
            this.logger.LogDebug("\n{0}", node.OuterXml);

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

                        this.logger.LogDebug(next.OuterXml);
                    }
                }
            }
        }
    }
}
