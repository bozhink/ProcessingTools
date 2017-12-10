namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Processors.Processors.Dates;

    [System.ComponentModel.Description("Tag dates.")]
    public class TagDatesCommand : DocumentTaggerCommand<IDatesTagger>, ITagDatesCommand
    {
        public TagDatesCommand(IDatesTagger tagger)
            : base(tagger)
        {
        }
    }
}
