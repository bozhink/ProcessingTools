namespace ProcessingTools.Processors.Contracts.Models.Bio.Taxonomy.Parsers
{
    using ProcessingTools.Enumerations;

    internal interface IMinimalTaxonNamePart
    {
        string FullName { get; set; }

        string Name { get; }

        SpeciesPartType Rank { get; }

        int ContentHash { get; }
    }
}
