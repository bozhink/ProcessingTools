using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace Base
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

        public void TagEnvironmentsRecords()
        {
            xml = Format.Format.NormalizeNlmToSystemXml(config, xml);
            ParseXmlStringToXmlDocument();
            XmlElement testXmlNode = xmlDocument.CreateElement("test");

            // Set the XPath string to select all nodes which may contain environments’ strings
            string xpath;
            if (config.NlmStyle)
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
                XmlNodeList nodeList = xmlDocument.SelectNodes(xpath, namespaceManager);

                // Connect to Environments database and use its records to tag Xml
                using (SqlConnection connection = new SqlConnection(config.environmentsDataSourceString))
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
                                    string pattern = "\\b((?i)" + Regex.Replace(Regex.Escape(contentString), "'", "\\W") + ")\\b";
                                    if (Regex.Match(node.InnerText, pattern).Success)
                                    {
                                        pattern = "(?<!<[^>]+)" + pattern + "(?![^<>]*>)";
                                        if (!Regex.Match(node.InnerXml, pattern).Success)
                                        {
                                            Alert.Message("\n\n");
                                            Alert.Message(node.InnerXml);
                                            Alert.Message();
                                            string xx = TagNodeContent(node.InnerXml, Regex.Escape(contentString), "<envo>");
                                            Alert.Message(xx);
                                            Alert.Message("\n\n");
                                            Alert.Message(TagOrderNormalizer(xx, "envo"));
                                            Alert.Message("\n\n");
                                            //throw new Exception("InnerXml does not match whath InnerText does: \n\n''" +
                                            //    node.InnerXml + "''\n\n''" + reader.GetString(0) + "''");
                                        }
                                        string replace = Regex.Replace(node.InnerXml, pattern,
                                            //@"<envo id=""" + reader.GetInt32(1) + @""">$1</envo>");
                                            @"<envo id=""" + reader.GetInt32(1) + @""" envo-id=""" + reader.GetString(2) + @""">$1</envo>");
                                        //@"<envo>$1</envo>");
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


            xml = xmlDocument.OuterXml;
            if (config.NlmStyle)
            {
                xml = Format.Format.NormalizeSystemToNlmXml(config, xml);
            }
        }


        public string TagNodeContent(string text, string keyString, string openTag)
        {
            string tagName = Regex.Match(openTag, @"(?<=<)[^\s>/""']+").Value;
            string closeTag = "</" + tagName + ">";
            openTag = Regex.Replace(openTag, "/", "");

            //openTag = "[envo]";
            //closeTag = "[/envo]";

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

        /*
         * TODO
         */
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
            try
            {
                testXmlNode.InnerXml = text;
            }
            catch (XmlException e)
            {
                //
                Alert.Message("Invalid Xml");
                text = Regex.Replace(text,
                    @"(<([^>\s/]+)[^>/]*>)(.*?)(?!</\2>)(<" + tagName + @"\b[^>]*>)",
                    "$1$3</$2>$4$1");
                Alert.Message(text);
            }
            return testXmlNode.InnerXml;
        }

        public string TagOrderNormalizer1(string text)
        {
            Exception InvalidXml = new Exception("Invalid XML");
            StringBuilder sb = new StringBuilder(), charStack = new StringBuilder();
            List<string> stack = new List<string>();
            int len = text.Length;
            char ch;

            for (int i = 0; i < len; ++i)
            {
                ch = text[i];
                if (ch == '<')
                {
                    // tag begins
                    charStack.Clear();
                    charStack.Append(ch); // charStack "<"
                    try
                    {
                        ch = text[++i];
                        if (ch == '?')
                        {
                            // Processing Instruction
                            charStack.Append(ch); // charStack "<?"
                            ch = text[++i];
                            while (true)
                            {
                                charStack.Append(ch);
                                if ((text[++i] == '>') && (ch == '?')) // Do not change order of operands around &&
                                {
                                    break;
                                }
                                ch = text[i];
                            }  // charStack "<?....?"
                            // Here ch = '?' and text[i] = '>'
                            ch = text[i]; // Useless, but so we avoid errors
                            charStack.Append(ch); // charStack "<?....?>"
                            // Here ch = '>' and text[i] = '>'
                            sb.Append(charStack.ToString());
                        }
                        else if (ch == '!')
                        {
                            // comment, CDATA or DOCTYPE
                            charStack.Append(ch); // charStack "<!"
                            ch = text[++i];
                            if (ch == '-' && text[i + 1] == '-')
                            {
                                // Comment
                                do
                                {
                                    charStack.Append(ch);
                                    ch = text[++i];
                                } while (!((ch == '-') && (text[i - 1] == '-') && (text[i + 1] == '>')));
                                // charStack "<!--...-"
                                charStack.Append(ch); // charStack "<!--...--"
                                ch = text[++i];
                                charStack.Append(ch); // charStack "<!--...-->"

                                sb.Append(charStack.ToString());
                            }
                            else if (String.Compare(text.Substring(i, 7), "[CDATA[") == 0)
                            {
                                // <![CDATA[...]]>
                                do
                                {
                                    charStack.Append(ch);
                                    ch = text[++i];
                                } while (!((ch == ']') && (text[i - 1] == ']') && (text[i + 1] == '>')));
                                // charStack "<![CDATA[...]"
                                charStack.Append(ch); // charStack "<![CDATA[...]]"
                                ch = text[++i];
                                charStack.Append(ch); // charStack "<![CDATA[...]]>"

                                sb.Append(charStack.ToString());
                            }
                            else if (String.Compare(text.Substring(i, 7), "DOCTYPE") == 0)
                            {
                                // DOCTYPE
                                // ch == 'D'
                                List<char> st = new List<char>();
                                st.Clear();
                                while (true)
                                {
                                    charStack.Append(ch);
                                    ch = text[++i];
                                    if (ch == '<')
                                    {
                                        st.Add('<');
                                    }
                                    if (ch == '>')
                                    {
                                        int count = st.Count;
                                        if (count < 1)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            st.RemoveAt(count - 1);
                                        }
                                    }
                                }
                                ch = text[++i];
                                charStack.Append(ch);

                                sb.Append(charStack.ToString());
                            }
                        }
                        else
                        {
                            // Normal tag
                            if (ch != '/')
                            {
                                // Open tag
                                string tagName = Regex.Match(text.Substring(i), @"\A\S+").Value;
                                stack.Add(tagName);

                                do
                                {
                                    charStack.Append(ch);
                                    ch = text[++i];
                                } while (ch != '>');
                                charStack.Append(ch);

                                if (text[i - 1] == '/')
                                {
                                    // Tag is self-closed
                                    stack.RemoveAt(stack.Count - 1);
                                }
                            }
                            else
                            {
                                // Close tag
                                charStack.Append(ch);
                            }
                            sb.Append(charStack.ToString());
                        }
                    }
                    catch (Exception e)
                    {
                        Alert.Message("TagOrderNormalizer: Error parsing Xml or invalid Xml.\n\n" + e.Message);
                    }
                }
                else
                {
                    // other
                    sb.Append(ch);
                }
            }

            return sb.ToString();
        }
    }
}
