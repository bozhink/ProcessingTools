namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Processors.Processors.Bio.Taxonomy.Formatters;

    [System.ComponentModel.Description("Format treatments.")]
    public class FormatTreatmentsCommand : DocumentFormatterCommand<ITreatmentFormatter>, IFormatTreatmentsCommand
    {
        public FormatTreatmentsCommand(ITreatmentFormatter formatter)
            : base(formatter)
        {
        }
    }
}
