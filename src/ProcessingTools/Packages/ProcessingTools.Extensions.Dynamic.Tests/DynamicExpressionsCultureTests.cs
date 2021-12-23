// <copyright file="DynamicExpressionsCultureTests.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Dynamic.Tests
{
    using System;
    using System.Globalization;
    using System.Linq.Expressions;
    using System.Threading;
    using NUnit.Framework;

    /// <summary>
    /// Dynamic expressions culture tests.
    /// </summary>
    [TestFixture]
    public class DynamicExpressionsCultureTests
    {
        /// <summary>
        /// Test initialize method.
        /// </summary>
        [SetUp]
        public void Initialize()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-PT");
        }

        /// <summary>
        /// Parse typed literal should return corresponding type expression.
        /// </summary>
        /// <param name="type">Type of the expression.</param>
        /// <param name="literal">String literal to be parsed.</param>
        /// <param name="value">Expected value.</param>
        [Test(TestOf = typeof(DynamicExpressionParser))]
        [TestCase(typeof(float), "1.0F", 1.0F)]
        [TestCase(typeof(float), "1.0f", 1.0F)]
        [TestCase(typeof(float), "-1.0F", -1.0F)]
        [TestCase(typeof(float), "-1.0f", -1.0F)]
        [TestCase(typeof(float), "1e1F", 10.0F)]
        [TestCase(typeof(float), "1e1f", 10.0F)]
        [TestCase(typeof(float), "-1e1F", -10.0F)]
        [TestCase(typeof(float), "-1e1f", -10.0F)]
        [TestCase(typeof(float), "1e+1F", 10.0F)]
        [TestCase(typeof(float), "1e+1f", 10.0F)]
        [TestCase(typeof(float), "-1e+1F", -10.0F)]
        [TestCase(typeof(float), "-1e+1f", -10.0F)]
        [TestCase(typeof(float), "1e-1F", 0.1F)]
        [TestCase(typeof(float), "1e-1f", 0.1F)]
        [TestCase(typeof(float), "-1e-1F", -0.1F)]
        [TestCase(typeof(float), "-1e-1f", -0.1F)]
        [TestCase(typeof(double), "1.0", 1.0)]
        [TestCase(typeof(double), "-1.0", -1.0)]
        [TestCase(typeof(double), "1e1", 10.0)]
        [TestCase(typeof(double), "-1e1", -10.0)]
        [TestCase(typeof(double), "1e+1", 10.0)]
        [TestCase(typeof(double), "-1e+1", -10.0)]
        [TestCase(typeof(double), "1e-1", 0.1)]
        [TestCase(typeof(double), "-1e-1", -0.1)]
        [TestCase(typeof(long), "1L", 1L)]
        [TestCase(typeof(long), "1l", 1L)]
        [TestCase(typeof(long), "-1L", -1L)]
        [TestCase(typeof(long), "-1l", -1L)]
        [TestCase(typeof(ulong), "1L", 1L)]
        [TestCase(typeof(ulong), "1l", 1L)]
        [TestCase(typeof(int), "1", 1)]
        [TestCase(typeof(int), "-1", -1)]
        [TestCase(typeof(uint), "1", 1)]
        [TestCase(typeof(sbyte), "1", 1)]
        [TestCase(typeof(sbyte), "-1", -1)]
        [TestCase(typeof(byte), "1", 1)]
        public void Parse_TypedLiteral_ShouldReturnCorrespondingTypeExpression(Type type, string literal, object value)
        {
            // Arrange + Act
            var expression = (ConstantExpression)DynamicExpressionParser.Parse(type, literal);

            // Assert
            Assert.AreEqual(type, expression.Type);
            Assert.AreEqual(value, expression.Value);
        }

        /// <summary>
        /// Parse <see cref="decimal"/> literal should return decimal type expression.
        /// </summary>
        /// <param name="literal">String literal to be parsed.</param>
        /// <param name="value">Expected value.</param>
        [Test(TestOf = typeof(DynamicExpressionParser))]
        [TestCase("1.0M", 1.0)]
        [TestCase("1.0m", 1.0)]
        [TestCase("-1.0M", -1.0)]
        [TestCase("-1.0m", -1.0)]
        [TestCase("1e1M", 10.0)]
        [TestCase("1e1m", 10.0)]
        [TestCase("-1e1M", -10.0)]
        [TestCase("-1e1m", -10.0)]
        [TestCase("1e+1M", 10.0)]
        [TestCase("1e+1m", 10.0)]
        [TestCase("-1e+1M", -10.0)]
        [TestCase("-1e+1m", -10.0)]
        [TestCase("1e-1M", 0.1)]
        [TestCase("1e-1m", 0.1)]
        [TestCase("-1e-1M", -0.1)]
        [TestCase("-1e-1m", -0.1)]
        public void Parse_DecimalLiteral_ShouldReturnDecimalTypeExpression(string literal, double value)
        {
            // Arrange + Act
            var expression = (ConstantExpression)DynamicExpressionParser.Parse(typeof(decimal), literal);

            // Assert
            Assert.AreEqual(typeof(decimal), expression.Type);
            Assert.AreEqual((decimal)value, expression.Value);
        }
    }
}
