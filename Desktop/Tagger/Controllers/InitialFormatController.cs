namespace ProcessingTools.Tagger.Controllers
{
    using Contracts;
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
