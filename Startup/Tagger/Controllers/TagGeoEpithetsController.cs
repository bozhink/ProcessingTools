namespace ProcessingTools.MainProgram.Controllers
{
    using System;
    using System.Xml;

    using Contracts;
    using Factories;
    using ProcessingTools.Attributes;
    using ProcessingTools.Data.Miners.Common.Contracts;
    using ProcessingTools.Geo.Data.Miners.Contracts;

    [Description("Tag geo epithets.")]
    public class TagGeoEpithetsController : StringTaggerControllerFactory, ITagGeoEpithetsController
    {
        private readonly IGeoEpithetsDataMiner miner;
        private readonly XmlElement tagModel;

        public TagGeoEpithetsController(IGeoEpithetsDataMiner miner)
        {
            if (miner == null)
            {
                throw new ArgumentNullException("miner");
            }

            this.miner = miner;

            XmlDocument document = new XmlDocument();
            this.tagModel = document.CreateElement("named-content");
            this.tagModel.SetAttribute("content-type", "geo epithet");
        }

        protected override IStringDataMiner Miner => this.miner;

        protected override XmlElement TagModel => this.tagModel;
    }
}