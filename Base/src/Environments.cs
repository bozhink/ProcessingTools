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
                                    string pattern = "\\b((?i)" + Regex.Replace(Regex.Escape(reader.GetString(0)), "'", "\\W") + ")\\b";
                                    if (Regex.Match(node.InnerText, pattern).Success)
                                    {
                                        pattern = "(?<!<[^>]+)" + pattern + "(?![^<>]*>)";
                                        if (!Regex.Match(node.InnerXml, pattern).Success)
                                        {
                                            Alert.Message("\n\n");
                                            Alert.Message(node.InnerXml);
                                            Alert.Message();
                                            Alert.Message(TagNodeContent(node.InnerXml, Regex.Escape(reader.GetString(0)), "<envo>"));
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
            string closeTag = "</" + Regex.Match(openTag, @"(?<=<)[^\s>/""']+").Value + ">";
            openTag = Regex.Replace(openTag, "/", "");

            openTag = "[envo]";
            closeTag = "[/envo]";

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
        public string TagOrderNormalizer(string text)
        {
            StringBuilder sb = new StringBuilder(), charStack = new StringBuilder();



            return sb.ToString();
        }
    }
}
