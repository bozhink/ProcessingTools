// <copyright file="TextContentHarvester.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Harvesters.Content
{
    using System;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts.Xml;
    using ProcessingTools.Harvesters.Contracts.Content;

    /// <summary>
    /// Text Content Harvester
    /// </summary>
    public class TextContentHarvester : ITextContentHarvester
    {
        private readonly ITextContentTransformerFactory transformerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextContentHarvester"/> class.
        /// </summary>
        /// <param name="transformerFactory">Factory of <see cref="IXmlTransformer"/>.</param>
        public TextContentHarvester(ITextContentTransformerFactory transformerFactory)
        {
            this.transformerFactory = transformerFactory ?? throw new ArgumentNullException(nameof(transformerFactory));
        }

        /// <inheritdoc/>
        public async Task<string> HarvestAsync(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var content = await this.transformerFactory
                .GetTextContentTransformer()
                .TransformAsync(context)
                .ConfigureAwait(false);

            content = Regex.Replace(content, @"(?<=\n)\s+", string.Empty);

            return content;
        }
    }
}
