namespace ProcessingTools.Tagger.Commands.Commands
{
    using System.ComponentModel;
    using ProcessingTools.Processors.Contracts.Processors.Bio.EnvironmentTerms;
    using ProcessingTools.Tagger.Commands.Contracts.Commands;
    using ProcessingTools.Tagger.Commands.Generics;

    [Description("Tag envo terms using local database.")]
    public class TagEnvironmentTermsCommand : GenericDocumentTaggerCommand<IEnvironmentTermsTagger>, ITagEnvironmentTermsCommand
    {
        public TagEnvironmentTermsCommand(IEnvironmentTermsTagger tagger)
            : base(tagger)
        {
        }
    }
}
