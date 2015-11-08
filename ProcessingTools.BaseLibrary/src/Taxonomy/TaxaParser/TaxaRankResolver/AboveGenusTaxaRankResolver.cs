namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AboveGenusTaxaRankResolver : ITaxaRankResolver
    {
        private const string Rank = "above-genus";

        public TaxaRankResolverComplexResult Resolve(IEnumerable<string> scientificNames)
        {
            return new TaxaRankResolverComplexResult
            {
                Result = new HashSet<ITaxonRank>(scientificNames.Select(this.Resolve)),
                Error = null
            };
        }

        public ITaxonRank Resolve(string scientificName)
        {
            return new SimpleTaxonRank
            {
                ScientificName = scientificName,
                Rank = AboveGenusTaxaRankResolver.Rank
            };
        }
    }
}
