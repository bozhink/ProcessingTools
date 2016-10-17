namespace ProcessingTools.Bio.Taxonomy.Processors.Models.Parsers
{
    using System;
    using System.Linq.Expressions;

    internal interface ITaxonNamePart : IMinimalTaxonNamePart
    {
        string Id { get; }

        bool IsAbbreviated { get; }

        bool IsResolved { get; }

        bool IsModified { get; set; }

        Expression<Func<ITaxonNamePart, bool>> MatchExpression { get; }
        string Name { get; }

        string Pattern { get; }
    }
}
