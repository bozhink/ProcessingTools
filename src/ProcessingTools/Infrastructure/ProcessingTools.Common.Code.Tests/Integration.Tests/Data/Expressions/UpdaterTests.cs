// <copyright file="UpdaterTests.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Code.Tests.Integration.Tests.Data.Expressions
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Common.Code.Data.Expressions;
    using ProcessingTools.Common.Code.Tests.Models;
    using ProcessingTools.Contracts.DataAccess.Expressions;
    using ProcessingTools.Extensions.Dynamic;

    /// <summary>
    /// Updater tests.
    /// </summary>
    [TestFixture]
    public class UpdaterTests
    {
        /// <summary>
        /// Updater with null updateExpression should throw ArgumentNullException with "updateExpression" ParamName.
        /// </summary>
        [Test(Description = @"Updater with null updateExpression should throw ArgumentNullException with ""updateExpression"" ParamName.", TestOf = typeof(Updater<ITweet>))]
        public void Updater_WithNullUpdateExpression_ShouldThrowArgumentNullExceptionWithUpdateExpressionParamName()
        {
            // Arrange + Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new Updater<ITweet>(null);
            });

            Assert.AreEqual("updateExpression", exception.ParamName);
        }

        /// <summary>
        /// Updater with valid updateExpression should correctly initialize the updateExpression field.
        /// </summary>
        [Test(Description = @"Updater with valid updateExpression should correctly initialize the updateExpression field.", TestOf = typeof(Updater<ITweet>))]
        public void Updater_WithValiUpdateExpression_ShouldCorrectlyInitializeUpdateExpressionField()
        {
            // Arrange
            const string UpdateExpressionFieldName = "updateExpression";

            var updateExpressionMock = new Mock<IUpdateExpression<ITweet>>();
            var updateExpression = updateExpressionMock.Object;
            var updater = new Updater<ITweet>(updateExpression);

            // Act
            var updateExpressionFieldValue = PrivateField.GetInstanceField<Updater<ITweet>>(updater, UpdateExpressionFieldName);

            // Assert
            Assert.IsNotNull(updateExpressionFieldValue);
            Assert.AreSame(updateExpression, updateExpressionFieldValue);
        }

        /// <summary>
        /// Updater with valid updateExpression should correctly initialize the UpdateExpression property.
        /// </summary>
        [Test(Description = @"Updater with valid updateExpression should correctly initialize the UpdateExpression property.", TestOf = typeof(Updater<ITweet>))]
        public void Updater_WithValiUpdateExpression_ShouldCorrectlyInitializeUpdateExpressionProperty()
        {
            // Arrange
            var updateExpressionMock = new Mock<IUpdateExpression<ITweet>>();
            var updateExpression = updateExpressionMock.Object;
            var updater = new Updater<ITweet>(updateExpression);

            // Act
            var updateExpressionValue = updater.UpdateExpression;

            // Assert
            Assert.IsNotNull(updateExpressionValue);
            Assert.AreSame(updateExpression, updateExpressionValue);
        }

        /// <summary>
        /// Updater with valid updateExpression  on Invoke with null object should throw AggregateException with inner ArgumentNullException with "obj" ParamName.
        /// </summary>
        [Test(Description = @"Updater with valid updateExpression  on Invoke with null object should throw AggregateException with inner ArgumentNullException with ""obj"" ParamName.", TestOf = typeof(Updater<ITweet>))]
        [MaxTime(2000)]
        public void Updater_WithValidUpdateExpressionOnInvokeWithNullObject_ShouldThrowAggregateExceptionWithInnerArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var updateExpressionMock = new Mock<IUpdateExpression<ITweet>>();
            var updateExpression = updateExpressionMock.Object;
            var updater = new Updater<ITweet>(updateExpression);

            // Act + Assert
            var exception = Assert.ThrowsAsync<AggregateException>(async () =>
            {
                await updater.InvokeAsync(null).ConfigureAwait(false);
            });

            Assert.AreEqual(1, exception.InnerExceptions.Count);

            var innerException = exception.InnerExceptions.Single();
            Assert.IsInstanceOf<ArgumentNullException>(innerException);

            var argumentNullException = innerException as ArgumentNullException;
            Assert.AreEqual("obj", argumentNullException.ParamName);
        }

        /// <summary>
        /// Updater with valid updateExpression with single valid Set command on Invoke with valid input object should correctly set corresponding property of the input object.
        /// </summary>
        /// <returns>Task.</returns>
        [Test(Description = @"Updater with valid updateExpression with single valid Set command on Invoke with valid input object should correctly set corresponding property of the input object.", TestOf = typeof(Updater<ITweet>))]
        [MaxTime(2000)]
        public async Task Updater_WithValidUpdateExpressionWithSingleValidSetCommandOnInvokeWithValidInputObject_ShouldCorrectlySetCorrespondingPropertyOfTheInputObject()
        {
            // Arrange
            const string SetMethodName = "Set";
            const string ContentPropertyName = nameof(ITweet.Content);
            string contentValue = "Some string content";

            var updateContentCommandMock = new Mock<IUpdateCommand>();
            updateContentCommandMock
                .SetupGet(c => c.UpdateVerb)
                .Returns(SetMethodName);
            updateContentCommandMock
                .SetupGet(c => c.FieldName)
                .Returns(ContentPropertyName);
            updateContentCommandMock
                .SetupGet(c => c.Value)
                .Returns(contentValue);

            var updateExpressionMock = new Mock<IUpdateExpression<ITweet>>();
            updateExpressionMock
                .SetupGet(e => e.UpdateCommands)
                .Returns(new[]
                {
                    updateContentCommandMock.Object,
                });

            var updateExpression = updateExpressionMock.Object;

            var updater = new Updater<ITweet>(updateExpression);

            string targetContent = null;
            var targetObjectMock = new Mock<ITweet>();
            targetObjectMock
                .SetupSet(t => t.Content = It.IsAny<string>())
                .Callback<string>(s => targetContent = s);

            var targetObject = targetObjectMock.Object;

            // Act
            await updater.InvokeAsync(targetObject).ConfigureAwait(false);

            // Assert
            Assert.IsNotNull(targetObject);

            updateExpressionMock.VerifyGet(e => e.UpdateCommands, Times.Once);

            updateContentCommandMock.VerifyGet(c => c.UpdateVerb, Times.Never);
            updateContentCommandMock.VerifyGet(c => c.FieldName, Times.Once);
            updateContentCommandMock.VerifyGet(c => c.Value, Times.Once);

            targetObjectMock.VerifySet(t => t.Content = contentValue, Times.Once);
            targetObjectMock.VerifySet(t => t.DatePosted = It.IsAny<DateTime>(), Times.Never);
            targetObjectMock.VerifySet(t => t.Faves = It.IsAny<int>(), Times.Never);

            Assert.AreEqual(contentValue, targetContent);
        }

        /// <summary>
        /// Updater with valid updateExpression with two different valid Set commands on Invoke with valid input object should correctly set corresponding properties of the input object.
        /// </summary>
        /// <returns>Task.</returns>
        [Test(Description = @"Updater with valid updateExpression with two different valid Set commands on Invoke with valid input object should correctly set corresponding properties of the input object.", TestOf = typeof(Updater<ITweet>))]
        [MaxTime(2000)]
        public async Task Updater_WithValidUpdateExpressionWithTwoDifferentValidSetCommandsOnInvokeWithValidInputObject_ShouldCorrectlySetCorrespondingPropertiesOfTheInputObject()
        {
            // Arrange
            const string SetMethodName = "Set";

            const string ContentPropertyName = nameof(ITweet.Content);
            string contentValue = "Some string content";

            const string FavesPropertyName = nameof(ITweet.Faves);
            int favesValue = 3;

            var updateContentCommandMock = new Mock<IUpdateCommand>();
            updateContentCommandMock
                .SetupGet(c => c.UpdateVerb)
                .Returns(SetMethodName);
            updateContentCommandMock
                .SetupGet(c => c.FieldName)
                .Returns(ContentPropertyName);
            updateContentCommandMock
                .SetupGet(c => c.Value)
                .Returns(contentValue);

            var updateFavesCommandMock = new Mock<IUpdateCommand>();
            updateFavesCommandMock
                .SetupGet(c => c.UpdateVerb)
                .Returns(SetMethodName);
            updateFavesCommandMock
                .SetupGet(c => c.FieldName)
                .Returns(FavesPropertyName);
            updateFavesCommandMock
                .SetupGet(c => c.Value)
                .Returns(favesValue);

            var updateExpressionMock = new Mock<IUpdateExpression<ITweet>>();
            updateExpressionMock
                .SetupGet(e => e.UpdateCommands)
                .Returns(new[]
                {
                    updateContentCommandMock.Object,
                    updateFavesCommandMock.Object,
                });

            var updateExpression = updateExpressionMock.Object;

            var updater = new Updater<ITweet>(updateExpression);

            string targetContent = null;
            int targetFaves = -1;
            var targetObjectMock = new Mock<ITweet>();
            targetObjectMock
                .SetupSet(t => t.Content = It.IsAny<string>())
                .Callback<string>(s => targetContent = s);
            targetObjectMock
                .SetupSet(t => t.Faves = It.IsAny<int>())
                .Callback<int>(f => targetFaves = f);

            var targetObject = targetObjectMock.Object;

            // Act
            await updater.InvokeAsync(targetObject).ConfigureAwait(false);

            // Assert
            Assert.IsNotNull(targetObject);

            updateExpressionMock.VerifyGet(e => e.UpdateCommands, Times.Once);

            updateContentCommandMock.VerifyGet(c => c.UpdateVerb, Times.Never);
            updateContentCommandMock.VerifyGet(c => c.FieldName, Times.Once);
            updateContentCommandMock.VerifyGet(c => c.Value, Times.Once);

            updateFavesCommandMock.VerifyGet(c => c.UpdateVerb, Times.Never);
            updateFavesCommandMock.VerifyGet(c => c.FieldName, Times.Once);
            updateFavesCommandMock.VerifyGet(c => c.Value, Times.Once);

            targetObjectMock.VerifySet(t => t.Content = contentValue, Times.Once);
            targetObjectMock.VerifySet(t => t.DatePosted = It.IsAny<DateTime>(), Times.Never);
            targetObjectMock.VerifySet(t => t.Faves = favesValue, Times.Once);

            Assert.AreEqual(contentValue, targetContent);
            Assert.AreEqual(favesValue, targetFaves);
        }

        /// <summary>
        /// Updater with valid updateExpression with three different valid Set commands on Invoke with valid input object should correctly set corresponding properties of the input object.
        /// </summary>
        /// <returns>Task.</returns>
        [Test(Description = @"Updater with valid updateExpression with three different valid Set commands on Invoke with valid input object should correctly set corresponding properties of the input object.", TestOf = typeof(Updater<ITweet>))]
        [MaxTime(2000)]
        public async Task Updater_WithValidUpdateExpressionWithTThreeDifferentValidSetCommandsOnInvokeWithValidInputObject_ShouldCorrectlySetCorrespondingPropertiesOfTheInputObject()
        {
            // Arrange
            const string SetMethodName = "Set";

            const string ContentPropertyName = nameof(ITweet.Content);
            string contentValue = "Some string content";

            const string FavesPropertyName = nameof(ITweet.Faves);
            int favesValue = 3;

            const string DatePostedPropertyName = nameof(ITweet.DatePosted);
            DateTime datePostedValue = DateTime.UtcNow;

            var updateContentCommandMock = new Mock<IUpdateCommand>();
            updateContentCommandMock
                .SetupGet(c => c.UpdateVerb)
                .Returns(SetMethodName);
            updateContentCommandMock
                .SetupGet(c => c.FieldName)
                .Returns(ContentPropertyName);
            updateContentCommandMock
                .SetupGet(c => c.Value)
                .Returns(contentValue);

            var updateFavesCommandMock = new Mock<IUpdateCommand>();
            updateFavesCommandMock
                .SetupGet(c => c.UpdateVerb)
                .Returns(SetMethodName);
            updateFavesCommandMock
                .SetupGet(c => c.FieldName)
                .Returns(FavesPropertyName);
            updateFavesCommandMock
                .SetupGet(c => c.Value)
                .Returns(favesValue);

            var updateDatePostedCommandMock = new Mock<IUpdateCommand>();
            updateDatePostedCommandMock
                .SetupGet(c => c.UpdateVerb)
                .Returns(SetMethodName);
            updateDatePostedCommandMock
                .SetupGet(c => c.FieldName)
                .Returns(DatePostedPropertyName);
            updateDatePostedCommandMock
                .SetupGet(c => c.Value)
                .Returns(datePostedValue);

            var updateExpressionMock = new Mock<IUpdateExpression<ITweet>>();
            updateExpressionMock
                .SetupGet(e => e.UpdateCommands)
                .Returns(new[]
                {
                    updateContentCommandMock.Object,
                    updateFavesCommandMock.Object,
                    updateDatePostedCommandMock.Object,
                });

            var updateExpression = updateExpressionMock.Object;

            var updater = new Updater<ITweet>(updateExpression);

            string targetContent = null;
            int targetFaves = -1;
            DateTime targetDatePosted = new DateTime(1900, 1, 1);
            var targetObjectMock = new Mock<ITweet>();
            targetObjectMock
                .SetupSet(t => t.Content = It.IsAny<string>())
                .Callback<string>(s => targetContent = s);
            targetObjectMock
                .SetupSet(t => t.Faves = It.IsAny<int>())
                .Callback<int>(f => targetFaves = f);
            targetObjectMock
                .SetupSet(t => t.DatePosted = It.IsAny<DateTime>())
                .Callback<DateTime>(d => targetDatePosted = d);

            var targetObject = targetObjectMock.Object;

            // Act
            await updater.InvokeAsync(targetObject).ConfigureAwait(false);

            // Assert
            Assert.IsNotNull(targetObject);

            updateExpressionMock.VerifyGet(e => e.UpdateCommands, Times.Once);

            updateContentCommandMock.VerifyGet(c => c.UpdateVerb, Times.Never);
            updateContentCommandMock.VerifyGet(c => c.FieldName, Times.Once);
            updateContentCommandMock.VerifyGet(c => c.Value, Times.Once);

            updateFavesCommandMock.VerifyGet(c => c.UpdateVerb, Times.Never);
            updateFavesCommandMock.VerifyGet(c => c.FieldName, Times.Once);
            updateFavesCommandMock.VerifyGet(c => c.Value, Times.Once);

            updateDatePostedCommandMock.VerifyGet(c => c.UpdateVerb, Times.Never);
            updateDatePostedCommandMock.VerifyGet(c => c.FieldName, Times.Once);
            updateDatePostedCommandMock.VerifyGet(c => c.Value, Times.Once);

            targetObjectMock.VerifySet(t => t.Content = contentValue, Times.Once);
            targetObjectMock.VerifySet(t => t.DatePosted = datePostedValue, Times.Once);
            targetObjectMock.VerifySet(t => t.Faves = favesValue, Times.Once);

            Assert.AreEqual(contentValue, targetContent);
            Assert.AreEqual(favesValue, targetFaves);
            Assert.AreEqual(datePostedValue, targetDatePosted);
        }

        /// <summary>
        /// Updater with valid updateExpression with single Set command with erroneous fieldName on Invoke with valid input object should throw AggregateException with inner InvalidOperationException with message containing "Property" and "is not found".
        /// </summary>
        [Test(Description = @"Updater with valid updateExpression with single Set command with erroneous fieldName on Invoke with valid input object should throw AggregateException with inner InvalidOperationException with message containing ""Property"" and ""is not found"".", TestOf = typeof(Updater<ITweet>))]
        [MaxTime(2000)]
        public void Updater_WithValidUpdateExpressionWithSingleSetCommandWithErroneousFieldNameOnInvokeWithValidInputObject_ShouldThrowAggregateExceptionWithInnerInvalidOperationExceptionWithMessageContainingPropertyAndIsNotFound()
        {
            // Arrange
            const string SetMethodName = "Set";
            const string UserNamePropertyName = "UserName";
            string userNameValue = "Some string user name";

            var updateUserNameCommandMock = new Mock<IUpdateCommand>();
            updateUserNameCommandMock
                .SetupGet(c => c.UpdateVerb)
                .Returns(SetMethodName);
            updateUserNameCommandMock
                .SetupGet(c => c.FieldName)
                .Returns(UserNamePropertyName);
            updateUserNameCommandMock
                .SetupGet(c => c.Value)
                .Returns(userNameValue);

            var updateExpressionMock = new Mock<IUpdateExpression<ITweet>>();
            updateExpressionMock
                .SetupGet(e => e.UpdateCommands)
                .Returns(new[]
                {
                    updateUserNameCommandMock.Object,
                });

            var updateExpression = updateExpressionMock.Object;

            var updater = new Updater<ITweet>(updateExpression);

            var targetObjectMock = new Mock<ITweet>();

            var targetObject = targetObjectMock.Object;

            // Act + Assert
            var exception = Assert.ThrowsAsync<AggregateException>(async () =>
            {
                await updater.InvokeAsync(targetObject).ConfigureAwait(false);
            });

            Assert.AreEqual(1, exception.InnerExceptions.Count);

            var innerException = exception.InnerExceptions.Single();
            Assert.IsInstanceOf<InvalidOperationException>(innerException);

            StringAssert.Contains("Property", innerException.Message);
            StringAssert.Contains("is not found", innerException.Message);

            Assert.IsNotNull(targetObject);

            updateExpressionMock.VerifyGet(e => e.UpdateCommands, Times.Once);

            updateUserNameCommandMock.VerifyGet(c => c.UpdateVerb, Times.Never);
            updateUserNameCommandMock.VerifyGet(c => c.FieldName, Times.Exactly(2));
            updateUserNameCommandMock.VerifyGet(c => c.Value, Times.Never);

            targetObjectMock.VerifySet(t => t.Content = It.IsAny<string>(), Times.Never);
            targetObjectMock.VerifySet(t => t.DatePosted = It.IsAny<DateTime>(), Times.Never);
            targetObjectMock.VerifySet(t => t.Faves = It.IsAny<int>(), Times.Never);
        }

        /// <summary>
        /// Updater with valid updateExpression with single Set command with fieldName of non-settable property on Invoke with valid input object should throw AggregateException with inner InvalidOperationException with message containing "Set method of property" and "is not found".
        /// </summary>
        [Test(Description = @"Updater with valid updateExpression with single Set command with fieldName of non-settable property on Invoke with valid input object should throw AggregateException with inner InvalidOperationException with message containing ""Set method of property"" and ""is not found"".", TestOf = typeof(Updater<ITweet>))]
        [MaxTime(2000)]
        public void Updater_WithValidUpdateExpressionWithSingleSetCommandWithFieldNameOfNonSettablePropertyOnInvokeWithValidInputObject_ShouldThrowAggregateExceptionWithInnerInvalidOperationExceptionWithMessageContainingSetMethodOfPropertyAndIsNotFound()
        {
            // Arrange
            const string SetMethodName = "Set";
            const string IdPropertyName = nameof(ITweet.Id);
            int id = 42;

            var updateIdCommandMock = new Mock<IUpdateCommand>();
            updateIdCommandMock
                .SetupGet(c => c.UpdateVerb)
                .Returns(SetMethodName);
            updateIdCommandMock
                .SetupGet(c => c.FieldName)
                .Returns(IdPropertyName);
            updateIdCommandMock
                .SetupGet(c => c.Value)
                .Returns(id);

            var updateExpressionMock = new Mock<IUpdateExpression<ITweet>>();
            updateExpressionMock
                .SetupGet(e => e.UpdateCommands)
                .Returns(new[]
                {
                    updateIdCommandMock.Object,
                });

            var updateExpression = updateExpressionMock.Object;

            var updater = new Updater<ITweet>(updateExpression);

            var targetObjectMock = new Mock<ITweet>();

            var targetObject = targetObjectMock.Object;

            // Act + Assert
            var exception = Assert.ThrowsAsync<AggregateException>(async () =>
            {
                await updater.InvokeAsync(targetObject).ConfigureAwait(false);
            });

            Assert.AreEqual(1, exception.InnerExceptions.Count);

            var innerException = exception.InnerExceptions.Single();
            Assert.IsInstanceOf<InvalidOperationException>(innerException);

            StringAssert.Contains("Set method of property", innerException.Message);
            StringAssert.Contains("is not found", innerException.Message);

            Assert.IsNotNull(targetObject);

            updateExpressionMock.VerifyGet(e => e.UpdateCommands, Times.Once);

            updateIdCommandMock.VerifyGet(c => c.UpdateVerb, Times.Never);
            updateIdCommandMock.VerifyGet(c => c.FieldName, Times.Exactly(2));
            updateIdCommandMock.VerifyGet(c => c.Value, Times.Never);

            targetObjectMock.VerifySet(t => t.Content = It.IsAny<string>(), Times.Never);
            targetObjectMock.VerifySet(t => t.DatePosted = It.IsAny<DateTime>(), Times.Never);
            targetObjectMock.VerifySet(t => t.Faves = It.IsAny<int>(), Times.Never);
        }
    }
}
