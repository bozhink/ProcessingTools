namespace ProcessingTools.Processors.Contracts.Models.Bio.Taxonomy.Parsers
{
    using System.Collections.Generic;

    internal interface IMinimalTaxonName
    {
        IEnumerable<IMinimalTaxonNamePart> Parts { get; }
    }
}
