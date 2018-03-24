// <copyright file="ZooBankRegistrationXmlGenerator.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Bio.ZooBank
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Bio.ZooBank;

    /// <summary>
    /// ZooBank registration XML generator.
    /// </summary>
    public class ZooBankRegistrationXmlGenerator : IZooBankRegistrationXmlGenerator
    {
        private readonly IZooBankTransformerFactory transformerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZooBankRegistrationXmlGenerator"/> class.
        /// </summary>
        /// <param name="transformerFactory">Transformer factory.</param>
        public ZooBankRegistrationXmlGenerator(IZooBankTransformerFactory transformerFactory)
        {
            this.transformerFactory = transformerFactory ?? throw new ArgumentNullException(nameof(transformerFactory));
        }

        /// <inheritdoc/>
        public async Task<object> GenerateAsync(IDocument context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var content = await this.transformerFactory
                .GetZooBankRegistrationTransformer()
                .TransformAsync(context.Xml)
                .ConfigureAwait(false);

            context.Xml = content;

            return true;
        }
    }
}
