namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Xml;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Geo.Data.Miners.Contracts;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;

    [Description("Tag coordinates.")]
    public class TagCoordinatesController : StringMinerTaggerControllerFactory, ITagCoordinatesController
    {
        public TagCoordinatesController(ICoordinatesDataMiner miner, IStringTagger tagger)
            : base(miner, tagger)
        {
        }

        protected override Func<XmlDocument, XmlElement> BuildTagModel => document =>
        {
            var tagModel = document.CreateElement(ElementNames.GeoCoordinateElementName);

            return tagModel;
        };
    }
}
