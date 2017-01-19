namespace ProcessingTools.Tagger.Controllers
{
    using Contracts.Controllers;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.ExternalLinks;

    [Description("Tag web links and DOI.")]
    public class TagWebLinksController : GenericDocumentTaggerController<IExternalLinksTagger>, ITagWebLinksController
    {
        public TagWebLinksController(IExternalLinksTagger tagger)
            : base(tagger)
        {
        }
    }
}
