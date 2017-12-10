namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Processors.Processors.Floats;

    [System.ComponentModel.Description("Tag table foot-notes.")]
    public class TagTableFootnoteCommand : XmlContextTaggerCommand<ITableFootNotesTagger>, ITagTableFootnoteCommand
    {
        public TagTableFootnoteCommand(ITableFootNotesTagger tagger)
            : base(tagger)
        {
        }
    }
}
