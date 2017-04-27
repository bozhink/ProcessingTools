namespace ProcessingTools.Tagger.Commands.Commands
{
    using Contracts.Commands;
    using Generics;
    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Processors.Bio.EnvironmentTerms;

    [Description("Tag envo terms using EXTRACT.")]
    public class TagEnvironmentTermsWithExtractCommand : GenericDocumentTaggerCommand<IEnvironmentTermsWithExtractTagger>, ITagEnvironmentTermsWithExtractCommand
    {
        public TagEnvironmentTermsWithExtractCommand(IEnvironmentTermsWithExtractTagger tagger)
            : base(tagger)
        {
        }
    }
}
