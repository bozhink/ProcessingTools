namespace ProcessingTools.Xml.Wrappers
{
    using System;
    using System.Xml;
    using Contracts.Wrappers;
    using ProcessingTools.Contracts;
    using ProcessingTools.Enumerations;

    public class DocumentWrapper : IDocumentWrapper
    {
        private readonly IDocumentFactory documentFactory;

        public DocumentWrapper(IDocumentFactory documentFactory)
        {
            if (documentFactory == null)
            {
                throw new ArgumentNullException(nameof(documentFactory));
            }

            this.documentFactory = documentFactory;
        }

        public IDocument Create()
        {
            return this.documentFactory.Create(Resources.DocumentWrapper);
        }

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
