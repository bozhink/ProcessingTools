namespace ProcessingTools.Tagger.Controllers
{
    using Contracts.Controllers;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Geo;

    [Description("Tag geographic deviations.")]
    public class TagGeographicDeviationsController : GenericDocumentTaggerController<IGeographicDeviationsTagger>, ITagGeographicDeviationsController
    {
        public TagGeographicDeviationsController(IGeographicDeviationsTagger tagger)
            : base(tagger)
        {
        }
    }
}
