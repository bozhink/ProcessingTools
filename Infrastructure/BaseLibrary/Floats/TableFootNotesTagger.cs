namespace ProcessingTools.BaseLibrary.Floats
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Xml;

    using ProcessingTools.Contracts;
    using ProcessingTools.DocumentProvider;
    using ProcessingTools.Nlm.Publishing.Constants;

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
            XmlNodeList tableWrapList = this.XmlDocument.SelectNodes(".//table-wrap[table-wrap-foot[fn[label][@id]]]");
            if (tableWrapList.Count < 1)
            {
                this.logger?.Log("There are no table-wrap nodes with correctly formatted footnotes: table-wrap-foot/fn[@id][label]");
                return false;
            }

            this.logger?.Log("Number of correctly formatted table-wrap-s: {0}", tableWrapList.Count);
            foreach (XmlNode tableWrap in tableWrapList)
            {
                var tableFootnotes = new Dictionary<string, string>();

                // Get foot-note's label and corresponding @id-s
                foreach (XmlNode footnote in tableWrap.SelectNodes("./table-wrap-foot/fn[label][@id]"))
                {
                    var label = footnote[ElementNames.Label].InnerText.Trim();
                    var id = footnote.Attributes[AttributeNames.Id].Value.Trim();

                    tableFootnotes.Add(label, id);
                }

                foreach (string label in tableFootnotes.Keys)
                {
                    string xpath = $".//table//sup[normalize-space(.)='{label}']";

                    foreach (XmlNode footnoteCitation in tableWrap.SelectNodes(xpath))
                    {
                        this.WrapCitationInXref(tableFootnotes[label], footnoteCitation);
                    }
                }
            }

            return true;
        }

        private void WrapCitationInXref(string id, XmlNode footnoteCitation)
        {
            var xref = footnoteCitation.OwnerDocument.CreateElement(ElementNames.XRef);
            xref.SetAttribute(AttributeNames.RefType, RefTypeAttributeValues.TableFn);
            xref.SetAttribute(AttributeNames.RId, id);
            xref.InnerXml = footnoteCitation.OuterXml;

            footnoteCitation.ParentNode.ReplaceChild(xref, footnoteCitation);
        }
    }
}