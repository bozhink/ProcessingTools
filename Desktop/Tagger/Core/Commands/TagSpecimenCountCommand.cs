namespace ProcessingTools.Tagger.Commands
{
    using Contracts.Commands;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Bio;

    [Description("Tag specimen count.")]
    public class TagSpecimenCountCommand : GenericDocumentTaggerCommand<ISpecimenCountTagger>, ITagSpecimenCountCommand
    {
        public TagSpecimenCountCommand(ISpecimenCountTagger tagger)
            : base(tagger)
        {
        }
    }
}
