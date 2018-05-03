namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Processors.Contracts.Layout;

    [System.ComponentModel.Description("Initial format.")]
    public class InitialFormatCommand : DocumentFormatterCommand<IDocumentInitialFormatter>, IInitialFormatCommand
    {
        public InitialFormatCommand(IDocumentInitialFormatter formatter)
            : base(formatter)
        {
        }
    }
}
