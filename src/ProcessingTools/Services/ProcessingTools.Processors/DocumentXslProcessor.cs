// <copyright file="DocumentXslProcessor.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Xml;
    using ProcessingTools.Processors.Contracts;

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
