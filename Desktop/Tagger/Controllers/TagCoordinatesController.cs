namespace ProcessingTools.Tagger.Controllers
{
    using System.Xml;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Geo.Data.Miners.Contracts;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;

    [Description("Tag coordinates.")]
    public class TagCoordinatesController : StringMinerTaggerControllerFactory, ITagCoordinatesController
    {
        private readonly XmlElement tagModel;

        public TagCoordinatesController(ICoordinatesDataMiner miner, IDocumentFactory documentFactory, IStringTagger tagger)
            : base(miner, documentFactory, tagger)
        {
            XmlDocument document = new XmlDocument();
            this.tagModel = document.CreateElement(ElementNames.GeoCoordinateElementName);
        }

        protected override XmlElement TagModel => this.tagModel;
    }
}
