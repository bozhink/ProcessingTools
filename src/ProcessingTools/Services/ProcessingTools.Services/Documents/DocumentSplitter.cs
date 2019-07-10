// <copyright file="DocumentSplitter.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services;
using ProcessingTools.Contracts.Services.Documents;

namespace ProcessingTools.Services.Documents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ProcessingTools.Common.Constants.Schema;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Document splitter.
    /// </summary>
    public class DocumentSplitter : IDocumentSplitter
    {
        private readonly IDocumentFactory documentFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentSplitter"/> class.
        /// </summary>
        /// <param name="documentFactory">Document factory.</param>
        public DocumentSplitter(IDocumentFactory documentFactory)
        {
            this.documentFactory = documentFactory ?? throw new ArgumentNullException(nameof(documentFactory));
        }

        /// <inheritdoc/>
        public IEnumerable<IDocument> Split(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            return document.SelectNodes(XPathStrings.HigherDocumentStructure)
                .Select(n => this.documentFactory.Create(n.OuterXml));
        }
    }
}
