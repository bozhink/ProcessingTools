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

    [Description("Tag geographic deviations.")]
    public class TagGeographicDeviationsController : StringMinerTaggerControllerFactory, ITagGeographicDeviationsController
    {
        private readonly XmlElement tagModel;

        public TagGeographicDeviationsController(IGeographicDeviationsDataMiner miner, IDocumentFactory documentFactory, IStringTagger tagger)
            : base(miner, documentFactory, tagger)
        {
            XmlDocument document = new XmlDocument();
            this.tagModel = document.CreateElement(ElementNames.NamedContent);
            this.tagModel.SetAttribute(AttributeNames.ContentType, ContentTypeConstants.GeographicDeviationContentType);
        }

        protected override XmlElement TagModel => this.tagModel;
    }
}
