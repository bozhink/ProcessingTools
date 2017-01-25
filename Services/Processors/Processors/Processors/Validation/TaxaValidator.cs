namespace ProcessingTools.Processors.Processors.Validation
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Harvesters.Contracts.Harvesters.Bio;
    using ProcessingTools.Processors.Contracts.Validation;
    using ProcessingTools.Services.Validation.Contracts.Services;
    using ProcessingTools.Services.Validation.Models;

    public class TaxaValidator : ITaxaValidator
    {
        private readonly ITaxonNamesHarvester harvester;
        private readonly ITaxaValidationService validationService;

        public TaxaValidator(
            ITaxonNamesHarvester harvester,
            ITaxaValidationService validationService)
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

            var data = await this.harvester.Harvest(document.XmlDocument);
            var scientificNames = data?.Distinct()
                .Select(s => new TaxonNameServiceModel
                {
                    Name = s
                })
                .ToArray();

            if (scientificNames == null || scientificNames.Length < 1)
            {
                reporter.AppendContent("Warning: No taxon names found.");
                return false;
            }

            var result = await this.validationService.Validate(scientificNames);

            var nonValidItems = result.Where(r => r.ValidationStatus != ValidationStatus.Valid)
                .Select(r => r.ValidatedObject.Name)
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
