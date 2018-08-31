// <copyright file="TableFootnotesTagger.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Processors.Floats
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Xml;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Common.Constants.Schema;
    using ProcessingTools.Processors.Contracts.Floats;

    /// <summary>
    /// Table footnotes tagger.
    /// </summary>
    public class TableFootnotesTagger : ITableFootnotesTagger
    {
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TableFootnotesTagger"/> class.
        /// </summary>
        /// <param name="logger">Logger</param>
        public TableFootnotesTagger(ILogger<TableFootnotesTagger> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public Task<object> TagAsync(XmlNode context) => Task.Run(() => this.TagSync(context));

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
                this.logger.LogWarning("There are no table-wrap nodes with correctly formatted footnotes: table-wrap-foot/fn[@id][label]");

                return false;
            }

            this.logger.LogDebug("Number of correctly formatted table-wrap-s: {0}", tableWrapList.Count);

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
            xref.SetAttribute(AttributeNames.RefType, AttributeValues.RefTypeTableFootnote);
            xref.SetAttribute(AttributeNames.ReferenceId, id);
            xref.InnerXml = footnoteCitation.OuterXml;

            footnoteCitation.ParentNode.ReplaceChild(xref, footnoteCitation);
        }
    }
}
