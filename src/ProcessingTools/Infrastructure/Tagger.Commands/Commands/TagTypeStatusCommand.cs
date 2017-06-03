namespace ProcessingTools.Tagger.Commands.Commands
{
    using System.ComponentModel;
    using ProcessingTools.Processors.Contracts.Processors.Bio;
    using ProcessingTools.Tagger.Commands.Contracts.Commands;
    using ProcessingTools.Tagger.Commands.Generics;

    [Description("Tag type status.")]
    public class TagTypeStatusCommand : GenericDocumentTaggerCommand<ITypeStatusTagger>, ITagTypeStatusCommand
    {
        public TagTypeStatusCommand(ITypeStatusTagger tagger)
            : base(tagger)
        {
        }
    }
}
