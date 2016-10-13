namespace ProcessingTools.Tagger.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Bio.Taxonomy.Extensions;
    using ProcessingTools.Bio.Taxonomy.Types;
    using ProcessingTools.Contracts;

    public class ExtractTaxaController : TaggerControllerFactory, IExtractTaxaController
    {
        private readonly ILogger logger;

        public ExtractTaxaController(IDocumentFactory documentFactory, ILogger logger)
            : base(documentFactory)
        {
            this.logger = logger;
        }

        protected override Task Run(IDocument document, ProgramSettings settings)
        {
            return Task.Run(() =>
            {
                if (settings.ExtractTaxa)
                {
                    this.logger?.Log(Messages.ExtractAllTaxaMessage);
                    document.XmlDocument
                        .ExtractTaxa(true)
                        .OrderBy(i => i)
                        .ToList()
                        .ForEach(t => this.logger?.Log(t));
                    return;
                }

                if (settings.ExtractLowerTaxa)
                {
                    this.logger?.Log(Messages.ExtractLowerTaxaMessage);
                    document.XmlDocument
                        .ExtractTaxa(true, TaxonType.Lower)
                        .OrderBy(i => i)
                        .ToList()
                        .ForEach(t => this.logger?.Log(t));
                }

                if (settings.ExtractHigherTaxa)
                {
                    this.logger?.Log(Messages.ExtractHigherTaxaMessage);
                    document.XmlDocument
                        .ExtractTaxa(true, TaxonType.Higher)
                        .OrderBy(i => i)
                        .ToList()
                        .ForEach(t => this.logger?.Log(t));
                }
            });
        }
    }
}