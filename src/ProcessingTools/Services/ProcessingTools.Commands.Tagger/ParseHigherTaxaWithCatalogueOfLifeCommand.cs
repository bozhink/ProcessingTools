// <copyright file="ParseHigherTaxaWithCatalogueOfLifeCommand.cs" company="ProcessingTools">
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
    /// Parse higher taxa with Catalogue of Life command.
    /// </summary>
    [System.ComponentModel.Description("Parse higher taxa using CoL.")]
    public class ParseHigherTaxaWithCatalogueOfLifeCommand : ParseHigherTaxaCommand<ICatalogueOfLifeTaxaRankResolver>, IParseHigherTaxaWithCatalogueOfLifeCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParseHigherTaxaWithCatalogueOfLifeCommand"/> class.
        /// </summary>
        /// <param name="parser">Instance of <see cref="IHigherTaxaParserWithDataService{ICatalogueOfLifeTaxaRankResolver,ITaxonRank}"/>.</param>
        /// <param name="reporter">Instance of <see cref="IReporter"/>.</param>
        public ParseHigherTaxaWithCatalogueOfLifeCommand(IHigherTaxaParserWithDataService<ICatalogueOfLifeTaxaRankResolver, ITaxonRank> parser, IReporter reporter)
            : base(parser, reporter)
        {
        }
    }
}
