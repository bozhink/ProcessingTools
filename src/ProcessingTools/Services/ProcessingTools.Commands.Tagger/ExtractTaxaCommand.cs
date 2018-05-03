namespace ProcessingTools.Commands.Tagger
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Commands.Models.Contracts;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Harvesters.Contracts.Bio;

    public class ExtractTaxaCommand : IExtractTaxaCommand
    {
        private readonly ITaxonNamesHarvester harvester;
        private readonly IReporter reporter;

        public ExtractTaxaCommand(ITaxonNamesHarvester harvester, IReporter reporter)
        {
            this.harvester = harvester ?? throw new ArgumentNullException(nameof(harvester));
            this.reporter = reporter ?? throw new ArgumentNullException(nameof(reporter));
        }

        public async Task<object> RunAsync(IDocument document, ICommandSettings settings)
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
                var data = await this.harvester.HarvestAsync(context);

                string message = @"
        Extract all taxa.
";

                this.BuildReport(message, data);
            }
            else
            {
                if (settings.ExtractLowerTaxa)
                {
                    var data = await this.harvester.HarvestLowerTaxaAsync(context).ConfigureAwait(false);

                    string message = @"
        Extract higher taxa.
";

                    this.BuildReport(message, data);
                }

                if (settings.ExtractHigherTaxa)
                {
                    var data = await this.harvester.HarvestHigherTaxaAsync(context).ConfigureAwait(false);

                    string message = @"
        Extract lower taxa.
";

                    this.BuildReport(message, data);
                }
            }

            await this.reporter.MakeReportAsync().ConfigureAwait(false);

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
