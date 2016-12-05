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

namespace ProcessingTools.Harvesters.Harvesters.ExternalLinks
{
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
