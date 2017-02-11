namespace ProcessingTools.Harvesters.Harvesters.ExternalLinks
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Xml;
    using Abstractions;
    using Contracts.Factories;
    using Contracts.Harvesters.ExternalLinks;
    using Contracts.Models.ExternalLinks;
    using Models.ExternalLinks;
    using ProcessingTools.Xml.Contracts.Serialization;
    using ProcessingTools.Xml.Contracts.Wrappers;

    public class ExternalLinksHarvester : AbstractGenericEnumerableXmlHarvester<IExternalLinkModel>, IExternalLinksHarvester
    {
        private readonly IXmlTransformDeserializer serializer;
        private readonly IExternalLinksTransformersFactory transformersFactory;

        public ExternalLinksHarvester(
            IXmlContextWrapper contextWrapper,
            IXmlTransformDeserializer serializer,
            IExternalLinksTransformersFactory transformersFactory)
            : base(contextWrapper)
        {
            if (serializer == null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }

            if (transformersFactory == null)
            {
                throw new ArgumentNullException(nameof(transformersFactory));
            }

            this.serializer = serializer;
            this.transformersFactory = transformersFactory;
        }

        protected override async Task<IEnumerable<IExternalLinkModel>> Run(XmlDocument document)
        {
            var transformer = this.transformersFactory.GetExternalLinksTransformer();
            var items = await this.serializer.Deserialize<ExternalLinksModel>(transformer, document.OuterXml);

            return items.ExternalLinks;
        }
    }
}
