namespace ProcessingTools.Bio.Taxonomy.Processors.Models.Parsers
{
    using System.Collections.Generic;

    internal interface IMinimalTaxonName
    {
        IEnumerable<IMinimalTaxonNamePart> Parts { get; }
    }
}
