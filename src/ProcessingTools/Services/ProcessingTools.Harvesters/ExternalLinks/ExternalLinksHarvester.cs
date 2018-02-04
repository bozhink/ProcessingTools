// <copyright file="ExternalLinksHarvester.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Harvesters.ExternalLinks
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts.Harvesters;
    using ProcessingTools.Contracts.Harvesters.ExternalLinks;
    using ProcessingTools.Models.Contracts.Harvesters.ExternalLinks;
    using ProcessingTools.Contracts.Xml;
    using ProcessingTools.Harvesters.Models.ExternalLinks;

    /// <summary>
    /// External Links Harvester.
    /// </summary>
    public class ExternalLinksHarvester : IExternalLinksHarvester
    {
        private readonly IEnumerableXmlHarvesterCore<IExternalLinkModel> harvesterCore;
        private readonly IXmlTransformDeserializer serializer;
        private readonly IExternalLinksTransformersFactory transformersFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalLinksHarvester"/> class.
        /// </summary>
        /// <param name="harvesterCore">Core harvester to be used.</param>
        /// <param name="serializer"><see cref="IXmlTransformDeserializer"/> to be invoked.</param>
        /// <param name="transformersFactory">Factory of <see cref="IXmlTransformer"/>.</param>
        public ExternalLinksHarvester(IEnumerableXmlHarvesterCore<IExternalLinkModel> harvesterCore, IXmlTransformDeserializer serializer, IExternalLinksTransformersFactory transformersFactory)
        {
            this.harvesterCore = harvesterCore ?? throw new ArgumentNullException(nameof(harvesterCore));
            this.serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            this.transformersFactory = transformersFactory ?? throw new ArgumentNullException(nameof(transformersFactory));
        }

        /// <inheritdoc/>
        public Task<IExternalLinkModel[]> HarvestAsync(XmlNode context) => this.harvesterCore.HarvestAsync(context: context, actionAsync: this.RunAsync);

        private async Task<IExternalLinkModel[]> RunAsync(XmlDocument document)
        {
            var transformer = this.transformersFactory.GetExternalLinksTransformer();
            var items = await this.serializer.Deserialize<ExternalLinksModel>(transformer, document.OuterXml).ConfigureAwait(false);

            return items?.ExternalLinks ?? new IExternalLinkModel[] { };
        }
    }
}
