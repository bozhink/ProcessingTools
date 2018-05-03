namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Processors.Contracts.Bio.Codes;

    [System.ComponentModel.Description("Tag specimen codes.")]
    public class TagSpecimenCodesCommand : DocumentTaggerCommand<ISpecimenCodesByPatternTagger>, ITagSpecimenCodesCommand
    {
        public TagSpecimenCodesCommand(ISpecimenCodesByPatternTagger tagger)
            : base(tagger)
        {
        }
    }
}
