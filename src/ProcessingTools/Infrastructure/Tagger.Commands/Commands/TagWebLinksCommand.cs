namespace ProcessingTools.Tagger.Commands.Commands
{
    using System.ComponentModel;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Processors.Processors.ExternalLinks;
    using ProcessingTools.Tagger.Commands.Generics;

    [Description("Tag web links and DOI.")]
    public class TagWebLinksCommand : GenericDocumentTaggerCommand<IExternalLinksTagger>, ITagWebLinksCommand
    {
        public TagWebLinksCommand(IExternalLinksTagger tagger)
            : base(tagger)
        {
        }
    }
}
