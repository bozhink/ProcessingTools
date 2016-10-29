namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts.Controllers;

    using ProcessingTools.Attributes;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;
    using ProcessingTools.Harvesters.Contracts.ExternalLinks;
    using ProcessingTools.Services.Validation.Contracts;
    using ProcessingTools.Services.Validation.Models;

    [Description("Validate external links.")]
    public class ValidateExternalLinksController : IValidateExternalLinksController
    {
        private readonly IExternalLinksHarvester harvester;
        private readonly IUrlValidationService service;
        private readonly ILogger logger;

        public ValidateExternalLinksController(
            IExternalLinksHarvester harvester,
            IUrlValidationService service,
            ILogger logger)
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
            this.logger = logger;
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

            var externalLinks = await this.harvester.Harvest(document.XmlDocument.DocumentElement);

            if (externalLinks == null)
            {
                this.logger?.Log(LogType.Info, "No external links are found.");
                return false;
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

            this.logger?.Log("Non-valid external links:\n|\t{0}\n", string.Join("\n|\t", nonValidItems));

            return true;
        }
    }
}