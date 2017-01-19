namespace ProcessingTools.Tagger.Commands
{
    using Contracts.Commands;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Bio.Codes;

    [Description("Tag institutional codes.")]
    public class TagInstitutionalCodesController : GenericDocumentTaggerController<IInstitutionalCodesTagger>, ITagInstitutionalCodesController
    {
        public TagInstitutionalCodesController(IInstitutionalCodesTagger tagger)
            : base(tagger)
        {
        }
    }
}
