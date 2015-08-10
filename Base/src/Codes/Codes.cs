using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;


// institutionalCode -> @description
// specimenCode -> @institutionalCode


namespace ProcessingTools.Base
{
    public class Codes : TaggerBase
    {
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
            TagContent tag = new TagContent("institution");

            string query = @"select [Name] from [dbo].[institutions] order by len([Name]) desc;";

            dataProvider.Xml = this.Xml;
            dataProvider.ExecuteSimpleReplaceUsingDatabase(xpathProvider.SelectContentNodesXPath, query, tag);
            this.Xml = dataProvider.Xml;

            // WARNING: here is set len(name) > 1!
            query = @"select [NameOfInstitution], [URL] from [dbo].[biorepositories] where len([NameOfInstitution]) > 1 order by len([NameOfInstitution]) desc;";

            dataProvider.Xml = this.Xml;
            dataProvider.ExecuteSimpleReplaceUsingDatabase(xpathProvider.SelectContentNodesXPath, query, tag);
            this.Xml = dataProvider.Xml;
        }

        public void TagInstitutionalCodes(IXPathProvider xpathProvider, IDataProvider dataProvider)
        {
            TagContent tagName = new TagContent("institutionalCode");

            // WARNING: here is set len(name) > 1!
            string query = @"select [InstitutionalCode], [URL] from [dbo].[biorepositories] where len([InstitutionalCode]) > 1 order by len([InstitutionalCode]) desc;";

            dataProvider.Xml = this.Xml;
            dataProvider.ExecuteSimpleReplaceUsingDatabase(xpathProvider.SelectContentNodesXPath, query, tagName, true);
            this.Xml = dataProvider.Xml;
        }

        public void TagSpecimenCodes(IXPathProvider xpathProvider)
        {
            const string CodePattern = @"\b[A-Z0-9](\s?[\.:\\\/–—−-]?\s?[A-Z0-9]\s?)+[A-Z0-9]\b";

            List<string> potentialSpecimenCodes = ExtractPotentialSpecimenCodes(CodePattern);

            Alert.Log("\n\n" + potentialSpecimenCodes.Count + " code words in article\n");
            //foreach (string word in potentialSpecimenCodes)
            //{
            //    Alert.Log(word);
            //}

            Alert.Log("\n\nPlausible specimen codes\n\n");

            List<string> plausibleSpecimenCodes = GetPlausibleSpecimenCodesBasedOnInstitutionalCodes(potentialSpecimenCodes);

            TagContent specimenCodeTag = new TagContent("specimenCode");

            ReplaceSpecimenCodesInXml(xpathProvider.SelectContentNodesXPathTemplate, plausibleSpecimenCodes, specimenCodeTag);
        }

        private void ReplaceSpecimenCodesInXml(string xpathTemplate, List<string> specimenCodes, TagContent specimenCodeTag)
        {
            foreach (string specimenCode in specimenCodes)
            {
                Alert.Log(specimenCode);

                TagTextInXmlDocument(specimenCode, specimenCodeTag, xpathTemplate);
            }

            // Try to guess some other specimen codes following the tagged ones.
            GuessSequentalSpecimenCodes(specimenCodeTag, xpathTemplate);

            /*
             * Here we might have nested <specimenCode> which probably is due to mistaken codes.
             */
            {
                string nestedSpecimenCodesXpath = string.Format("//{0}[{0}]", specimenCodeTag.Name);
                foreach (XmlNode nestedSpecimenCodesNode in this.XmlDocument.SelectNodes(nestedSpecimenCodesXpath, this.NamespaceManager))
                {
                    Alert.Log("WARNING: Nested specimen codes: " + nestedSpecimenCodesNode.InnerXml);
                }
            }
        }

        /*
         * *********************************************************************************************************************************
         *
         * Tag known
         *
         * *********************************************************************************************************************************
         */
        /*
         * MNHN-IU-2013-9128
         * RMNH.CRUS.D.56397
         * RMNH.CRUS.D.2604
         * MNCN 15.05/7180
         * MNCN 15.05/60013H
         * MNCN 15.05/60013P
         * MNCN 15.05/7477P
         * MNHN #AR 13335
         * ISEA 001.4045, 001.4047, 001.4058
         * S08-12075
         * GRA0002851-0
         * Z-000004443
         * S-G-1519
         * B 10 0154930
         */

