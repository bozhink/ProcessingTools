namespace ProcessingTools.Tagger.Commands.Commands
{
    using System.ComponentModel;
    using ProcessingTools.Processors.Contracts.Processors.Bio.Codes;
    using ProcessingTools.Tagger.Commands.Contracts.Commands;
    using ProcessingTools.Tagger.Commands.Generics;

    [Description("Tag institutional codes.")]
    public class TagInstitutionalCodesCommand : GenericDocumentTaggerCommand<IInstitutionalCodesTagger>, ITagInstitutionalCodesCommand
    {
        public TagInstitutionalCodesCommand(IInstitutionalCodesTagger tagger)
            : base(tagger)
        {
        }
    }
}
