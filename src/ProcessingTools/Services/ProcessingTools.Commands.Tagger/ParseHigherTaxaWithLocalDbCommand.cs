// <copyright file="ParseHigherTaxaWithLocalDbCommand.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;
    using ProcessingTools.Processors.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;

    /// <summary>
    /// Parse higher taxa with local database command.
    /// </summary>
    [System.ComponentModel.Description("Parse higher taxa with local database.")]
    public class ParseHigherTaxaWithLocalDbCommand : ParseHigherTaxaCommand<ILocalDbTaxaRankResolver>, IParseHigherTaxaWithLocalDbCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParseHigherTaxaWithLocalDbCommand"/> class.
        /// </summary>
        /// <param name="parser">Instance of <see cref="IHigherTaxaParserWithDataService{ILocalDbTaxaRankResolver,ITaxonRank}"/>.</param>
        /// <param name="reporter">Instance of <see cref="IReporter"/>.</param>
        public ParseHigherTaxaWithLocalDbCommand(IHigherTaxaParserWithDataService<ILocalDbTaxaRankResolver, ITaxonRank> parser, IReporter reporter)
            : base(parser, reporter)
        {
        }
    }
}
