// <copyright file="IrisData.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace IrisClustering.Models
{
    using Microsoft.ML.Runtime.Api;

    /// <summary>
    /// IrisData is the input data class and has definitions for each feature from the data set. Use the Column attribute to specify the indices of the source columns in the data set file.
    /// </summary>
    public class IrisData
    {
        /// <summary>
        /// Gets or sets the sepal length,
        /// </summary>
        [Column("0")]
        public float SepalLength { get; set; }

        /// <summary>
        /// Gets or sets the sepal width.
        /// </summary>
        [Column("1")]
        public float SepalWidth { get; set; }

        /// <summary>
        /// Gets or sets the petal length.
        /// </summary>
        [Column("2")]
        public float PetalLength { get; set; }

        /// <summary>
        /// Gets or sets the petal width.
        /// </summary>
        [Column("3")]
        public float PetalWidth { get; set; }
    }
}
