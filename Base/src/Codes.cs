using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;


// institutionalCode -> @description
// specimenCode -> @institutionalCode


namespace ProcessingTools.Base
{
    public class Codes : Base
    {
        private const string SelectNodesToTagAbbreviationsXPathTemplate = "//node()[count(ancestor-or-self::node()[name()='abbrev'])=0][contains(string(.),'{0}')][count(.//node()[contains(string(.),'{0}')])=0]";

        private const string AbbreviationReplaceTagName = "abbreviationReplaceTagName";

        public Codes(Config config, string xml)
            : base(config, xml)
        {
        }

        public Codes(Base baseObject)
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

            this.ParseXmlStringToXmlDocument();

            TagContent specimenCodeTag = new TagContent("specimenCode");

            ReplaceSpecimenCodesInXml(xpathProvider.SelectContentNodesXPathTemplate, plausibleSpecimenCodes, specimenCodeTag);

            this.ParseXmlDocumentToXmlString();
        }

        public void TagKnownSpecimenCodes(IXPathProvider xpathProvider)
        {
            this.ParseXmlStringToXmlDocument();

            List<string> knownSpecimenCodes = new List<string>();

            //// Add to plausible various known codes: Janzen, OSUC, CASENT, LACMENT, ...
            GetJanzenCodes(knownSpecimenCodes);
            GetPrefixNumericCodes(knownSpecimenCodes);

            knownSpecimenCodes = knownSpecimenCodes.Distinct().ToList();

            TagContent specimenCodeTag = new TagContent("specimenCode");
            ReplaceSpecimenCodesInXml(xpathProvider.SelectContentNodesXPathTemplate, knownSpecimenCodes, specimenCodeTag);

            this.ParseXmlDocumentToXmlString();
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
                foreach (XmlNode nestedSpecimenCodesNode in this.xmlDocument.SelectNodes(nestedSpecimenCodesXpath, this.NamespaceManager))
                {
                    Alert.Log("WARNING: Nested specimen codes: " + nestedSpecimenCodesNode.InnerXml);
                }
            }
        }

        private void GetJanzenCodes(List<string> specimenCodes)
        {
            // Janzen codes:
            // yy-SRNP-xxxxxx
            // DHJPARxxxxxxx
            {
                List<string> janzenSpecimenCodes = new List<string>();

                Regex srnpCodes = new Regex(@"(?i)\b\w{1,3}\W{1,3}SRNP\W{1,3}\w{1,8}\b");
                janzenSpecimenCodes.AddRange(GetMatchesInXmlText(this.xmlDocument, srnpCodes, false));

                Regex dhjparCodes = new Regex(@"(?i)\bDHJPAR\w{1,8}\b");
                janzenSpecimenCodes.AddRange(GetMatchesInXmlText(this.xmlDocument, dhjparCodes, false));

                if (janzenSpecimenCodes.Count > 0)
                {
                    janzenSpecimenCodes = janzenSpecimenCodes.Distinct().ToList();
                    specimenCodes.AddRange(janzenSpecimenCodes);
                }
            }
        }

        private void GetPrefixNumericCodes(List<string> specimenCodes)
        {
            // USNM 123392
            // AMNH 235608
            // FMNH 21077
            // 4-digit individual code including the notion “Baur” (e.g., “Baur 2410”); see doi: 10.3897/zookeys.514.9910
            {
                List<string> prefixNumericSpecimenCodes = new List<string>();

                Regex prefixNumericCodes = new Regex(@"(?i)\b(?:USNM|ZMB|SMF|ZUTC(?: Iso\.)?|PCGMK|IRIPP Iso\-|ZMMSU|UFES|ALP|AMNH|MHNG|DZSJRP|BM|CM|MN|LDM|FMNH|Baur)\W{0,3}\d{3,}(?:\.\d+)*\b");
                prefixNumericSpecimenCodes = GetMatchesInXmlText(this.xmlDocument, prefixNumericCodes);

                prefixNumericSpecimenCodes = prefixNumericSpecimenCodes.Distinct().ToList();

                if (prefixNumericSpecimenCodes.Count > 0)
                {
                    specimenCodes.AddRange(prefixNumericSpecimenCodes);
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
            TagContent replacement = new TagContent(tag);
            replacement.FullTag = replacement.OpenTag + "$1" + replacement.CloseTag;

            string xpath = string.Format(xpathTemplate, tag.Name);
            foreach (XmlNode node in this.xmlDocument.SelectNodes(xpath, this.NamespaceManager))
            {
                string replace = node.InnerXml;

                while (guessNextCode.Match(replace).Success)
                {
                    replace = guessNextCode.Replace(replace, replacement.FullTag);
                }

                try
                {
                    XmlNode testNode = this.xmlDocument.CreateElement("test-node");
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
            xmlDoc.LoadXml(this.xml);
            XmlNodeList institutionalCodesXmlNodes = xmlDoc.SelectNodes("//institutionalCode", this.NamespaceManager);
            List<string> institutionalCodes = GetStringListOfUniqueXmlNodeContent(institutionalCodesXmlNodes);
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
            cleanedXmlDocument.LoadXml(this.xml);
            cleanedXmlDocument.InnerXml = Regex.Replace(
                cleanedXmlDocument.InnerXml,
                @"(?<=</xref>)\s*:\s*" + codePattern,
                string.Empty);
            cleanedXmlDocument.LoadXml(
                XsltOnString.ApplyTransform(this.Config.codesRemoveNonCodeNodes, cleanedXmlDocument));

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
