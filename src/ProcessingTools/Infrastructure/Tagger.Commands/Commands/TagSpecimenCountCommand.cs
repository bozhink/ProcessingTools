namespace ProcessingTools.Tagger.Commands.Commands
{
    using System.ComponentModel;
    using ProcessingTools.Processors.Contracts.Processors.Bio;
    using ProcessingTools.Tagger.Commands.Contracts.Commands;
    using ProcessingTools.Tagger.Commands.Generics;

    [Description("Tag specimen count.")]
    public class TagSpecimenCountCommand : GenericDocumentTaggerCommand<ISpecimenCountTagger>, ITagSpecimenCountCommand
    {
        public TagSpecimenCountCommand(ISpecimenCountTagger tagger)
            : base(tagger)
        {
        }
    }
}
