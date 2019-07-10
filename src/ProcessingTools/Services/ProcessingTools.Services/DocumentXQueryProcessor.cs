// <copyright file="DocumentXQueryProcessor.cs" company="ProcessingTools">
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
        public async Task ProcessAsync(IDocument context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var content = await this.factory
                .CreateTransformer(xqueryFileName: this.XQueryFileName)
                .TransformAsync(context.Xml)
                .ConfigureAwait(false);

            context.Xml = content;
        }
    }
}
