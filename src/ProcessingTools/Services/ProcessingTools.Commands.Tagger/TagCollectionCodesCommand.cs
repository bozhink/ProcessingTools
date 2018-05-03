namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Processors.Contracts.Bio.Codes;

    [System.ComponentModel.Description("Tag collection codes.")]
    public class TagCollectionCodesCommand : DocumentTaggerCommand<ICollectionCodesTagger>, ITagCollectionCodesCommand
    {
        public TagCollectionCodesCommand(ICollectionCodesTagger tagger)
            : base(tagger)
        {
        }
    }
}
