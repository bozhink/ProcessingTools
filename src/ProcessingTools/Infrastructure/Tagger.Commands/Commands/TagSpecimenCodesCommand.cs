namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
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
