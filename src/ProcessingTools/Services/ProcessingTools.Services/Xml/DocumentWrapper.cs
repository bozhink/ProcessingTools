// <copyright file="DocumentWrapper.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Xml
{
    using System;
    using System.Xml;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Xml;
    using ProcessingTools.Enumerations;

    /// <summary>
    /// Document wrapper.
    /// </summary>
    public class DocumentWrapper : IDocumentWrapper
    {
        private readonly IDocumentFactory documentFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentWrapper"/> class.
        /// </summary>
        /// <param name="documentFactory">Document factory.</param>
        public DocumentWrapper(IDocumentFactory documentFactory)
        {
            this.documentFactory = documentFactory ?? throw new ArgumentNullException(nameof(documentFactory));
        }

        /// <inheritdoc/>
        public IDocument Create()
        {
            XmlDocument xmlDocument = new XmlDocument();
            var element = xmlDocument.CreateElement("document:document-wrapper", "urn:processing-tools-xml:document-wrapper");

            return this.documentFactory.Create(element.OuterXml);
        }

        /// <inheritdoc/>
        public IDocument Create(XmlNode context, SchemaType schemaType)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var document = this.Create();
            document.XmlDocument.DocumentElement.InnerXml = context.InnerXml;
            document.SchemaType = schemaType;
            return document;
        }
    }
}
