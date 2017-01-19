namespace ProcessingTools.Tagger.Commands
{
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Contracts.Commands;
    using ProcessingTools.Bio.Taxonomy.Extensions;
    using ProcessingTools.Bio.Taxonomy.Types;
    using ProcessingTools.Contracts;

    public class ExtractTaxaCommand : IExtractTaxaCommand
    {
        private readonly ILogger logger;

        public ExtractTaxaCommand(ILogger logger)
        {
            this.logger = logger;
        }

        public Task<object> Run(IDocument document, IProgramSettings settings)
        {
            if (settings.ExtractTaxa)
            {
                this.logger?.Log(Messages.ExtractAllTaxaMessage);
                document.XmlDocument
                    .ExtractTaxa(true)
                    .OrderBy(i => i)
                    .ToList()
                    .ForEach(t => this.logger?.Log(t));

                return Task.FromResult<object>(true);
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

            return Task.FromResult<object>(true);
        }
    }
}