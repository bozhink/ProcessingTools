namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System.Collections.Generic;

    public interface ITaxaRankResolver
    {
        ITaxonRank Resolve(string scientificName);

        TaxaRankResolverComplexResult Resolve(IEnumerable<string> scientificNames);
    }
}
