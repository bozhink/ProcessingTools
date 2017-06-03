namespace ProcessingTools.Tagger.Commands.Commands
{
    using System.ComponentModel;
    using ProcessingTools.Processors.Contracts.Processors.Bio.Taxonomy.Parsers;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;
    using ProcessingTools.Tagger.Commands.Contracts.Commands;
    using ProcessingTools.Tagger.Commands.Generics;

    [Description("Parse treatment meta with CoL.")]
    public class ParseTreatmentMetaWithCatalogueOfLifeCommand : GenericDocumentParserCommand<ITreatmentMetaParserWithDataService<ICatalogueOfLifeTaxaClassificationResolver>>, IParseTreatmentMetaWithCatalogueOfLifeCommand
    {
        public ParseTreatmentMetaWithCatalogueOfLifeCommand(ITreatmentMetaParserWithDataService<ICatalogueOfLifeTaxaClassificationResolver> parser)
            : base(parser)
        {
        }
    }
}
