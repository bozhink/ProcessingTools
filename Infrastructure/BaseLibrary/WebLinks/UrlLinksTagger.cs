namespace ProcessingTools.BaseLibrary.HyperLinks
{
    using System.Text.RegularExpressions;
    using System.Xml;

    using Configurator;
    using Contracts;

    public class UrlLinksTagger : Base, IBaseTagger
    {
        public UrlLinksTagger(Config config, string xml)
            : base(config, xml)
        {
        }

        public UrlLinksTagger(IBase baseObject)
            : base(baseObject)
        {
        }

        public void Tag()
        {
            this.TagWebLinks();
            this.TagIPAddresses();
        }

        private void TagIPAddresses()
        {
            string xml = this.Xml;

            // Tag IP addresses
            xml = Regex.Replace(xml, "(?<!<[^>]+)(http(s?)://((\\d{1,3}\\.){3,3}\\d{1,3})(:\\d+)?(/[^<>\n\"\\s]*[A-Za-z0-9/])?)", "<ext-link ext-link-type=\"uri\" xlink:href=\"$1\">$1</ext-link>");
            xml = Regex.Replace(xml, "(?<!<[^>]+)((s?)ftp://((\\d{1,3}\\.){3,3}\\d{1,3})(:\\d+)?(/[^<>\n\"\\s]*[A-Za-z0-9/])?)", "<ext-link ext-link-type=\"ftp\" xlink:href=\"$1\">$1</ext-link>");

            this.Xml = xml;
        }

        private void TagWebLinks()
        {
            try
            {
                string xpath = "//p|//license-p|//li|//th|//td|//mixed-citation|//element-citation|//nlm-citation|//tp:nomenclature-citation";
                XmlNodeList nodeList = this.XmlDocument.SelectNodes(xpath, this.NamespaceManager);
                foreach (XmlNode node in nodeList)
                {
                    node.InnerXml = Regex.Replace(
                        node.InnerXml,
                        @"(?<!<[^>]+)(((http(s)?:\/\/)|(www\.))([\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?))",
                        "<ext-link ext-link-type=\"uri\" xlink:href=\"http$4://$5$6\">$1</ext-link>");
                }
            }
            catch
            {
                throw;
            }
        }
    }
}