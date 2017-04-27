namespace ProcessingTools.Contracts.Models.Bio.Taxonomy
{
    using System.Collections.Generic;

    public interface ITaxonClassification : IExtendedTaxonRank
    {
        ICollection<ITaxonRank> Classification { get; }
    }
}
