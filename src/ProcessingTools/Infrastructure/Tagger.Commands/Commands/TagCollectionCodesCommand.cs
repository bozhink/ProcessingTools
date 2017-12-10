namespace ProcessingTools.Tagger.Commands.Commands
{
    using System.ComponentModel;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Processors.Processors.Bio.Codes;
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
