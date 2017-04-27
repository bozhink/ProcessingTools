namespace ProcessingTools.Tagger.Commands.Commands
{
    using Contracts.Commands;
    using Generics;
    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Processors.Bio.Codes;

    [Description("Tag collection codes.")]
    public class TagCollectionCodesCommand : GenericDocumentTaggerCommand<ICollectionCodesTagger>, ITagCollectionCodesCommand
    {
        public TagCollectionCodesCommand(ICollectionCodesTagger tagger)
            : base(tagger)
        {
        }
    }
}