        private static string[] codePrefixes = new string[]
        {
            @"ALP",
            @"AMNH",
            @"Bau",
            @"BM",
            @"BR",
            @"C",
            @"CASENT",
            @"CM",
            @"COI",
            @"DZSJRP",
            @"FLAS",
            @"FMNH",
            @"G",
            @"HBG",
            @"HEID",
            @"IRIPP Iso\-",
            @"K",
            @"L",
            @"LACMENT",
            @"LDM",
            @"LISC",
            @"LISU",
            @"M",
            @"MA",
            @"MHNG",
            @"MN",
            @"NHM",
            @"OSUC",
            @"P",
            @"PCGMK",
            @"PR",
            @"PRE",
            @"PTBG",
            @"SMF",
            @"UFES",
            @"USNM",
            @"W",
            @"WAG",
            @"ZMB",
            @"ZMMSU",
            @"ZUTC",
            @"ZUTC Iso\.",
        };

        public List<SpecimenCode> GetPrefixNumericCodes()
        {
            List<SpecimenCode> prefixNumericSpecimenCodes = new List<SpecimenCode>();
            string textContent = this.TextContent;

            for (int i = 0, length = codePrefixes.Length; i < length; ++i)
            {
                string prefix = codePrefixes[i];
                Regex prefixNumericCodes = new Regex(@"(?i)\b(?:" + prefix + @")\W{0,3}\d{3,}(?:\.\d+)*\b");
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

            TagContent specimenCodeTag = new TagContent("specimenCode");
            ReplaceSpecimenCodesInXml(xpathProvider.SelectContentNodesXPathTemplate, knownSpecimenCodes, specimenCodeTag);
        }

        private void ReplaceSpecimenCodesInXml(string xpathTemplate, List<SpecimenCode> specimenCodes, TagContent specimenCodeTag)
        {
            foreach (SpecimenCode specimenCode in specimenCodes)
            {
                TagContent codeTag = new TagContent(specimenCodeTag);
                codeTag.Attributes += @" prefix=""" + specimenCode.Prefix + @""" type=""" + specimenCode.Type + @"""";
                TagTextInXmlDocument(specimenCode.Code, codeTag, xpathTemplate);
            }

            GuessSequentalPrefixNumericSpecimenCodes(specimenCodeTag, xpathTemplate);
        }

        private void GuessSequentalPrefixNumericSpecimenCodes(TagContent tag, string xpathTemplate)
        {
            // <specimenCode full-string="UQIC 221451"><institutionalCode attribute1="http://grbio.org/institution/university-queensland-insect-collection">UQIC</institutionalCode> 221451</specimenCode>, 221452, 221447, 221448, 221450, 221454, 221456
            // <specimenCode full-string="UQIC 221451">.*?</specimenCode>, 221452, 221447, 221448, 221450, 221454, 221456
            // <specimenCode full-string="UQIC 221451">.*?</specimenCode>, <specimenCode full-string="221452">221452</specimenCode>, 221447, 221448, 221450, 221454, 221456
            // <specimenCode full-string="UQIC 221451">.*?</specimenCode>, <specimenCode full-string="UQIC 221452">221452</specimenCode>, 221447, 221448, 221450, 221454, 221456

            string guessNextCodePattern = @"(?<=" + tag.CloseTag + @"\W{1,3})((?:[0-9](?:<[^>]*>)*){1,20})";
            Regex guessNextCode = new Regex(guessNextCodePattern);

            TagContent replacementTag = new TagContent(tag);
            string replacement = replacementTag.OpenTag + "$1" + replacementTag.CloseTag;

            string xpath = string.Format(xpathTemplate, tag.Name);
            foreach (XmlNode node in this.XmlDocument.SelectNodes(xpath, this.NamespaceManager))
            {
                string replace = node.InnerXml;
                while (guessNextCode.Match(replace).Success)
                {
                    replace = guessNextCode.Replace(replace, replacement);
                }

                ReplaceInnerXmlOfXmlNode(node, replace);

                foreach (XmlNode prefixNumeric in node.SelectNodes(".//*[@prefix][@type='prefix-numeric']", this.NamespaceManager))
                {
                    Alert.Log(prefixNumeric.OuterXml);
                    // prefixNumeric.NextSibling is text node due to the Regex above!
                    if (prefixNumeric.NextSibling != null)
                    {
                        string fullString = prefixNumeric.Attributes["full-string"].InnerText;
                        int fullStringLenght = fullString.Length;

                        // TODO
                        for (XmlNode nextSpecimenCode = prefixNumeric.NextSibling.NextSibling;
                             nextSpecimenCode != null && nextSpecimenCode.Name == tag.Name && nextSpecimenCode.Attributes.Count == 0;
                             nextSpecimenCode = nextSpecimenCode.NextSibling == null ? null : nextSpecimenCode.NextSibling.NextSibling)
                        {
                            string nextSpecimenCodeText = nextSpecimenCode.InnerText;
                            int length = nextSpecimenCodeText.Length;

                            foreach (XmlAttribute attribute in prefixNumeric.Attributes)
                            {
                                XmlAttribute attr = (XmlAttribute)attribute.CloneNode(true);
                                if (attr.Name == "full-string")
                                {
                                    attr.InnerText = fullString.Substring(0, fullStringLenght - length) + nextSpecimenCodeText;
                                }

                                nextSpecimenCode.Attributes.Append(attr);
                            }

                            Alert.Log(nextSpecimenCode.OuterXml);
                        }
                    }
                }
            }
        }

        private static void ReplaceInnerXmlOfXmlNode(XmlNode node, string replace)
        {
            string nodeInnerXml = node.InnerXml;
            bool reset = false;
            try
            {
                reset = true;
                node.InnerXml = replace;
                reset = false;
            }
            catch (Exception e)
            {
                Alert.Log("\nInvalid replacement string:\n" + replace + "\n\n");
                Alert.RaiseExceptionForMethod(e, "ReplaceInnerXmlOfXmlNode", 0, "Guess specimen codes.");
            }
            finally
            {
                if (reset)
                {
                    node.InnerXml = nodeInnerXml;
                }
            }
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

        /// <summary>
        /// Gets all plausible specimen codes which contains a used in the article instirurional code.
        /// </summary>
        /// <param name="potentialSpecimenCodes">The list of potential specimen codes.</param>
        /// <returns>Filtered list of plausible specimen codes.</returns>
        private List<string> GetPlausibleSpecimenCodesBasedOnInstitutionalCodes(List<string> potentialSpecimenCodes)
        {
            List<string> result = new List<string>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(this.Xml);
            XmlNodeList institutionalCodesXmlNodes = xmlDoc.SelectNodes("//institutionalCode", this.NamespaceManager);
            List<string> institutionalCodes = institutionalCodesXmlNodes.GetStringListOfUniqueXmlNodeContent();
            foreach (string institutionalCode in institutionalCodes)
            {
                /*
                 * Here we believe that instituional codes are not specimen codes.
                 */
                string pattern = @"\A" + institutionalCode + @"(\s?[\.:\\\/–—−-]?\s?[A-Z0-9]\s?)+[A-Z0-9]\b";
                Regex matchPattern = new Regex(pattern);
                List<string> codeWordsList = (from word in potentialSpecimenCodes
                                              where matchPattern.Match(word).Success
                                              select word).ToList();
                if (codeWordsList != null)
                {
                    result.AddRange(codeWordsList);
                }
            }

            result = result.Distinct().ToList();
            return result;
        }

        private List<string> ExtractPotentialSpecimenCodes(string codePattern)
        {
            XmlDocument cleanedXmlDocument = new XmlDocument();
            cleanedXmlDocument.LoadXml(this.Xml);
            cleanedXmlDocument.InnerXml = Regex.Replace(
                cleanedXmlDocument.InnerXml,
                @"(?<=</xref>)\s*:\s*" + codePattern,
                string.Empty);
            cleanedXmlDocument.LoadXml(
                cleanedXmlDocument.ApplyXslTransform(this.Config.codesRemoveNonCodeNodes));

            List<string> potentialCodeWords = new List<string>();
            for (Match m = Regex.Match(cleanedXmlDocument.InnerText, codePattern); m.Success; m = m.NextMatch())
            {
                potentialCodeWords.Add(m.Value);
            }

            potentialCodeWords = potentialCodeWords.Distinct().ToList();
            potentialCodeWords.Sort();
            return potentialCodeWords;
        }
    }
}
