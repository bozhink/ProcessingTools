namespace ProcessingTools.Special.Processors.Processors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Processors.Special;

    public class TestFeaturesProvider : ITestFeaturesProvider
    {
        public void ExtractSystemChecklistAuthority(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            foreach (var node in document.SelectNodes("//fields/taxon_authors_and_year/value[normalize-space(.)!='']"))
            {
                node.InnerText = Regex.Replace(node.InnerText, @"\s+and\s+", " &amp; ");
                node.InnerText = Regex.Replace(node.InnerText, @"(?<=[^,])\s+(?=\d)", ", ");
            }
        }

        public void MoveAuthorityTaxonNamePartToTaxonAuthorityTagInTaxPubTpNomenclaure(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            string xpath = "//tp:nomenclature[tp:taxon-authority][normalize-space(tp:taxon-authority)=''][tn[tn-part[@type='authority']]]";

            foreach (var node in document.SelectNodes(xpath))
            {
                XmlNode taxonAuthority = node.SelectSingleNode("tp:taxon-authority", document.NamespaceManager);

                XmlNode authority = node.SelectSingleNode("tn/tn-part[@type='authority'][position()=last()]");

                taxonAuthority.InnerText = authority.InnerText;
                authority.ParentNode.RemoveChild(authority);
            }
        }

        public void WrapEmptySuperscriptsInFootnoteXrefTag(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            string xpath = "//sup[normalize-space()='']";

            int counter = 0;

            foreach (var node in document.SelectNodes(xpath))
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

        public void RenumerateFootNotes(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var footnoteIds = new HashSet<string>(document.SelectNodes("//back//fn-group//fn/@id")
                .Cast<XmlNode>()
                .Select(n => n.InnerText))
                .ToArray();

            var reindexDictionary = new Dictionary<string, int>();
            for (int i = 0; i < footnoteIds.Length; ++i)
            {
                reindexDictionary.Add(footnoteIds[i], i + 1);
            }

            // Add label tags in fn
            foreach (var fn in document.SelectNodes("//back//fn-group//fn[not(label)]"))
            {
                try
                {
                    string id = fn.Attributes["id"]?.InnerText;

                    XmlElement label = document.XmlDocument.CreateElement("label");
                    label.InnerText = reindexDictionary[id].ToString();

                    fn.PrependChild(label);
                }
                catch
                {
                    // Skip
                }
            }

            // Update the content of xref[@ref-type='fn']
            foreach (var xref in document.SelectNodes("//xref[@ref-type='fn'][name(..)!='contrib']"))
            {
                try
                {
                    string id = xref.Attributes["rid"]?.InnerText;
                    xref.InnerText = reindexDictionary[id].ToString();
                }
                catch
                {
                    // Skip
                }
            }

            // Update fn/@id
            foreach (var id in document.SelectNodes("//back//fn-group//fn/@id"))
            {
                string key = id.InnerText;
                id.InnerText = $"FN{reindexDictionary[key]}";
            }

            // Update xref[@ref-type='fn']/@rid
            foreach (var rid in document.SelectNodes("//xref[@ref-type='fn'][name(..)!='contrib']/@rid"))
            {
                string key = rid.InnerText;
                rid.InnerText = $"FN{reindexDictionary[key]}";
            }
        }
    }
}
