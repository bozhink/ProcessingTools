namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Processors.Processors.Bio;

    [System.ComponentModel.Description("Tag specimen count.")]
    public class TagSpecimenCountCommand : DocumentTaggerCommand<ISpecimenCountTagger>, ITagSpecimenCountCommand
    {
        public TagSpecimenCountCommand(ISpecimenCountTagger tagger)
            : base(tagger)
        {
        }
    }
}
