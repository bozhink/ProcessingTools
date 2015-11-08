namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using Contracts;
    using System.Collections.Generic;

    public class TaxaRankResolverComplexResult : IComplexResult<IEnumerable<ITaxonRank>, IEnumerable<TaxaRankResolverException>>
    {
        public IEnumerable<TaxaRankResolverException> Error { get; set; }

        public IEnumerable<ITaxonRank> Result { get; set; }
    }
}