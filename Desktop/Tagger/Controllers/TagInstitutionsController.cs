namespace ProcessingTools.Tagger.Controllers
{
    using Contracts.Controllers;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Institutions;

    [Description("Tag institutions.")]
    public class TagInstitutionsController : GenericDocumentTaggerController<IInstitutionsTagger>, ITagInstitutionsController
    {
        public TagInstitutionsController(IInstitutionsTagger tagger)
            : base(tagger)
        {
        }
    }
}
