// <copyright file="GavinLaurensParser.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Special;

namespace ProcessingTools.Services.Special
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models;

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
