// <copyright file="DynamicExpressionsTests.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Tests.Unit.Tests.Linq.Dynamic
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Extensions.Linq.Dynamic;

    /// <summary>
    /// Dynamic Expressions Tests
    /// </summary>
    [TestClass]
    public class DynamicExpressionsTests
    {
        /// <summary>
        /// Parse parameter expression method call should return int expression.
        /// </summary>
        [TestMethod]
        public void Parse_ParameterExpressionMethodCall_ShouldReturnIntExpression()
        {
            var expression = DynamicExpressionParser.Parse(
                new[] { Expression.Parameter(typeof(int), "x") },
                typeof(int),
                "x + 1");
            Assert.AreEqual(typeof(int), expression.Type);
        }

        /// <summary>
        /// Parse tuple to string method call should return string lambda expression.
        /// </summary>
        [TestMethod]
        public void Parse_TupleToStringMethodCall_ShouldReturnStringLambdaExpression()
        {
            var expression = DynamicExpressionParser.ParseLambda(
                typeof(Tuple<int>),
                typeof(string),
                "it.ToString()");
            Assert.AreEqual(typeof(string), expression.ReturnType);
        }

        /// <summary>
        /// ParseLambda delegate type method call should return event handler lambda expression.
        /// </summary>
        [TestMethod]
        public void ParseLambda_DelegateTypeMethodCall_ShouldReturnEventHandlerLambdaExpression()
        {
            var expression = DynamicExpressionParser.ParseLambda(
                typeof(EventHandler),
                new[] { Expression.Parameter(typeof(object), "sender"), Expression.Parameter(typeof(EventArgs), "e") },
                null,
                "sender.ToString()");

            Assert.AreEqual(typeof(void), expression.ReturnType);
            Assert.AreEqual(typeof(EventHandler), expression.Type);
        }

        /// <summary>
        /// ParseLambda void method call should return action delegate.
        /// </summary>
        [TestMethod]
        public void ParseLambda_VoidMethodCall_ShouldReturnActionDelegate()
        {
            var expression = DynamicExpressionParser.ParseLambda(
                typeof(System.IO.FileStream),
                null,
                "it.Close()");
            Assert.AreEqual(typeof(void), expression.ReturnType);
            Assert.AreEqual(typeof(Action<System.IO.FileStream>), expression.Type);
        }

        /// <summary>
        /// CreateClass should be thread safe.
        /// </summary>
        [TestMethod]
        public void CreateClass_ShouldBeThreadSafe()
        {
            const int numOfTasks = 15;

            var properties = new[] { new DynamicProperty("prop1", typeof(string)) };

            var tasks = new List<Task>(numOfTasks);

            for (var i = 0; i < numOfTasks; i++)
            {
                tasks.Add(Task.Factory.StartNew(() => DynamicExpressionParser.CreateClass(properties)));
            }

            Task.WaitAll(tasks.ToArray());
        }
    }
}
