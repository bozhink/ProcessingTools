namespace ProcessingTools.Processors.Contracts.Models.Bio.Taxonomy.Parsers
{
    using System;
    using System.Linq.Expressions;

    internal interface ITaxonNamePart : IMinimalTaxonNamePart
    {
        string Id { get; }

        bool IsAbbreviated { get; }

        bool IsModified { get; set; }

        bool IsResolved { get; }

        Expression<Func<ITaxonNamePart, bool>> MatchExpression { get; }

        string Pattern { get; }
    }
}
