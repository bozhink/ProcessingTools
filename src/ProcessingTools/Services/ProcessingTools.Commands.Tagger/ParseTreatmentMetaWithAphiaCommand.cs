// <copyright file="ParseTreatmentMetaWithAphiaCommand.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Processors.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;

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
