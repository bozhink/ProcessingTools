// <copyright file="TaxiTripFarePrediction.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace TaxiFares.Models
{
    using Microsoft.ML.Runtime.Api;

    /// <summary>
    /// TaxiTripFarePrediction class represents predicted results. It has a single float field, FareAmount, with a Score ColumnName attribute applied. In case of the regression task the Score column contains predicted label values.
    /// </summary>
    public class TaxiTripFarePrediction
    {
        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        [ColumnName("Score")]
        public float FareAmount { get; set; }
    }
}
