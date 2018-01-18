// <copyright file="TextContentHarvester.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Harvesters.Content
{
    using System;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts.Harvesters.Content;
    using ProcessingTools.Contracts.Xml;

    /// <summary>
    /// Text Content Harvester
    /// </summary>
    public class TextContentHarvester : ITextContentHarvester
    {
        private readonly ITextContentTransformersFactory transformersFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextContentHarvester"/> class.
        /// </summary>
        /// <param name="transformersFactory">Factory of <see cref="IXmlTransformer"/>.</param>
        public TextContentHarvester(ITextContentTransformersFactory transformersFactory)
        {
            this.transformersFactory = transformersFactory ?? throw new ArgumentNullException(nameof(transformersFactory));
        }

        /// <inheritdoc/>
        public async Task<string> HarvestAsync(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var content = await this.transformersFactory
                .GetTextContentTransformer()
                .TransformAsync(context)
                .ConfigureAwait(false);

            content = Regex.Replace(content, @"(?<=\n)\s+", string.Empty);

            return content;
        }
    }
}
