// <copyright file="ParseTreatmentMaterialsCommand.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Processors.Contracts.Bio.Materials;

    /// <summary>
    /// Parse treatment materials command.
    /// </summary>
    [System.ComponentModel.Description("Parse treatment materials.")]
    public class ParseTreatmentMaterialsCommand : DocumentParserCommand<ITreatmentMaterialsParser>, IParseTreatmentMaterialsCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParseTreatmentMaterialsCommand"/> class.
        /// </summary>
        /// <param name="parser">Instance of <see cref="ITreatmentMaterialsParser"/>.</param>
        public ParseTreatmentMaterialsCommand(ITreatmentMaterialsParser parser)
            : base(parser)
        {
        }
    }
}
