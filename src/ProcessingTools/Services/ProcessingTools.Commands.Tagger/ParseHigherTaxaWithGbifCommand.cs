// <copyright file="ParseHigherTaxaWithGbifCommand.cs" company="ProcessingTools">
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
