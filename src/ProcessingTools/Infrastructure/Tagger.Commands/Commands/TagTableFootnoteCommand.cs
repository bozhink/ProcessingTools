namespace ProcessingTools.Tagger.Commands.Commands
{
    using System.ComponentModel;
    using ProcessingTools.Processors.Contracts.Processors.Floats;
    using ProcessingTools.Tagger.Commands.Contracts.Commands;
    using ProcessingTools.Tagger.Commands.Generics;

    [Description("Tag table foot-notes.")]
    public class TagTableFootnoteCommand : GenericXmlContextTaggerCommand<ITableFootNotesTagger>, ITagTableFootnoteCommand
    {
        public TagTableFootnoteCommand(ITableFootNotesTagger tagger)
            : base(tagger)
        {
        }
    }
}
