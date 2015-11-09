namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System.Collections.Generic;
    using System.Linq;

    public class AboveGenusTaxaRankResolver : ITaxaRankResolver
    {
        private const string Rank = "above-genus";

        public TaxaRankResolverInternalResult Resolve(IEnumerable<string> scientificNames)
        {
            return new TaxaRankResolverInternalResult
            {
                Results = new HashSet<ITaxonRank>(scientificNames.Select(this.Resolve)),
                Exceptions = null
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
