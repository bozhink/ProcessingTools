namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Processors.Processors.Institutions;

    [System.ComponentModel.Description("Tag institutions.")]
    public class TagInstitutionsCommand : DocumentTaggerCommand<IInstitutionsTagger>, ITagInstitutionsCommand
    {
        public TagInstitutionsCommand(IInstitutionsTagger tagger)
            : base(tagger)
        {
        }
    }
}
