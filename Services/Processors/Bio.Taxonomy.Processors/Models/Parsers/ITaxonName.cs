namespace ProcessingTools.Bio.Taxonomy.Processors.Models.Parsers
{
    using System.Linq;
    using ProcessingTools.Bio.Taxonomy.Types;

    internal interface ITaxonName
    {
        string Id { get; }

        long Position { get; }

        TaxonType Type { get; }

        IQueryable<ITaxonNamePart> Parts { get; }
    }
}
