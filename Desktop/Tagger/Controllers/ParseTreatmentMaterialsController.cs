namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Processors.Contracts.Materials;
    using ProcessingTools.Contracts;

    [Description("Parse treatment materials.")]
    public class ParseTreatmentMaterialsController : IParseTreatmentMaterialsController
    {
        private readonly ITreatmentMaterialsParser parser;

        public ParseTreatmentMaterialsController(IDocumentFactory documentFactory, ITreatmentMaterialsParser parser)
        {
            if (parser == null)
            {
                throw new ArgumentNullException(nameof(parser));
            }

            this.parser = parser;
        }

        public async Task<object> Run(IDocument document, IProgramSettings settings)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            return await this.parser.Parse(document);
        }
    }
}
