namespace ProcessingTools.BaseLibrary.Floats
{
    using System.Collections;
    using System.Threading.Tasks;
    using System.Xml;

    using ProcessingTools.Contracts;
    using ProcessingTools.DocumentProvider;

    public class TableFootNotesTagger : TaxPubDocument, ITagger
    {
        private ILogger logger;

        public TableFootNotesTagger(string xml, ILogger logger)
            : base(xml)
        {
            this.logger = logger;
        }

        public Task Tag() => Task.Run(() => this.TagSync());

        private object TagSync()
        {
            // Get list of table-wrap with correctly formatted foot-notes
            XmlNodeList tableWrapList = this.XmlDocument.SelectNodes("//table-wrap[table-wrap-foot[fn[label][@id]]]", this.NamespaceManager);
            if (tableWrapList.Count < 1)
            {
                this.logger?.Log("There are no table-wrap nodes with correctly formatted footnotes: table-wrap-foot/fn[@id][label]");
                return false;
            }

            this.logger?.Log("Number of correctly formatted table-wrap-s: {0}", tableWrapList.Count);
            foreach (XmlNode tableWrap in tableWrapList)
            {
                Hashtable tableFootnotes = new Hashtable();

                // Get foot-note's label and corresponding @id-s
                foreach (XmlNode fn in tableWrap.SelectNodes(".//fn[label][@id]", this.NamespaceManager))
                {
                    tableFootnotes.Add(fn["label"].InnerText.Trim(), fn.Attributes["id"].Value.Trim());
                }

                foreach (string tableFootnoteKey in tableFootnotes.Keys)
                {
                    foreach (XmlNode footnoteSup in tableWrap.SelectNodes(".//table//sup[normalize-space(.)='" + tableFootnoteKey + "']", this.NamespaceManager))
                    {
                        this.TagCitationInXref(tableFootnotes, tableFootnoteKey, footnoteSup);
                    }
                }
            }

            return true;
        }

        private void TagCitationInXref(Hashtable tableFootnotes, string tableFootnoteKey, XmlNode footnoteSup)
        {
            XmlNode xrefTableFootNote = footnoteSup.OwnerDocument.CreateElement("xref");

            XmlAttribute refType = footnoteSup.OwnerDocument.CreateAttribute("ref-type");
            refType.InnerXml = "table-fn";
            xrefTableFootNote.Attributes.Append(refType);

            XmlAttribute rid = footnoteSup.OwnerDocument.CreateAttribute("rid");
            rid.InnerXml = tableFootnotes[tableFootnoteKey].ToString();
            xrefTableFootNote.Attributes.Append(rid);

            xrefTableFootNote.InnerXml = footnoteSup.OuterXml;

            footnoteSup.ParentNode.ReplaceChild(xrefTableFootNote, footnoteSup);
        }
    }
}