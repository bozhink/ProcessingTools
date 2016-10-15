using System.Collections.Generic;

namespace ProcessingTools.Bio.Taxonomy.Processors.Models.Parsers
{
    public interface ITaxonName
    {
        string Id { get; }

        long Position { get; }

        string Type { get; }

        ICollection<ITaxonNamePart> Parts { get; }
    }
}