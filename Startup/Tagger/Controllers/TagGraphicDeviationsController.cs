namespace ProcessingTools.MainProgram.Controllers
{
    using System;
    using System.Xml;

    using Contracts;
    using Factories;
    using ProcessingTools.Data.Miners.Common.Contracts;
    using ProcessingTools.Data.Miners.Contracts;

    public class TagGraphicDeviationsController : StringTaggerControllerFactory, ITagGraphicDeviationsController
    {
        private readonly IGeographicDeviationsDataMiner miner;
        private readonly XmlElement tagModel;

        public TagGraphicDeviationsController(IGeographicDeviationsDataMiner miner)
        {
            if (miner == null)
            {
                throw new ArgumentNullException("miner");
            }

            this.miner = miner;

            XmlDocument document = new XmlDocument();
            this.tagModel = document.CreateElement("named-content");
            this.tagModel.SetAttribute("content-type", "geographic deviation");
        }

        protected override IStringDataMiner Miner => this.miner;

        protected override XmlElement TagModel => this.tagModel;
    }
}