namespace ProcessingTools.Tagger.Controllers
{
    using System.Xml;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Data.Miners.Contracts;
    using ProcessingTools.Constants.Content;
    using ProcessingTools.Contracts;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;
    using ProcessingTools.Nlm.Publishing.Constants;

    [Description("Tag morphological epithets.")]
    public class TagMorphologicalEpithetsController : StringMinerTaggerControllerFactory, ITagMorphologicalEpithetsController
    {
        private readonly XmlElement tagModel;

        public TagMorphologicalEpithetsController(IMorphologicalEpithetsDataMiner miner, IDocumentFactory documentFactory, IStringTagger tagger)
            : base(miner, documentFactory, tagger)
        {
            var document = new XmlDocument();
            this.tagModel = document.CreateElement(ElementNames.NamedContent);
            this.tagModel.SetAttribute(AttributeNames.ContentType, ContentTypeConstants.MorphologicalEpithetContentType);
        }

        protected override XmlElement TagModel => this.tagModel;
    }
}
