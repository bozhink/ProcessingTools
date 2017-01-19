namespace ProcessingTools.Tagger.Controllers
{
    using Contracts.Controllers;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Bio.Codes;

    [Description("Tag collection codes.")]
    public class TagCollectionCodesController : GenericDocumentTaggerController<ICollectionCodesTagger>, ITagCollectionCodesController
    {
        public TagCollectionCodesController(ICollectionCodesTagger tagger)
            : base(tagger)
        {
        }
    }
}
