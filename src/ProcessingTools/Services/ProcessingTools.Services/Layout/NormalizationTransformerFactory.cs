// <copyright file="NormalizationTransformerFactory.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Layout
{
    using System;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Contracts.Services.Layout;
    using ProcessingTools.Contracts.Services.Xml;

    /// <summary>
    /// Normalization transformer factory.
    /// </summary>
    public class NormalizationTransformerFactory : INormalizationTransformerFactory
    {
        private readonly IFormatTransformerFactory transformerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="NormalizationTransformerFactory"/> class.
        /// </summary>
        /// <param name="transformerFactory">Transformer factory.</param>
        public NormalizationTransformerFactory(IFormatTransformerFactory transformerFactory)
        {
            this.transformerFactory = transformerFactory ?? throw new ArgumentNullException(nameof(transformerFactory));
        }

        /// <inheritdoc/>
        public IXmlTransformer Create(SchemaType schemaType)
        {
            switch (schemaType)
            {
                case SchemaType.Nlm:
                    return this.transformerFactory.GetFormatToNlmTransformer();

                default:
                    return this.transformerFactory.GetFormatToSystemTransformer();
            }
        }
    }
}
