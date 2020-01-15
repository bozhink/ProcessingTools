﻿// <copyright file="TextContentHarvester.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Content
{
    using System;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts.Services.Content;
    using ProcessingTools.Contracts.Services.Xml;

    /// <summary>
    /// Text content harvester.
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