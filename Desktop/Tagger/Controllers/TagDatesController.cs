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

    [Description("Tag dates.")]
    public class TagDatesController : StringMinerTaggerControllerFactory, ITagDatesController
    {
        private readonly XmlElement tagModel;

        public TagDatesController(IDatesDataMiner miner, IDocumentFactory documentFactory, IStringTagger tagger)
            : base(miner, documentFactory, tagger)
        {
            XmlDocument document = new XmlDocument();
            this.tagModel = document.CreateElement(ElementNames.NamedContent);
            this.tagModel.SetAttribute(AttributeNames.ContentType, ContentTypeConstants.DateContentType);
        }

        protected override XmlElement TagModel => this.tagModel;
    }
}
