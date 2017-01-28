namespace ProcessingTools.Tagger.Core.Commands
{
    using Contracts.Commands;
    using Generics;
    using ProcessingTools.Attributes;
    using ProcessingTools.Layout.Processors.Contracts.Formatters;

    [Description("Initial format.")]
    public class InitialFormatCommand : GenericDocumentFormatterCommand<IDocumentInitialFormatter>, IInitialFormatCommand
    {
        public InitialFormatCommand(IDocumentInitialFormatter formatter)
            : base(formatter)
        {
        }
    }
}
