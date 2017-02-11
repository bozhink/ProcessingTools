namespace ProcessingTools.Tagger.Commands.Commands
{
    using Contracts.Commands;
    using Generics;
    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Processors.Institutions;

    [Description("Tag institutions.")]
    public class TagInstitutionsCommand : GenericDocumentTaggerCommand<IInstitutionsTagger>, ITagInstitutionsCommand
    {
        public TagInstitutionsCommand(IInstitutionsTagger tagger)
            : base(tagger)
        {
        }
    }
}
