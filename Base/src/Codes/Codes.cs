using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;


// institutional_code -> @description
// specimen_code -> @institutionalCode
// <institutional_code description="Australian National Insect Collection, CSIRO, Canberra City, Australia" attribute1="http://grbio.org/institution/queensland-museum">ANIC</institutional_code>


namespace ProcessingTools.Base
{
    public class Codes : TaggerBase
    {
        private TagContent specimenCodeTag = new TagContent("specimen_code");
        private TagContent institutionTag = new TagContent("institution");
        private TagContent institutionalCodeTag = new TagContent("institutional_code");

        public Codes(Config config, string xml)
            : base(config, xml)
        {
        }

        public Codes(TaggerBase baseObject)
            : base(baseObject)
        {
        }

        public void TagInstitutions(IXPathProvider xpathProvider, IDataProvider dataProvider)
        {
            string query = @"select [Name] from [dbo].[institutions] order by len([Name]) desc;";

            dataProvider.Xml = this.Xml;
            dataProvider.ExecuteSimpleReplaceUsingDatabase(xpathProvider.SelectContentNodesXPath, query, this.institutionTag);
            this.Xml = dataProvider.Xml;

            // WARNING: here is set len(name) > 1!
            query = @"select [NameOfInstitution], [URL] from [dbo].[biorepositories] where len([NameOfInstitution]) > 1 order by len([NameOfInstitution]) desc;";

            dataProvider.Xml = this.Xml;
            dataProvider.ExecuteSimpleReplaceUsingDatabase(xpathProvider.SelectContentNodesXPath, query, this.institutionTag);
            this.Xml = dataProvider.Xml;
        }

        public void TagInstitutionalCodes(IXPathProvider xpathProvider, IDataProvider dataProvider)
        {
            // WARNING: here is set len(name) > 1!
            string query = @"select [InstitutionalCode], [NameOfInstitution], [URL] from [dbo].[biorepositories] where len([InstitutionalCode]) > 1 order by len([InstitutionalCode]) desc;";

            dataProvider.Xml = this.Xml;
            dataProvider.ExecuteSimpleReplaceUsingDatabase(xpathProvider.SelectContentNodesXPath, query, this.institutionalCodeTag, true);
            this.Xml = dataProvider.Xml;
        }

        public void TagSpecimenCodes(IXPathProvider xpathProvider)
        {
            const string CodePattern = @"\b[A-Z0-9](\s?[\.:\\\/–—−-]?\s?[A-Z0-9]\s?)+[A-Z0-9]\b";

            List<string> potentialSpecimenCodes = ExtractPotentialSpecimenCodes(CodePattern);

            Alert.Log("\n\n" + potentialSpecimenCodes.Count + " code words in article\n");
            foreach (string word in potentialSpecimenCodes)
            {
                Alert.Log(word);
            }

            Alert.Log("\n\nPlausible specimen codes\n\n");

            List<SpecimenCode> plausibleSpecimenCodes = GetPlausibleSpecimenCodesBasedOnInstitutionalCodes(potentialSpecimenCodes);

            ReplaceSpecimenCodesInXml(xpathProvider.SelectContentNodesXPathTemplate, plausibleSpecimenCodes, this.specimenCodeTag);

            GuessSequentalSpecimenCodes(this.specimenCodeTag, xpathProvider.SelectContentNodesXPathTemplate);
        }

        private List<string> ExtractPotentialSpecimenCodes(string codePattern)
        {
            XmlDocument cleanedXmlDocument = new XmlDocument();
            cleanedXmlDocument.PreserveWhitespace = true;
            cleanedXmlDocument.LoadXml(this.Xml);
            cleanedXmlDocument.InnerXml = Regex.Replace(
                cleanedXmlDocument.InnerXml,
                @"(?<=</xref>)\s*:\s*" + codePattern,
                string.Empty);
            cleanedXmlDocument.LoadXml(
                cleanedXmlDocument.ApplyXslTransform(this.Config.codesRemoveNonCodeNodes));

            Regex matchCodePattern = new Regex(codePattern);
            return cleanedXmlDocument.InnerText.GetMatchesInText(matchCodePattern, true);
        }

        /// <summary>
        /// Gets all plausible specimen codes which contains a used in the article instirurional code.
        /// </summary>
        /// <param name="potentialSpecimenCodes">The list of potential specimen codes.</param>
        /// <returns>Filtered list of plausible specimen codes.</returns>
        private List<SpecimenCode> GetPlausibleSpecimenCodesBasedOnInstitutionalCodes(List<string> potentialSpecimenCodes)
        {
            List<SpecimenCode> result = new List<SpecimenCode>();

            XmlNodeList institutionalCodesXmlNodes = this.XmlDocument.SelectNodes("//institutionalCode", this.NamespaceManager);
            List<string> institutionalCodes = institutionalCodesXmlNodes.GetStringListOfUniqueXmlNodeContent();
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

            result = result.Distinct().ToList();
            return result;
        }

