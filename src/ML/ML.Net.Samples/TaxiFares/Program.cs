// <copyright file="Program.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace TaxiFares
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.ML;
    using Microsoft.ML.Data;
    using Microsoft.ML.Models;
    using Microsoft.ML.Trainers;
    using Microsoft.ML.Transforms;
    using TaxiFares.Models;

    /// <summary>
    /// Main Program.
    /// </summary>
    public static class Program
    {
        private static readonly string DataPath = Path.Combine(Environment.CurrentDirectory, "Data", "taxi-fare-train.csv");
        private static readonly string TestDataPath = Path.Combine(Environment.CurrentDirectory, "Data", "taxi-fare-test.csv");
        private static readonly string ModelPath = Path.Combine(Environment.CurrentDirectory, "Data", "Model.zip");

        /// <summary>
        /// Program entry point.
        /// </summary>
        /// <param name="args">Command line parameters.</param>
        /// <returns>Task</returns>
        public static async Task Main(string[] args)
        {
            PredictionModel<TaxiTrip, TaxiTripFarePrediction> model = await TrainAsync();
            Evaluate(model);

            TaxiTripFarePrediction prediction = model.Predict(TestTrips.Trip1);
            Console.WriteLine("Predicted fare: {0}, actual fare: 29.5", prediction.FareAmount);
        }

        private static async Task<PredictionModel<TaxiTrip, TaxiTripFarePrediction>> TrainAsync()
        {
            var pipeline = new LearningPipeline
            {
                // Load and transform data
                // The first step to perform is to load data from the training data set. In our case, training data set is stored in the text file with a path defined by the DataPath field. That file has the header with the column names, so the first row should be ignored while loading data. Columns in the file are separated by the comma (",").
                new TextLoader(DataPath).CreateFrom<TaxiTrip>(useHeader: true, separator: ','),

                // When the model is trained and evaluated, by default, the values in the Label column are considered as correct values to be predicted. As we want to predict the taxi trip fare, copy the FareAmount column into the Label column.
                new ColumnCopier(("FareAmount", "Label")),

                // The algorithm that trains the model requires numeric features, so you have to transform the categorical data (VendorId, RateCode, and PaymentType) values into numbers. To do that, use CategoricalOneHotVectorizer, which assigns different numeric key values to the different values in each of the columns.
                new CategoricalOneHotVectorizer("VendorId", "RateCode", "PaymentType"),

                // The last step in data preparation combines all of the feature columns into the Features column using the ColumnConcatenator transformation class. By default, a learning algorithm processes only features from the Features column.
                new ColumnConcatenator("Features", "VendorId", "RateCode", "PassengerCount", "TripDistance", "PaymentType"),

                // Choose a learning algorithm
                // After adding the data to the pipeline and transforming it into the correct input format, you select a learning algorithm (learner). The learner trains the model. You chose a regression task for this problem, so you use a FastTreeRegressor learner, which is one of the regression learners provided by ML.NET.
                // FastTreeRegressor learner utilizes gradient boosting. Gradient boosting is a machine learning technique for regression problems. It builds each regression tree in a step-wise fashion. It uses a pre-defined loss function to measure the error in each step and correct for it in the next. The result is a prediction model that is actually an ensemble of weaker prediction models. For more information about gradient boosting, see Boosted Decision Tree Regression.
                new FastTreeRegressor()
            };

            // Train the model
            var model = pipeline.Train<TaxiTrip, TaxiTripFarePrediction>();

            // Save the model
            await model.WriteAsync(ModelPath);

            return model;
        }

        private static void Evaluate(PredictionModel<TaxiTrip, TaxiTripFarePrediction> model)
        {
            var testData = new TextLoader(TestDataPath).CreateFrom<TaxiTrip>(useHeader: true, separator: ',');

            var evaluator = new RegressionEvaluator();

            var metrics = evaluator.Evaluate(model, testData);

            // RMS is one of the evaluation metrics of the regression model. The lower it is, the better the model is.
            Console.WriteLine($"RMS = {metrics.Rms}");

            // RSquared is another evaluation metric of the regression models. RSquared takes values between 0 and 1. The closer its value is to 1, the better the model is.
            Console.WriteLine($"RSquared = {metrics.RSquared}");
        }
    }
}
