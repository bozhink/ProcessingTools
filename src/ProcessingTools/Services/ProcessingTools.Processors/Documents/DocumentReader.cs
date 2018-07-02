// <copyright file="DocumentReader.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Documents
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Processors.Contracts.Documents;
    using ProcessingTools.Services.Contracts.IO;

    /// <summary>
    /// Document reader.
    /// </summary>
    public class DocumentReader : IDocumentReader
    {
        private readonly IDocumentFactory documentFactory;
        private readonly IXmlReadService reader;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentReader"/> class.
        /// </summary>
        /// <param name="documentFactory">Document factory.</param>
        /// <param name="reader">XML reader.</param>
        public DocumentReader(IDocumentFactory documentFactory, IXmlReadService reader)
        {
            this.documentFactory = documentFactory ?? throw new ArgumentNullException(nameof(documentFactory));
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }

        /// <inheritdoc/>
        public async Task<IDocument> ReadDocumentAsync(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            var xmldocument = await this.reader.ReadFileToXmlDocumentAsync(fileName).ConfigureAwait(false);

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
