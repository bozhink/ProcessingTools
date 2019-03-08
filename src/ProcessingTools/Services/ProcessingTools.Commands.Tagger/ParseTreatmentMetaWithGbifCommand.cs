﻿// <copyright file="ParseTreatmentMetaWithGbifCommand.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Processors.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;

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