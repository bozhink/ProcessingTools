namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Xml;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Data.Miners.Contracts;
    using ProcessingTools.Data.Miners.Common.Contracts;

    [Description("Tag type status.")]
    public class TagTypeStatusController : StringTaggerControllerFactory, ITagTypeStatusController
    {
        private readonly ITypeStatusDataMiner miner;
        private readonly XmlElement tagModel;

        public TagTypeStatusController(ITypeStatusDataMiner miner)
        {
            if (miner == null)
            {
                throw new ArgumentNullException(nameof(miner));
            }

            this.miner = miner;

            XmlDocument document = new XmlDocument();
            this.tagModel = document.CreateElement("named-content");
            this.tagModel.SetAttribute("content-type", "dwc:typeStatus");
        }

        protected override IStringDataMiner Miner => this.miner;

        protected override XmlElement TagModel => this.tagModel;
    }
}
