// <copyright file="Program.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace LinearRegression
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CNTK;

    /// <summary>
    /// Main program.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Entry point.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        private static void Main(string[] args)
        {
            // Step 1: Create some Demo helpers
            Console.Title = "Linear Regression with CNTK!";
            Console.WriteLine("#### Linear Regression with CNTK! ####");
            Console.WriteLine();

            // Define device
            var device = DeviceDescriptor.UseDefaultDevice();

            // Step 2: define values, and variables
            Variable x = Variable.InputVariable(new int[] { 1 }, DataType.Float, "input");
            Variable y = Variable.InputVariable(new int[] { 1 }, DataType.Float, "output");

            // Step 2: define training data set from table above
            var xValues = Value.CreateBatch(new NDShape(1, 1), new float[] { 1f, 2f, 3f, 4f, 5f }, device);
            var yValues = Value.CreateBatch(new NDShape(1, 1), new float[] { 3f, 5f, 7f, 9f, 11f }, device);

            // Step 3: create linear regression model
            var lr = CreateLRModel(x, device);

            // Network model contains only two parameters b and w, so we query
            // the model in order to get parameter values
            var paramValues = lr.Inputs.Where(z => z.IsParameter).ToList();
            var totalParameters = paramValues.Sum(c => c.Shape.TotalSize);
            Console.WriteLine($"LRM has {totalParameters} params, {paramValues[0].Name} and {paramValues[1].Name}.");

            // Step 4: create trainer
            var trainer = CreateTrainer(lr, y);

            // Ştep 5: training
            for (int i = 1; i <= 200; i++)
            {
                var d = new Dictionary<Variable, Value>
                {
                    { x, xValues },
                    { y, yValues }
                };

                trainer.TrainMinibatch(d, true, device);

                var loss = trainer.PreviousMinibatchLossAverage();
                var eval = trainer.PreviousMinibatchEvaluationAverage();

                if (i % 20 == 0)
                {
                    Console.WriteLine($"It={i}, Loss={loss}, Eval={eval}");
                }

                if (i == 200)
                {
                    // Print weights
                    var b0_name = paramValues[0].Name;
                    var b0 = new Value(paramValues[0].GetValue()).GetDenseData<float>(paramValues[0]);
                    var b1_name = paramValues[1].Name;
                    var b1 = new Value(paramValues[1].GetValue()).GetDenseData<float>(paramValues[1]);
                    Console.WriteLine($" ");
                    Console.WriteLine($"Training process finished with the following regression parameters:");
                    Console.WriteLine($"b={b0[0][0]} / {b0_name}, w={b1[0][0]} / {b1_name}");
                    Console.WriteLine($" ");
                }
            }
        }

        private static Function CreateLRModel(Variable x, DeviceDescriptor device)
        {
            // Initializer for parameters
            var initV = CNTKLib.GlorotUniformInitializer(1.0, 1, 0, 1);

            // Bias
            var b = new Parameter(new NDShape(1, 1), DataType.Float, initV, device, "b");

            // Weights
            var w = new Parameter(new NDShape(2, 1), DataType.Float, initV, device, "w");

            // Matrix product
            var wx = CNTKLib.Times(w, x, "wx");

            // Layer
            var l = CNTKLib.Plus(b, wx, "wx_b");

            return l;
        }

        private static Trainer CreateTrainer(Function network, Variable target)
        {
            // Learning rate
            var lrate = 0.082;
            var lr = new TrainingParameterScheduleDouble(lrate);

            ////// Network parameters
            ////var zparams = new ParameterVector(network.Parameters().ToList());

            // Create loss and eval
            Function loss = CNTKLib.SquaredError(network, target);
            Function eval = CNTKLib.SquaredError(network, target);

            // Learners
            var llr = new List<Learner>();
            var msgd = Learner.SGDLearner(network.Parameters(), lr, new AdditionalLearningOptions { });
            llr.Add(msgd);

            // Trainer
            var trainer = Trainer.CreateTrainer(network, loss, eval, llr);

            return trainer;
        }
    }
}
