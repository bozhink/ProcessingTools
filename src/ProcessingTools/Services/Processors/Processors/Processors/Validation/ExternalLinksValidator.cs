namespace ProcessingTools.Processors.Processors.Validation
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Harvesters.ExternalLinks;
    using ProcessingTools.Contracts.Processors.Processors.Validation;
    using ProcessingTools.Contracts.Services.Validation;
    using ProcessingTools.Enumerations;

    public class ExternalLinksValidator : IExternalLinksValidator
    {
        private readonly IExternalLinksHarvester harvester;
        private readonly IUrlValidationService validationService;

        public ExternalLinksValidator(
            IExternalLinksHarvester harvester,
            IUrlValidationService validationService)
        {
            this.harvester = harvester ?? throw new ArgumentNullException(nameof(harvester));
            this.validationService = validationService ?? throw new ArgumentNullException(nameof(validationService));
        }

        public async Task<object> ValidateAsync(IDocument context, IReporter reporter)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (reporter == null)
            {
                throw new ArgumentNullException(nameof(reporter));
            }

            var data = await this.harvester.HarvestAsync(context.XmlDocument.DocumentElement);
            var externalLinks = data?.Select(e => e.FullAddress)
                .Distinct()
                .ToArray();
            if (externalLinks == null || externalLinks.Length < 1)
            {
                reporter.AppendContent("Warning: No external links found.");
                return false;
            }

            var result = await this.validationService.ValidateAsync(externalLinks).ConfigureAwait(false);

            var nonValidItems = result.Where(r => r.ValidationStatus != ValidationStatus.Valid)
                .Select(r => $"{r.ValidatedObject} / {r.ValidationStatus.ToString()} /")
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
