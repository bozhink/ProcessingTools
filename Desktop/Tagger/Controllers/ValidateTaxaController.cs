namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.BaseLibrary;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;
    using ProcessingTools.Services.Validation.Contracts;
    using ProcessingTools.Services.Validation.Models;

    [Description("Taxa validation using Global Names Resolver.")]
    public class ValidateTaxaController : TaggerControllerFactory, IValidateTaxaController
    {
        private readonly ITaxaValidationService service;

        public ValidateTaxaController(ITaxaValidationService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this.service = service;
        }

        protected override async Task Run(IDocument document, ProgramSettings settings, ILogger logger)
        {
            var scientificNames = document.XmlDocument.ExtractTaxa(true)
                .Select(s => new TaxonNameServiceModel
                {
                    Name = s
                })?.ToArray();

            if (scientificNames == null || scientificNames.Length < 1)
            {
                logger?.Log(LogType.Warning, "No taxa found.");
                return;
            }

            var result = await this.service.Validate(scientificNames);

            var notFoundNames = result.Where(r => r.ValidationStatus != ValidationStatus.Valid)
                .Select(r => r.ValidatedObject.Name)
                .OrderBy(i => i);

            logger?.Log("Not found taxa names:\n|\t{0}\n", string.Join("\n|\t", notFoundNames));
        }
    }
}
