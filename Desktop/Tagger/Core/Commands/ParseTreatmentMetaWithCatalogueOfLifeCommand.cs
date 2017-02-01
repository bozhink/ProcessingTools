namespace ProcessingTools.Tagger.Core.Commands
{
    using Contracts.Commands;
    using Generics;
    using ProcessingTools.Attributes;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;
    using Processors.Contracts.Processors.Bio.Taxonomy.Parsers;

    [Description("Parse treatment meta with CoL.")]
    public class ParseTreatmentMetaWithCatalogueOfLifeCommand : GenericDocumentParserCommand<ITreatmentMetaParserWithDataService<ICatalogueOfLifeTaxaClassificationResolver>>, IParseTreatmentMetaWithCatalogueOfLifeCommand
    {
        public ParseTreatmentMetaWithCatalogueOfLifeCommand(ITreatmentMetaParserWithDataService<ICatalogueOfLifeTaxaClassificationResolver> parser)
            : base(parser)
        {
        }
    }
}
