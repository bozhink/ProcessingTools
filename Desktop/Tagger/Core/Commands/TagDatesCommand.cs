namespace ProcessingTools.Tagger.Core.Commands
{
    using Contracts.Commands;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Dates;

    [Description("Tag dates.")]
    public class TagDatesCommand : GenericDocumentTaggerCommand<IDatesTagger>, ITagDatesCommand
    {
        public TagDatesCommand(IDatesTagger tagger)
            : base(tagger)
        {
        }
    }
}
