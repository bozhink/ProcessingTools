namespace ProcessingTools.Processors.Processors.Validation
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Taxonomy.Extensions;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Processors.Contracts.Validation;
    using ProcessingTools.Services.Validation.Contracts;
    using ProcessingTools.Services.Validation.Models;

    public class TaxaValidator : ITaxaValidator
    {
        private readonly ITaxaValidationService service;
        private readonly ILogger logger;

        public TaxaValidator(ITaxaValidationService service, ILogger logger)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this.service = service;
            this.logger = logger;
        }

        public async Task<object> Validate(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var scientificNames = document.XmlDocument.ExtractTaxa(true)
                .Select(s => new TaxonNameServiceModel
                {
                    Name = s
                })?.ToArray();

            if (scientificNames == null || scientificNames.Length < 1)
            {
                this.logger?.Log(LogType.Warning, "No taxa found.");
                return false;
            }

            var result = await this.service.Validate(scientificNames);

            var notFoundNames = result.Where(r => r.ValidationStatus != ValidationStatus.Valid)
                .Select(r => r.ValidatedObject.Name)
                .OrderBy(i => i);

            this.logger?.Log("Not found taxa names:\n|\t{0}\n", string.Join("\n|\t", notFoundNames));

            return true;
        }
    }
}
