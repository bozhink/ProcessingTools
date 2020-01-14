// <copyright file="ParseHigherTaxaWithCatalogueOfLifeCommand.cs" company="ProcessingTools">
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
    /// Parse higher taxa with Catalogue of Life command.
    /// </summary>
    [System.ComponentModel.Description("Parse higher taxa using CoL.")]
    public class ParseHigherTaxaWithCatalogueOfLifeCommand : ParseHigherTaxaCommand<ICatalogueOfLifeTaxonRankResolver>, IParseHigherTaxaWithCatalogueOfLifeCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParseHigherTaxaWithCatalogueOfLifeCommand"/> class.
        /// </summary>
        /// <param name="parser">Instance of <see cref="IHigherTaxaParserWithDataService{ICatalogueOfLifeTaxaRankResolver,ITaxonRank}"/>.</param>
        /// <param name="reporter">Instance of <see cref="IReporter"/>.</param>
        public ParseHigherTaxaWithCatalogueOfLifeCommand(IHigherTaxaParserWithDataService<ICatalogueOfLifeTaxonRankResolver, ITaxonRank> parser, IReporter reporter)
            : base(parser, reporter)
        {
        }
    }
}
