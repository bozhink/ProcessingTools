namespace ProcessingTools.Tagger.Controllers
{
    using Contracts;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Floats;

    [Description("Tag floats.")]
    public class TagFloatsController : GenericXmlContextTaggerController<IFloatsTagger>, ITagFloatsController
    {
        public TagFloatsController(IFloatsTagger tagger)
            : base(tagger)
        {
        }
    }
}
