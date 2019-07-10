// <copyright file="ParseHigherTaxaAboveGenusCommand.cs" company="ProcessingTools">
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
    /// Parse higher taxa above genus command.
    /// </summary>
    [System.ComponentModel.Description("Make higher taxa of type 'above-genus'.")]
    public class ParseHigherTaxaAboveGenusCommand : ParseHigherTaxaCommand<IAboveGenusTaxonRankResolver>, IParseHigherTaxaAboveGenusCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParseHigherTaxaAboveGenusCommand"/> class.
        /// </summary>
        /// <param name="parser">Instance of <see cref="IHigherTaxaParserWithDataService{IAboveGenusTaxaRankResolver,ITaxonRank}"/>.</param>
        /// <param name="reporter">Instance of <see cref="IReporter"/>.</param>
        public ParseHigherTaxaAboveGenusCommand(IHigherTaxaParserWithDataService<IAboveGenusTaxonRankResolver, ITaxonRank> parser, IReporter reporter)
            : base(parser, reporter)
        {
        }
    }
}
