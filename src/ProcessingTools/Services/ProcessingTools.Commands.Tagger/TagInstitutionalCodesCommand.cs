namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Processors.Contracts.Bio.Codes;

    [System.ComponentModel.Description("Tag institutional codes.")]
    public class TagInstitutionalCodesCommand : DocumentTaggerCommand<IInstitutionalCodesTagger>, ITagInstitutionalCodesCommand
    {
        public TagInstitutionalCodesCommand(IInstitutionalCodesTagger tagger)
            : base(tagger)
        {
        }
    }
}
