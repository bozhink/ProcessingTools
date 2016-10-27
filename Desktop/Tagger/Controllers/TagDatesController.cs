namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Xml;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Constants.Content;
    using ProcessingTools.Data.Miners.Contracts;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;
    using ProcessingTools.Nlm.Publishing.Constants;

    [Description("Tag dates.")]
    public class TagDatesController : StringMinerTaggerControllerFactory, ITagDatesController
    {
        public TagDatesController(IDatesDataMiner miner, IStringTagger tagger)
            : base(miner, tagger)
        {
        }

        protected override Func<XmlDocument, XmlElement> BuildTagModel => document =>
        {
            var tagModel = document.CreateElement(ElementNames.NamedContent);
            tagModel.SetAttribute(AttributeNames.ContentType, ContentTypeConstants.DateContentType);

            return tagModel;
        };
    }
}
