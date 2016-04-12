namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Xml;

    using Contracts;
    using Factories;

    using ProcessingTools.Bio.Data.Miners.Contracts;
    using ProcessingTools.Data.Miners.Common.Contracts;
    using ProcessingTools.Infrastructure.Attributes;

    [Description("Tag specimen count.")]
    public class TagSpecimenCountController : StringTaggerControllerFactory, ITagSpecimenCountController
    {
        private readonly ISpecimenCountDataMiner miner;
        private readonly XmlElement tagModel;

        public TagSpecimenCountController(ISpecimenCountDataMiner miner)
        {
            if (miner == null)
            {
                throw new ArgumentNullException(nameof(miner));
            }

            this.miner = miner;

            XmlDocument document = new XmlDocument();
            this.tagModel = document.CreateElement("named-content");
            this.tagModel.SetAttribute("content-type", "specimen-count");
        }

        protected override IStringDataMiner Miner => this.miner;

        protected override XmlElement TagModel => this.tagModel;
    }
}
