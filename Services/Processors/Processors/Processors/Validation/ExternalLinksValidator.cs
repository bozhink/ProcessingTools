namespace ProcessingTools.Processors.Processors.Validation
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Harvesters.Contracts.Harvesters.ExternalLinks;
    using ProcessingTools.Processors.Contracts.Validation;
    using ProcessingTools.Services.Validation.Contracts.Services;
    using ProcessingTools.Services.Validation.Models;

    public class ExternalLinksValidator : IExternalLinksValidator
    {
        private readonly IExternalLinksHarvester harvester;
        private readonly IUrlValidationService validationService;

        public ExternalLinksValidator(
            IExternalLinksHarvester harvester,
            IUrlValidationService validationService)
        {
            if (harvester == null)
            {
                throw new ArgumentNullException(nameof(harvester));
            }

            if (validationService == null)
            {
                throw new ArgumentNullException(nameof(validationService));
            }

            this.harvester = harvester;
            this.validationService = validationService;
        }

        public async Task<object> Validate(IDocument document, IReporter reporter)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (reporter == null)
            {
                throw new ArgumentNullException(nameof(reporter));
            }

            var data = await this.harvester.Harvest(document.XmlDocument.DocumentElement);
            var externalLinks = data?.Distinct()
                .Select(e => new UrlServiceModel
                {
                    BaseAddress = e.BaseAddress,
                    Address = e.Uri
                })
                .ToArray();

            if (externalLinks == null || externalLinks.Length < 1)
            {
                reporter.AppendContent("Warning: No external links found.");
                return false;
            }

            var result = await this.validationService.Validate(externalLinks);

            var nonValidItems = result.Where(r => r.ValidationStatus != ValidationStatus.Valid)
                .Select(r => $"{r.ValidatedObject.FullAddress} / {r.ValidationStatus.ToString()} /")
                .OrderBy(i => i);

            reporter.AppendContent("Non-valid external links:");
            foreach (var taxonName in nonValidItems)
            {
                reporter.AppendContent(string.Format("\t{0}", taxonName));
            }

            return true;
        }
    }
}
