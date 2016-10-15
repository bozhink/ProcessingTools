using System.Collections.Generic;

namespace ProcessingTools.Bio.Taxonomy.Processors.Models.Parsers
{
    using ProcessingTools.Bio.Taxonomy.Types;

    public interface ITaxonName
    {
        string Id { get; }

        long Position { get; }

        TaxonType Type { get; }

        ICollection<ITaxonNamePart> Parts { get; }
    }
}