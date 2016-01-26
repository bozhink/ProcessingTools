namespace ProcessingTools.MainProgram.Controllers
{
    using System;
    using System.Xml;

    using Contracts;
    using Factories;
    using ProcessingTools.Attributes;
    using ProcessingTools.Data.Miners.Common.Contracts;
    using ProcessingTools.Geo.Data.Miners.Contracts;

    [Description("Tag geo names.")]
    public class TagGeoNamesController : StringTaggerControllerFactory, ITagGeoNamesController
    {
        private readonly IGeoNamesDataMiner miner;
        private readonly XmlElement tagModel;

        public TagGeoNamesController(IGeoNamesDataMiner miner)
        {
            if (miner == null)
            {
                throw new ArgumentNullException("miner");
            }

            this.miner = miner;

            XmlDocument document = new XmlDocument();
            this.tagModel = document.CreateElement("named-content");
            this.tagModel.SetAttribute("content-type", "geo name");
        }

        protected override IStringDataMiner Miner => this.miner;

        protected override XmlElement TagModel => this.tagModel;
    }
}
