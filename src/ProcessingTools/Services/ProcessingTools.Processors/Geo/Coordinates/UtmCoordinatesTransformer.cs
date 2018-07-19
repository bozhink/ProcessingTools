// <copyright file="UtmCoordinatesTransformer.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Geo.Coordinates
{
    using System;
    using ProcessingTools.Geo.Contracts;
    using ProcessingTools.Processors.Contracts.Geo.Coordinates;

    /// <summary>
    /// UTM Coordinates Transformer
    /// </summary>
    public class UtmCoordinatesTransformer : IUtmCoordinatesTransformer
    {
        private readonly IUtmCoordinatesConverter converter;

        /// <summary>
        /// Initializes a new instance of the <see cref="UtmCoordinatesTransformer"/> class.
        /// </summary>
        /// <param name="converter">Instance of <see cref="IUtmCoordinatesConverter"/>.</param>
        public UtmCoordinatesTransformer(IUtmCoordinatesConverter converter)
        {
            this.converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        /// <inheritdoc/>
        public double[] TransformDecimal2Utm(double latitude, double longitude, string utmZone) => this.converter.TransformDecimal2Utm(latitude, longitude, utmZone);

        /// <inheritdoc/>
        public double[] TransformUtm2Decimal(double utmEasting, double utmNorthing, string utmZone) => this.converter.TransformUtm2Decimal(utmEasting, utmNorthing, utmZone);
    }
}
