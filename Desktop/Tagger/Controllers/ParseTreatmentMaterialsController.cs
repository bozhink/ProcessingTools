namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Processors.Contracts.Materials;
    using ProcessingTools.Contracts;

    [Description("Parse treatment materials.")]
    public class ParseTreatmentMaterialsController : TaggerControllerFactory, IParseTreatmentMaterialsController
    {
        private readonly ITreatmentMaterialsParser parser;

        public ParseTreatmentMaterialsController(IDocumentFactory documentFactory, ITreatmentMaterialsParser parser)
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
