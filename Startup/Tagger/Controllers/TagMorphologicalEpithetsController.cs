namespace ProcessingTools.MainProgram.Controllers
{
    using System;
    using System.Xml;

    using Contracts;
    using Factories;
    using ProcessingTools.Bio.Data.Miners.Contracts;
    using ProcessingTools.Data.Miners.Common.Contracts;

    public class TagMorphologicalEpithetsController : StringTaggerControllerFactory, ITagMorphologicalEpithetsController
    {
        private readonly XmlElement tagModel;
        private readonly IMorphologicalEpithetsDataMiner miner;

        public TagMorphologicalEpithetsController(IMorphologicalEpithetsDataMiner miner)
        {
            if (miner == null)
            {
                throw new ArgumentNullException("miner");
            }

            this.miner = miner;

            XmlDocument document = new XmlDocument();
            this.tagModel = document.CreateElement("named-content");
            this.tagModel.SetAttribute("content-type", "morphological epithet");
        }

        protected override IStringDataMiner Miner => this.miner;

        protected override XmlElement TagModel => this.tagModel;
    }
}
