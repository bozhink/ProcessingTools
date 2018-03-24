namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Processors.Contracts.Floats;

    [System.ComponentModel.Description("Tag table foot-notes.")]
    public class TagTableFootnoteCommand : XmlContextTaggerCommand<ITableFootnotesTagger>, ITagTableFootnoteCommand
    {
        public TagTableFootnoteCommand(ITableFootnotesTagger tagger)
            : base(tagger)
        {
        }
    }
}
