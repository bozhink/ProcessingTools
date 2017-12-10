namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Processors.Processors.Bio.Codes;

    [System.ComponentModel.Description("Tag institutional codes.")]
    public class TagInstitutionalCodesCommand : DocumentTaggerCommand<IInstitutionalCodesTagger>, ITagInstitutionalCodesCommand
    {
        public TagInstitutionalCodesCommand(IInstitutionalCodesTagger tagger)
            : base(tagger)
        {
        }
    }
}
