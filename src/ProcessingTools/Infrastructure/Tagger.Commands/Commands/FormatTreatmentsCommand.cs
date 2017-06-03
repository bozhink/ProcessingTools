namespace ProcessingTools.Tagger.Commands.Commands
{
    using System.ComponentModel;
    using ProcessingTools.Processors.Contracts.Processors.Bio.Taxonomy.Formatters;
    using ProcessingTools.Tagger.Commands.Contracts.Commands;
    using ProcessingTools.Tagger.Commands.Generics;

    [Description("Format treatments.")]
    public class FormatTreatmentsCommand : GenericDocumentFormatterCommand<ITreatmentFormatter>, IFormatTreatmentsCommand
    {
        public FormatTreatmentsCommand(ITreatmentFormatter formatter)
            : base(formatter)
        {
        }
    }
}
