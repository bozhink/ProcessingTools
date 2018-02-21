namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Processors.Contracts.Bio.Taxonomy;

    [System.ComponentModel.Description("Format treatments.")]
    public class FormatTreatmentsCommand : DocumentFormatterCommand<ITreatmentFormatter>, IFormatTreatmentsCommand
    {
        public FormatTreatmentsCommand(ITreatmentFormatter formatter)
            : base(formatter)
        {
        }
    }
}
