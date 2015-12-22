namespace ProcessingTools.BaseLibrary
{
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml;

    using Bio.Harvesters.Contracts;

    using Configurator;
    using Contracts;
    using Models;

    public class Environments : TaggerBase, IBaseTagger
    {
        private const string EnvoTagName = "envo";
        private IEnvoTermsHarvester harvester;

        public Environments(string xml, IEnvoTermsHarvester harvester)
            : base(xml)
        {
            this.harvester = harvester;
        }

        public Environments(Config config, string xml, IEnvoTermsHarvester harvester)
            : base(config, xml)
        {
            this.harvester = harvester;
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
                this.harvester.Harvest(this.TextContent);
                var terms = this.harvester.Data
                    .Select(t => new EnvoTermResponseModel
                    {
                        EntityId = t.EntityId,
                        EnvoId = t.EnvoId,
                        Content = t.Content
                    })
                    .ToList();

                XmlNodeList nodeList = this.XmlDocument.SelectNodes(xpath, this.NamespaceManager);
                foreach (var term in terms)
                {
                    testXmlNode.InnerText = term.Content;
                    term.Content = testXmlNode.InnerXml;

                    string envoOpenTag = "<" + EnvoTagName +
                        @" EnvoTermUri=""" + term.Uri + @"""" +
                        @" ID=""" + term.EntityId + @"""" +
                        @" EnvoID=""" + term.EnvoId + @"""" +
                        @" VerbatimTerm=""" + term.Content + @"""" +
                        @">";

                    string envoCloseTag = "</" + EnvoTagName + ">";

                    foreach (XmlNode node in nodeList)
                    {
                        string pattern = "\\b((?i)" + Regex.Escape(term.Content).Replace("'", "\\W") + ")\\b";
                        if (Regex.IsMatch(node.InnerXml, pattern))
                        {
                            // The text content of the current node contains this environment string
                            pattern = "(?<!<[^>]+)" + pattern + "(?![^<>]*>)";
                            string replace = node.InnerXml;

                            if (!Regex.IsMatch(node.InnerXml, pattern))
                            {
                                /*
                                 * The xml-as-string content of the current node does not contain this environment string.
                                 * Here we suppose that there is some tag inside the environment-string in the xml node.
                                 */

                                // Tag the xml-node-content using non-regex skip-tag matches
                                replace = node.InnerXml.TagNodeContent(term.Content, envoOpenTag);

                                XmlNode envoNode = this.XmlDocument.CreateElement(EnvoTagName);
                                replace = replace.TagOrderNormalizer(envoNode);
                            }
                            else
                            {
                                replace = Regex.Replace(node.InnerXml, pattern, envoOpenTag + "$1" + envoCloseTag);
                            }

                            node.InnerXml = replace;
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
