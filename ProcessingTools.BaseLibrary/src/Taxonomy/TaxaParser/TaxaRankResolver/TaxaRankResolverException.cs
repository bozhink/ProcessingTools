namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using Contracts;

    public class TaxaRankResolverException : Exception
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

        public ExceptionSeverity Severity { get; set; }

        public ICollection<KeyValuePair<string, string>> Messages { get; set; }
    }
}