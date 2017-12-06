namespace ProcessingTools.Tagger.Commands.Commands
{
    using System.ComponentModel;
    using ProcessingTools.Contracts.Processors.Processors.Dates;
    using ProcessingTools.Tagger.Commands.Contracts.Commands;
    using ProcessingTools.Tagger.Commands.Generics;

    [Description("Tag dates.")]
    public class TagDatesCommand : GenericDocumentTaggerCommand<IDatesTagger>, ITagDatesCommand
    {
        public TagDatesCommand(IDatesTagger tagger)
            : base(tagger)
        {
        }
    }
}
