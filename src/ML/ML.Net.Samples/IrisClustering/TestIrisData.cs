// <copyright file="TestIrisData.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace IrisClustering
{
    using IrisClustering.Models;

    /// <summary>
    /// Test data.
    /// </summary>
    internal static class TestIrisData
    {
        /// <summary>
        /// Test item.
        /// </summary>
        internal static readonly IrisData Setosa = new IrisData
        {
            SepalLength = 5.1f,
            SepalWidth = 3.5f,
            PetalLength = 1.4f,
            PetalWidth = 0.2f
        };
    }
}
