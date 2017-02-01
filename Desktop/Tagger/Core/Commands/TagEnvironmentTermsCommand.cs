namespace ProcessingTools.Tagger.Core.Commands
{
    using Contracts.Commands;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Processors.Bio.EnvironmentTerms;

    [Description("Tag envo terms using local database.")]
    public class TagEnvironmentTermsCommand : GenericDocumentTaggerCommand<IEnvironmentTermsTagger>, ITagEnvironmentTermsCommand
    {
        public TagEnvironmentTermsCommand(IEnvironmentTermsTagger tagger)
            : base(tagger)
        {
        }
    }
}
