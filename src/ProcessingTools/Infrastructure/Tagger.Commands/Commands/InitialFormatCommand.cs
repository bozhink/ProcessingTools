namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Layout.Processors.Contracts.Formatters;

    [System.ComponentModel.Description("Initial format.")]
    public class InitialFormatCommand : DocumentFormatterCommand<IDocumentInitialFormatter>, IInitialFormatCommand
    {
        public InitialFormatCommand(IDocumentInitialFormatter formatter)
            : base(formatter)
        {
        }
    }
}
