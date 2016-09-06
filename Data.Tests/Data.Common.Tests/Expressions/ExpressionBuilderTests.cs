namespace ProcessingTools.Data.Common.Tests.Expressions
{
    using System;
    using System.Linq;

    using NUnit.Framework;

    using ProcessingTools.Data.Common.Expressions;
    using ProcessingTools.Data.Common.Tests.Fakes.Models.Contracts;

    [TestFixture]
    public class ExpressionBuilderTests
    {
        [Test(Description = @"ExpressionBuilder with single valid Set should create one Set command in UpdateCommands", Author = "Bozhin Karaivanov", TestOf = typeof(ExpressionBuilder<ITweet>))]
        public void ExpressionBuilder_WithSingleValidSet_ShouldCreateOneSetCommandInUpdateCommands()
        {
            // Arrange
            var value = "Some string content";

            // Arrange + Act
            var updateExpression = ExpressionBuilder<ITweet>.UpdateExpression.Set(t => t.Content, value);

            // Assert
            Assert.AreEqual(1, updateExpression.UpdateCommands.Count(), "Number of UpdateCommands should be 1.");

            var command = updateExpression.UpdateCommands.Single();
            Assert.IsNotNull(command, "IUpdateCommand object should not be null.");

            Assert.AreEqual("Set", command.UpdateVerb, @"UpdateVerb of the IUpdateCommand should be ""Set"".");
            Assert.AreEqual("Content", command.FieldName, @"FieldName of the IUpdateCommand should be ""Content"".");
            Assert.AreSame(value, command.Value, @"Value of the IUpdateCommand should be """ + value + @""".");
        }

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
            Assert.IsNotNull(commands, "UpdateCommands object should not be null.");

            Assert.AreEqual(2, commands.Length, "Number of UpdateCommands should be 2.");

            Assert.AreEqual("Set", commands[0].UpdateVerb, @"UpdateVerb of the first IUpdateCommand should be ""Set"".");
            Assert.AreEqual("Content", commands[0].FieldName, @"FieldName of the first IUpdateCommand should be ""Content"".");
            Assert.AreSame(contentValue, commands[0].Value, @"Value of the first IUpdateCommand should be """ + contentValue + @""".");

            Assert.AreEqual("Set", commands[1].UpdateVerb, @"UpdateVerb of the first IUpdateCommand should be ""Set"".");
            Assert.AreEqual("DatePosted", commands[1].FieldName, @"FieldName of the first IUpdateCommand should be ""DatePosted"".");
            Assert.AreEqual(datePostedValue.ToString(), commands[1].Value.ToString(), @"Value of the first IUpdateCommand should be """ + datePostedValue.ToString() + @""".");
        }

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
            Assert.IsNotNull(commands, "UpdateCommands object should not be null.");

            Assert.AreEqual(3, commands.Length, "Number of UpdateCommands should be 3.");

            Assert.AreEqual("Set", commands[0].UpdateVerb, @"UpdateVerb of the first IUpdateCommand should be ""Set"".");
            Assert.AreEqual("Content", commands[0].FieldName, @"FieldName of the first IUpdateCommand should be ""Content"".");
            Assert.AreSame(contentValue, commands[0].Value, @"Value of the first IUpdateCommand should be """ + contentValue + @""".");

            Assert.AreEqual("Set", commands[1].UpdateVerb, @"UpdateVerb of the first IUpdateCommand should be ""Set"".");
            Assert.AreEqual("DatePosted", commands[1].FieldName, @"FieldName of the first IUpdateCommand should be ""DatePosted"".");
            Assert.AreEqual(datePostedValue.ToString(), commands[1].Value.ToString(), @"Value of the first IUpdateCommand should be """ + datePostedValue.ToString() + @""".");

            Assert.AreEqual("Set", commands[2].UpdateVerb, @"UpdateVerb of the first IUpdateCommand should be ""Set"".");
            Assert.AreEqual("Faves", commands[2].FieldName, @"FieldName of the first IUpdateCommand should be ""Faves"".");
            Assert.AreEqual(favesValue, commands[2].Value, @"Value of the first IUpdateCommand should be """ + favesValue + @""".");
        }
    }
}
