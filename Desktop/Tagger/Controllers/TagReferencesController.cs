namespace ProcessingTools.Tagger.Controllers
{
    using Contracts;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.References;

    [Description("Tag references.")]
    public class TagReferencesController : GenericXmlContextTaggerController<IReferencesTagger>, ITagReferencesController
    {
        public TagReferencesController(IReferencesTagger tagger)
            : base(tagger)
        {
        }
    }
}
