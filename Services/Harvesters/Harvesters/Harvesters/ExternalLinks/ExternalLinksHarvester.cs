namespace ProcessingTools.Harvesters.ExternalLinks
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using Abstractions;
    using Contracts.ExternalLinks;
    using Contracts.Transformers;
    using Models.ExternalLinks;

    using ProcessingTools.Xml.Contracts.Providers;
    using ProcessingTools.Xml.Contracts.Serialization;

    public class ExternalLinksHarvester : AbstractGenericQueryableXmlHarvester<IExternalLinkModel>, IExternalLinksHarvester
    {
        private readonly IXmlTransformDeserializer<IGetExternalLinksTransformer> transformer;

        public ExternalLinksHarvester(
            IXmlContextWrapperProvider contextWrapperProvider,
            IXmlTransformDeserializer<IGetExternalLinksTransformer> transformer)
            : base(contextWrapperProvider)
        {
            if (transformer == null)
            {
                throw new ArgumentNullException(nameof(transformer));
            }

            this.transformer = transformer;
        }

        protected override async Task<IQueryable<IExternalLinkModel>> Run(XmlDocument document)
        {
            var items = await this.transformer.Deserialize<ExternalLinksModel>(document.OuterXml);

            return items.ExternalLinks?.AsQueryable();
        }
    }
}
