namespace ProcessingTools.MainProgram.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;

    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;
    using ProcessingTools.Harvesters.Contracts;
    using ProcessingTools.Infrastructure.Attributes;
    using ProcessingTools.Services.Validation.Contracts;
    using ProcessingTools.Services.Validation.Models;

    [Description("Validate extenal links.")]
    public class ValidateExternalLinksController : TaggerControllerFactory, IValidateExternalLinksController
    {
        private IExternalLinksHarvester harvester;
        private IUrlValidationService service;

        public ValidateExternalLinksController(IExternalLinksHarvester harvester, IUrlValidationService service)
        {
            if (harvester == null)
            {
                throw new ArgumentNullException("harvester");
            }

            if (service == null)
            {
                throw new ArgumentNullException("service");
            }

            this.harvester = harvester;
            this.service = service;
        }

        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            var externalLinks = (await this.harvester.Harvest(document.DocumentElement))
                .Select(e => new UrlModel
                {
                    BaseAddress = e.BaseAddress,
                    Address = e.Uri
                })
                .ToArray();

            var result = await this.service.Validate(externalLinks);

            var nonValidItems = result.Where(r => r.ValidationStatus != ValidationStatus.Valid)
                .Select(r => $"{r.ValidatedObject.FullAddress} / {r.ValidationStatus.ToString()} /");

            logger?.Log("Non-valid external links:\n|\t{0}\n", string.Join("\n|\t", nonValidItems));
        }
    }
}