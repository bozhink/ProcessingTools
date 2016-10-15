namespace ProcessingTools.Bio.Taxonomy.Processors.Models.Parsers
{
    using ProcessingTools.Bio.Taxonomy.Types;

    public interface ITaxonNamePart
    {
        string FullName { get; set; }

        string Id { get; }

        bool IsAbbreviated { get; }

        bool IsModified { get; set; }

        string Name { get; }

        SpeciesPartType Rank { get; }
    }
}
