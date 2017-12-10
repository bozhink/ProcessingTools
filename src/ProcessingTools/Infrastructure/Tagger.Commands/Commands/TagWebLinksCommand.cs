namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Processors.Processors.ExternalLinks;

    [System.ComponentModel.Description("Tag web links and DOI.")]
    public class TagWebLinksCommand : DocumentTaggerCommand<IExternalLinksTagger>, ITagWebLinksCommand
    {
        public TagWebLinksCommand(IExternalLinksTagger tagger)
            : base(tagger)
        {
        }
    }
}
