namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System.Collections.Generic;

    public interface ITaxaRankResolver
    {
        ITaxonRank Resolve(string scientificName);

        IEnumerable<ITaxonRank> Resolve(IEnumerable<string> scientificNames);
    }
}
