namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Xml;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Data.Miners.Common.Contracts;
    using ProcessingTools.Geo.Data.Miners.Contracts;

    [Description("Tag coordinates.")]
    public class TagCoordinatesController : StringTaggerControllerFactory, ITagCoordinatesController
    {
        private readonly ICoordinatesDataMiner miner;
        private readonly XmlElement tagModel;

        public TagCoordinatesController(ICoordinatesDataMiner miner)
        {
            if (miner == null)
            {
                throw new ArgumentNullException(nameof(miner));
            }

            this.miner = miner;

            XmlDocument document = new XmlDocument();
            this.tagModel = document.CreateElement("locality-coordinates");
        }

        protected override IStringDataMiner Miner => this.miner;

        protected override XmlElement TagModel => this.tagModel;
    }
}