// <copyright file="DocumentXslProcessor.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Xml;

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
        public Task ProcessAsync(IDocument context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return this.ProcessInternalAsync(context);
        }

        private async Task ProcessInternalAsync(IDocument context)
        {
            var content = await this.factory
                .CreateTransformerFromFile(fileName: this.XslFileName)
                .TransformAsync(context.Xml)
                .ConfigureAwait(false);

            context.Xml = content;
        }
    }
}
