namespace ProcessingTools.Tagger.Controllers
{
    using Contracts.Controllers;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Bio;

    [Description("Tag type status.")]
    public class TagTypeStatusController : GenericDocumentTaggerController<ITypeStatusTagger>, ITagTypeStatusController
    {
        public TagTypeStatusController(ITypeStatusTagger tagger)
            : base(tagger)
        {
        }
    }
}
