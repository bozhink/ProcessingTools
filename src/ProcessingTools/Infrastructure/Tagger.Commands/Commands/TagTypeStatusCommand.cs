namespace ProcessingTools.Tagger.Commands.Commands
{
    using System.ComponentModel;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Processors.Processors.Bio;
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
