﻿// <copyright file="ParseTreatmentMetaWithAphiaCommand.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Commands.Tagger;
using ProcessingTools.Contracts.Services.Bio.Taxonomy;

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;

    /// <summary>
    /// Parse treatment meta with Aphia command.
    /// </summary>
    [System.ComponentModel.Description("Parse treatment meta with Aphia.")]
    public class ParseTreatmentMetaWithAphiaCommand : DocumentParserCommand<ITreatmentMetaParserWithDataService<IAphiaTaxonClassificationResolver>>, IParseTreatmentMetaWithAphiaCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParseTreatmentMetaWithAphiaCommand"/> class.
        /// </summary>
        /// <param name="parser">Instance of <see cref="ITreatmentMetaParserWithDataService{IAphiaTaxaClassificationResolver}"/>.</param>
        public ParseTreatmentMetaWithAphiaCommand(ITreatmentMetaParserWithDataService<IAphiaTaxonClassificationResolver> parser)
            : base(parser)
        {
        }
    }
}
