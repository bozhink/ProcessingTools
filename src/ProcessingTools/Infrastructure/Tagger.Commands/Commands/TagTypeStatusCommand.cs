namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Processors.Contracts.Bio;

    [System.ComponentModel.Description("Tag type status.")]
    public class TagTypeStatusCommand : DocumentTaggerCommand<ITypeStatusTagger>, ITagTypeStatusCommand
    {
        public TagTypeStatusCommand(ITypeStatusTagger tagger)
            : base(tagger)
        {
        }
    }
}
