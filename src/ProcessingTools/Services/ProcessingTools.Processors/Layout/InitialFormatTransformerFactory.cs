// <copyright file="InitialFormatTransformerFactory.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Layout
{
    using System;
    using ProcessingTools.Contracts.Xml;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Processors.Contracts.Layout;

    /// <summary>
    /// Initial format transformer factory.
    /// </summary>
    public class InitialFormatTransformerFactory : IInitialFormatTransformerFactory
    {
        private readonly IFormatTransformerFactory transformerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="InitialFormatTransformerFactory"/> class.
        /// </summary>
        /// <param name="transformerFactory">Transformer factory.</param>
        public InitialFormatTransformerFactory(IFormatTransformerFactory transformerFactory)
        {
            this.transformerFactory = transformerFactory ?? throw new ArgumentNullException(nameof(transformerFactory));
        }

        /// <inheritdoc/>
        public IXmlTransformer Create(SchemaType schemaType)
        {
            switch (schemaType)
            {
                case SchemaType.Nlm:
                    return this.transformerFactory.GetNlmInitialFormatTransformer();

                default:
                    return this.transformerFactory.GetSystemInitialFormatTransformer();
            }
        }
    }
}