        /*
         * *********************************************************************************************************************************
         *
         * Tag known
         *
         * *********************************************************************************************************************************
         */
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

        private static string[] codePrefixes = new string[]
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

        public List<SpecimenCode> GetPrefixNumericCodes()
        {
            List<SpecimenCode> prefixNumericSpecimenCodes = new List<SpecimenCode>();
            string textContent = this.TextContent;

            for (int i = 0, length = codePrefixes.Length; i < length; ++i)
            {
                string prefix = codePrefixes[i];
                Regex prefixNumericCodes = new Regex(@"(?i)\b(?:" + prefix + @")\W{0,3}\d{2,}(?:[\/\.]?\d+)*\b");
                if (prefixNumericCodes.Match(textContent).Success)
                {
                    prefixNumericSpecimenCodes.AddRange(
                        textContent
                            .GetMatchesInText(prefixNumericCodes, true)
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
                this.XmlDocument.GetMatchesInXmlText(srnpCodes, false)
                    .Select(a => new SpecimenCode("SRNP", "Janzen", a))
                    .ToList<SpecimenCode>());

            Regex dhjparCodes = new Regex(@"(?i)\bDHJPAR\w{1,8}\b");
            janzenSpecimenCodes.AddRange(
                this.XmlDocument.GetMatchesInXmlText(dhjparCodes, false)
                    .Select(a => new SpecimenCode("DHJPAR", "Janzen", a))
                    .ToList<SpecimenCode>());

            if (janzenSpecimenCodes.Count > 0)
            {
                janzenSpecimenCodes = janzenSpecimenCodes.Distinct().ToList();
            }

            return janzenSpecimenCodes;
        }


        public void TagKnownSpecimenCodes(IXPathProvider xpathProvider)
        {
            List<SpecimenCode> knownSpecimenCodes = new List<SpecimenCode>();

            knownSpecimenCodes.AddRange(GetJanzenCodes());
            knownSpecimenCodes.AddRange(GetPrefixNumericCodes());

            knownSpecimenCodes = knownSpecimenCodes.Distinct().ToList();

            ReplaceSpecimenCodesInXml(xpathProvider.SelectContentNodesXPathTemplate, knownSpecimenCodes, this.specimenCodeTag);

            GuessSequentalPrefixNumericSpecimenCodes(xpathProvider.SelectContentNodesXPathTemplate, this.specimenCodeTag);
        }

        private void ReplaceSpecimenCodesInXml(string xpathTemplate, List<SpecimenCode> specimenCodes, TagContent tag)
        {
            foreach (SpecimenCode specimenCode in specimenCodes)
            {
                TagContent codeTag = new TagContent(tag);
                codeTag.Attributes += @" prefix=""" + specimenCode.Prefix + @""" type=""" + specimenCode.Type + @"""";
                TagTextInXmlDocument(specimenCode.Code, codeTag, xpathTemplate);
            }

            /*
             * Here we might have nested <specimenCode> which probably is due to mistaken codes.
             */
            {
                string nestedSpecimenCodesXpath = string.Format("//{0}[{0}]", tag.Name);
                foreach (XmlNode nestedSpecimenCodesNode in this.XmlDocument.SelectNodes(nestedSpecimenCodesXpath, this.NamespaceManager))
                {
                    Alert.Log("WARNING: Nested specimen codes: " + nestedSpecimenCodesNode.InnerXml);
                }
            }
        }

        private void GuessSequentalPrefixNumericSpecimenCodes(string xpathTemplate, TagContent tag)
        {
            // <specimenCode full-string="UQIC 221451"><institutionalCode attribute1="http://grbio.org/institution/university-queensland-insect-collection">UQIC</institutionalCode> 221451</specimenCode>, 221452, 221447, 221448, 221450, 221454, 221456
            // <specimenCode full-string="UQIC 221451">.*?</specimenCode>, 221452, 221447, 221448, 221450, 221454, 221456
            // <specimenCode full-string="UQIC 221451">.*?</specimenCode>, <specimenCode full-string="221452">221452</specimenCode>, 221447, 221448, 221450, 221454, 221456
            // <specimenCode full-string="UQIC 221451">.*?</specimenCode>, <specimenCode full-string="UQIC 221452">221452</specimenCode>, 221447, 221448, 221450, 221454, 221456

            string guessNextCodePattern = @"(?<=" + tag.CloseTag + @"(?:\W{1,5}|(?:\W*?<!--[\w\s]*?-->)+\W*))((?:[0-9][\/\.]?(?:<[^>]*>)*){1,20})";
            Regex guessNextCode = new Regex(guessNextCodePattern);

            GuessNextSpecimenCodesByRegex(xpathTemplate, tag, guessNextCode, ".//*[@prefix][@type='prefix-numeric']");
        }

        /// <summary>
        /// Tags next specimen codes when we have some tagged ones.
        /// </summary>
        /// <param name="tag">The tag model.</param>
        /// <param name="xpathTemplate">XPath string template of the type "//node-to-search-in[{0}]".</param>
        private void GuessSequentalSpecimenCodes(TagContent tag, string xpathTemplate)
        {
            // <specimenCode full-string="UQIC 221451"><institutionalCode attribute1="http://grbio.org/institution/university-queensland-insect-collection">UQIC</institutionalCode> 221451</specimenCode>, 221452, 221447, 221448, 221450, 221454, 221456
            // <specimenCode full-string="UQIC 221451">.*?</specimenCode>, 221452, 221447, 221448, 221450, 221454, 221456
            // <specimenCode full-string="UQIC 221451">.*?</specimenCode>, <specimenCode full-string="221452">221452</specimenCode>, 221447, 221448, 221450, 221454, 221456
            // <specimenCode full-string="UQIC 221451">.*?</specimenCode>, <specimenCode full-string="UQIC 221452">221452</specimenCode>, 221447, 221448, 221450, 221454, 221456

            string guessNextCodePattern = "(?<=" + tag.CloseTag + @"\W{1,3})(\b[A-Z0-9](?:<[^>]*>)*(?:\s?[\.:\\\/–—−-]?\s?[A-Z0-9]\s?(?:<[^>]*>)*){1,20}[A-Z0-9]\b)";
            Regex guessNextCode = new Regex(guessNextCodePattern);

            TagContent replacementTag = new TagContent(tag);
            replacementTag.FullTag = replacementTag.OpenTag + "$1" + replacementTag.CloseTag;

            string replacement = replacementTag.FullTag;

            string xpath = string.Format(xpathTemplate, tag.Name);
            foreach (XmlNode node in this.XmlDocument.SelectNodes(xpath, this.NamespaceManager))
            {
                string replace = node.InnerXml;

                while (guessNextCode.Match(replace).Success)
                {
                    replace = guessNextCode.Replace(replace, replacement);
                }

                try
                {
                    XmlNode testNode = this.XmlDocument.CreateElement("test-node");
                    testNode.InnerXml = replace;
                }
                catch (Exception e)
                {
                    replace = node.InnerXml;

                    Alert.Log("\nInvalid replacement string:\n" + replace + "\n\n");
                    Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0, "Guess specimen codes.");
                }
                finally
                {
                    node.InnerXml = replace;
                }
            }
        }

        private void GuessNextSpecimenCodesByRegex(string xpathTemplate, TagContent tag, Regex guessNextCode, string xpathToSelectSpecimenCodeTags)
        {
            string replacement = tag.OpenTag + "$1" + tag.CloseTag;

            string xpath = string.Format(xpathTemplate, tag.Name);
            foreach (XmlNode node in this.XmlDocument.SelectNodes(xpath, this.NamespaceManager))
            {
                string replace = node.InnerXml;
                while (guessNextCode.Match(replace).Success)
                {
                    replace = guessNextCode.Replace(replace, replacement);
                }

                node.SafeReplaceInnerXml(replace);

                foreach (XmlNode specimenCodeNode in node.SelectNodes(xpathToSelectSpecimenCodeTags, this.NamespaceManager))
                {
                    SetAttributesOfSequentalSpecimenCodes(specimenCodeNode, tag);
                }
            }
        }

        private static void SetAttributesOfSequentalSpecimenCodes(XmlNode node, TagContent tag)
        {
            Alert.Log("\n{0}", node.OuterXml);

            if (node.NextSibling != null)
            {
                string nodeFullString = node.Attributes["full-string"].InnerText;
                int nodeFullStringLenght = nodeFullString.Length;

                for (XmlNode next = node.NextSibling; next != null; next = next.NextSibling)
                {
                    if (next.Name == tag.Name)
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

                        Alert.Log(next.OuterXml);
                    }
                }
            }
        }
    }
}
