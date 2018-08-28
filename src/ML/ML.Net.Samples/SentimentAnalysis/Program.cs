// <copyright file="Program.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace SentimentAnalysis
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.ML;
    using Microsoft.ML.Data;
    using Microsoft.ML.Models;
    using Microsoft.ML.Runtime.Api;
    using Microsoft.ML.Trainers;
    using Microsoft.ML.Transforms;
    using SentimentAnalysis.Models;

    /// <summary>
    /// Main Program.
    /// </summary>
    public static class Program
    {
        private static readonly string DataPath = Path.Combine(Environment.CurrentDirectory, "Data", "wikipedia-detox-250-line-data.tsv");
        private static readonly string TestDataPath = Path.Combine(Environment.CurrentDirectory, "Data", "wikipedia-detox-250-line-test.tsv");
        private static readonly string ModelPath = Path.Combine(Environment.CurrentDirectory, "Data", "Model.zip");

        /// <summary>
        /// Program entry point.
        /// </summary>
        /// <param name="args">Command line parameters.</param>
        /// <returns>Task</returns>
        public static async Task Main(string[] args)
        {
            var model = await TrainAsync().ConfigureAwait(false);
            Evaluate(model);
            Predict(model);
        }

        /// <summary>
        /// Trains model with training data.
        /// </summary>
        /// <returns>Prediction model.</returns>
        public static async Task<PredictionModel<SentimentData, SentimentPrediction>> TrainAsync()
        {
            // Initialize a new instance of LearningPipeline that will include the data loading, data processing/featurization, and model.
            var pipeline = new LearningPipeline();

            // Ingest the data
            // The TextLoader object is the first part of the pipeline, and loads the training file data.
            pipeline.Add(new TextLoader(DataPath).CreateFrom<SentimentData>());

            // Data preprocess and feature engineering
            // Apply a TextFeaturizer to convert the SentimentText column into a numeric vector called Features used by the machine learning algorithm. This is the preprocessing/featurization step. Using additional components available in ML.NET can enable better results with your model.
            pipeline.Add(new TextFeaturizer("Features", "SentimentText"));

            // Choose a learning algorithm
            // The FastTreeBinaryClassifier object is a decision tree learner you'll use in this pipeline. Similar to the featurization step, trying out different learners available in ML.NET and changing their parameters leads to different results. For tuning, you can set hyperparameters like NumTrees, NumLeaves, and MinDocumentsInLeafs. These hyperparameters are set before anything affects the model and are model-specific. They're used to tune the decision tree for performance, so larger values can negatively impact performance.
            pipeline.Add(new FastTreeBinaryClassifier() { NumLeaves = 5, NumTrees = 5, MinDocumentsInLeafs = 2 });

            // Train the model
            // You train the model, PredictionModel<TInput,TOutput>, based on the dataset that has been loaded and transformed. pipeline.Train<SentimentData, SentimentPrediction>() trains the pipeline (loads the data, trains the featurizer and learner). The experiment is not executed until this happens.
            var model = pipeline.Train<SentimentData, SentimentPrediction>();

            // Save and Return the model trained to use for evaluation
            await model.WriteAsync(ModelPath).ConfigureAwait(false);

            return model;
        }

        /// <summary>
        /// Evaluates the model.
        /// </summary>
        /// <param name="model">Prediction model to be evaluated.</param>
        public static void Evaluate(PredictionModel<SentimentData, SentimentPrediction> model)
        {
            // Load the test dataset
            // The TextLoader class loads the new test dataset with the same schema. You can evaluate the model using this dataset as a quality check.
            var testData = new TextLoader(TestDataPath).CreateFrom<SentimentData>();

            // Create the binary evaluator
            // The BinaryClassificationEvaluator object computes the quality metrics for the PredictionModel using the specified dataset.
            var evaluator = new BinaryClassificationEvaluator();

            // Evaluate the model and create metrics
            // The BinaryClassificationMetrics contains the overall metrics computed by binary classification evaluators. To display these to determine the quality of the model, you need to get the metrics first.
            BinaryClassificationMetrics metrics = evaluator.Evaluate(model, testData);

            // Displaying the metrics for model validation
            Console.WriteLine();
            Console.WriteLine("PredictionModel quality metrics evaluation");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"Accuracy: {metrics.Accuracy:P2}");
            Console.WriteLine($"Auc: {metrics.Auc:P2}");
            Console.WriteLine($"F1Score: {metrics.F1Score:P2}");
        }

        /// <summary>
        /// Predicts the test data outcomes with the model.
        /// </summary>
        /// <param name="model">Prediction model to be used.</param>
        public static void Predict(PredictionModel<SentimentData, SentimentPrediction> model)
        {
            // Create test data
            IEnumerable<SentimentData> sentiments = new[]
            {
                new SentimentData
                {
                    SentimentText = "Please refrain from adding nonsense to Wikipedia."
                },
                new SentimentData
                {
                    SentimentText = "He is the best, and the article should say that."
                }
            };

            // Predict sentiment based on test data
            IEnumerable<SentimentPrediction> predictions = model.Predict(sentiments);

            // Combine test data and predictions for reporting
            var sentimentsAndPredictions = sentiments.Zip(predictions, (sentiment, prediction) => (sentiment, prediction));

            // Display the predicted results
            Console.WriteLine();
            Console.WriteLine("Sentiment Predictions");
            Console.WriteLine("---------------------");
            foreach (var (sentiment, prediction) in sentimentsAndPredictions)
            {
                Console.WriteLine($"Sentiment: {sentiment.SentimentText} | Prediction: {(prediction.Sentiment ? "Positive" : "Negative")}");
            }

            Console.WriteLine();
        }
    }
}
