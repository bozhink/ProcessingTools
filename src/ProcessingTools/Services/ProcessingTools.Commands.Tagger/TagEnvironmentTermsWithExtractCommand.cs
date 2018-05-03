namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
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
