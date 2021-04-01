// <copyright file="DocumentXQueryProcessor.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Xml;

    /// <summary>
    /// Document XQuery processor.
    /// </summary>
    public class DocumentXQueryProcessor : IDocumentXQueryProcessor
    {
        private readonly IXQueryTransformerFactory factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentXQueryProcessor"/> class.
        /// </summary>
        /// <param name="factory">XQuery transformer factory.</param>
        public DocumentXQueryProcessor(IXQueryTransformerFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <inheritdoc/>
        public string XQueryFileName { get; set; }

        /// <inheritdoc/>
        public Task ProcessAsync(IDocument context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return this.ProcessInternalAsync(context);
        }

        private async Task ProcessInternalAsync(IDocument context)
        {
            var content = await this.factory
                .CreateTransformer(xqueryFileName: this.XQueryFileName)
                .TransformAsync(context.Xml)
                .ConfigureAwait(false);

            context.Xml = content;
        }
    }
}
