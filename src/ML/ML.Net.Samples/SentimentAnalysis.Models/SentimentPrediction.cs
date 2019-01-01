// <copyright file="SentimentPrediction.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace SentimentAnalysis.Models
{
    using Microsoft.ML.Runtime.Api;

    /// <summary>
    /// SentimentPrediction is the class used for prediction after the model has been trained. It has a single boolean (Sentiment) and a PredictedLabel ColumnName attribute. The Label is used to create and train the model, and it's also used with a second dataset to evaluate the model. The PredictedLabel is used during prediction and evaluation.
    /// </summary>
    public class SentimentPrediction
    {
        /// <summary>
        /// Gets or sets a value indicating whether predicted sentiment label is positive or negative.
        /// </summary>
        [ColumnName("PredictedLabel")]
        public bool Sentiment { get; set; }
    }
}
