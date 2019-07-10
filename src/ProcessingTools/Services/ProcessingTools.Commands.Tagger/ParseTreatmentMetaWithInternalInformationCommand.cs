﻿// <copyright file="ParseTreatmentMetaWithInternalInformationCommand.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Commands.Tagger;
using ProcessingTools.Contracts.Services.Bio.Taxonomy;

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;

    /// <summary>
    /// Parse treatment meta with internal information command.
    /// </summary>
    [System.ComponentModel.Description("Parse treatment meta with internal information.")]
    public class ParseTreatmentMetaWithInternalInformationCommand : DocumentParserCommand<ITreatmentMetaParserWithInternalInformation>, IParseTreatmentMetaWithInternalInformationCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParseTreatmentMetaWithInternalInformationCommand"/> class.
        /// </summary>
        /// <param name="parser">Instance of <see cref="ITreatmentMetaParserWithInternalInformation"/>.</param>
        public ParseTreatmentMetaWithInternalInformationCommand(ITreatmentMetaParserWithInternalInformation parser)
            : base(parser)
        {
        }
    }
}
