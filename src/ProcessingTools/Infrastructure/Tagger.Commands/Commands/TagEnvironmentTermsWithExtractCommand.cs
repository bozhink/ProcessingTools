namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Processors.Contracts.Bio.EnvironmentTerms;

    [System.ComponentModel.Description("Tag envo terms using EXTRACT.")]
    public class TagEnvironmentTermsWithExtractCommand : DocumentTaggerCommand<IEnvironmentTermsWithExtractTagger>, ITagEnvironmentTermsWithExtractCommand
    {
        public TagEnvironmentTermsWithExtractCommand(IEnvironmentTermsWithExtractTagger tagger)
            : base(tagger)
        {
        }
    }
}
