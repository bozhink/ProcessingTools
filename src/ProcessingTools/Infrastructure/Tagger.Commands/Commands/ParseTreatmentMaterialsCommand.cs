﻿namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Processors.Contracts.Bio.Materials;

    [System.ComponentModel.Description("Parse treatment materials.")]
    public class ParseTreatmentMaterialsCommand : DocumentParserCommand<ITreatmentMaterialsParser>, IParseTreatmentMaterialsCommand
    {
        public ParseTreatmentMaterialsCommand(ITreatmentMaterialsParser parser)
            : base(parser)
        {
        }
    }
}
