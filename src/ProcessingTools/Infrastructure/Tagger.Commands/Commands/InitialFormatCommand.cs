namespace ProcessingTools.Tagger.Commands.Commands
{
    using System.ComponentModel;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Layout.Processors.Contracts.Formatters;
    using ProcessingTools.Tagger.Commands.Generics;

    [Description("Initial format.")]
    public class InitialFormatCommand : GenericDocumentFormatterCommand<IDocumentInitialFormatter>, IInitialFormatCommand
    {
        public InitialFormatCommand(IDocumentInitialFormatter formatter)
            : base(formatter)
        {
        }
    }
}
