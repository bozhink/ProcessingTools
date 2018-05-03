namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
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
