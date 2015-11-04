namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System.Collections.Generic;
    using System.Linq;

    public class AboveGenusTaxaRankResolver : ITaxaRankResolver
    {
        private const string Rank = "above-genus";

        public IEnumerable<ITaxonRank> Resolve(IEnumerable<string> scientificNames)
        {
            return new HashSet<ITaxonRank>(scientificNames.Select(this.Resolve));
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
