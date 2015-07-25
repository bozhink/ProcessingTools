using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace ProcessingTools.Base
{
    public class Environments : Base
    {
        public Environments()
            : base()
        {
        }

        public Environments(string xml)
            : base(xml)
        {
        }

        public Environments(Config config)
            : base(config)
        {
        }

        public Environments(Config config, string xml)
            : base(config, xml)
        {
        }

        public void TagEnvironmentsRecords()
        {
            this.xml = Base.NormalizeNlmToSystemXml(this.Config, this.xml);
            this.ParseXmlStringToXmlDocument();

            XmlElement testXmlNode = this.xmlDocument.CreateElement("test");

            // Set the XPath string to select all nodes which may contain environments’ strings
            string xpath = string.Empty;
            if (this.Config.NlmStyle)
            {
                xpath = "//p|//title|//label|//tp:nomenclature-citation";
            }
            else
            {
                // TODO
                xpath = "//p";
            }

            try
            {
                // Select all nodes which potentioaly contains environments’ records
                XmlNodeList nodeList = this.xmlDocument.SelectNodes(xpath, this.NamespaceManager);

                // Connect to Environments database and use its records to tag Xml
                using (SqlConnection connection = new SqlConnection(this.Config.environmentsDataSourceString))
                {
                    connection.Open();
                    string query = @"select
                                 [dbo].[environments_names].[Content] as content,
                                 [dbo].[environments_names].[ContentId] as id,
                                 [dbo].[environments_entities].[EnvoId] as envoId
                                from [dbo].[environments_names]
                                inner join [dbo].[environments_entities]
                                on [dbo].[environments_names].[ContentId]=[dbo].[environments_entities].[Id]
                                where content not like 'ENVO%'
                                order by len(content) desc;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // For each environments’ record in database try to tag the Xml content
                                foreach (XmlNode node in nodeList)
                                {
                                    testXmlNode.InnerText = reader.GetString(0);
                                    string contentString = testXmlNode.InnerText;
                                    string envoId = "ENVO_" + reader.GetString(2).Substring(5);
                                    string envoTagName = "envo";
                                    string envoOpenTag = "<" + envoTagName +
                                        ////@" EnvoTermUri=""http://purl.obolibrary.org/obo/" + envoId + @"""" + 
                                        @" EnvoTermUri=""" + envoId + @"""" +
                                        @" ID=""" + reader.GetInt32(1) + @"""" +
                                        @" EnvoID=""" + envoId + @"""" +
                                        @" VerbatimTerm=""" + reader.GetString(0) + @"""" +
                                        @">";

                                    string pattern = "\\b((?i)" + Regex.Replace(Regex.Escape(contentString), "'", "\\W") + ")\\b";
                                    if (Regex.Match(node.InnerText, pattern).Success)
                                    {
                                        // The text content of the current node contains this environment string
                                        pattern = "(?<!<[^>]+)" + pattern + "(?![^<>]*>)";
                                        string replace = node.InnerXml;

                                        if (!Regex.Match(node.InnerXml, pattern).Success)
                                        {
                                            // The xml-as-string content of the current node soes not contain this environment string
                                            // Here we suppose that there is some tag inside the environment-string in the xml node.

                                            // Tag the xml-node-content using non-regex skip-tag matches
                                            replace = this.TagNodeContent(node.InnerXml, contentString, envoOpenTag);
                                            replace = this.TagOrderNormalizer(replace, envoTagName);
                                        }
                                        else
                                        {
                                            replace = Regex.Replace(node.InnerXml, pattern, envoOpenTag + "$1</" + envoTagName + ">");
                                        }

                                        node.InnerXml = replace;
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

            this.xmlDocument.InnerXml = Regex.Replace(this.xmlDocument.InnerXml, @"(?<=\sEnvoTermUri="")", "http://purl.obolibrary.org/obo/");
            this.xml = this.xmlDocument.OuterXml;
            if (this.Config.NlmStyle)
            {
                this.xml = Base.NormalizeSystemToNlmXml(this.Config, this.xml);
            }
        }

        public string TagNodeContent(string text, string keyString, string openTag)
        {
            string tagName = Regex.Match(openTag, @"(?<=<)[^\s>/""']+").Value;
            string closeTag = "</" + tagName + ">";
            openTag = Regex.Replace(openTag, "/", string.Empty);

            StringBuilder sb = new StringBuilder();
            StringBuilder charStack = new StringBuilder();

            int j = 0, len = keyString.Length;

            for (int i = 0; i < text.Length; i++)
            {
                char ch = text[i];
                j = 0;
                if (ch == keyString[j])
                {
                    charStack.Clear();

                TestCharInKeyString:
                    while (ch == keyString[j])
                    {
                        charStack.Append(ch);
                        if (++j >= len)
                        {
                            break;
                        }

                        ch = text[++i];
                    }

                    if (j < len)
                    {
                        // In the loop above we didn’t reach the end of the keyString
                        // Here we have the ch character which is not appended to the charStack StringBuilder
                        charStack.Append(ch);
                        if (ch == '<')
                        {
                            while (ch != '>')
                            {
                                ch = text[++i];
                                charStack.Append(ch);
                            }

                            // Here ch == '>'. Everything is in charStack up to ch = '>'.
                            ch = text[++i];
                            goto TestCharInKeyString;
                        }
                        else
                        {
                            // This means that ch != keyString[j] because here we have no match
                            // then just append the stored content in charStack and go ahead
                            sb.Append(charStack.ToString());
                        }
                    }
                    else
                    {
                        // Here we have the whole keyString match in the charStack
                        sb.Append(openTag + charStack.ToString() + closeTag);
                    }
                }
                else
                {
                    sb.Append(ch);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Normalizes crossed tags where tagName is the name of the tag which must not be splitted.
        /// Example:
        ///   &lt;y&gt;__&lt;x1&gt;__&lt;x2&gt;__&lt;/y&gt;__&lt;/x2&gt;__&lt;/x1&gt;
        /// will be transformed to
        ///   &lt;y&gt;__&lt;x1&gt;__&lt;x2&gt;__&lt;/x2&gt;&lt;/x1&gt;&lt;/y&gt;&lt;x1&gt;&lt;x2&gt;__&lt;/x2&gt;__&lt;/x1&gt;
        /// </summary>
        /// <param name="text">text to be normalized</param>
        /// <param name="tagName">name of the non-breakable tag</param>
        /// <returns>normalized text</returns>
        public string TagOrderNormalizer(string text, string tagName)
        {
            XmlElement testXmlNode = xmlDocument.CreateElement("test");

            // Control counter and maximal value of iterations
            const int MaxNumberOfIterations = 100;
            int iter = 0;
        NormalizeXmlNode:
            try
            {
                testXmlNode.InnerXml = text;
            }
            catch (XmlException)
            {
                //// <tagName> ... <x1> ...<x2> ... </x2> ... </x1> ... </x3> ... </x4> ... </tagName>
                //// will be
                //// </x3></x4><tagName><x4><x3> ... <x1> ...<x2> ... </x2> ... </x1> ... </x3> ... </x4> ... </tagName>

                List<TagContent> tags = this.GetTagModel(text);

                /*
                 * Broken block are pieces of xml in which the number of opening and closing tags is equal,
                 * i.e. the problem in broken blocks are crossed tags
                 * 
                 * Example of different broken blocks
                 * 
                 * envo
                 * some-tag x="y"
                 * nested-tag y="z" z="w"
                 * /envo
                 * /nested-tag
                 * /some-tag
                 * 
                 * some-tag
                 * nested-tag y="z" z="w"
                 * envo
                 * /nested-tag
                 * /some-tag
                 * /envo
                 * 
                 */
                for (int brokenBlockIndex = 0, len = tags.Count; brokenBlockIndex < len; brokenBlockIndex++)
                {
                    /*
                     * Generate the RegEx pattern to select every broken block
                     */

                    string singleBrokenPattern = this.GenerateSingleBrokenBlockSearchPattern(tags, ref brokenBlockIndex);

                    for (Match m = Regex.Match(text, singleBrokenPattern); m.Success; m = m.NextMatch())
                    {
                        string matchValue = m.Value;

                        // Get the rightmost match of the singleBrokenPattern in m.Value
                        {
                            Match m1;
                            while (true)
                            {
                                m1 = Regex.Match(matchValue.Substring(2), singleBrokenPattern);
                                if (!m1.Success)
                                {
                                    break;
                                }

                                matchValue = m1.Value;
                            }
                        }

                        string replace = matchValue;
                        List<TagContent> localTags = this.GetTagModel(replace);

                        // Here we have 2 cases: localTags[0].Name == 'tagName' and localTags[localTags.Count - 1].Name == '/tagName'
                        int firstLocalItem = 0;
                        int lastLocalItem = localTags.Count - 1;

                        if (string.Compare(tagName, localTags[firstLocalItem].Name) == 0)
                        {
                            // The first tag in localTags is the tagName-tag
                            // <envo>.*?<some-tag\ x="y">.*?<nested-tag\ y="z"\ z="w">.*?</envo>.*?</nested-tag>.*?</some-tag>
                            string prefix = Regex.Replace(replace, @"\A(.*?)</" + tagName + @">.*\Z", "$1");
                            string suffix = Regex.Replace(replace, @"\A.*?</" + tagName + @">(.*)\Z", "$1");
                            string body = "</" + tagName + ">";

                            for (int i = firstLocalItem + 1; i <= lastLocalItem; ++i)
                            {
                                if (string.Compare(tagName, localTags[i].Name.Substring(1)) == 0)
                                {
                                    // Here we are looking for the closing tag
                                    break;
                                }

                                body = "</" + localTags[i].Name + ">" + body + localTags[i].FullTag;
                            }

                            replace = prefix + body + suffix;
                        }
                        else
                        {
                            // The last tag in localTags is the tagName-tag
                            // <some-tag>.*?<nested-tag\ y="z"\ z="w">.*?<envo>.*?</nested-tag>.*?</some-tag>.*?</envo>
                            string prefix = Regex.Replace(replace, @"\A(.*)<" + tagName + @"[^>]*>.*?\Z", "$1");
                            string suffix = Regex.Replace(replace, @"\A.*<" + tagName + @"[^>]*>(.*?)\Z", "$1");
                            string body = Regex.Replace(replace, @"\A.*(<" + tagName + @"[^>]*>).*?\Z", "$1");

                            for (int i = firstLocalItem; i < lastLocalItem; ++i)
                            {
                                if (string.Compare(tagName, localTags[i].Name) == 0)
                                {
                                    // Here we are looking for the opening tag
                                    break;
                                }

                                body = "</" + localTags[i].Name + ">" + body + localTags[i].FullTag;
                            }

                            replace = prefix + body + suffix;
                        }

                        text = Regex.Replace(text, Regex.Escape(matchValue), replace);
                    }
                }

                // Try again if text is a valid XmlNode content
                if (++iter > MaxNumberOfIterations)
                {
                    throw new Exception("TagOrderNormalizer: invalid XmlBlock");
                }

                goto NormalizeXmlNode;
            }

            return testXmlNode.InnerXml;
        }

        private List<TagContent> GetTagModel(string text)
        {
            List<TagContent> tags = new List<TagContent>();

            for (Match m = Regex.Match(text, @"(?<=<)/?[^\s/<>""'!\?]+[^>/]*(?=>)"); m.Success; m = m.NextMatch())
            {
                // For every tag name
                TagContent tag = new TagContent();
                tag.Name = Regex.Match(m.Value, @"\A/?[^\s/<>""'!\?]+").Value;
                tag.Attributes = Regex.Match(m.Value, @"\s+.*\Z").Value;
                tag.FullTag = "<" + m.Value + ">";

                if (tag.IsClosingTag)
                {
                    int lastItemIndex = tags.Count - 1;
                    if (string.Compare(tags[lastItemIndex].Name, tag.Name.Substring(1)) == 0)
                    {
                        tags.RemoveAt(lastItemIndex);
                    }
                    else
                    {
                        tags.Add(tag);
                    }
                }
                else
                {
                    tags.Add(tag);
                }
            }

            return tags;
        }

        private string GenerateSingleBrokenBlockSearchPattern(List<TagContent> tags, ref int initialIndexInTags)
        {
            StringBuilder singleBrokenPattern = new StringBuilder();
            List<string> brokenStack = new List<string>();

            for (int k = initialIndexInTags; k < tags.Count; k++)
            {
                singleBrokenPattern.Append(Regex.Escape(tags[k].FullTag));

                /*
                 * The main idea here is that the number of opening tags must be equal to the number of closing tag.
                 */
                if (tags[k].IsClosingTag)
                {
                    for (int kk = brokenStack.Count - 1; kk >= 0; kk--)
                    {
                        if (string.Compare(brokenStack[kk], tags[k].Name.Substring(1)) == 0)
                        {
                            brokenStack.RemoveAt(kk);
                            break;
                        }
                    }
                }
                else
                {
                    brokenStack.Add(tags[k].Name);
                }

                if (brokenStack.Count > 0)
                {
                    singleBrokenPattern.Append(".*?");
                }
                else
                {
                    initialIndexInTags = k;
                    break;
                }
            }

            return singleBrokenPattern.ToString();
        }
    }
}
