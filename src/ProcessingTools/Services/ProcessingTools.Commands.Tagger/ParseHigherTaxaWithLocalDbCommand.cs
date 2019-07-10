// <copyright file="ParseHigherTaxaWithLocalDbCommand.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Commands.Tagger;
using ProcessingTools.Contracts.Services;
using ProcessingTools.Contracts.Services.Bio.Taxonomy;

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;

    /// <summary>
    /// Parse higher taxa with local database command.
    /// </summary>
    [System.ComponentModel.Description("Parse higher taxa with local database.")]
    public class ParseHigherTaxaWithLocalDbCommand : ParseHigherTaxaCommand<ILocalDbTaxonRankResolver>, IParseHigherTaxaWithLocalDbCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParseHigherTaxaWithLocalDbCommand"/> class.
        /// </summary>
        /// <param name="parser">Instance of <see cref="IHigherTaxaParserWithDataService{ILocalDbTaxaRankResolver,ITaxonRank}"/>.</param>
        /// <param name="reporter">Instance of <see cref="IReporter"/>.</param>
        public ParseHigherTaxaWithLocalDbCommand(IHigherTaxaParserWithDataService<ILocalDbTaxonRankResolver, ITaxonRank> parser, IReporter reporter)
            : base(parser, reporter)
        {
        }
    }
}
