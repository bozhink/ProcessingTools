// <copyright file="ClusterPrediction.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace IrisClustering.Models
{
    using Microsoft.ML.Runtime.Api;

    /// <summary>
    /// The ClusterPrediction class represents the output of the clustering model applied to an IrisData instance. Use the ColumnName attribute to bind the PredictedClusterId and Distances fields to the PredictedLabel and Score columns respectively.
    /// </summary>
    public class ClusterPrediction
    {
        /// <summary>
        /// Gets or sets  the ID of the predicted cluster.
        /// </summary>
        [ColumnName("PredictedLabel")]
        public uint PredictedClusterId { get; set; }

        /// <summary>
        /// Gets or sets the array with squared Euclidean distances to the cluster centroids. The array length is equal to the number of clusters.
        /// </summary>
        [ColumnName("Score")]
        public float[] Distances { get; set; }
    }
}
