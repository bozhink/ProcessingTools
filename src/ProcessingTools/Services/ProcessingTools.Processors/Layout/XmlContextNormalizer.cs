// <copyright file="XmlContextNormalizer.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Layout
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts.Xml;
    using ProcessingTools.Processors.Contracts;

    /// <summary>
    /// XML context normalizer.
    /// </summary>
    public class XmlContextNormalizer : IXmlContextNormalizer
    {
        private readonly IXmlTransformer transformer;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlContextNormalizer"/> class.
        /// </summary>
        /// <param name="transformer"><see cref="IXmlTransformer"/></param>
        public XmlContextNormalizer(IXmlTransformer transformer)
        {
            this.transformer = transformer ?? throw new ArgumentNullException(nameof(transformer));
        }

        /// <inheritdoc/>
        public Task<string> NormalizeAsync(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return this.transformer.TransformAsync(context);
        }
    }
}
