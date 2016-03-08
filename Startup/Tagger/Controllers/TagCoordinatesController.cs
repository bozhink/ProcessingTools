namespace ProcessingTools.MainProgram.Controllers
{
    using System;
    using System.Xml;

    using Contracts;
    using Factories;

    using ProcessingTools.Data.Miners.Common.Contracts;
    using ProcessingTools.Geo.Data.Miners.Contracts;
    using ProcessingTools.Infrastructure.Attributes;

    [Description("Tag coordinates.")]
    public class TagCoordinatesController : StringTaggerControllerFactory, ITagCoordinatesController
    {
        private readonly ICoordinatesDataMiner miner;
        private readonly XmlElement tagModel;

        public TagCoordinatesController(ICoordinatesDataMiner miner)
        {
            if (miner == null)
            {
                throw new ArgumentNullException("miner");
            }

            this.miner = miner;

            XmlDocument document = new XmlDocument();
            this.tagModel = document.CreateElement("locality-coordinates");
        }

        protected override IStringDataMiner Miner => this.miner;

        protected override XmlElement TagModel => this.tagModel;
    }
}