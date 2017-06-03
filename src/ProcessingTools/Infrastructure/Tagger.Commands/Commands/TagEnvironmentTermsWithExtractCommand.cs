namespace ProcessingTools.Tagger.Commands.Commands
{
    using System.ComponentModel;
    using ProcessingTools.Processors.Contracts.Processors.Bio.EnvironmentTerms;
    using ProcessingTools.Tagger.Commands.Contracts.Commands;
    using ProcessingTools.Tagger.Commands.Generics;

    [Description("Tag envo terms using EXTRACT.")]
    public class TagEnvironmentTermsWithExtractCommand : GenericDocumentTaggerCommand<IEnvironmentTermsWithExtractTagger>, ITagEnvironmentTermsWithExtractCommand
    {
        public TagEnvironmentTermsWithExtractCommand(IEnvironmentTermsWithExtractTagger tagger)
            : base(tagger)
        {
        }
    }
}
