namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System.Collections.Generic;
    using Contracts;

    public class TaxaRankResolverInternalResult : IInternalResultBag<ITaxonRank, TaxaRankResolverException>
    {
        public ICollection<TaxaRankResolverException> Exceptions { get; set; }

        public ICollection<ITaxonRank> Results { get; set; }
    }
}