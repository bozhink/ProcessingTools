namespace ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Models
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts;
    using ProcessingTools.Enumerations;

    public interface ITaxonRankEntity : INameable
    {
        bool IsWhiteListed { get; }

        ICollection<TaxonRankType> Ranks { get; }
    }
}
