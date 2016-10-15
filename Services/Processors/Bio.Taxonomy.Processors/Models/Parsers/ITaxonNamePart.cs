namespace ProcessingTools.Bio.Taxonomy.Processors.Models.Parsers
{
    public interface ITaxonNamePart
    {
        string FullName { get; set; }

        string Id { get; }

        bool IsAbbreviated { get; }

        bool IsModified { get; set; }

        string Name { get; }

        string Rank { get; }
    }
}
