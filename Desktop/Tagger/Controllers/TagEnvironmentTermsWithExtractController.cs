namespace ProcessingTools.Tagger.Controllers
{
    using Contracts;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Processors.Contracts.EnvironmentTerms;

    [Description("Tag envo terms using EXTRACT.")]
    public class TagEnvironmentTermsWithExtractController : GenericDocumentTaggerController<IEnvironmentTermsWithExtractTagger>, ITagEnvironmentTermsWithExtractController
    {
        public TagEnvironmentTermsWithExtractController(IEnvironmentTermsWithExtractTagger tagger)
            : base(tagger)
        {
        }
    }
}
