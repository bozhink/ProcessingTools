// <copyright file="DynamicExpressionsTests.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Dynamic.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using NUnit.Framework;

    /// <summary>
    /// Dynamic expressions tests.
    /// </summary>
    [TestFixture]
    public class DynamicExpressionsTests
    {
        /// <summary>
        /// Parse parameter expression method call should return int expression.
        /// </summary>
        [Test(TestOf = typeof(DynamicExpressionParser))]
        public void Parse_ParameterExpressionMethodCall_ShouldReturnIntExpression()
        {
            // Arrange + Act
            var expression = DynamicExpressionParser.Parse(
                new[] { Expression.Parameter(typeof(int), "x") },
                typeof(int),
                "x + 1");

            // Assert
            Assert.AreEqual(typeof(int), expression.Type);
        }

        /// <summary>
        /// Parse tuple to string method call should return string lambda expression.
        /// </summary>
        [Test(TestOf = typeof(DynamicExpressionParser))]
        public void Parse_TupleToStringMethodCall_ShouldReturnStringLambdaExpression()
        {
            // Arrange + Act
            var expression = DynamicExpressionParser.ParseLambda(
                typeof(Tuple<int>),
                typeof(string),
                "it.ToString()");

            // Assert
            Assert.AreEqual(typeof(string), expression.ReturnType);
        }

        /// <summary>
        /// ParseLambda delegate type method call should return event handler lambda expression.
        /// </summary>
        [Test(TestOf = typeof(DynamicExpressionParser))]
        public void ParseLambda_DelegateTypeMethodCall_ShouldReturnEventHandlerLambdaExpression()
        {
            // Arrange + Act
            var expression = DynamicExpressionParser.ParseLambda(
                typeof(EventHandler),
                new[] { Expression.Parameter(typeof(object), "sender"), Expression.Parameter(typeof(EventArgs), "e") },
                null,
                "sender.ToString()");

            // Assert
            Assert.AreEqual(typeof(void), expression.ReturnType);
            Assert.AreEqual(typeof(EventHandler), expression.Type);
        }

        /// <summary>
        /// ParseLambda void method call should return action delegate.
        /// </summary>
        [Test(TestOf = typeof(DynamicExpressionParser))]
        public void ParseLambda_VoidMethodCall_ShouldReturnActionDelegate()
        {
            // Arrange + Act
            var expression = DynamicExpressionParser.ParseLambda(
                typeof(System.IO.FileStream),
                null,
                "it.Close()");

            // Assert
            Assert.AreEqual(typeof(void), expression.ReturnType);
            Assert.AreEqual(typeof(Action<System.IO.FileStream>), expression.Type);
        }

        /// <summary>
        /// CreateClass should be thread safe.
        /// </summary>
        [Test(TestOf = typeof(DynamicExpressionParser))]
        public void CreateClass_ShouldBeThreadSafe()
        {
            // Arrange
            const int numOfTasks = 15;

            var properties = new[] { new DynamicProperty("prop1", typeof(string)) };

            var tasks = new List<Task>(numOfTasks);

            // Act
            for (var i = 0; i < numOfTasks; i++)
            {
                tasks.Add(Task.Run(() => DynamicExpressionParser.CreateClass(properties)));
            }

            // Assert
            Assert.IsTrue(tasks.Any());

            Task.WaitAll(tasks.ToArray());
        }
    }
}
