namespace ProcessingTools.Tagger.Commands.Commands
{
    using Contracts.Commands;
    using Generics;
    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Processors.Dates;

    [Description("Tag dates.")]
    public class TagDatesCommand : GenericDocumentTaggerCommand<IDatesTagger>, ITagDatesCommand
    {
        public TagDatesCommand(IDatesTagger tagger)
            : base(tagger)
        {
        }
    }
}
