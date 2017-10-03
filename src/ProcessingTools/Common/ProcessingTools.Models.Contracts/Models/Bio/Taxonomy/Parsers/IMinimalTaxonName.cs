namespace ProcessingTools.Processors.Contracts.Models.Bio.Taxonomy.Parsers
{
    using System.Collections.Generic;

    public interface IMinimalTaxonName
    {
        IEnumerable<IMinimalTaxonNamePart> Parts { get; }
    }
}
