namespace ProcessingTools.MainProgram.Controllers
{
    using System;
    using System.Xml;

    using Contracts;
    using Factories;
    using ProcessingTools.Attributes;
    using ProcessingTools.Data.Miners.Common.Contracts;
    using ProcessingTools.Data.Miners.Contracts;

    [Description("Tag geographic deviations.")]
    public class TagGeographicDeviationsController : StringTaggerControllerFactory, ITagGeographicDeviationsController
    {
        private readonly IGeographicDeviationsDataMiner miner;
        private readonly XmlElement tagModel;

        public TagGeographicDeviationsController(IGeographicDeviationsDataMiner miner)
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