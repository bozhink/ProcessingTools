// <copyright file="UpdateExpressionTests.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Common.Tests.Expressions
{
    using System;
    using System.Linq;
    using NUnit.Framework;
    using ProcessingTools.Common.Code.Data.Expressions;
    using ProcessingTools.Common.Code.Tests;
    using ProcessingTools.Common.Code.Tests.Models;

    /// <summary>
    /// UpdateExpression tests.
    /// </summary>
    [TestFixture]
    public class UpdateExpressionTests
    {
        /// <summary>
        /// UpdateExpression with default constructor should return valid object.
        /// </summary>
        [Test(Description = @"UpdateExpression with default constructor should return valid object", Author = "Bozhin Karaivanov", TestOf = typeof(UpdateExpression<ITweet>))]
        public void UpdateExpression_WithDefaultConstructor_ShouldReturnValidObject()
        {
            // Arrange
            var updateExpression = new UpdateExpression<ITweet>();

            // Assert
            Assert.IsNotNull(updateExpression, "UpdateExpression<ITweet> should not be null.");
        }

        /// <summary>
        /// UpdateExpression with default constructor should initialize updateCommands field
        /// </summary>
        [Test(Description = @"UpdateExpression with default constructor should initialize updateCommands field", Author = "Bozhin Karaivanov", TestOf = typeof(UpdateExpression<ITweet>))]
        public void UpdateExpression_WithDefaultConstructor_ShouldInitializeUpdateCommandsField()
        {
            // Arrange
            const string UpdateCommandsFieldName = "updateCommands";

            var updateExpression = new UpdateExpression<ITweet>();

            // Act
            var updateCommandsField = PrivateField.GetInstanceField<UpdateExpression<ITweet>>(updateExpression, UpdateCommandsFieldName);

            // Assert
            Assert.IsNotNull(updateCommandsField, "UpdateCommands field should be correctly initialized.");
        }

        /// <summary>
        /// UpdateExpression with default constructor should initialize UpdateCommands Property.
        /// </summary>
        [Test(Description = @"UpdateExpression with default constructor should initialize UpdateCommands Property", Author = "Bozhin Karaivanov", TestOf = typeof(UpdateExpression<ITweet>))]
        public void UpdateExpression_WithDefaultConstructor_ShouldInitializeUpdateCommandsProperty()
        {
            // Arrange
            var updateExpression = new UpdateExpression<ITweet>();

            // Assert
            Assert.IsNotNull(updateExpression.UpdateCommands, "UpdateCommands property should be correctly initialized.");
        }

        /// <summary>
        /// UpdateExpression set null fieldName should throw ArgumentNullException with "fieldName" ParamName
        /// </summary>
        /// <param name="fieldName">Field name.</param>
        [TestCase(null, Description = @"UpdateExpression set null fieldName should throw ArgumentNullException with ""fieldName"" ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(UpdateExpression<ITweet>))]
        [TestCase("", Description = @"UpdateExpression set empty-string fieldName should throw ArgumentNullException with ""fieldName"" ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(UpdateExpression<ITweet>))]
        [TestCase("          ", Description = @"UpdateExpression set white-space-string fieldName should throw ArgumentNullException with ""fieldName"" ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(UpdateExpression<ITweet>))]
        public void UpdateExpression_SetNullOrWhiteSpaceFieldName_ShouldThrowArgumentNullExceptionWithFieldNameParamName(string fieldName)
        {
            // Arrange
            var updateExpression = new UpdateExpression<ITweet>();
            var value = "Some value";

            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() => updateExpression.Set(fieldName, value), @"ArgumentNullException should be thrown.");

            Assert.AreEqual(nameof(fieldName), exception.ParamName, @"ParamName should be ""fieldName"".");
        }

        /// <summary>
        /// UpdateExpression set valid string fieldName and null value object should register single Set command in UpdateCommands
        /// </summary>
        /// <param name="fieldName">Field name.</param>
        /// <param name="value">Value of the field.</param>
        [TestCase("fieldName", null, Description = @"UpdateExpression set valid string fieldName and null value object should register single Set command in UpdateCommands", Author = "Bozhin Karaivanov", TestOf = typeof(UpdateExpression<ITweet>))]
        [TestCase("fieldName", "Some string", Description = @"UpdateExpression set valid string fieldName and string value object should register single Set command in UpdateCommands", Author = "Bozhin Karaivanov", TestOf = typeof(UpdateExpression<ITweet>))]
        [TestCase("fieldName", 42, Description = @"UpdateExpression set valid string fieldName and int value object should register single Set command in UpdateCommands", Author = "Bozhin Karaivanov", TestOf = typeof(UpdateExpression<ITweet>))]
        [TestCase("fieldName", 42L, Description = @"UpdateExpression set valid string fieldName and long value object should register single Set command in UpdateCommands", Author = "Bozhin Karaivanov", TestOf = typeof(UpdateExpression<ITweet>))]
        public void UpdateExpression_SetValidStringFieldNameAndAnyValueObject_ShouldRegisterSingleSetCommandInUpdateCommands(string fieldName, object value)
        {
            // Arrange
            var updateExpression = new UpdateExpression<ITweet>();

            // Act
            updateExpression.Set(fieldName, value);

            // Assert
            Assert.AreEqual(1, updateExpression.UpdateCommands.Count(), "Number of UpdateCommands should be 1.");

            var command = updateExpression.UpdateCommands.Single();
            Assert.IsNotNull(command, "IUpdateCommand object should not be null.");

            Assert.AreEqual("Set", command.UpdateVerb, @"UpdateVerb of the IUpdateCommand should be ""Set"".");
            Assert.AreEqual(fieldName, command.FieldName, @"FieldName of the IUpdateCommand should be """ + fieldName + @""".");
            Assert.AreSame(value, command.Value, @"Value of the IUpdateCommand should be """ + value + @""".");
        }

        /// <summary>
        /// UpdateExpression set null field should throw ArgumentNullException with "field" ParamName.
        /// </summary>
        [Test(Description = @"UpdateExpression set null field should throw ArgumentNullException with ""field"" ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(UpdateExpression<ITweet>))]
        public void UpdateExpression_SetNullField_ShouldThrowArgumentNullExceptionWithFieldParamName()
        {
            // Arrange
            var updateExpression = new UpdateExpression<ITweet>();
            var value = "Some value";

            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() => updateExpression.Set<string>(null, value), @"ArgumentNullException should be thrown.");

            Assert.AreEqual("field", exception.ParamName, @"ParamName should be ""field"".");
        }

        /// <summary>
        /// UpdateExpression{ITweet}.Set valid expression for Content field and null value should register single Set command in UpdateCommands.
        /// </summary>
        [Test(Description = @"UpdateExpression<ITweet>.Set valid expression for Content field and null value should register single Set command in UpdateCommands", Author = "Bozhin Karaivanov", TestOf = typeof(UpdateExpression<ITweet>))]
        public void UpdateExpression_SetValidExpressionForContentFieldAndNullValue_ShouldRegisterSingleSetCommandInUpdateCommands()
        {
            // Arrange
            var updateExpression = new UpdateExpression<ITweet>();
            string value = null;

            // Act
            updateExpression.Set(t => t.Content, value);

            // Assert
            Assert.AreEqual(1, updateExpression.UpdateCommands.Count(), "Number of UpdateCommands should be 1.");

            var command = updateExpression.UpdateCommands.Single();
            Assert.IsNotNull(command, "IUpdateCommand object should not be null.");

            Assert.AreEqual("Set", command.UpdateVerb, @"UpdateVerb of the IUpdateCommand should be ""Set"".");
            Assert.AreEqual("Content", command.FieldName, @"FieldName of the IUpdateCommand should be ""Content"".");
            Assert.AreSame(value, command.Value, @"Value of the IUpdateCommand should be """ + value + @""".");
        }

        /// <summary>
        /// UpdateExpression{ITweet}.Set valid expression for Content field and valid value should register single Set command in UpdateCommands.
        /// </summary>
        [Test(Description = @"UpdateExpression<ITweet>.Set valid expression for Content field and valid value should register single Set command in UpdateCommands", Author = "Bozhin Karaivanov", TestOf = typeof(UpdateExpression<ITweet>))]
        public void UpdateExpression_SetValidExpressionForContentFieldAndValidValue_ShouldRegisterSingleSetCommandInUpdateCommands()
        {
            // Arrange
            var updateExpression = new UpdateExpression<ITweet>();
            string value = "Some string";

            // Act
            updateExpression.Set(t => t.Content, value);

            // Assert
            Assert.AreEqual(1, updateExpression.UpdateCommands.Count(), "Number of UpdateCommands should be 1.");

            var command = updateExpression.UpdateCommands.Single();
            Assert.IsNotNull(command, "IUpdateCommand object should not be null.");

            Assert.AreEqual("Set", command.UpdateVerb, @"UpdateVerb of the IUpdateCommand should be ""Set"".");
            Assert.AreEqual("Content", command.FieldName, @"FieldName of the IUpdateCommand should be ""Content"".");
            Assert.AreSame(value, command.Value, @"Value of the IUpdateCommand should be """ + value + @""".");
        }

        /// <summary>
        /// UpdateExpression{ITweet}.Set valid expression for DatePosted field and valid value should register single Set command in UpdateCommands.
        /// </summary>
        [Test(Description = @"UpdateExpression<ITweet>.Set valid expression for DatePosted field and valid value should register single Set command in UpdateCommands", Author = "Bozhin Karaivanov", TestOf = typeof(UpdateExpression<ITweet>))]
        public void UpdateExpression_SetValidExpressionForDatePostedFieldAndValidValue_ShouldRegisterSingleSetCommandInUpdateCommands()
        {
            // Arrange
            var updateExpression = new UpdateExpression<ITweet>();
            var value = DateTime.UtcNow;

            // Act
            updateExpression.Set(t => t.DatePosted, value);

            // Assert
            Assert.AreEqual(1, updateExpression.UpdateCommands.Count(), "Number of UpdateCommands should be 1.");

            var command = updateExpression.UpdateCommands.Single();
            Assert.IsNotNull(command, "IUpdateCommand object should not be null.");

            Assert.AreEqual("Set", command.UpdateVerb, @"UpdateVerb of the IUpdateCommand should be ""Set"".");
            Assert.AreEqual("DatePosted", command.FieldName, @"FieldName of the IUpdateCommand should be ""DatePosted"".");
            Assert.AreEqual(value, command.Value, $@"Value of the IUpdateCommand should be ""{value}"".");
        }

        /// <summary>
        /// UpdateExpression{ITweet}.Set valid expression for Faves field and valid value should register single Set command in UpdateCommands.
        /// </summary>
        [Test(Description = @"UpdateExpression<ITweet>.Set valid expression for Faves field and valid value should register single Set command in UpdateCommands", Author = "Bozhin Karaivanov", TestOf = typeof(UpdateExpression<ITweet>))]
        public void UpdateExpression_SetValidExpressionForFavesFieldAndValidValue_ShouldRegisterSingleSetCommandInUpdateCommands()
        {
            // Arrange
            var updateExpression = new UpdateExpression<ITweet>();
            var value = 123;

            // Act
            updateExpression.Set(t => t.Faves, value);

            // Assert
            Assert.AreEqual(1, updateExpression.UpdateCommands.Count(), "Number of UpdateCommands should be 1.");

            var command = updateExpression.UpdateCommands.Single();
            Assert.IsNotNull(command, "IUpdateCommand object should not be null.");

            Assert.AreEqual("Set", command.UpdateVerb, @"UpdateVerb of the IUpdateCommand should be ""Set"".");
            Assert.AreEqual("Faves", command.FieldName, @"FieldName of the IUpdateCommand should be ""Faves"".");
            Assert.AreEqual(value, command.Value, @"Value of the IUpdateCommand should be """ + value + @""".");
        }
    }
}
