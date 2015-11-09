namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System;
    using Infrastructure.Exceptions;

    public class TaxaRankResolverException : KeyValuePairMultiMessageException
    {
        public TaxaRankResolverException()
        {
        }

        public TaxaRankResolverException(string message)
            : base(message)
        {
        }

        public TaxaRankResolverException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}