// <copyright file="SentimentData.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace SentimentAnalysis.Models
{
    using Microsoft.ML.Runtime.Api;

    /// <summary>
    /// SentimentData is the input dataset class and has a float (Sentiment) that has a value for sentiment of either positive or negative, and a string for the comment (SentimentText). Both fields have Column attributes attached to them. This attribute describes the order of each field in the data file, and which is the Label field.
    /// </summary>
    public class SentimentData
    {
        /// <summary>
        /// Gets or sets the sentiment value.
        /// </summary>
        [Column(ordinal: "0", name: "Label")]
        public float Sentiment { get; set; }

        /// <summary>
        /// Gets or sets the sentiment text.
        /// </summary>
        [Column(ordinal: "1")]
        public string SentimentText { get; set; }
    }
}
