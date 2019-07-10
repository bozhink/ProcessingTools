// <copyright file="ParseTreatmentMetaWithCatalogueOfLifeCommand.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Commands.Tagger;
using ProcessingTools.Contracts.Services.Bio.Taxonomy;

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;

    /// <summary>
    /// Parse treatment meta with Catalogue of Life command.
    /// </summary>
    [System.ComponentModel.Description("Parse treatment meta with CoL.")]
    public class ParseTreatmentMetaWithCatalogueOfLifeCommand : DocumentParserCommand<ITreatmentMetaParserWithDataService<ICatalogueOfLifeTaxonClassificationResolver>>, IParseTreatmentMetaWithCatalogueOfLifeCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParseTreatmentMetaWithCatalogueOfLifeCommand"/> class.
        /// </summary>
        /// <param name="parser">Instance of <see cref="ITreatmentMetaParserWithDataService{ICatalogueOfLifeTaxaClassificationResolver}"/>.</param>
        public ParseTreatmentMetaWithCatalogueOfLifeCommand(ITreatmentMetaParserWithDataService<ICatalogueOfLifeTaxonClassificationResolver> parser)
            : base(parser)
        {
        }
    }
}
