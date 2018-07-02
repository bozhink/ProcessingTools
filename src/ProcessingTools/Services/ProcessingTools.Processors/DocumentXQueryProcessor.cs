// <copyright file="DocumentXQueryProcessor.cs" company="ProcessingTools">
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
