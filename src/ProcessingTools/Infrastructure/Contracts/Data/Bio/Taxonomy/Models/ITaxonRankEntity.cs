namespace ProcessingTools.Contracts.Data.Bio.Taxonomy.Models
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Enumerations;

    public interface ITaxonRankEntity : INameable
    {
        bool IsWhiteListed { get; }

        ICollection<TaxonRankType> Ranks { get; }
    }
}
