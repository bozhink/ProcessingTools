namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.BaseLibrary.Taxonomy;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Contracts;

    [Description("Parse treatment meta with Aphia.")]
    public class ParseTreatmentMetaWithAphiaController : TaggerControllerFactory, IParseTreatmentMetaWithAphiaController
    {
        private readonly ITreatmentMetaParser<IAphiaTaxaClassificationResolverDataService> parser;

        public ParseTreatmentMetaWithAphiaController(
            IDocumentFactory documentFactory,
            ITreatmentMetaParser<IAphiaTaxaClassificationResolverDataService> parser)
            : base(documentFactory)
        {
            if (parser == null)
            {
                throw new ArgumentNullException(nameof(parser));
            }

            this.parser = parser;
        }

        protected override async Task Run(IDocument document, ProgramSettings settings)
        {
            await this.parser.Parse(document);
        }
    }
}
