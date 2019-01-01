// <copyright file="IrisData.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace Iris.Models
{
    using Microsoft.ML.Runtime.Api;

    /// <summary>
    /// <para>IrisData is used to provide training data, and as input for prediction operations</para>
    /// <para>- First 4 properties are inputs/features used to predict the label</para>
    /// <para>- Label is what you are predicting, and is only set when training</para>
    /// </summary>
    public class IrisData
    {
        /// <summary>
        /// Gets or sets the sepal length.
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

        /// <summary>
        /// Gets or sets the classification label.
        /// </summary>
        [Column("4")]
        [ColumnName("Label")]
        public string Label { get; set; }
    }
}
