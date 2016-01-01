namespace ProcessingTools.BaseLibrary
{
    using System.Text.RegularExpressions;
    using System.Xml;

    public class Test : Base
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

        public void MoveAuthorityTaxonNamePartToTaxonAuthorityTagInTaxPubTpNomenclaure()
        {
            string xpath = "//tp:nomenclature[tp:taxon-authority][normalize-space(tp:taxon-authority)=''][tn[tn-part[@type='authority']]]";

            foreach (XmlNode node in this.XmlDocument.SelectNodes(xpath, this.NamespaceManager))
            {
                XmlNode taxonAuthority = node.SelectSingleNode("tp:taxon-authority", this.NamespaceManager);

                XmlNode authority = node.SelectSingleNode("tn/tn-part[@type='authority'][position()=last()]", this.NamespaceManager);

                taxonAuthority.InnerText = authority.InnerText;
                authority.ParentNode.RemoveChild(authority);
            }
        }
    }
}
