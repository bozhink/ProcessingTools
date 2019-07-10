// <copyright file="DocumentXslProcessor.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services;
using ProcessingTools.Contracts.Services.Xml;

namespace ProcessingTools.Services
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Document XSL processor.
    /// </summary>
    public class DocumentXslProcessor : IDocumentXslProcessor
    {
        private readonly IXslTransformerFactory factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentXslProcessor"/> class.
        /// </summary>
        /// <param name="factory">XSL transformer factory.</param>
        public DocumentXslProcessor(IXslTransformerFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <inheritdoc/>
        public string XslFileName { get; set; }

        /// <inheritdoc/>
        public async Task ProcessAsync(IDocument context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var content = await this.factory
                .CreateTransformerFromFile(fileName: this.XslFileName)
                .TransformAsync(context.Xml)
                .ConfigureAwait(false);

            context.Xml = content;
        }
    }
}
