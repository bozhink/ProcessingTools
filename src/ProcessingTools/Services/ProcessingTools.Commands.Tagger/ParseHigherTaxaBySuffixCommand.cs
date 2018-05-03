// <copyright file="ParseHigherTaxaBySuffixCommand.cs" company="ProcessingTools">
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
    /// Parse higher taxa by suffix command.
    /// </summary>
    [System.ComponentModel.Description("Parse higher taxa by suffix.")]
    public class ParseHigherTaxaBySuffixCommand : ParseHigherTaxaCommand<ISuffixHigherTaxaRankResolver>, IParseHigherTaxaBySuffixCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParseHigherTaxaBySuffixCommand"/> class.
        /// </summary>
        /// <param name="parser">Instance of <see cref="IHigherTaxaParserWithDataService{ISuffixHigherTaxaRankResolver,ITaxonRank}"/>.</param>
        /// <param name="reporter">Instance of <see cref="IReporter"/>.</param>
        public ParseHigherTaxaBySuffixCommand(IHigherTaxaParserWithDataService<ISuffixHigherTaxaRankResolver, ITaxonRank> parser, IReporter reporter)
            : base(parser, reporter)
        {
        }
    }
}
