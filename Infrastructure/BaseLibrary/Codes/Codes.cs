﻿/*
institutional_code -> @description
specimen_code -> @institutionalCode
<institutional_code description="Australian National Insect Collection, CSIRO, Canberra City, Australia" attribute1="http://grbio.org/institution/queensland-museum">ANIC</institutional_code>
*/

namespace ProcessingTools.BaseLibrary
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml;

    using Contracts;
    using ProcessingTools.Common;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts;
    using ProcessingTools.DocumentProvider;
    using ProcessingTools.Extensions;
    using ProcessingTools.Xml.Extensions;

    public class Codes : TaxPubDocument
    {
        private const string CodesRemoveNonCodeNodesXslPathKey = "CodesRemoveNonCodeNodesXslPath";
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

        private ILogger logger;

        public Codes(string xml, ILogger logger)
            : base(xml)
        {
            this.logger = logger;
            this.specimenCodeTagModel = this.XmlDocument.CreateElement(SpecimenCodeTagName);
        }

        private static string CodesRemoveNonCodeNodesXslPath => Dictionaries.FileNames.GetOrAdd(CodesRemoveNonCodeNodesXslPathKey, ConfigurationManager.AppSettings[CodesRemoveNonCodeNodesXslPathKey]);

        public void TagInstitutions(IDataProvider dataProvider)
        {
            // WARNING: here is set len(name) > 1!
            string query = @"select [NameOfInstitution] as [name], [Url] as [url] from [grbio].[Biorepositories] where len([NameOfInstitution]) > 1 order by len([NameOfInstitution]) desc;";

            dataProvider.Xml = this.Xml;
            dataProvider.ExecuteSimpleReplaceUsingDatabase(XPathConstants.SelectContentNodesXPath, query, InstitutionTagName);
            this.Xml = dataProvider.Xml;
        }

        public void TagInstitutionalCodes(IDataProvider dataProvider)
        {
            // WARNING: here is set len(name) > 1!
            string query = @"select [InstitutionalCode] as [institutional_code], [NameOfInstitution] as [description], [Url] as [url] from [grbio].[Biorepositories] where len([InstitutionalCode]) > 1 order by len([InstitutionalCode]) desc;";

            dataProvider.Xml = this.Xml;
            dataProvider.ExecuteSimpleReplaceUsingDatabase(XPathConstants.SelectContentNodesXPath, query, InstitutionalCodeTagName, true);
            this.Xml = dataProvider.Xml;
        }

        public void TagSpecimenCodes()
        {
            const string CodePattern = @"\b[A-Z0-9](\s?[\.:\\\/–—−-]?\s?[A-Z0-9]\s?)+[A-Z0-9]\b";

            IEnumerable<string> potentialSpecimenCodes = this.ExtractPotentialSpecimenCodes(CodePattern);

            this.logger?.Log("\n\n" + potentialSpecimenCodes.Count() + " code words in article\n");

            foreach (string word in potentialSpecimenCodes)
            {
                this.logger?.Log(word);
            }

            this.logger?.Log("\n\nPlausible specimen codes\n\n");

            IEnumerable<SpecimenCode> plausibleSpecimenCodes = this.GetPlausibleSpecimenCodesBasedOnInstitutionalCodes(potentialSpecimenCodes);

            this.ReplaceSpecimenCodesInXml(XPathConstants.SelectContentNodesXPathTemplate, plausibleSpecimenCodes, this.specimenCodeTagModel);

            this.GuessSequentalSpecimenCodes(this.specimenCodeTagModel, XPathConstants.SelectContentNodesXPathTemplate);
        }

        public List<SpecimenCode> GetPrefixNumericCodes()
        {
            List<SpecimenCode> prefixNumericSpecimenCodes = new List<SpecimenCode>();
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
        /// <returns>ICollection of found different Janzen speciment codes.</returns>
        /// <example>
        /// Janzen codes:
        /// yy-SRNP-xxxxxx
        /// DHJPARxxxxxxx
        /// </example>
        public List<SpecimenCode> GetJanzenCodes()
        {
            List<SpecimenCode> janzenSpecimenCodes = new List<SpecimenCode>();

            Regex srnpCodes = new Regex(@"(?i)\b\w{1,3}\W{1,3}SRNP\W{1,3}\w{1,8}\b");
            janzenSpecimenCodes.AddRange(
                this.XmlDocument.InnerText.GetMatches(srnpCodes)
                    .Select(a => new SpecimenCode("SRNP", "Janzen", a))
                    .ToList<SpecimenCode>());

            Regex dhjparCodes = new Regex(@"(?i)\bDHJPAR\w{1,8}\b");
            janzenSpecimenCodes.AddRange(
                this.XmlDocument.InnerText.GetMatches(dhjparCodes)
                    .Select(a => new SpecimenCode("DHJPAR", "Janzen", a))
                    .ToList<SpecimenCode>());

            if (janzenSpecimenCodes.Count > 0)
            {
                janzenSpecimenCodes = janzenSpecimenCodes.Distinct().ToList();
            }

            return janzenSpecimenCodes;
        }

        public void TagKnownSpecimenCodes()
        {
            List<SpecimenCode> knownSpecimenCodes = new List<SpecimenCode>();

            knownSpecimenCodes.AddRange(this.GetJanzenCodes());
            knownSpecimenCodes.AddRange(this.GetPrefixNumericCodes());

            knownSpecimenCodes = knownSpecimenCodes.Distinct().ToList();

            this.ReplaceSpecimenCodesInXml(XPathConstants.SelectContentNodesXPathTemplate, knownSpecimenCodes, this.specimenCodeTagModel);

            this.GuessSequentalPrefixNumericSpecimenCodes(XPathConstants.SelectContentNodesXPathTemplate, this.specimenCodeTagModel);
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
            cleanedXmlDocument.LoadXml(
                cleanedXmlDocument.ApplyXslTransform(CodesRemoveNonCodeNodesXslPath));

            Regex matchCodePattern = new Regex(codePattern);
            return cleanedXmlDocument.InnerText.GetMatches(matchCodePattern);
        }

        /// <summary>
        /// Gets all plausible specimen codes which contains a used in the article instirurional code.
        /// </summary>
        /// <param name="potentialSpecimenCodes">The list of potential specimen codes.</param>
        /// <returns>Filtered list of plausible specimen codes.</returns>
        private IEnumerable<SpecimenCode> GetPlausibleSpecimenCodesBasedOnInstitutionalCodes(IEnumerable<string> potentialSpecimenCodes)
        {
            List<SpecimenCode> result = new List<SpecimenCode>();

            XmlNodeList institutionalCodesXmlNodes = this.XmlDocument.SelectNodes("//institutional_code", this.NamespaceManager);

            IEnumerable<string> institutionalCodes = institutionalCodesXmlNodes.GetStringListOfUniqueXmlNodeContent();
            foreach (string institutionalCode in institutionalCodes)
            {
                /*
                 * Here we believe that instituional codes are not specimen codes.
                 */
                string pattern = @"\A" + institutionalCode + @"(\s?[\.:\\\/–—−-]?\s?[A-Z0-9]\s?)+[A-Z0-9]\b";
                Regex matchPattern = new Regex(pattern);
                List<SpecimenCode> codeWordsList = (from word in potentialSpecimenCodes
                                                    where matchPattern.Match(word).Success
                                                    select word)
                                                    .Select(s => new SpecimenCode(institutionalCode, "plausible", s))
                                                    .ToList();
                if (codeWordsList != null)
                {
                    result.AddRange(codeWordsList);
                }
            }

            return result.Distinct();
        }
        
        private void ReplaceSpecimenCodesInXml(
            string xpathTemplate,
            IEnumerable<SpecimenCode> specimenCodes,
            XmlElement tagModel)
        {
            foreach (SpecimenCode specimenCode in specimenCodes)
            {
                XmlElement codeTag = (XmlElement)tagModel.CloneNode(true);
                codeTag.SetAttribute("prefix", specimenCode.Prefix);
                codeTag.SetAttribute("type", specimenCode.Type);

                specimenCode.Code
                    .TagContentInDocument(codeTag, xpathTemplate, this.NamespaceManager, this.XmlDocument, true, false, this.logger)
                    .Wait();
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

        private void GuessSequentalPrefixNumericSpecimenCodes(string xpathTemplate, XmlElement tagModel)
        {
            //// <specimenCode full-string="UQIC 221451"><institutionalCode attribute1="http://grbio.org/institution/university-queensland-insect-collection">UQIC</institutionalCode> 221451</specimenCode>, 221452, 221447, 221448, 221450, 221454, 221456
            //// <specimenCode full-string="UQIC 221451">.*?</specimenCode>, 221452, 221447, 221448, 221450, 221454, 221456
            //// <specimenCode full-string="UQIC 221451">.*?</specimenCode>, <specimenCode full-string="221452">221452</specimenCode>, 221447, 221448, 221450, 221454, 221456
            //// <specimenCode full-string="UQIC 221451">.*?</specimenCode>, <specimenCode full-string="UQIC 221452">221452</specimenCode>, 221447, 221448, 221450, 221454, 221456

            string closeTag = $"</{tagModel.Name}>";
            string guessNextCodePattern = @"(?<=" + closeTag + @"(?:\W{1,5}|(?:\W*?<!--[\w\s]*?-->)+\W*))((?:\d[\/\.]?(?:<[^>]*>)*){1,20}(?:(?i)[a-z]\b)?)";
            Regex guessNextCode = new Regex(guessNextCodePattern);

            this.GuessNextSpecimenCodesByRegex(xpathTemplate, tagModel, guessNextCode, ".//*[@prefix][@type='prefix-numeric']");
        }

        /// <summary>
        /// Tags next specimen codes when we have some tagged ones.
        /// </summary>
        /// <param name="tagModel">The tag model.</param>
        /// <param name="xpathTemplate">XPath string template of the type "//node-to-search-in[{0}]".</param>
        private void GuessSequentalSpecimenCodes(XmlElement tagModel, string xpathTemplate)
        {
            //// <specimenCode full-string="UQIC 221451"><institutionalCode attribute1="http://grbio.org/institution/university-queensland-insect-collection">UQIC</institutionalCode> 221451</specimenCode>, 221452, 221447, 221448, 221450, 221454, 221456
            //// <specimenCode full-string="UQIC 221451">.*?</specimenCode>, 221452, 221447, 221448, 221450, 221454, 221456
            //// <specimenCode full-string="UQIC 221451">.*?</specimenCode>, <specimenCode full-string="221452">221452</specimenCode>, 221447, 221448, 221450, 221454, 221456
            //// <specimenCode full-string="UQIC 221451">.*?</specimenCode>, <specimenCode full-string="UQIC 221452">221452</specimenCode>, 221447, 221448, 221450, 221454, 221456

            string closeTag = $"</{tagModel.Name}>";
            string guessNextCodePattern = "(?<=" + closeTag + @"\W{1,3})(\b[A-Z0-9](?:<[^>]*>)*(?:\s?[\.:\\\/–—−-]?\s?[A-Z0-9]\s?(?:<[^>]*>)*){1,20}[A-Z0-9]\b)";
            Regex guessNextCode = new Regex(guessNextCodePattern);

            this.GuessNextSpecimenCodesByRegex(xpathTemplate, tagModel, guessNextCode, string.Format("//{0}[count(@*)!=0]", tagModel.Name));
        }

        private void GuessNextSpecimenCodesByRegex(string xpathTemplate, XmlElement tagModel, Regex guessNextCode, string xpathToSelectSpecimenCodeTags)
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

                node.SafeReplaceInnerXml(nodeInnerXml, this.logger).Wait();

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
