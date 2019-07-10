﻿// <copyright file="ExternalLinksHarvester.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services;
using ProcessingTools.Contracts.Services.ExternalLinks;
using ProcessingTools.Contracts.Services.Models.ExternalLinks;
using ProcessingTools.Contracts.Services.Xml;

namespace ProcessingTools.Services.ExternalLinks
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Services.Models.ExternalLinks;

    /// <summary>
    /// External Links Harvester.
    /// </summary>
    public class ExternalLinksHarvester : IExternalLinksHarvester
    {
        private readonly IEnumerableXmlHarvesterCore<IExternalLinkModel> harvesterCore;
        private readonly IXmlTransformDeserializer serializer;
        private readonly IExternalLinksTransformerFactory transformerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalLinksHarvester"/> class.
        /// </summary>
        /// <param name="harvesterCore">Core harvester to be used.</param>
        /// <param name="serializer"><see cref="IXmlTransformDeserializer"/> to be invoked.</param>
        /// <param name="transformerFactory">Factory of <see cref="IXmlTransformer"/>.</param>
        public ExternalLinksHarvester(IEnumerableXmlHarvesterCore<IExternalLinkModel> harvesterCore, IXmlTransformDeserializer serializer, IExternalLinksTransformerFactory transformerFactory)
        {
            this.harvesterCore = harvesterCore ?? throw new ArgumentNullException(nameof(harvesterCore));
            this.serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            this.transformerFactory = transformerFactory ?? throw new ArgumentNullException(nameof(transformerFactory));
        }

        /// <inheritdoc/>
        public Task<IExternalLinkModel[]> HarvestAsync(XmlNode context) => this.harvesterCore.HarvestAsync(context: context, actionAsync: this.RunAsync);

        private async Task<IExternalLinkModel[]> RunAsync(XmlDocument document)
        {
            var transformer = this.transformerFactory.GetExternalLinksTransformer();
            var items = await this.serializer.DeserializeAsync<ExternalLinksModel>(transformer, document.OuterXml).ConfigureAwait(false);

            return items?.ExternalLinks ?? Array.Empty<IExternalLinkModel>();
        }
    }
}
