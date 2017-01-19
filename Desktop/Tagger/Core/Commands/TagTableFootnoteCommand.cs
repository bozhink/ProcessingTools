namespace ProcessingTools.Tagger.Commands
{
    using Contracts.Commands;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Floats;

    [Description("Tag table foot-notes.")]
    public class TagTableFootnoteController : GenericXmlContextTaggerController<ITableFootNotesTagger>, ITagTableFootnoteController
    {
        public TagTableFootnoteController(ITableFootNotesTagger tagger)
            : base(tagger)
        {
        }
    }
}
