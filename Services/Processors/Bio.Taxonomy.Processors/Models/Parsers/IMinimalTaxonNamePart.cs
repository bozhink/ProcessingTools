namespace ProcessingTools.Bio.Taxonomy.Processors.Models.Parsers
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
