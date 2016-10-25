namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Processors.Contracts.Formatters;
    using ProcessingTools.Contracts;

    [Description("Format treatments.")]
    public class FormatTreatmentsController : TaggerControllerFactory, IFormatTreatmentsController
    {
        private readonly ITreatmentFormatter formatter;

        public FormatTreatmentsController(IDocumentFactory documentFactory, ITreatmentFormatter formatter)
            : base(documentFactory)
        {
            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            this.formatter = formatter;
        }

        protected override async Task Run(IDocument document, IProgramSettings settings)
        {
            await this.formatter.Format(document);
        }
    }
}
