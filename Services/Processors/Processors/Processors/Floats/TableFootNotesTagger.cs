namespace ProcessingTools.Processors.Floats
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;

    using ProcessingTools.Contracts;
    using ProcessingTools.Nlm.Publishing.Constants;

    public class TableFootNotesTagger : ITableFootNotesTagger
    {
        private readonly ILogger logger;

        public TableFootNotesTagger(ILogger logger)
        {
            this.logger = logger;
        }

        public Task<object> Tag(XmlNode context) => Task.Run(() => this.TagSync(context));

        private object TagSync(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // Get list of table-wrap with correctly formatted foot-notes
            XmlNodeList tableWrapList = context.SelectNodes(".//table-wrap[table-wrap-foot[fn[label][@id]]]");

            if (tableWrapList.Count < 1)
            {
                this.logger?.Log("There are no table-wrap nodes with correctly formatted footnotes: table-wrap-foot/fn[@id][label]");

                return false;
            }

            this.logger?.Log("Number of correctly formatted table-wrap-s: {0}", tableWrapList.Count);

            foreach (XmlNode tableWrap in tableWrapList)
            {
                var footnotes = this.GetFootnoteLabelAndIdPairsInTableWrap(tableWrap);

                foreach (string label in footnotes.Keys)
                {
                    string xpath = $".//table//sup[normalize-space(.)='{label}']";

                    foreach (XmlNode footnoteCitation in tableWrap.SelectNodes(xpath))
                    {
                        this.WrapCitationInXref(footnotes[label], footnoteCitation);
                    }
                }
            }

            return true;
        }

        private IDictionary<string, string> GetFootnoteLabelAndIdPairsInTableWrap(XmlNode tableWrap)
        {
            var footnotes = new Dictionary<string, string>();

            // Get foot-note's label and corresponding @id-s
            foreach (XmlNode footnote in tableWrap.SelectNodes("./table-wrap-foot/fn[label][@id]"))
            {
                var label = footnote[ElementNames.Label].InnerText.Trim();
                var id = footnote.Attributes[AttributeNames.Id].Value.Trim();

                footnotes.Add(label, id);
            }

            return footnotes;
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
