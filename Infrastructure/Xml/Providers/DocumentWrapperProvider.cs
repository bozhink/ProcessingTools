namespace ProcessingTools.Xml.Providers
{
    using System;
    using Contracts.Providers;
    using ProcessingTools.Contracts;

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
    }
}
