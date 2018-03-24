namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Processors.Contracts.Institutions;

    [System.ComponentModel.Description("Tag institutions.")]
    public class TagInstitutionsCommand : DocumentTaggerCommand<IInstitutionsTagger>, ITagInstitutionsCommand
    {
        public TagInstitutionsCommand(IInstitutionsTagger tagger)
            : base(tagger)
        {
        }
    }
}
