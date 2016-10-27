namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Xml;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Data.Miners.Contracts;
    using ProcessingTools.Constants.Content;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;
    using ProcessingTools.Nlm.Publishing.Constants;

    [Description("Tag type status.")]
    public class TagTypeStatusController : StringMinerTaggerControllerFactory, ITagTypeStatusController
    {

        public TagTypeStatusController(ITypeStatusDataMiner miner, IStringTagger tagger)
            : base(miner, tagger)
        {
        }

        protected override Func<XmlDocument, XmlElement> BuildTagModel => document =>
        {
            var tagModel = document.CreateElement(ElementNames.NamedContent);
            tagModel.SetAttribute(AttributeNames.ContentType, ContentTypeConstants.TypeStatusContentType);

            return tagModel;
        };
    }
}
