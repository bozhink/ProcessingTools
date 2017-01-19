namespace ProcessingTools.Tagger.Commands
{
    using Contracts.Commands;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Bio.EnvironmentTerms;

    [Description("Tag envo terms using local database.")]
    public class TagEnvironmentTermsCommand : GenericDocumentTaggerCommand<IEnvironmentTermsTagger>, ITagEnvironmentTermsCommand
    {
        public TagEnvironmentTermsCommand(IEnvironmentTermsTagger tagger)
            : base(tagger)
        {
        }
    }
}
