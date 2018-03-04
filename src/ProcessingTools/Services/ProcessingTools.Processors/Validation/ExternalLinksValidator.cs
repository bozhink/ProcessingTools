// <copyright file="ExternalLinksValidator.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Validation
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Harvesters.Contracts.ExternalLinks;
    using ProcessingTools.Processors.Contracts.Validation;
    using ProcessingTools.Services.Contracts.Validation;

    /// <summary>
    /// External links validator.
    /// </summary>
    public class ExternalLinksValidator : IExternalLinksValidator
    {
        private readonly IExternalLinksHarvester harvester;
        private readonly IUrlValidationService validationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalLinksValidator"/> class.
        /// </summary>
        /// <param name="harvester">External links harvester.</param>
        /// <param name="validationService">URL validation service.</param>
        public ExternalLinksValidator(IExternalLinksHarvester harvester, IUrlValidationService validationService)
        {
            this.harvester = harvester ?? throw new ArgumentNullException(nameof(harvester));
            this.validationService = validationService ?? throw new ArgumentNullException(nameof(validationService));
        }

        /// <inheritdoc/>
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

            var data = await this.harvester.HarvestAsync(context.XmlDocument.DocumentElement).ConfigureAwait(false);
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
