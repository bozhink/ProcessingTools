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

        private const string ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=""C:\Users\Bozhin Karaivanov\Documents\Visual Studio 2013\Projects\ProcessingTools\Base\Data\MainDictionary.mdf"";Integrated Security=True";

        public Codes()
            : base()
        {
        }

        public Codes(string xml)
            : base(xml)
        {
        }

        public static Abbreviation ConvertAbbrevXmlNodeToAbbreviation(XmlNode abbrev)
        {
            Abbreviation abbreviation = new Abbreviation();

            abbreviation.Content = Regex.Replace(
                        Regex.Replace(
                            Regex.Replace(
                                abbrev.InnerXml,
                                @"<def.+</def>",
                                string.Empty),
                            @"<def[*>]</def>|</?b[^>]*>",
                            string.Empty),
                        @"\A\W+|\W+\Z",
                        string.Empty);

            if (abbrev.Attributes["content-type"] != null)
            {
                abbreviation.ContentType = abbrev.Attributes["content-type"].InnerText;
            }

            if (abbrev["def"] != null)
            {
                abbreviation.Definition = Regex.Replace(
                    Regex.Replace(
                        abbrev["def"].InnerXml,
                        "<[^>]*>",
                        string.Empty),
                    @"\A[=,;:\s–—−-]|[=,;:\s–—−-]\Z|\s+(?=\s)",
                    string.Empty);
            }

            return abbreviation;
        }

        public void TagAbbreviationsInText()
        {
            this.ParseXmlStringToXmlDocument();

            // Do not change this sequence
            this.TagAbbreviationsInSpecificNode("//graphic|//media|//disp-formula-group");
            this.TagAbbreviationsInSpecificNode("//chem-struct-wrap|//fig|//supplementary-material|//table-wrap");
            this.TagAbbreviationsInSpecificNode("//fig-group|//table-wrap-group");
            this.TagAbbreviationsInSpecificNode("//boxed-text");
            this.TagAbbreviationsInSpecificNode("/");

            this.xmlDocument.InnerXml = Regex.Replace(this.xmlDocument.InnerXml, "</?" + AbbreviationReplaceTagName + "[^>]*>", string.Empty);
            this.xml = this.xmlDocument.OuterXml;
        }

        public void TagQuantities()
        {
            string xpath = "//p|//license-p|//li|//th|//td|//mixed-citation|//element-citation|//nlm-citation|//tp:nomenclature-citation";

            this.ParseXmlStringToXmlDocument();
            foreach (XmlNode node in xmlDocument.SelectNodes(xpath, this.namespaceManager))
            {
                string replace = node.InnerXml;

                // 0.6–1.9 mm, 1.1–1.7 × 0.5–0.8 mm
                string pattern = @"(?<!<[^>]+)((?:(?:[–—−‒-]\s*)?\d+(?:[,\.]\d+)?(?:\s*[×\*])?\s*)+(?:[kdcmµ]m|[º°˚]\s*C|bp|ft|m|[kdcmµ]M|[dcmµ][lL]|[kdcmµ]mol|mile|mi|min(?:ute)|\%)\b)(?![^<>]*>)";
                Match m = Regex.Match(replace, pattern);
                if (m.Success)
                {
                    // Alert.Message(m.Value);
                    replace = Regex.Replace(replace, pattern, "<quantity>$1</quantity>");
                    node.InnerXml = replace;
                }
            }

            this.ParseXmlDocumentToXmlString();
        }

        public void TagDirections()
        {
            string xpath = "//p|//license-p|//li|//th|//td|//mixed-citation|//element-citation|//nlm-citation|//tp:nomenclature-citation";

            this.ParseXmlStringToXmlDocument();
            foreach (XmlNode node in this.xmlDocument.SelectNodes(xpath, this.namespaceManager))
            {
                string replace = node.InnerXml;

                // 24 km W
                string pattern = @"(<quantity>.*?</quantity>\W{0,4}(?:[NSEW][NSEW\s\.-]{0,5}(?!\w)|(?i)(?:east|west|south|notrh)+))";
                Match m = Regex.Match(replace, pattern);
                if (m.Success)
                {
                    // Alert.Message(m.Value);
                    replace = Regex.Replace(replace, pattern, "<direction>$1</direction>");
                    node.InnerXml = replace;
                }
            }

            this.ParseXmlDocumentToXmlString();
        }

        public void TagDates()
        {
            string xpath = "//p|//license-p|//li|//th|//td|//mixed-citation|//element-citation|//nlm-citation|//tp:nomenclature-citation";

            this.ParseXmlStringToXmlDocument();
            foreach (XmlNode node in this.xmlDocument.SelectNodes(xpath, this.namespaceManager))
            {
                string replace = node.InnerXml;

                // 18 Jan 2008
                {
                    string pattern = @"(?<!<[^>]+)((?i)(?:(?:(?:(?:[1-2][0-9]|3[0-1]|0?[1-9])(?:\s*[–—−‒-]\s*))+|(?<!\S)(?:[1-2][0-9]|3[0-1]|0?[1-9]))[^\w<>]{0,4})?(?:(?:Jan(?:uary)?|Febr?(?:uary)?|Mar(?:ch)?|Apr(?:il)?|May|June?|July?|Aug(?:ust)?|Sept?(?:ember)?|Oct(?:ober)?|Nov(?:ember)?|Dec(?:ember)?)\s*(?:[–—−‒-]|to)\s*)+[^\w<>]{0,4}(?:1[6-9][0-9]|20[0-9])[0-9](?![0-9]))(?![^<>]*>)";
                    Match m = Regex.Match(replace, pattern);
                    if (m.Success)
                    {
                        // Alert.Message(m.Value);
                        replace = Regex.Replace(replace, pattern, "<date>$1</date>");
                        node.InnerXml = replace;
                    }
                }

                // 22–25.I.2007
                {
                    ////string pattern = @"((?i)(?:(?:(?:(?:[1-2][0-9]|3[0-1]|0?[1-9])(?:\s*[–—−‒-]\s*))+|(?<!\S)(?:[1-2][0-9]|3[0-1]|0?[1-9]))[^\w<>]{0,4})?(?<![a-z])(?:I|II|III|IV|V|VI|VII|VIII|IX|X|XI|XII)[^\w<>]{0,4}(?:1[6-9][0-9]|20[0-9])[0-9](?![0-9]))";
                    string pattern = @"(?<!<[^>]+)((?i)(?:(?:(?:(?:[1-2][0-9]|3[0-1]|0?[1-9])(?:\s*[–—−‒-]\s*))+|(?<!\S)(?:[1-2][0-9]|3[0-1]|0?[1-9]))[^\w<>]{0,4})?\b(?:I|II|III|IV|V|VI|VII|VIII|IX|X|XI|XII)\b[^\w<>]{0,4}(?:1[6-9][0-9]|20[0-9])[0-9](?![0-9]))(?![^<>]*>)";
                    Match m = Regex.Match(replace, pattern);
                    if (m.Success)
                    {
                        // Alert.Message(m.Value);
                        replace = Regex.Replace(replace, pattern, "<date>$1</date>");
                        node.InnerXml = replace;
                    }
                }
            }

            this.ParseXmlDocumentToXmlString();
        }

        public void TagSpecimenCount()
        {
            string xpath = "//p|//license-p|//li|//th|//td|//mixed-citation|//element-citation|//nlm-citation|//tp:nomenclature-citation";

            this.ParseXmlStringToXmlDocument();
            foreach (XmlNode node in this.xmlDocument.SelectNodes(xpath, this.namespaceManager))
            {
                string replace = node.InnerXml;

                // 1♀
                {
                    string pattern = @"(?<!<[^>]+)((?:\d+(?:\s*[–—−‒-]?\s*))+[^\w<>]{0,5}[♀♂])(?![^<>]*>)";
                    Match m = Regex.Match(replace, pattern);
                    if (m.Success)
                    {
                        // Alert.Message(m.Value);
                        replace = Regex.Replace(replace, pattern, "<specimen-count>$1</specimen-count>");
                        node.InnerXml = replace;
                    }
                }
            }

            this.ParseXmlDocumentToXmlString();
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

        public void SelectCodes()
        {
            const string CodePattern = @"\b[A-Z0-9](\s?[\.:\\\/\–\—\−\-]?\s?[A-Z0-9]\s?)+[A-Z0-9]\b";
            this.ParseXmlStringToXmlDocument();

            XmlDocument cleanedXmlDocument = new XmlDocument();
            cleanedXmlDocument.LoadXml(this.xmlDocument.OuterXml);
            cleanedXmlDocument.InnerXml = Regex.Replace(cleanedXmlDocument.InnerXml, @"(?<=</xref>)\s*:\s*" + CodePattern, string.Empty);
            cleanedXmlDocument.LoadXml(XsltOnString.ApplyTransform(config.codesRemoveNonCodeNodes, cleanedXmlDocument));

            List<string> potentialCodeWords = new List<string>();
            for (Match m = Regex.Match(cleanedXmlDocument.InnerText, CodePattern); m.Success; m = m.NextMatch())
            {
                potentialCodeWords.Add(m.Value);
            }

            potentialCodeWords = potentialCodeWords.Distinct().ToList();
            potentialCodeWords.Sort();
            Alert.Message("\n\n" + potentialCodeWords.Count + " code words in article\n");
            ////foreach (string word in potentialCodeWords)
            ////{
            ////    Alert.Message(word);
            ////    var instCodes = from code in institutionalCodes
            ////                    where Regex.Match(word, @"\A" + code).Success
            ////                    select code;
            ////    List<string> matchigCodes = instCodes.ToList();
            ////    foreach(string code in matchigCodes)
            ////    {
            ////        Alert.Message("\t-->\t" + code);
            ////    }
            ////}

            List<string> institutionalCodes = GetStringListOfUniqueXmlNodeContent(this.xmlDocument.SelectNodes("//institutional-code", this.namespaceManager));
            List<string> codeWords = new List<string>();
            foreach (string code in institutionalCodes)
            {
                List<string> codeWordsList = (from word in potentialCodeWords
                                              where Regex.Match(word, @"\A" + code).Success
                                              select word).ToList();
                if (codeWordsList != null)
                {
                    codeWords.AddRange(codeWordsList);
                }
            }

            codeWords = codeWords.Distinct().ToList();

            this.ParseXmlStringToXmlDocument();
            string xpath = "//p";
            foreach (XmlNode node in this.xmlDocument.SelectNodes(xpath, this.namespaceManager))
            {
                Alert.Message(node.InnerXml);

                foreach (string codeWord in codeWords)
                {
                    // TODO
                    node.InnerXml = Regex.Replace(node.InnerXml, "(" + codeWord + ")", "<specimen-code>$1</specimen-code>");
                }

                
            }

            this.ParseXmlDocumentToXmlString();
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
                XmlNodeList nodeList = this.xmlDocument.SelectNodes(xpath, this.namespaceManager);
                using (SqlConnection connection = new SqlConnection(ConnectionString))
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
                        }
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

        private void TagAbbreviationsInSpecificNode(string selectSpecificNodeXPath)
        {
            XmlNodeList specificNodes = xmlDocument.SelectNodes(selectSpecificNodeXPath, namespaceManager);
            foreach (XmlNode specificNode in specificNodes)
            {
                List<Abbreviation> abbreviationsList = specificNode.SelectNodes(".//abbrev", namespaceManager)
                    .Cast<XmlNode>().Select(a => ConvertAbbrevXmlNodeToAbbreviation(a)).ToList();

                foreach (Abbreviation abbreviation in abbreviationsList)
                {
                    string xpath = string.Format(SelectNodesToTagAbbreviationsXPathTemplate, abbreviation.Content);
                    foreach (XmlNode nodeInspecificNode in specificNode.SelectNodes(xpath, this.namespaceManager))
                    {
                        bool doReplace = false;
                        if (nodeInspecificNode.InnerXml == string.Empty)
                        {
                            if (nodeInspecificNode.OuterXml.IndexOf("<!--") == 0)
                            {
                                // This node is a comment. Do not replace matches here.
                                doReplace = false;
                            }
                            else if (nodeInspecificNode.OuterXml.IndexOf("<?") == 0)
                            {
                                // This node is a processing instruction. Do not replace matches here.
                                doReplace = false;
                            }
                            else if (nodeInspecificNode.OuterXml.IndexOf("<!DOCTYPE") == 0)
                            {
                                // This node is a DOCTYPE node. Do not replace matches here.
                                doReplace = false;
                            }
                            else if (nodeInspecificNode.OuterXml.IndexOf("<![CDATA[") == 0)
                            {
                                // This node is a CDATA node. Do nothing?
                                doReplace = false;
                            }
                            else
                            {
                                // This node is a text node. Tag this tex¾t and replace in InnerXml
                                doReplace = true;
                            }
                        }
                        else
                        {
                            // This is a named node
                            doReplace = true;
                        }

                        if (doReplace)
                        {
                            XmlElement newNode = xmlDocument.CreateElement("abbreviationReplaceTagName");
                            newNode.InnerXml = Regex.Replace(nodeInspecificNode.OuterXml, abbreviation.SearchPattern, abbreviation.ReplacePattern);
                            nodeInspecificNode.ParentNode.ReplaceChild(newNode, nodeInspecificNode);
                        }
                    }
                }
            }
        }

        public struct Abbreviation
        {
            public string Content;

            public string ContentType;

            public string Definition;

            public string SearchPattern
            {
                get
                {
                    return "\\b(" + this.Content + ")\\b";
                }
            }

            public string ReplacePattern
            {
                get
                {
                    return "<abbrev" +
                        ((this.ContentType == null || this.ContentType == string.Empty) ? string.Empty : @" content-type=""" + this.ContentType + @"""") +
                        ((this.Definition == null || this.Definition == string.Empty) ? string.Empty : @" xlink:title=""" + this.Definition + @"""") +
                        @" xmlns:xlink=""http://www.w3.org/1999/xlink""" +
                        ">$1</abbrev>";
                }
            }
        }
    }
}
