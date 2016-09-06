namespace ProcessingTools.DocumentProvider.Factories
{
    using System;
    using Contracts;

    public class TaxPubDocumentFactory : ITaxPubDocumentFactory
    {
        public ITaxPubDocument Create()
        {
            return new TaxPubDocument();
        }

        public ITaxPubDocument Create(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException(nameof(content));
            }

            return new TaxPubDocument(content);
        }
    }
}
