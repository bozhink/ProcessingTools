namespace ProcessingTools.Processors.Contracts.Models.Bio.Taxonomy.Parsers
{
    using System.Linq;
    using ProcessingTools.Enumerations;

    internal interface ITaxonName
    {
        string Id { get; }

        long Position { get; }

        TaxonType Type { get; }

        IQueryable<ITaxonNamePart> Parts { get; }
    }
}
