namespace ProcessingTools.Tagger.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.BaseLibrary;
    using ProcessingTools.Bio.Taxonomy.Types;
    using ProcessingTools.Contracts;

    public class ExtractTaxaController : TaggerControllerFactory, IExtractTaxaController
    {
        public ExtractTaxaController(IDocumentFactory documentFactory)
            : base(documentFactory)
        {
        }

        protected override Task Run(IDocument document, ProgramSettings settings, ILogger logger)
        {
            return Task.Run(() =>
            {
                if (settings.ExtractTaxa)
                {
                    logger?.Log(Messages.ExtractAllTaxaMessage);
                    document.XmlDocument
                        .ExtractTaxa(true)
                        .OrderBy(i => i)
                        .ToList()
                        .ForEach(t => logger?.Log(t));
                    return;
                }

                if (settings.ExtractLowerTaxa)
                {
                    logger?.Log(Messages.ExtractLowerTaxaMessage);
                    document.XmlDocument
                        .ExtractTaxa(true, TaxaType.Lower)
                        .OrderBy(i => i)
                        .ToList()
                        .ForEach(t => logger?.Log(t));
                }

                if (settings.ExtractHigherTaxa)
                {
                    logger?.Log(Messages.ExtractHigherTaxaMessage);
                    document.XmlDocument
                        .ExtractTaxa(true, TaxaType.Higher)
                        .OrderBy(i => i)
                        .ToList()
                        .ForEach(t => logger?.Log(t));
                }
            });
        }
    }
}