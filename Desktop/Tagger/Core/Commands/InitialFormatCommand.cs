namespace ProcessingTools.Tagger.Commands
{
    using Contracts.Commands;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Layout.Processors.Contracts.Formatters;

    [Description("Initial format.")]
    public class InitialFormatController : GenericDocumentFormatterController<IDocumentInitialFormatter>, IInitialFormatController
    {
        public InitialFormatController(IDocumentInitialFormatter formatter)
            : base(formatter)
        {
        }
    }
}
