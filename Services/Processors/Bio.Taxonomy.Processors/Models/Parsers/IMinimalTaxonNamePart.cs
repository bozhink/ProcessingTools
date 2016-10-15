namespace ProcessingTools.Bio.Taxonomy.Processors.Models.Parsers
{
    using ProcessingTools.Bio.Taxonomy.Types;

    internal interface IMinimalTaxonNamePart
    {
        string FullName { get; set; }

        SpeciesPartType Rank { get; }
    }
}
