// <copyright file="ParseHigherTaxaBySuffixCommand.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;

    /// <summary>
    /// Parse higher taxa by suffix command.
    /// </summary>
    [System.ComponentModel.Description("Parse higher taxa by suffix.")]
    public class ParseHigherTaxaBySuffixCommand : ParseHigherTaxaCommand<ISuffixHigherTaxonRankResolver>, IParseHigherTaxaBySuffixCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParseHigherTaxaBySuffixCommand"/> class.
        /// </summary>
        /// <param name="parser">Instance of <see cref="IHigherTaxaParserWithDataService{ISuffixHigherTaxaRankResolver,ITaxonRank}"/>.</param>
        /// <param name="reporter">Instance of <see cref="IReporter"/>.</param>
        public ParseHigherTaxaBySuffixCommand(IHigherTaxaParserWithDataService<ISuffixHigherTaxonRankResolver, ITaxonRank> parser, IReporter reporter)
            : base(parser, reporter)
        {
        }
    }
}
