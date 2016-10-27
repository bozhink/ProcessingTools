namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Xml;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Data.Miners.Contracts;
    using ProcessingTools.Constants.Content;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;
    using ProcessingTools.Nlm.Publishing.Constants;

    [Description("Tag specimen count.")]
    public class TagSpecimenCountController : StringMinerTaggerControllerFactory, ITagSpecimenCountController
    {
        public TagSpecimenCountController(ISpecimenCountDataMiner miner, IStringTagger tagger)
            : base(miner, tagger)
        {
        }

        protected override Func<XmlDocument, XmlElement> BuildTagModel => document =>
        {
            var tagModel = document.CreateElement(ElementNames.NamedContent);
            tagModel.SetAttribute(AttributeNames.ContentType, ContentTypeConstants.SpecimenCountContentType);

            return tagModel;
        };
    }
}
