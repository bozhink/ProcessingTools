// <copyright file="IrisPrediction.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace Iris.Models
{
    using Microsoft.ML.Runtime.Api;

    /// <summary>
    /// <see cref="IrisPrediction"/> is the result returned from prediction operations.
    /// </summary>
    public class IrisPrediction
    {
        /// <summary>
        /// Gets or sets the predicted labels.
        /// </summary>
        [ColumnName("PredictedLabel")]
        public string PredictedLabels { get; set; }
    }
}
