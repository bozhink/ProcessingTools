namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Xml;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Constants.Content;
    using ProcessingTools.Geo.Data.Miners.Contracts;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;
    using ProcessingTools.Nlm.Publishing.Constants;

    [Description("Tag geo names.")]
    public class TagGeoNamesController : StringMinerTaggerControllerFactory, ITagGeoNamesController
    {
        public TagGeoNamesController(IGeoNamesDataMiner miner, IStringTagger tagger)
            : base(miner, tagger)
        {
        }

        protected override Func<XmlDocument, XmlElement> BuildTagModel => document =>
        {
            var tagModel = document.CreateElement(ElementNames.NamedContent);
            tagModel.SetAttribute(AttributeNames.ContentType, ContentTypeConstants.GeoNameContentType);

            return tagModel;
        };
    }
}
