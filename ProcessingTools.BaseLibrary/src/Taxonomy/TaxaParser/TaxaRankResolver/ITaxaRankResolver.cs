namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System.Collections.Generic;

    public interface ITaxaRankResolver
    {
        ITaxonRank Resolve(string scientificName);

        TaxaRankResolverInternalResult Resolve(IEnumerable<string> scientificNames);
    }
}
