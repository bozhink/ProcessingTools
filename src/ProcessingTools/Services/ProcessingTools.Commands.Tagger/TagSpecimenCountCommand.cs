namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Processors.Contracts.Bio;

    [System.ComponentModel.Description("Tag specimen count.")]
    public class TagSpecimenCountCommand : DocumentTaggerCommand<ISpecimenCountTagger>, ITagSpecimenCountCommand
    {
        public TagSpecimenCountCommand(ISpecimenCountTagger tagger)
            : base(tagger)
        {
        }
    }
}
