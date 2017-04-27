namespace ProcessingTools.Tagger.Commands.Commands
{
    using Contracts.Commands;
    using Generics;
    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Processors.Bio;

    [Description("Tag type status.")]
    public class TagTypeStatusCommand : GenericDocumentTaggerCommand<ITypeStatusTagger>, ITagTypeStatusCommand
    {
        public TagTypeStatusCommand(ITypeStatusTagger tagger)
            : base(tagger)
        {
        }
    }
}
