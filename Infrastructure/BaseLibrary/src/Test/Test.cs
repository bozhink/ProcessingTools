namespace ProcessingTools.BaseLibrary
{
    using System.Text.RegularExpressions;
    using System.Xml;

    public class Test : TaggerBase
    {
        public Test(string xml)
            : base(xml)
        {
        }

        public void ExtractSystemChecklistAuthority()
        {
            foreach (XmlNode node in this.XmlDocument.SelectNodes("//fields/taxon_authors_and_year/value[normalize-space(.)!='']", this.NamespaceManager))
            {
                node.InnerText = Regex.Replace(node.InnerText, @"\s+and\s+", " &amp; ");
                node.InnerText = Regex.Replace(node.InnerText, @"(?<=[^,])\s+(?=\d)", ", ");
            }
        }
    }
}
