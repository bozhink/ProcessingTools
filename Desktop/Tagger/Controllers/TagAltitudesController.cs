namespace ProcessingTools.Tagger.Controllers
{
    using System.Xml;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Constants.Content;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Miners.Contracts;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;
    using ProcessingTools.Nlm.Publishing.Constants;

    [Description("Tag altitudes.")]
    public class TagAltitudesController : StringMinerTaggerControllerFactory, ITagAltitudesController
    {
        private readonly XmlElement tagModel;

        public TagAltitudesController(IAltitudesDataMiner miner, IDocumentFactory documentFactory, IStringTagger tagger)
            : base(miner, documentFactory, tagger)
        {
            var document = new XmlDocument();
            this.tagModel = document.CreateElement(ElementNames.NamedContent);
            this.tagModel.SetAttribute(AttributeNames.ContentType, ContentTypeConstants.AltitudeContentType);
        }

        protected override XmlElement TagModel => this.tagModel;
    }
}
