namespace ProcessingTools.Bio.Taxonomy.Data.Common.Models.Contracts
{
    using System.Collections.Generic;
    using ProcessingTools.Bio.Taxonomy.Types;
    using ProcessingTools.Contracts;

    public interface ITaxonRankEntity : INameable
    {
        bool IsWhiteListed { get; }

        ICollection<TaxonRankType> Ranks { get; }
    }
}
