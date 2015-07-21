using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Base
{
    public class Codes : Base
    {
        private const string SelectNodesToTagAbbreviationsXPathTemplate = "//node()[count(ancestor-or-self::node()[name()='abbrev'])=0][contains(string(.),'{0}')][count(.//node()[contains(string(.),'{0}')])=0]";

        private const string AbbreviationReplaceTagName = "abbreviationReplaceTagName";

        public Codes()
            : base()
        {
        }

        public Codes(string xml)
            : base(xml)
        {
        }

        public Codes(Config config)
            : base(config)
        {
        }

        public Codes(Config config, string xml)
            : base(config, xml)
        {
        }

        public Codes(Base baseObject)
            : base(baseObject)
        {
        }

        public void TagSpecimenCount()
        {
            string xpath = "//p|//license-p|//li|//th|//td|//mixed-citation|//element-citation|//nlm-citation|//tp:nomenclature-citation";

            this.ParseXmlStringToXmlDocument();
            foreach (XmlNode node in this.xmlDocument.SelectNodes(xpath, this.NamespaceManager))
            {
                string replace = node.InnerXml;

                // 1♀
                {
                    string pattern = @"(?<!<[^>]+)((?i)(?:\d+(?:\s*[–—−‒-]?\s*))+[^\w<>]{0,5}(?:[♀♂]|male|female)+)(?![^<>]*>)";
                    Match m = Regex.Match(replace, pattern);
                    if (m.Success)
                    {
                        replace = Regex.Replace(replace, pattern, "<specimen-count>$1</specimen-count>");
                        node.InnerXml = replace;
                    }
                }
            }

            this.ParseXmlDocumentToXmlString();
        }

        public void TagProducts()
        {
            string xpath = "//p|//license-p|//li|//th|//td|//mixed-citation|//element-citation|//nlm-citation|//tp:nomenclature-citation";
            string query = @"select [dbo].[products].[Name] as name from [dbo].[products] order by len(name) desc;";
            string tagName = "product";
            this.ExecuteSimpleReplaceUsingDatabase(xpath, query, tagName);
        }

        public void TagGeonames()
        {
            string xpath = "//p|//license-p|//li|//th|//td|//mixed-citation|//element-citation|//nlm-citation|//tp:nomenclature-citation";
            string query = @"select [dbo].[geonames].[Name] as name from [dbo].[geonames] order by len(name) desc;";
            string tagName = "geoname";
            this.ExecuteSimpleReplaceUsingDatabase(xpath, query, tagName);
        }

        public void TagMorphology()
        {
            string xpath = "//p|//license-p|//li|//th|//td|//mixed-citation|//element-citation|//nlm-citation|//tp:nomenclature-citation";
            string query = @"select [dbo].[morphology].[Name] as name from [dbo].[morphology] order by len(name) desc;";
            string tagName = "morphology-part";
            this.ExecuteSimpleReplaceUsingDatabase(xpath, query, tagName);
        }

        public void TagInstitutions()
        {
            string xpath = "//p|//license-p|//li|//th|//td|//mixed-citation|//element-citation|//nlm-citation|//tp:nomenclature-citation";
            TagContent tag = new TagContent("institution");

            string query = @"select [Name] as name from [dbo].[institutions] order by len(name) desc;";
            this.ExecuteSimpleReplaceUsingDatabase(xpath, query, tag);

            // WARNING: here is set len(name) > 1!
            query = @"select [NameOfInstitution], [URL] from [dbo].[biorepositories] where len([NameOfInstitution]) > 1 order by len([NameOfInstitution]) desc;";
            this.ExecuteSimpleReplaceUsingDatabase(xpath, query, tag);
        }

        public void TagInstitutionalCodes()
        {
            string xpath = "//p|//license-p|//li|//th|//td|//mixed-citation|//element-citation|//nlm-citation|//tp:nomenclature-citation";
            TagContent tagName = new TagContent("institutional-code");

            // WARNING: here is set len(name) > 1!
            string query = @"select [InstitutionalCode], [URL] from [dbo].[biorepositories] where len([InstitutionalCode]) > 1 order by len([InstitutionalCode]) desc;";
            this.ExecuteSimpleReplaceUsingDatabase(xpath, query, tagName, true);
        }

        public void TagSpecimenCodes()
        {
            const string XPathTemplate = "//p[{0}]|//license-p[{0}]|//li[{0}]|//th[{0}]|//td[{0}]|//mixed-citation[{0}]|//element-citation[{0}]|//nlm-citation[{0}]|//tp:nomenclature-citation[{0}]";
            const string CodePattern = @"\b[A-Z0-9](\s?[\.:\\\/–—−-]?\s?[A-Z0-9]\s?)+[A-Z0-9]\b";

            List<string> potentialSpecimenCodes = ExtractPotentialSpecimenCodes(CodePattern);

            Alert.Message("\n\n" + potentialSpecimenCodes.Count + " code words in article\n");
            foreach (string word in potentialSpecimenCodes)
            {
                Alert.Message(word);
            }

            Alert.Message("\n\nPlausible specimen codes\n\n");

            List<string> plausibleSpecimenCodes = GetPlausibleSpecimenCodesBasedOnInstitutionalCodes(potentialSpecimenCodes);

            this.ParseXmlStringToXmlDocument();

            //// Add to plausible various known codes: Janzen, OSUC, CASENT, LACMENT, ...
            TagJanzenCodes(plausibleSpecimenCodes);

            plausibleSpecimenCodes = plausibleSpecimenCodes.Distinct().ToList();

            TagContent specimenCodeTag = new TagContent("specimen-code");

            foreach (string specimenCode in plausibleSpecimenCodes)
            {
                Alert.Message(specimenCode);

                TagTextInXmlDocument(specimenCode, specimenCodeTag, XPathTemplate);
            }

            // Try to guess some other specimen codes following the tagged ones.
            GuessSequentalSpecimenCodes(specimenCodeTag, XPathTemplate);

            /*
             * Here we might have nested <specimen-code> which probably is due to mistaken codes.
             */
            {
                string nestedSpecimenCodesXpath = string.Format("//{0}[{0}]", specimenCodeTag.Name);
                foreach (XmlNode nestedSpecimenCodesNode in this.xmlDocument.SelectNodes(nestedSpecimenCodesXpath, this.NamespaceManager))
                {
                    Alert.Message("WARNING: Nested specimen codes: " + nestedSpecimenCodesNode.InnerXml);
                }
            }

            this.ParseXmlDocumentToXmlString();
        }

        private void TagJanzenCodes(List<string> specimenCodes)
        {
            // Janzen codes:
            // yy-SRNP-xxxxxx
            // DHJPARxxxxxxx
            {
                List<string> janzenSpecimenCodes = new List<string>();
                string searchableText = this.xmlDocument.InnerText;

                Regex srnpCodes = new Regex(@"(?i)\b\w{1,3}\W{1,3}SRNP\W{1,3}\w{1,8}\b");
                for (Match m = srnpCodes.Match(searchableText); m.Success; m = m.NextMatch())
                {
                    janzenSpecimenCodes.Add(m.Value);
                }

                Regex dhjparCodes = new Regex(@"(?i)\bDHJPAR\w{1,8}\b");
                for (Match m = dhjparCodes.Match(searchableText); m.Success; m = m.NextMatch())
                {
                    janzenSpecimenCodes.Add(m.Value);
                }

                janzenSpecimenCodes = janzenSpecimenCodes.Distinct().ToList();

                if (janzenSpecimenCodes.Count > 0)
                {
                    specimenCodes.AddRange(janzenSpecimenCodes);
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
            // <specimen-code full-string="UQIC 221451"><institutional-code attribute1="http://grbio.org/institution/university-queensland-insect-collection">UQIC</institutional-code> 221451</specimen-code>, 221452, 221447, 221448, 221450, 221454, 221456
            // <specimen-code full-string="UQIC 221451">.*?</specimen-code>, 221452, 221447, 221448, 221450, 221454, 221456

            Regex guessNextCode = new Regex("(?<=" + tag.CloseTag + @"\W{1,3})(\b[A-Z0-9](?:<[^>]*>)*(?:\s?[\.:\\\/–—−-]?\s?[A-Z0-9]\s?(?:<[^>]*>)*){1,20}[A-Z0-9]\b)");

            string xpath = string.Format(xpathTemplate, tag.Name);
            foreach (XmlNode node in this.xmlDocument.SelectNodes(xpath, this.NamespaceManager))
            {
                TagContent replacement = new TagContent(tag);
                replacement.FullTag = replacement.OpenTag + "$1" + replacement.CloseTag;

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

                    Alert.Message("\nInvalid replacement string:\n" + replace + "\n\n");
                    Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0, "Guess specimen codes.");
                }
                finally
                {
                    node.InnerXml = replace;
                }
            }
        }

        /// <summary>
        /// Tags plain text string (no regex) in the xmlDocument.
        /// </summary>
        /// <param name="textToTag">The plain text string to be tagged in the XML.</param>
        /// <param name="tag">The tag model.</param>
        /// <param name="xpathTemplate">XPath string template of the type "//node-to-search-in[{0}]".</param>
        /// <param name="isCaseSensitive">Must be the search case sensitive?</param>
        private void TagTextInXmlDocument(string textToTag, TagContent tag, string xpathTemplate, bool isCaseSensitive = true)
        {
            string xpath = string.Format(xpathTemplate, "contains(string(.),'" + textToTag + "')");
            XmlNodeList nodeList = this.xmlDocument.SelectNodes(xpath, this.NamespaceManager);

            TagTextInXmlDocument(textToTag, tag, nodeList);
        }

        /// <summary>
        /// Tags plain text string (no regex) in the xmlDocument.
        /// </summary>
        /// <param name="textToTag">The plain text string to be tagged in the XML.</param>
        /// <param name="tag">The tag model.</param>
        /// <param name="nodeList">The list of nodes where we try to tag textToTag.</param>
        /// <param name="isCaseSensitive">Must be the search case sensitive?</param>
        private void TagTextInXmlDocument(string textToTag, TagContent tag, XmlNodeList nodeList, bool isCaseSensitive = true)
        {
            string textToTagEscaped = Regex.Replace(Regex.Escape(textToTag), "'", "\\W");
            string textToTagPattern = @"(?:<[\w\!][^>]*>)*\b" + Regex.Replace(textToTagEscaped, @"([^\\])(?!\Z)", "$1(?:<[^>]*>)*") + @"\b(?:<[\/\!][^>]*>)*";

            string caseSensitiveness = string.Empty;
            if (!isCaseSensitive)
            {
                caseSensitiveness = "(?i)";
            }

            Regex textTotagPatternRegex = new Regex("(?<!<[^>]+)(" + caseSensitiveness + textToTagPattern + ")(?![^<>]*>)");
            Regex textToTagRegex = new Regex("(?<!<[^>]+)\\b(" + caseSensitiveness + textToTagEscaped + ")(?![^<>]*>)");

            TagContent replacement = new TagContent(tag);
            replacement.Attributes += @" full-string=""" + textToTag + @"""";
            replacement.FullTag = replacement.OpenTag + "$1" + replacement.CloseTag;

            foreach (XmlNode node in nodeList)
            {
                string replace = node.InnerXml;

                /*
                 * Here we need this if because the use of textTotagPatternRegex is potentialy dangerous:
                 * this is dynamically generated regex which might be too complex and slow.
                 */
                if (textToTagRegex.Match(node.InnerText).Length == textToTagRegex.Match(node.InnerXml).Length)
                {
                    replace = textToTagRegex.Replace(replace, replacement.FullTag);
                }
                else
                {
                    replace = textTotagPatternRegex.Replace(replace, replacement.FullTag);
                }

                try
                {
                    XmlNode testNode = this.xmlDocument.CreateElement("test-node");
                    testNode.InnerXml = replace;
                }
                catch (Exception e)
                {
                    Alert.Message("\nInvalid replacement string:\n" + replace + "\n\n");

                    replace = node.InnerXml;

                    Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0, "Tag text in xmlDocument.");
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
            XmlNodeList institutionalCodesXmlNodes = xmlDoc.SelectNodes("//institutional-code", this.NamespaceManager);
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

        private void ExecuteSimpleReplaceUsingDatabase(string xpath, string query, TagContent tag, bool caseSensitive = false)
        {
            string patternTemplate = string.Empty;
            if (caseSensitive)
            {
                patternTemplate = "(?<!<[^>]+)\\b({0})\\b(?![^<>]*>)";
            }
            else
            {
                patternTemplate = "(?<!<[^>]+)\\b((?i){0})\\b(?![^<>]*>)";
            }

            this.ParseXmlStringToXmlDocument();
            try
            {
                string connectionString = this.Config.mainDictionaryDataSourceString;
                XmlNodeList nodeList = this.xmlDocument.SelectNodes(xpath, this.NamespaceManager);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string contentString = reader.GetString(0);

                                if (reader.FieldCount > 1)
                                {
                                    StringBuilder attributes = new StringBuilder();
                                    for (int i = 1, len = reader.FieldCount; i < len; ++i)
                                    {
                                        attributes.Append(
                                            string.Format(
                                                @" attribute{0}=""{1}""",
                                                i,
                                                Regex.Replace(
                                                    Regex.Replace(
                                                        reader.GetString(i),
                                                        "&",
                                                        "&amp;"),
                                                    @"""",
                                                    "&quot;")));
                                    }

                                    tag.Attributes = attributes.ToString();
                                }
                                else
                                {
                                    tag.Attributes = string.Empty;
                                }

                                string replaceSubstitution = tag.OpenTag + "$1" + tag.CloseTag;
                                string pattern = string.Format(patternTemplate, Regex.Replace(Regex.Escape(contentString), "'", "\\W"));
                                foreach (XmlNode node in nodeList)
                                {
                                    if (Regex.Match(node.InnerText, pattern).Success)
                                    {
                                        node.InnerXml = Regex.Replace(node.InnerXml, pattern, replaceSubstitution);
                                    }
                                }
                            }

                            reader.Dispose();
                            reader.Close();
                        }

                        command.Cancel();
                        command.Dispose();
                    }

                    connection.Dispose();
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, 1);
            }

            this.ParseXmlDocumentToXmlString();
        }

        private void ExecuteSimpleReplaceUsingDatabase(string xpath, string query, string tagName)
        {
            TagContent tag = new TagContent(tagName);
            this.ExecuteSimpleReplaceUsingDatabase(xpath, query, tag);
        }
    }
}
