namespace ProcessingTools.Processors.Processors.Validation
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Harvesters.Contracts.Harvesters.Bio;
    using ProcessingTools.Processors.Contracts.Validation;
    using ProcessingTools.Services.Validation.Contracts;
    using ProcessingTools.Services.Validation.Models;

    public class TaxaValidator : ITaxaValidator
    {
        private readonly ITaxonNamesHarvester harvester;
        private readonly ITaxaValidationService validationService;
        private readonly ILogger logger;

        public TaxaValidator(
            ITaxonNamesHarvester harvester,
            ITaxaValidationService validationService,
            ILogger logger)
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
            this.logger = logger;
        }

        public async Task<object> Validate(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
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
                this.logger?.Log(LogType.Warning, "No taxa found.");
                return false;
            }

            var result = await this.validationService.Validate(scientificNames);

            var notFoundNames = result.Where(r => r.ValidationStatus != ValidationStatus.Valid)
                .Select(r => r.ValidatedObject.Name)
                .OrderBy(i => i);

            this.logger?.Log("Not found taxa names:\n|\t{0}\n", string.Join("\n|\t", notFoundNames));

            return true;
        }
    }
}
