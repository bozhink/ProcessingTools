namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Xml;

    using Contracts;
    using Factories;
    using ProcessingTools.Data.Miners.Common.Contracts;
    using ProcessingTools.Data.Miners.Contracts;
    using ProcessingTools.Infrastructure.Attributes;

    [Description("Tag institutions.")]
    public class TagInstitutionsController : StringTaggerControllerFactory, ITagInstitutionsController
    {
        private readonly IInstitutionsDataMiner miner;
        private readonly XmlElement tagModel;

        public TagInstitutionsController(IInstitutionsDataMiner miner)
        {
            if (miner == null)
            {
                throw new ArgumentNullException("miner");
            }

            this.miner = miner;

            XmlDocument document = new XmlDocument();
            this.tagModel = document.CreateElement("named-content");
            this.tagModel.SetAttribute("content-type", "institution");
        }

        protected override IStringDataMiner Miner => this.miner;

        protected override XmlElement TagModel => this.tagModel;
    }
}
