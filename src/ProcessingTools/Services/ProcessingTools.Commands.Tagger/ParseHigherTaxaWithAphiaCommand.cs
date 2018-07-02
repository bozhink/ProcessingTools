// <copyright file="ParseHigherTaxaWithAphiaCommand.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
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
    /// Parse higher taxa with Aphia command.
    /// </summary>
    [System.ComponentModel.Description("Parse higher taxa using Aphia.")]
    public class ParseHigherTaxaWithAphiaCommand : ParseHigherTaxaCommand<IAphiaTaxaRankResolver>, IParseHigherTaxaWithAphiaCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParseHigherTaxaWithAphiaCommand"/> class.
        /// </summary>
        /// <param name="parser">Instance of <see cref="IHigherTaxaParserWithDataService{IAphiaTaxaRankResolver,ITaxonRank}"/></param>
        /// <param name="reporter">Instance of <see cref="IReporter"/>.</param>
        public ParseHigherTaxaWithAphiaCommand(IHigherTaxaParserWithDataService<IAphiaTaxaRankResolver, ITaxonRank> parser, IReporter reporter)
            : base(parser, reporter)
        {
        }
    }
}
