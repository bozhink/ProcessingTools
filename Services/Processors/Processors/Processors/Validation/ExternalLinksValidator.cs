namespace ProcessingTools.Processors.Validation
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts.Validation;

    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;
    using ProcessingTools.Harvesters.Contracts.ExternalLinks;
    using ProcessingTools.Services.Validation.Contracts;
    using ProcessingTools.Services.Validation.Models;

    public class ExternalLinksValidator : IExternalLinksValidator
    {
        private readonly IExternalLinksHarvester harvester;
        private readonly IUrlValidationService service;
        private readonly ILogger logger;

        public ExternalLinksValidator(IExternalLinksHarvester harvester, IUrlValidationService service, ILogger logger)
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

        public async Task<object> Validate(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
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
