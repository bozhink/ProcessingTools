namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System;
    using System.Collections.Generic;

    public class AboveGenusTaxaRankResolver : ITaxaRankResolver
    {
        private const string Rank = "above-genus";

        public IEnumerable<ITaxonRank> Resolve(IEnumerable<string> scientificNames)
        {
            throw new NotImplementedException();
        }

        public ITaxonRank Resolve(string scientificName)
        {
            throw new NotImplementedException();
        }
    }
}
