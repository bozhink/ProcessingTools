namespace ProcessingTools.Harvesters.ExternalLinks
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts.Harvesters.ExternalLinks;
    using ProcessingTools.Contracts.Models.Harvesters.ExternalLinks;
    using ProcessingTools.Contracts.Xml;
    using ProcessingTools.Harvesters.Abstractions;
    using ProcessingTools.Harvesters.Models.ExternalLinks;

    public class ExternalLinksHarvester : AbstractEnumerableXmlHarvester<IExternalLinkModel>, IExternalLinksHarvester
    {
        private readonly IXmlTransformDeserializer serializer;
        private readonly IExternalLinksTransformersFactory transformersFactory;

        public ExternalLinksHarvester(IXmlContextWrapper contextWrapper, IXmlTransformDeserializer serializer, IExternalLinksTransformersFactory transformersFactory)
            : base(contextWrapper)
        {
            this.serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            this.transformersFactory = transformersFactory ?? throw new ArgumentNullException(nameof(transformersFactory));
        }

        protected override async Task<IExternalLinkModel[]> RunAsync(XmlDocument document)
        {
            var transformer = this.transformersFactory.GetExternalLinksTransformer();
            var items = await this.serializer.Deserialize<ExternalLinksModel>(transformer, document.OuterXml);

            return items.ExternalLinks;
        }
    }
}
