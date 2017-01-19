namespace ProcessingTools.Tagger.Controllers
{
    using Contracts.Controllers;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Bio;

    [Description("Tag morphological epithets.")]
    public class TagMorphologicalEpithetsController : GenericDocumentTaggerController<IMorphologicalEpithetsTagger>, ITagMorphologicalEpithetsController
    {
        public TagMorphologicalEpithetsController(IMorphologicalEpithetsTagger tagger)
            : base(tagger)
        {
        }
    }
}
