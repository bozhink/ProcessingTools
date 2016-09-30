namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;
    using ProcessingTools.Harvesters.Contracts;
    using ProcessingTools.Services.Validation.Contracts;
    using ProcessingTools.Services.Validation.Models;

    [Description("Validate external links.")]
    public class ValidateExternalLinksController : TaggerControllerFactory, IValidateExternalLinksController
    {
        private IExternalLinksHarvester harvester;
        private IUrlValidationService service;

        public ValidateExternalLinksController(
            IDocumentFactory documentFactory,
            IExternalLinksHarvester harvester,
            IUrlValidationService service)
            : base(documentFactory)
        {
            if (harvester == null)
            {
                throw new ArgumentNullException(nameof(harvester));
            }

            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this.harvester = harvester;
            this.service = service;
        }

        protected override async Task Run(IDocument document, ProgramSettings settings, ILogger logger)
        {
            var externalLinks = await this.harvester.Harvest(document.XmlDocument.DocumentElement);

            if (externalLinks == null)
            {
                logger?.Log(LogType.Info, "No external links are found.");
                return;
            }

            var linksToValidate = externalLinks.Select(e => new UrlServiceModel
            {
                BaseAddress = e.BaseAddress,
                Address = e.Uri
            })
            .ToArray();

            var result = await this.service.Validate(linksToValidate);

            var nonValidItems = result.Where(r => r.ValidationStatus != ValidationStatus.Valid)
                .Select(r => $"{r.ValidatedObject.FullAddress} / {r.ValidationStatus.ToString()} /")
                .OrderBy(i => i);

            logger?.Log("Non-valid external links:\n|\t{0}\n", string.Join("\n|\t", nonValidItems));
        }
    }
}