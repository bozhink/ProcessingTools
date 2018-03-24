﻿namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Processors.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;

    [System.ComponentModel.Description("Parse treatment meta with Aphia.")]
    public class ParseTreatmentMetaWithAphiaCommand : DocumentParserCommand<ITreatmentMetaParserWithDataService<IAphiaTaxaClassificationResolver>>, IParseTreatmentMetaWithAphiaCommand
    {
        public ParseTreatmentMetaWithAphiaCommand(ITreatmentMetaParserWithDataService<IAphiaTaxaClassificationResolver> parser)
            : base(parser)
        {
        }
    }
}
