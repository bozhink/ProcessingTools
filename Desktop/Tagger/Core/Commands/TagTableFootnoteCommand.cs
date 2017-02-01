namespace ProcessingTools.Tagger.Core.Commands
{
    using Contracts.Commands;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Processors.Floats;

    [Description("Tag table foot-notes.")]
    public class TagTableFootnoteCommand : GenericXmlContextTaggerCommand<ITableFootNotesTagger>, ITagTableFootnoteCommand
    {
        public TagTableFootnoteCommand(ITableFootNotesTagger tagger)
            : base(tagger)
        {
        }
    }
}
