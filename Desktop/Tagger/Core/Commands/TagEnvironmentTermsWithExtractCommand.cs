namespace ProcessingTools.Tagger.Controllers
{
    using Contracts.Controllers;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Bio.EnvironmentTerms;

    [Description("Tag envo terms using EXTRACT.")]
    public class TagEnvironmentTermsWithExtractController : GenericDocumentTaggerController<IEnvironmentTermsWithExtractTagger>, ITagEnvironmentTermsWithExtractController
    {
        public TagEnvironmentTermsWithExtractController(IEnvironmentTermsWithExtractTagger tagger)
            : base(tagger)
        {
        }
    }
}
