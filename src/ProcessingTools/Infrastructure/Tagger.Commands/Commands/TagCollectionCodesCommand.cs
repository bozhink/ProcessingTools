namespace ProcessingTools.Tagger.Commands.Commands
{
    using System.ComponentModel;
    using ProcessingTools.Processors.Contracts.Processors.Bio.Codes;
    using ProcessingTools.Tagger.Commands.Contracts.Commands;
    using ProcessingTools.Tagger.Commands.Generics;

    [Description("Tag collection codes.")]
    public class TagCollectionCodesCommand : GenericDocumentTaggerCommand<ICollectionCodesTagger>, ITagCollectionCodesCommand
    {
        public TagCollectionCodesCommand(ICollectionCodesTagger tagger)
            : base(tagger)
        {
        }
    }
}
