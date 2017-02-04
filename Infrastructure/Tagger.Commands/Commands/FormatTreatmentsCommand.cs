namespace ProcessingTools.Tagger.Commands.Commands
{
    using Contracts.Commands;
    using Generics;
    using ProcessingTools.Attributes;
    using Processors.Contracts.Processors.Bio.Taxonomy.Formatters;

    [Description("Format treatments.")]
    public class FormatTreatmentsCommand : GenericDocumentFormatterCommand<ITreatmentFormatter>, IFormatTreatmentsCommand
    {
        public FormatTreatmentsCommand(ITreatmentFormatter formatter)
            : base(formatter)
        {
        }
    }
}
