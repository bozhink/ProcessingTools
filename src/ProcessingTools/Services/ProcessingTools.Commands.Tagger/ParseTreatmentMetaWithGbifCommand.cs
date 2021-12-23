// <copyright file="ParseTreatmentMetaWithGbifCommand.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Bio.Taxonomy.External.GbifApiV09.Contracts;
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;

    /// <summary>
    /// Parse treatment meta with GBIF command.
    /// </summary>
    [System.ComponentModel.Description("Parse treatment meta with GBIF.")]
    public class ParseTreatmentMetaWithGbifCommand : DocumentParserCommand<ITreatmentMetaParserWithDataService<IGbifTaxonClassificationResolver>>, IParseTreatmentMetaWithGbifCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParseTreatmentMetaWithGbifCommand"/> class.
        /// </summary>
        /// <param name="parser">Instance of <see cref="ITreatmentMetaParserWithDataService{IGbifTaxaClassificationResolver}"/>.</param>
        public ParseTreatmentMetaWithGbifCommand(ITreatmentMetaParserWithDataService<IGbifTaxonClassificationResolver> parser)
            : base(parser)
        {
        }
    }
}
