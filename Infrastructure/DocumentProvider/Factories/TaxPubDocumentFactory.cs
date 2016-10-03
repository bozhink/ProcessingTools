namespace ProcessingTools.DocumentProvider.Factories
{
    using System;
    using Contracts;
    using ProcessingTools.Contracts;

    public class TaxPubDocumentFactory : ITaxPubDocumentFactory
    {
        public IDocument Create()
        {
            return new TaxPubDocument();
        }

        public IDocument Create(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException(nameof(content));
            }

            return new TaxPubDocument(content);
        }
    }
}
