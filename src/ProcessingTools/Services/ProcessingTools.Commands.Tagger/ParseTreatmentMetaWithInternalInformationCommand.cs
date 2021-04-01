// <copyright file="ParseTreatmentMetaWithInternalInformationCommand.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;

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
