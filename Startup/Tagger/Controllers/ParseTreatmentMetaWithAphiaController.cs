namespace ProcessingTools.MainProgram.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;
    using ProcessingTools.BaseLibrary.Taxonomy;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Contracts;

    public class ParseTreatmentMetaWithAphiaController : TaggerControllerFactory, IParseTreatmentMetaWithAphiaController
    {
        private readonly IAphiaTaxaClassificationDataService service;

        public ParseTreatmentMetaWithAphiaController(IAphiaTaxaClassificationDataService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }

            this.service = service;
        }

        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            var parser = new TreatmentMetaParser(this.service, document.OuterXml, logger);

            await parser.Parse();

            document.LoadXml(parser.Xml);
        }
    }
}