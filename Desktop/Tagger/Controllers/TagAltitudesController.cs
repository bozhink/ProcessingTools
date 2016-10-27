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

    [Description("Tag altitudes.")]
    public class TagAltitudesController : StringMinerTaggerControllerFactory, ITagAltitudesController
    {
        public TagAltitudesController(IAltitudesDataMiner miner, IStringTagger tagger)
            : base(miner, tagger)
        {
        }

        protected override Func<XmlDocument, XmlElement> BuildTagModel => document =>
        {
            var tagModel = document.CreateElement(ElementNames.NamedContent);
            tagModel.SetAttribute(AttributeNames.ContentType, ContentTypeConstants.AltitudeContentType);

            return tagModel;
        };
    }
}
