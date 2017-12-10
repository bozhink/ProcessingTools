namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Processors.Processors.Bio.EnvironmentTerms;

    [System.ComponentModel.Description("Tag envo terms using local database.")]
    public class TagEnvironmentTermsCommand : DocumentTaggerCommand<IEnvironmentTermsTagger>, ITagEnvironmentTermsCommand
    {
        public TagEnvironmentTermsCommand(IEnvironmentTermsTagger tagger)
            : base(tagger)
        {
        }
    }
}
