namespace ProcessingTools.MainProgram.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;

    using ProcessingTools.BaseLibrary;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;
    using ProcessingTools.Infrastructure.Attributes;
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
                throw new ArgumentNullException("service");
            }

            this.service = service;
        }

        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            var scientificNames = document.ExtractTaxa(true)
                .Select(s => new TaxonName
                {
                    Name = s
                })
                .ToArray();

            var result = await this.service.Validate(scientificNames);

            var notFoundNames = result.Where(r => r.ValidationStatus != ValidationStatus.Valid)
                .Select(r => r.ValidatedObject.Name);

            logger?.Log("Not found taxa names:\n|\t{0}\n", string.Join("\n|\t", notFoundNames));
        }
    }
}
