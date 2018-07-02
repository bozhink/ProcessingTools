// <copyright file="GavinLaurensParser.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Special
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Special;

    /// <summary>
    /// Gavin-Laurens parser.
    /// </summary>
    public class GavinLaurensParser : IGavinLaurensParser
    {
        private readonly ISpecialTransformerFactory transformerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="GavinLaurensParser"/> class.
        /// </summary>
        /// <param name="transformerFactory">Special transformer factory.</param>
        public GavinLaurensParser(ISpecialTransformerFactory transformerFactory)
        {
            this.transformerFactory = transformerFactory ?? throw new ArgumentNullException(nameof(transformerFactory));
        }

        /// <inheritdoc/>
        public async Task<object> ParseAsync(IDocument context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var transformer = this.transformerFactory.GetGavinLaurensTransformer();
            context.Xml = await transformer.TransformAsync(context.Xml).ConfigureAwait(false);

            return true;
        }
    }
}
