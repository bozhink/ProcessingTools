namespace ProcessingTools.BaseLibrary
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml;

    public class Test : ConfigurableDocument
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

        public void WrapEmptySuperscriptsInFootnoteXrefTag()
        {
            string xpath = "//sup[normalize-space()='']";

            int counter = 0;

            foreach (XmlNode node in this.XmlDocument.SelectNodes(xpath, this.NamespaceManager))
            {
                XmlElement sup = (XmlElement)node.CloneNode(true);
                sup.InnerText = $"{++counter}";

                XmlElement xref = node.OwnerDocument.CreateElement("xref");
                xref.SetAttribute("ref-type", "fn");
                xref.SetAttribute("rid", $"FN{counter}");
                xref.AppendChild(sup);

                node.ParentNode.ReplaceChild(xref, node);
            }
        }

        public void RenumerateFootNotes()
        {
            var footnoteIds = new HashSet<string>(this.XmlDocument.SelectNodes("//back//fn-group//fn/@id")
                .Cast<XmlNode>()
                .Select(n => n.InnerText))
                .ToArray();

            var reindexDictionary = new Dictionary<string, int>();
            for (int i = 0; i < footnoteIds.Length; ++i)
            {
                reindexDictionary.Add(footnoteIds[i], i + 1);
            }

            // Add label tags in fn
            foreach (XmlElement fn in this.XmlDocument.SelectNodes("//back//fn-group//fn[not(label)]"))
            {
                try
                {
                    string id = fn.Attributes["id"]?.InnerText;

                    XmlElement label = this.XmlDocument.CreateElement("label");
                    label.InnerText = reindexDictionary[id].ToString();

                    fn.PrependChild(label);
                }
                catch
                {
                }
            }

            // Update the content of xref[@ref-type='fn']
            foreach (XmlElement xref in this.XmlDocument.SelectNodes("//xref[@ref-type='fn'][name(..)!='contrib']"))
            {
                try
                {
                    string id = xref.Attributes["rid"]?.InnerText;
                    xref.InnerText = reindexDictionary[id].ToString();
                }
                catch
                {
                }
            }

            // Update fn/@id
            foreach (XmlAttribute id in this.XmlDocument.SelectNodes("//back//fn-group//fn/@id"))
            {
                string key = id.InnerText;
                id.InnerText = $"FN{reindexDictionary[key]}";
            }

            // Update xref[@ref-type='fn']/@rid
            foreach (XmlAttribute rid in this.XmlDocument.SelectNodes("//xref[@ref-type='fn'][name(..)!='contrib']/@rid"))
            {
                string key = rid.InnerText;
                rid.InnerText = $"FN{reindexDictionary[key]}";
            }


        }
    }
}
