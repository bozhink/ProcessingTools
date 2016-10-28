namespace ProcessingTools.Tagger.Controllers
{
    using Contracts;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Bio;

    [Description("Tag specimen count.")]
    public class TagSpecimenCountController : GenericDocumentTaggerController<ISpecimenCountTagger>, ITagSpecimenCountController
    {
        public TagSpecimenCountController(ISpecimenCountTagger tagger)
            : base(tagger)
        {
        }
    }
}
