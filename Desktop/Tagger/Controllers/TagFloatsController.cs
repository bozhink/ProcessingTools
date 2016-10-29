namespace ProcessingTools.Tagger.Controllers
{
    using Contracts.Controllers;
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
