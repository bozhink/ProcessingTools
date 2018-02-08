// <copyright file="AbbreviationsHarvester.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Harvesters.Abbreviations
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts.Harvesters;
    using ProcessingTools.Contracts.Harvesters.Abbreviations;
    using ProcessingTools.Contracts.Xml;
    using ProcessingTools.Harvesters.Models.Abbreviations;
    using ProcessingTools.Models.Contracts.Harvesters.Abbreviations;

    /// <summary>
    /// Abbreviations Harvester.
    /// </summary>
    public class AbbreviationsHarvester : IAbbreviationsHarvester
    {
        private readonly IEnumerableXmlHarvesterCore<IAbbreviationModel> harvesterCore;
        private readonly IXmlTransformDeserializer serializer;
        private readonly IAbbreviationsTransformersFactory transformersFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbbreviationsHarvester"/> class.
        /// </summary>
        /// <param name="harvesterCore">Core harvester to be used.</param>
        /// <param name="serializer"><see cref="IXmlTransformDeserializer"/> to be invoked.</param>
        /// <param name="transformersFactory">Factory of <see cref="IXmlTransformer"/>.</param>
        public AbbreviationsHarvester(IEnumerableXmlHarvesterCore<IAbbreviationModel> harvesterCore, IXmlTransformDeserializer serializer, IAbbreviationsTransformersFactory transformersFactory)
        {
            this.harvesterCore = harvesterCore ?? throw new ArgumentNullException(nameof(harvesterCore));
            this.serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            this.transformersFactory = transformersFactory ?? throw new ArgumentNullException(nameof(transformersFactory));
        }

        /// <inheritdoc/>
        public Task<IAbbreviationModel[]> HarvestAsync(XmlNode context) => this.harvesterCore.HarvestAsync(context: context, actionAsync: this.RunAsync);

        private async Task<IAbbreviationModel[]> RunAsync(XmlDocument document)
        {
            var transformer = this.transformersFactory.GetAbbreviationsTransformer();
            var model = await this.serializer.Deserialize<AbbreviationsXmlModel>(transformer, document.DocumentElement).ConfigureAwait(false);

            return model?.Abbreviations ?? new IAbbreviationModel[] { };
        }
    }
}
