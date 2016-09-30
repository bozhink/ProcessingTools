namespace ProcessingTools.DocumentProvider.Extensions
{
    using System;
    using System.Xml;

    using Contracts;

    public static class XmlExtensions
    {
        public static ITaxPubDocument ToTaxPubDocument(this XmlDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var result = new TaxPubDocument(document);
            return result;
        }
    }
}
