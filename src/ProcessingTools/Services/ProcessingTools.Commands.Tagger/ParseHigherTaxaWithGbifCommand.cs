// <copyright file="ParseHigherTaxaWithGbifCommand.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;

    /// <summary>
    /// Parse higher taxa with GBIF command.
    /// </summary>
    [System.ComponentModel.Description("Parse higher taxa using GBIF.")]
    public class ParseHigherTaxaWithGbifCommand : ParseHigherTaxaCommand<IGbifTaxonRankResolver>, IParseHigherTaxaWithGbifCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParseHigherTaxaWithGbifCommand"/> class.
        /// </summary>
        /// <param name="parser">Instance of <see cref="IHigherTaxaParserWithDataService{IGbifTaxaRankResolver,ITaxonRank}"/>.</param>
        /// <param name="reporter">Instance of <see cref="IReporter"/>.</param>
        public ParseHigherTaxaWithGbifCommand(IHigherTaxaParserWithDataService<IGbifTaxonRankResolver, ITaxonRank> parser, IReporter reporter)
            : base(parser, reporter)
        {
        }
    }
}
