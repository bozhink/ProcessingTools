namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.BaseLibrary.Taxonomy.Materials;
    using ProcessingTools.Contracts;

    [Description("Parse treatment materials.")]
    public class ParseTreatmentMaterialsController : TaggerControllerFactory, IParseTreatmentMaterialsController
    {
        private readonly ITreatmentMaterialsParser parser;

        public ParseTreatmentMaterialsController(ITreatmentMaterialsParser parser)
        {
            if (parser == null)
            {
                throw new ArgumentNullException(nameof(parser));
            }

            this.parser = parser;
        }

        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            await this.parser.Parse(document, namespaceManager);
        }
    }
}