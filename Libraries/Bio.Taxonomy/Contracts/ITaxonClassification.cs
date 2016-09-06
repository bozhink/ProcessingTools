namespace ProcessingTools.Bio.Taxonomy.Contracts
{
    using System.Collections.Generic;

    public interface ITaxonClassification : IExtendedTaxonRank
    {
        ICollection<ITaxonRank> Classification { get; }
    }
}