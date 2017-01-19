namespace ProcessingTools.Tagger.Commands
{
    using Contracts.Commands;
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
