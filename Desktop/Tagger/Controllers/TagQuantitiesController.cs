namespace ProcessingTools.Tagger.Controllers
{
    using Contracts;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Quantities;

    [Description("Tag quantities.")]
    public class TagQuantitiesController : GenericDocumentTaggerController<IQuantitiesTagger>, ITagQuantitiesController
    {
        public TagQuantitiesController(IQuantitiesTagger tagger)
            : base(tagger)
        {
        }
    }
}
