namespace ProcessingTools.Tagger.Controllers
{
    using Contracts;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Processors.Contracts.Formatters;

    [Description("Format treatments.")]
    public class FormatTreatmentsController : GenericDocumentFormatterController<ITreatmentFormatter>, IFormatTreatmentsController
    {
        public FormatTreatmentsController(ITreatmentFormatter formatter)
            : base(formatter)
        {
        }
    }
}
