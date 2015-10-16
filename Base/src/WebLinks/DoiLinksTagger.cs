namespace ProcessingTools.BaseLibrary.HyperLinks
{
    using System.Text.RegularExpressions;
    using Configurator;
    using Globals;

    public class DoiLinksTagger : Base, IBaseTagger
    {
        public DoiLinksTagger(Config config, string xml)
            : base(config, xml)
        {
        }

        public DoiLinksTagger(IBase baseObject)
            : base(baseObject)
        {
        }

        private void TagDoi()
        {
            string xml = this.Xml;

            // Remove blanks around brackets spanning numbers
            xml = Regex.Replace(xml, @"(\d)\s(\(|\[)([A-Z0-9]+)(\]|\))\s(\d)", "$1$2$3$4$5");

            // Tag DOI
            ////xml = Regex.Replace(xml, @"doi:(\s*)([^,<\s]*[A-Za-z0-9])", "doi: <ext-link ext-link-type=\"uri\" xlink:href=\"http://dx.doi.org/$2\">$2</ext-link>");
            xml = Regex.Replace(xml, @"(?<!<ext-link [^>]*>)(\b[Dd][Oo][Ii]\b:?)\s*(\d+\.[^,<>\s]+[A-Za-z0-9]((?<=&[A-Za-z0-9#]+);)?)", "$1 <ext-link ext-link-type=\"doi\" xlink:href=\"$2\">$2</ext-link>");

            // Some format
            xml = Regex.Replace(xml, "(</source>)((?i)doi:?)", "$1 $2");

            this.Xml = xml;
        }

        private void TagPmcLinks()
        {
            string xml = this.Xml;

            // PMid
            xml = Regex.Replace(xml, @"(?i)(?<=\bpmid\W?)(\d+)", "<ext-link ext-link-type=\"pmid\" xlink:href=\"$1\">$1</ext-link>");

            // PMCid
            xml = Regex.Replace(xml, @"(?i)(pmc\W?(\d+))", "<ext-link ext-link-type=\"pmcid\" xlink:href=\"PMC$2\">$1</ext-link>");

            xml = Regex.Replace(xml, @"(?i)(?<=\bpmcid\W?)(\d+)", "<ext-link ext-link-type=\"pmcid\" xlink:href=\"PMC$1\">$1</ext-link>");

            this.Xml = xml;
        }

        public void Tag()
        {
            this.TagDoi();
            this.TagPmcLinks();
        }

        public void Tag(IXPathProvider xpathProvider)
        {
            this.Tag();
        }
    }
}