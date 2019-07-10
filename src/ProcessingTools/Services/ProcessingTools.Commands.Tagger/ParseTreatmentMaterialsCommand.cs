// <copyright file="ParseTreatmentMaterialsCommand.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Commands.Tagger;
using ProcessingTools.Contracts.Services.Bio.Materials;

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;

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
