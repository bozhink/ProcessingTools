namespace ProcessingTools.BaseLibrary
{
    using System;
    using System.Data.SqlClient;
    using System.Text.RegularExpressions;
    using System.Xml;

    using Configurator;
    using Contracts;
    using Extensions;
    using ProcessingTools.Contracts.Log;

    public class DataProvider : HarvestableDocument, IDataProvider
    {
        private ILogger logger;

        public DataProvider(Config config, string xml, ILogger logger)
            : base(config, xml)
        {
            this.logger = logger;
        }

        public DataProvider(IBase baseObject, ILogger logger)
            : base(baseObject)
        {
            this.logger = logger;
        }

        public void ExecuteSimpleReplaceUsingDatabase(string xpath, string query, string tagName, bool caseSensitive = false)
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

            try
            {
                XmlNodeList nodeList = this.XmlDocument.SelectNodes(xpath, this.NamespaceManager);

                string connectionString = this.Config.MainDictionaryDataSourceString;
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                using (connection)
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        try
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    XmlElement element = this.XmlDocument.CreateElement(tagName);
                                    SetTagAttributes(element, reader);

                                    element.InnerText = "$1";
                                    string replacement = element.OuterXml;

                                    element.InnerText = reader.GetString(0);
                                    string contentString = Regex.Escape(element.InnerXml).RegexReplace("'", "\\W");

                                    string pattern = string.Format(patternTemplate, contentString);

                                    Regex matchEntry = new Regex(pattern);
                                    foreach (XmlNode node in nodeList)
                                    {
                                        if (matchEntry.IsMatch(node.InnerText))
                                        {
                                            node.InnerXml = matchEntry.Replace(node.InnerXml, replacement);
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            this.logger?.Log(e, string.Empty);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        private static void SetTagAttributes(XmlNode node, SqlDataReader reader)
        {
            node.Attributes.RemoveAll();
            if (reader.FieldCount > 1)
            {
                for (int i = 1, len = reader.FieldCount; i < len; ++i)
                {
                    XmlAttribute attribute = node.OwnerDocument.CreateAttribute(reader.GetName(i));
                    attribute.InnerText = reader.GetString(i);
                    node.Attributes.Append(attribute);
                }
            }
        }
    }
}
