﻿namespace ProcessingTools.Harvesters.Harvesters.ExternalLinks
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using Abstractions;
    using Contracts.Factories;
    using Contracts.Harvesters.ExternalLinks;
    using Contracts.Models.ExternalLinks;
    using Models.ExternalLinks;
    using ProcessingTools.Xml.Contracts.Providers;
    using ProcessingTools.Xml.Contracts.Serialization;

    public class ExternalLinksHarvester : AbstractGenericQueryableXmlHarvester<IExternalLinkModel>, IExternalLinksHarvester
    {
        private readonly IXmlTransformDeserializer serializer;
        private readonly IExternalLinksTransformersFactory transformersFactory;

        public ExternalLinksHarvester(
            IXmlContextWrapperProvider contextWrapperProvider,
            IXmlTransformDeserializer serializer,
            IExternalLinksTransformersFactory transformersFactory)
            : base(contextWrapperProvider)
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

        protected override async Task<IQueryable<IExternalLinkModel>> Run(XmlDocument document)
        {
            var transformer = this.transformersFactory.GetExternalLinksTransformer();
            var items = await this.serializer.Deserialize<ExternalLinksModel>(transformer, document.OuterXml);

            return items.ExternalLinks?.AsQueryable();
        }
    }
}
