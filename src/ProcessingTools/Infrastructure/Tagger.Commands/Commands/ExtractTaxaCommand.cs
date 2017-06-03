namespace ProcessingTools.Tagger.Commands.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Harvesters.Contracts.Harvesters.Bio;
    using ProcessingTools.Tagger.Commands.Contracts;
    using ProcessingTools.Tagger.Commands.Contracts.Commands;

    public class ExtractTaxaCommand : IExtractTaxaCommand
    {
        private readonly ITaxonNamesHarvester harvester;
        private readonly IReporter reporter;

        public ExtractTaxaCommand(ITaxonNamesHarvester harvester, IReporter reporter)
        {
            this.harvester = harvester ?? throw new ArgumentNullException(nameof(harvester));
            this.reporter = reporter ?? throw new ArgumentNullException(nameof(reporter));
        }

        public async Task<object> Run(IDocument document, ICommandSettings settings)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var context = document.XmlDocument.DocumentElement;

            if (settings.ExtractTaxa)
            {
                var data = await this.harvester.Harvest(context);
                this.BuildReport(Messages.ExtractAllTaxaMessage, data);
            }
            else
            {
                if (settings.ExtractLowerTaxa)
                {
                    var data = await this.harvester.HarvestLowerTaxa(context);
                    this.BuildReport(Messages.ExtractLowerTaxaMessage, data);
                }

                if (settings.ExtractHigherTaxa)
                {
                    var data = await this.harvester.HarvestHigherTaxa(context);
                    this.BuildReport(Messages.ExtractHigherTaxaMessage, data);
                }
            }

            await this.reporter.MakeReport();

            return true;
        }

        private void BuildReport(string message, IEnumerable<string> data)
        {
            this.reporter.AppendContent(message);
            foreach (var item in data.OrderBy(i => i))
            {
                this.reporter.AppendContent(item);
            }
        }
    }
}
