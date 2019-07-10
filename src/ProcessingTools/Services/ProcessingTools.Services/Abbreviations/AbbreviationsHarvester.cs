﻿// <copyright file="AbbreviationsHarvester.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services;
using ProcessingTools.Contracts.Services.Abbreviations;
using ProcessingTools.Contracts.Services.Models.Abbreviations;
using ProcessingTools.Contracts.Services.Xml;

namespace ProcessingTools.Services.Abbreviations
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Services.Models.Abbreviations;

    /// <summary>
    /// Abbreviations Harvester.
    /// </summary>
    public class AbbreviationsHarvester : IAbbreviationsHarvester
    {
        private readonly IEnumerableXmlHarvesterCore<IAbbreviationModel> harvesterCore;
        private readonly IXmlTransformDeserializer serializer;
        private readonly IAbbreviationsTransformerFactory transformerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbbreviationsHarvester"/> class.
        /// </summary>
        /// <param name="harvesterCore">Core harvester to be used.</param>
        /// <param name="serializer"><see cref="IXmlTransformDeserializer"/> to be invoked.</param>
        /// <param name="transformerFactory">Factory of <see cref="IXmlTransformer"/>.</param>
        public AbbreviationsHarvester(IEnumerableXmlHarvesterCore<IAbbreviationModel> harvesterCore, IXmlTransformDeserializer serializer, IAbbreviationsTransformerFactory transformerFactory)
        {
            this.harvesterCore = harvesterCore ?? throw new ArgumentNullException(nameof(harvesterCore));
            this.serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            this.transformerFactory = transformerFactory ?? throw new ArgumentNullException(nameof(transformerFactory));
        }

        /// <inheritdoc/>
        public Task<IAbbreviationModel[]> HarvestAsync(XmlNode context) => this.harvesterCore.HarvestAsync(context: context, actionAsync: this.RunAsync);

        private async Task<IAbbreviationModel[]> RunAsync(XmlDocument document)
        {
            var transformer = this.transformerFactory.GetAbbreviationsTransformer();
            var model = await this.serializer.DeserializeAsync<AbbreviationsXmlModel>(transformer, document.DocumentElement).ConfigureAwait(false);

            return model?.Abbreviations ?? Array.Empty<IAbbreviationModel>();
        }
    }
}
