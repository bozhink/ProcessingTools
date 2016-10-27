namespace ProcessingTools.Tagger.Controllers
{
    using Contracts;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Abbreviations;

    [Description("Tag abbreviations.")]
    public class TagAbbreviationsController : GenericXmlContextTaggerController<IAbbreviationsTagger>, ITagAbbreviationsController
    {
        public TagAbbreviationsController(IAbbreviationsTagger tagger)
            : base(tagger)
        {
        }
    }
}
