// <copyright file="TaxaValidator.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Validation
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Harvesters.Bio;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Processors.Contracts.Validation;
    using ProcessingTools.Services.Contracts.Validation;

    /// <summary>
    /// Taxa validator.
    /// </summary>
    public class TaxaValidator : ITaxaValidator
    {
        private readonly ITaxonNamesHarvester harvester;
        private readonly ITaxaValidationService validationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxaValidator"/> class.
        /// </summary>
        /// <param name="harvester">Taxon names harvester.</param>
        /// <param name="validationService">Taxa validation service.</param>
        public TaxaValidator(ITaxonNamesHarvester harvester, ITaxaValidationService validationService)
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

            var data = await this.harvester.HarvestAsync(context.XmlDocument).ConfigureAwait(false);
            var scientificNames = data?.Distinct().ToArray();

            if (scientificNames == null || scientificNames.Length < 1)
            {
                reporter.AppendContent("Warning: No taxon names found.");
                return false;
            }

            var result = await this.validationService.ValidateAsync(scientificNames).ConfigureAwait(false);

            var nonValidItems = result.Where(r => r.ValidationStatus != ValidationStatus.Valid)
                .Select(r => r.ValidatedObject)
                .OrderBy(i => i);

            reporter.AppendContent("Non-valid taxon names:");
            foreach (var taxonName in nonValidItems)
            {
                reporter.AppendContent(string.Format("\t{0}", taxonName));
            }

            return true;
        }
    }
}
