// <copyright file="Program.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace IrisClustering
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using IrisClustering.Models;
    using Microsoft.ML;
    using Microsoft.ML.Data;
    using Microsoft.ML.Trainers;
    using Microsoft.ML.Transforms;

    /// <summary>
    /// Main Program.
    /// </summary>
    public static class Program
    {
        private static readonly string DataPath = Path.Combine(Environment.CurrentDirectory, "Data", "iris.data");
        private static readonly string ModelPath = Path.Combine(Environment.CurrentDirectory, "Data", "IrisClusteringModel.zip");

        /// <summary>
        /// Program entry point.
        /// </summary>
        /// <param name="args">Command line parameters.</param>
        /// <returns>Task</returns>
        public static async Task Main(string[] args)
        {
            PredictionModel<IrisData, ClusterPrediction> model = await TrainAsync();

            var prediction = model.Predict(TestIrisData.Setosa);
            Console.WriteLine($"Cluster: {prediction.PredictedClusterId}");
            Console.WriteLine($"Distances: {string.Join(" ", prediction.Distances)}");
        }

        private static async Task<PredictionModel<IrisData, ClusterPrediction>> TrainAsync()
        {
            var pipeline = new LearningPipeline
            {
                // Load and transform data
                // The first step to perform is to load the training data set. In our case, the training data set is stored in the text file with a path defined by the DataPath field. Columns in the file are separated by the comma (",").
                new TextLoader(DataPath).CreateFrom<IrisData>(separator: ','),

                // The next step is to combine all of the feature columns into the Features column using the ColumnConcatenator transformation class. By default, a learning algorithm processes only features from the Features column.
                new ColumnConcatenator("Features", "SepalLength", "SepalWidth", "PetalLength", "PetalWidth"),

                // Choose a learning algorithm
                // After adding the data to the pipeline and transforming it into the correct input format, you select a learning algorithm (learner). The learner trains the model. ML.NET provides a KMeansPlusPlusClusterer learner that implements k-means algorithm with an improved method for choosing the initial cluster centroids.
                new KMeansPlusPlusClusterer() { K = 3 }
            };

            // Train the model
            var model = pipeline.Train<IrisData, ClusterPrediction>();

            // Save the model
            await model.WriteAsync(ModelPath);

            return model;
        }
    }
}
