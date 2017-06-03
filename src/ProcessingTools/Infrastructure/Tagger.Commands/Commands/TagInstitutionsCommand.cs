namespace ProcessingTools.Tagger.Commands.Commands
{
    using System.ComponentModel;
    using ProcessingTools.Processors.Contracts.Processors.Institutions;
    using ProcessingTools.Tagger.Commands.Contracts.Commands;
    using ProcessingTools.Tagger.Commands.Generics;

    [Description("Tag institutions.")]
    public class TagInstitutionsCommand : GenericDocumentTaggerCommand<IInstitutionsTagger>, ITagInstitutionsCommand
    {
        public TagInstitutionsCommand(IInstitutionsTagger tagger)
            : base(tagger)
        {
        }
    }
}
