namespace ProcessingTools.BaseLibrary
{
    using System.Data.SqlClient;
    using System.Text.RegularExpressions;
    using System.Xml;
    using Configurator;
    using Globals;

    public class Environments : TaggerBase, IBaseTagger
    {
        public Environments(string xml)
            : base(xml)
        {
        }

        public Environments(Config config, string xml)
            : base(config, xml)
        {
        }

        public void Tag()
        {
            XmlDocumentFragment testXmlNode = this.XmlDocument.CreateDocumentFragment();

            // Set the XPath string to select all nodes which may contain environments’ strings
            string xpath = string.Empty;
            switch (this.Config.ArticleSchemaType)
            {
                case SchemaType.Nlm:
                    xpath = "//p|//title|//label|//tp:nomenclature-citation";
                    break;

                default:
                    // TODO
                    xpath = "//p";
                    break;
            }

            try
            {
                // Select all nodes which potentioaly contains environments’ records
                XmlNodeList nodeList = this.XmlDocument.SelectNodes(xpath, this.NamespaceManager);

                // Connect to Environments database and use its records to tag Xml
                using (SqlConnection connection = new SqlConnection(this.Config.environmentsDataSourceString))
                {
                    connection.Open();

                    string query = @"SELECT * FROM [EnvironmentsDatabase].[Environments].[Envo_Terms_View]";

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
                                            //// The xml-as-string content of the current node soes not contain this environment string
                                            //// Here we suppose that there is some tag inside the environment-string in the xml node.

                                            // Tag the xml-node-content using non-regex skip-tag matches
                                            replace = node.InnerXml.TagNodeContent(contentString, envoOpenTag);

                                            XmlNode envoNode = this.XmlDocument.CreateElement(envoTagName);
                                            replace = replace.TagOrderNormalizer(envoNode);
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
                }
            }
            catch
            {
                throw;
            }

            this.XmlDocument.InnerXml = Regex.Replace(this.XmlDocument.InnerXml, @"(?<=\sEnvoTermUri="")", "http://purl.obolibrary.org/obo/");
        }

        public void Tag(IXPathProvider xpathProvider)
        {
            this.Tag();
        }
    }
}
