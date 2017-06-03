namespace ProcessingTools.Tagger.Commands.Commands
{
    using System.ComponentModel;
    using ProcessingTools.Processors.Contracts.Processors.ExternalLinks;
    using ProcessingTools.Tagger.Commands.Contracts.Commands;
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
