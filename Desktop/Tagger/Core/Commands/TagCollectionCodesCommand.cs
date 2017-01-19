namespace ProcessingTools.Tagger.Commands
{
    using Contracts.Commands;
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
