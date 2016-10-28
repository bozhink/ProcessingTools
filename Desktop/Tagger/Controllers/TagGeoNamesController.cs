namespace ProcessingTools.Tagger.Controllers
{
    using Contracts;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Geo;

    [Description("Tag geo names.")]
    public class TagGeoNamesController : GenericDocumentTaggerController<IGeoNamesTagger>, ITagGeoNamesController
    {
        public TagGeoNamesController(IGeoNamesTagger tagger)
            : base(tagger)
        {
        }
    }
}
