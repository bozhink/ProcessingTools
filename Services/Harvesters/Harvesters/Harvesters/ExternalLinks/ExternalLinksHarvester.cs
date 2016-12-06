namespace ProcessingTools.Harvesters.Harvesters.ExternalLinks
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Harvesters.Abstractions;
    using ProcessingTools.Harvesters.Contracts.ExternalLinks;
    using ProcessingTools.Harvesters.Contracts.Transformers;
    using ProcessingTools.Harvesters.Models.ExternalLinks;
    using ProcessingTools.Xml.Contracts.Providers;
    using ProcessingTools.Xml.Contracts.Serialization;

    public class ExternalLinksHarvester : AbstractGenericQueryableXmlHarvester<IExternalLinkModel>, IExternalLinksHarvester
    {
        private readonly IXmlTransformDeserializer serializer;
        private readonly IGetExternalLinksTransformer transformer;

        public ExternalLinksHarvester(
            IXmlContextWrapperProvider contextWrapperProvider,
            IXmlTransformDeserializer serializer,
            IGetExternalLinksTransformer transformer)
            : base(contextWrapperProvider)
        {
            if (serializer == null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }

            if (transformer == null)
            {
                throw new ArgumentNullException(nameof(transformer));
            }

            this.serializer = serializer;
            this.transformer = transformer;
        }

        protected override async Task<IQueryable<IExternalLinkModel>> Run(XmlDocument document)
        {
            var items = await this.serializer.Deserialize<ExternalLinksModel>(this.transformer, document.OuterXml);

            return items.ExternalLinks?.AsQueryable();
        }
    }
}
