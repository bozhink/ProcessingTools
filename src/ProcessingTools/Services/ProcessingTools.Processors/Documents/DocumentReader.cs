// <copyright file="DocumentReader.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Documents
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Processors.Contracts.Documents;
    using ProcessingTools.Services.Contracts.Files;

    /// <summary>
    /// Document reader.
    /// </summary>
    public class DocumentReader : IDocumentReader
    {
        private readonly IDocumentFactory documentFactory;
        private readonly IXmlFileContentDataService filesManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentReader"/> class.
        /// </summary>
        /// <param name="documentFactory">Document factory.</param>
        /// <param name="filesManager">File manager.</param>
        public DocumentReader(IDocumentFactory documentFactory, IXmlFileContentDataService filesManager)
        {
            this.documentFactory = documentFactory ?? throw new ArgumentNullException(nameof(documentFactory));
            this.filesManager = filesManager ?? throw new ArgumentNullException(nameof(filesManager));
        }

        /// <inheritdoc/>
        public async Task<IDocument> ReadDocumentAsync(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            var xmldocument = await this.filesManager.ReadXmlFile(fileName).ConfigureAwait(false);

            var document = this.documentFactory.Create(xmldocument.OuterXml);
            switch (document.XmlDocument.DocumentElement.Name)
            {
                case ElementNames.Article:
                    document.SchemaType = SchemaType.Nlm;
                    break;

                default:
                    document.SchemaType = SchemaType.System;
                    break;
            }

            return document;
        }
    }
}
