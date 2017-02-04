namespace ProcessingTools.Xml.Providers
{
    using System;
    using System.Xml;
    using Contracts.Providers;
    using ProcessingTools.Contracts;
    using Enumerations;

    public class DocumentWrapperProvider : IDocumentWrapperProvider
    {
        private readonly IDocumentFactory documentFactory;

        public DocumentWrapperProvider(IDocumentFactory documentFactory)
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
