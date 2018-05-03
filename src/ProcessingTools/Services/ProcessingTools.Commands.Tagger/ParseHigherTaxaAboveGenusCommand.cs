// <copyright file="ParseHigherTaxaAboveGenusCommand.cs" company="ProcessingTools">
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
    /// Parse higher taxa above genus command.
    /// </summary>
    [System.ComponentModel.Description("Make higher taxa of type 'above-genus'.")]
    public class ParseHigherTaxaAboveGenusCommand : ParseHigherTaxaCommand<IAboveGenusTaxaRankResolver>, IParseHigherTaxaAboveGenusCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParseHigherTaxaAboveGenusCommand"/> class.
        /// </summary>
        /// <param name="parser">Instance of <see cref="IHigherTaxaParserWithDataService{IAboveGenusTaxaRankResolver,ITaxonRank}"/>.</param>
        /// <param name="reporter">Instance of <see cref="IReporter"/>.</param>
        public ParseHigherTaxaAboveGenusCommand(IHigherTaxaParserWithDataService<IAboveGenusTaxaRankResolver, ITaxonRank> parser, IReporter reporter)
            : base(parser, reporter)
        {
        }
    }
}
