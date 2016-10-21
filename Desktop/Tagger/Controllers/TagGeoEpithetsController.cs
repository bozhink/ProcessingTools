namespace ProcessingTools.Tagger.Controllers
{
    using System.Xml;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Constants.Content;
    using ProcessingTools.Contracts;
    using ProcessingTools.Geo.Data.Miners.Contracts;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;
    using ProcessingTools.Nlm.Publishing.Constants;

    [Description("Tag geo epithets.")]
    public class TagGeoEpithetsController : StringMinerTaggerControllerFactory, ITagGeoEpithetsController
    {
        private readonly XmlElement tagModel;

        public TagGeoEpithetsController(IGeoEpithetsDataMiner miner, IDocumentFactory documentFactory, IStringTagger tagger)
            : base(miner, documentFactory, tagger)
        {
            XmlDocument document = new XmlDocument();
            this.tagModel = document.CreateElement(ElementNames.NamedContent);
            this.tagModel.SetAttribute(AttributeNames.ContentType, ContentTypeConstants.GeoEpithetContentType);
        }

        protected override XmlElement TagModel => this.tagModel;
    }
}
