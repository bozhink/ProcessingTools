namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Xml;

    using Contracts;
    using Factories;
    using ProcessingTools.Data.Miners.Common.Contracts;
    using ProcessingTools.Data.Miners.Contracts;
    using ProcessingTools.Infrastructure.Attributes;

    [Description("Tag dates.")]
    public class TagDatesController : StringTaggerControllerFactory, ITagDatesController
    {
        private readonly IDatesDataMiner miner;
        private readonly XmlElement tagModel;

        public TagDatesController(IDatesDataMiner miner)
        {
            if (miner == null)
            {
                throw new ArgumentNullException("miner");
            }

            this.miner = miner;

            XmlDocument document = new XmlDocument();
            this.tagModel = document.CreateElement("named-content");
            this.tagModel.SetAttribute("content-type", "date");
        }

        protected override IStringDataMiner Miner => this.miner;

        protected override XmlElement TagModel => this.tagModel;
    }
}