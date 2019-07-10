// <copyright file="ExpressionBuilderTests.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Code.Tests.Integration.Tests.Data.Expressions
{
    using System;
    using System.Linq;
    using NUnit.Framework;
    using ProcessingTools.Common.Code.Data.Expressions;
    using ProcessingTools.Common.Code.Tests.Models;

    /// <summary>
    /// Expression builder tests.
    /// </summary>
    [TestFixture]
    public class ExpressionBuilderTests
    {
        /// <summary>
        /// ExpressionBuilder with single valid Set should create one Set command in UpdateCommands.
        /// </summary>
        [Test(Description = @"ExpressionBuilder with single valid Set should create one Set command in UpdateCommands", Author = "Bozhin Karaivanov", TestOf = typeof(ExpressionBuilder<ITweet>))]
        public void ExpressionBuilder_WithSingleValidSet_ShouldCreateOneSetCommandInUpdateCommands()
        {
            // Arrange
            var value = "Some string content";

            // Arrange + Act
            var updateExpression = ExpressionBuilder<ITweet>.UpdateExpression.Set(t => t.Content, value);

            // Assert
            Assert.AreEqual(1, updateExpression.UpdateCommands.Count());

            var command = updateExpression.UpdateCommands.Single();
            Assert.IsNotNull(command);

            Assert.AreEqual("Set", command.UpdateVerb);
            Assert.AreEqual("Content", command.FieldName);
            Assert.AreSame(value, command.Value);
        }

        /// <summary>
        /// ExpressionBuilder with two different valid Set should create two Set command in UpdateCommands.
        /// </summary>
        [Test(Description = @"ExpressionBuilder with two different valid Set should create two Set command in UpdateCommands", Author = "Bozhin Karaivanov", TestOf = typeof(ExpressionBuilder<ITweet>))]
        public void ExpressionBuilder_WithTwoDifferentValidSet_ShouldCreateTwoSetCommandInUpdateCommands()
        {
            // Arrange
            var contentValue = "Some string content";
            var datePostedValue = DateTime.UtcNow;

            // Arrange + Act
            var updateExpression = ExpressionBuilder<ITweet>.UpdateExpression
                .Set(t => t.Content, contentValue)
                .Set(t => t.DatePosted, datePostedValue);

            // Assert
            var commands = updateExpression.UpdateCommands.ToArray();
            Assert.IsNotNull(commands);

            Assert.AreEqual(2, commands.Length);

            Assert.AreEqual("Set", commands[0].UpdateVerb);
            Assert.AreEqual("Content", commands[0].FieldName);
            Assert.AreSame(contentValue, commands[0].Value);

            Assert.AreEqual("Set", commands[1].UpdateVerb);
            Assert.AreEqual("DatePosted", commands[1].FieldName);
            Assert.AreEqual(datePostedValue, commands[1].Value);
        }

        /// <summary>
        /// ExpressionBuilder with three different valid Set should create three Set command in UpdateCommands.
        /// </summary>
        [Test(Description = @"ExpressionBuilder with three different valid Set should create three Set command in UpdateCommands", Author = "Bozhin Karaivanov", TestOf = typeof(ExpressionBuilder<ITweet>))]
        public void ExpressionBuilder_WithThreeDifferentValidSet_ShouldCreateThreeSetCommandInUpdateCommands()
        {
            // Arrange
            var contentValue = "Some string content";
            var datePostedValue = DateTime.UtcNow;
            var favesValue = 3;

            // Arrange + Act
            var updateExpression = ExpressionBuilder<ITweet>.UpdateExpression
                .Set(t => t.Content, contentValue)
                .Set(t => t.DatePosted, datePostedValue)
                .Set(t => t.Faves, favesValue);

            // Assert
            var commands = updateExpression.UpdateCommands.ToArray();
            Assert.IsNotNull(commands);

            Assert.AreEqual(3, commands.Length);

            Assert.AreEqual("Set", commands[0].UpdateVerb);
            Assert.AreEqual("Content", commands[0].FieldName);
            Assert.AreSame(contentValue, commands[0].Value);

            Assert.AreEqual("Set", commands[1].UpdateVerb);
            Assert.AreEqual("DatePosted", commands[1].FieldName);
            Assert.AreEqual(datePostedValue, commands[1].Value);

            Assert.AreEqual("Set", commands[2].UpdateVerb);
            Assert.AreEqual("Faves", commands[2].FieldName);
            Assert.AreEqual(favesValue, commands[2].Value);
        }
    }
}
