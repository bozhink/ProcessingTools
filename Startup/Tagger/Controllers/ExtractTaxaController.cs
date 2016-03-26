namespace ProcessingTools.Tagger.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;
    using ProcessingTools.BaseLibrary;
    using ProcessingTools.Bio.Taxonomy.Types;
    using ProcessingTools.Contracts;

    public class ExtractTaxaController : TaggerControllerFactory, IExtractTaxaController
    {
        protected override Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            return Task.Run(() =>
            {
                if (settings.ExtractTaxa)
                {
                    logger?.Log(Messages.ExtractAllTaxaMessage);
                    document
                        .ExtractTaxa(true)
                        .ToList()
                        .ForEach(t => logger?.Log(t));
                    return;
                }

                if (settings.ExtractLowerTaxa)
                {
                    logger?.Log(Messages.ExtractLowerTaxaMessage);
                    document
                        .ExtractTaxa(true, TaxaType.Lower)
                        .ToList()
                        .ForEach(t => logger?.Log(t));
                }

                if (settings.ExtractHigherTaxa)
                {
                    logger?.Log(Messages.ExtractHigherTaxaMessage);
                    document
                        .ExtractTaxa(true, TaxaType.Higher)
                        .ToList()
                        .ForEach(t => logger?.Log(t));
                }
            });
        }
    }
}