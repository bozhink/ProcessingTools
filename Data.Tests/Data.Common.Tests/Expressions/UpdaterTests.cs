namespace ProcessingTools.Data.Common.Tests.Expressions
{
    using System;
    using System.Linq;

    using Moq;
    using NUnit.Framework;

    using ProcessingTools.Data.Common.Expressions;
    using ProcessingTools.Data.Common.Expressions.Contracts;
    using ProcessingTools.Data.Common.Tests.Fakes.Models.Contracts;

    [TestFixture]
    public class UpdaterTests
    {
        [Test(Description = @"Updater with null updateExpression should throw ArgumentNullException with ""updateExpression"" ParamName.", Author = "Bozhin Karaivanov", TestOf = typeof(Updater<ITweet>))]
        public void Updater_WithNullUpdateExpression_ShouldThrowArgumentNullExceptionWithUpdateExpressionParamName()
        {
            // Arrange + Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(
                () =>
                {
                    var updater = new Updater<ITweet>(null);
                },
                "Updater with null updateExpression should throw ArgumentNullException.");

            Assert.AreEqual("updateExpression", exception.ParamName, @"ParamName should be ""updateExpression"".");
        }

        [Test(Description = @"Updater with valid updateExpression should correctly initialize the updateExpression field.", Author = "Bozhin Karaivanov", TestOf = typeof(Updater<ITweet>))]
        public void Updater_WithValiUpdateExpression_ShouldCorrectlyInitializeUpdateExpressionField()
        {
            // Arrange
            const string UpdateExpressionFieldName = "updateExpression";

            var updateExpressionMock = new Mock<IUpdateExpression<ITweet>>();
            var updateExpression = updateExpressionMock.Object;
            var updater = new Updater<ITweet>(updateExpression);

            // Act
            var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(updater);
            var updateExpressionFieldValue = privateObject.GetField(UpdateExpressionFieldName);

            // Assert
            Assert.IsNotNull(updateExpressionFieldValue, "Updater.updateExpression should not be null.");
            Assert.AreSame(updateExpression, updateExpressionFieldValue, "Updater.updateExpression should be set correctly.");
        }

        [Test(Description = @"Updater with valid updateExpression should correctly initialize the UpdateExpression property.", Author = "Bozhin Karaivanov", TestOf = typeof(Updater<ITweet>))]
        public void Updater_WithValiUpdateExpression_ShouldCorrectlyInitializeUpdateExpressionProperty()
        {
            // Arrange
            var updateExpressionMock = new Mock<IUpdateExpression<ITweet>>();
            var updateExpression = updateExpressionMock.Object;
            var updater = new Updater<ITweet>(updateExpression);

            // Act
            var updateExpressionValue = updater.UpdateExpression;

            // Assert
            Assert.IsNotNull(updateExpressionValue, "Updater.UpdateExpression should not be null.");
            Assert.AreSame(updateExpression, updateExpressionValue, "Updater.UpdateExpression should be set correctly.");
        }

        [Test(Description = @"Updater with valid updateExpression  on Invoke with null object should throw AggregateException with inner ArgumentNullException with ""obj"" ParamName.", Author = "Bozhin Karaivanov", TestOf = typeof(Updater<ITweet>))]
        [Timeout(2000)]
        public void Updater_WithValidUpdateExpressionOnInvokeWithNullObject_ShouldThrowAggregateExceptionWithInnerArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var updateExpressionMock = new Mock<IUpdateExpression<ITweet>>();
            var updateExpression = updateExpressionMock.Object;
            var updater = new Updater<ITweet>(updateExpression);

            // Act + Assert
            var exception = Assert.Throws<AggregateException>(
                () =>
                {
                    updater.Invoke(null).Wait();
                },
                "Updater.Invoke(null) should throw AggregateException.");

            Assert.AreEqual(1, exception.InnerExceptions.Count, "Number of inner exceptions should be 1.");

            var innerException = exception.InnerExceptions.Single();
            Assert.IsInstanceOf<ArgumentNullException>(innerException, "Inner exception should be instance of ArgumentNullException.");

            var argumentNullException = innerException as ArgumentNullException;
            Assert.AreEqual("obj", argumentNullException.ParamName, @"ParamName should be ""obj"".");
        }

        [Test(Description = @"Updater with valid updateExpression with single valid Set command on Invoke with valid input object should correctly set corresponding property of the input object.", Author = "Bozhin Karaivanov", TestOf = typeof(Updater<ITweet>))]
        [Timeout(2000)]
        public void Updater_WithValidUpdateExpressionWithSingleValidSetCommandOnInvokeWithValidInputObject_ShouldCorrectlySetCorrespondingPropertyOfTheInputObject()
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
                .Returns(new IUpdateCommand[]
                {
                    updateContentCommandMock.Object
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
            updater.Invoke(targetObject).Wait();

            // Assert
            Assert.IsNotNull(targetObject, "Updated target object should not be null.");

            updateExpressionMock.VerifyGet(e => e.UpdateCommands, Times.Once, "IUpdateExpression.UpdateCommands should be invoked once.");

            updateContentCommandMock.VerifyGet(c => c.UpdateVerb, Times.Never, "UpdateContentCommand.UpdateVerb should not be invoked.");
            updateContentCommandMock.VerifyGet(c => c.FieldName, Times.Once, "UpdateContentCommand.FieldName should be invoked once.");
            updateContentCommandMock.VerifyGet(c => c.Value, Times.Once, "UpdateContentCommand.Value should be invoked once.");

            targetObjectMock.VerifySet(t => t.Content = contentValue, Times.Once, "ITweet.Content should be set to appropriate value once.");
            targetObjectMock.VerifySet(t => t.DatePosted = It.IsAny<DateTime>(), Times.Never, "ITweet.DatePosted setter should not be invoked.");
            targetObjectMock.VerifySet(t => t.Faves = It.IsAny<int>(), Times.Never, "ITweet.Faves setter should not be invoked.");

            Assert.AreEqual(contentValue, targetContent, "Target object Content should be set correctly.");
        }

        [Test(Description = @"Updater with valid updateExpression with two different valid Set commands on Invoke with valid input object should correctly set corresponding properties of the input object.", Author = "Bozhin Karaivanov", TestOf = typeof(Updater<ITweet>))]
        [Timeout(2000)]
        public void Updater_WithValidUpdateExpressionWithTwoDifferentValidSetCommandsOnInvokeWithValidInputObject_ShouldCorrectlySetCorrespondingPropertiesOfTheInputObject()
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
                .Returns(new IUpdateCommand[]
                {
                    updateContentCommandMock.Object,
                    updateFavesCommandMock.Object
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
            updater.Invoke(targetObject).Wait();

            // Assert
            Assert.IsNotNull(targetObject, "Updated target object should not be null.");

            updateExpressionMock.VerifyGet(e => e.UpdateCommands, Times.Once, "IUpdateExpression.UpdateCommands should be invoked once.");

            updateContentCommandMock.VerifyGet(c => c.UpdateVerb, Times.Never, "UpdateContentCommand.UpdateVerb should not be invoked.");
            updateContentCommandMock.VerifyGet(c => c.FieldName, Times.Once, "UpdateContentCommand.FieldName should be invoked once.");
            updateContentCommandMock.VerifyGet(c => c.Value, Times.Once, "UpdateContentCommand.Value should be invoked once.");

            updateFavesCommandMock.VerifyGet(c => c.UpdateVerb, Times.Never, "UpdateFavesCommand.UpdateVerb should not be invoked.");
            updateFavesCommandMock.VerifyGet(c => c.FieldName, Times.Once, "UpdateFavesCommand.FieldName should be invoked once.");
            updateFavesCommandMock.VerifyGet(c => c.Value, Times.Once, "UpdateFavesCommand.Value should be invoked once.");

            targetObjectMock.VerifySet(t => t.Content = contentValue, Times.Once, "ITweet.Content should be set to appropriate value once.");
            targetObjectMock.VerifySet(t => t.DatePosted = It.IsAny<DateTime>(), Times.Never, "ITweet.DatePosted setter should not be invoked.");
            targetObjectMock.VerifySet(t => t.Faves = favesValue, Times.Once, "ITweet.Faves should be set to appropriate value once.");

            Assert.AreEqual(contentValue, targetContent, "Target object Content should be set correctly.");
            Assert.AreEqual(favesValue, targetFaves, "Target object Faves should be set correctly.");
        }

        [Test(Description = @"Updater with valid updateExpression with three different valid Set commands on Invoke with valid input object should correctly set corresponding properties of the input object.", Author = "Bozhin Karaivanov", TestOf = typeof(Updater<ITweet>))]
        [Timeout(2000)]
        public void Updater_WithValidUpdateExpressionWithTThreeDifferentValidSetCommandsOnInvokeWithValidInputObject_ShouldCorrectlySetCorrespondingPropertiesOfTheInputObject()
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
                .Returns(new IUpdateCommand[]
                {
                    updateContentCommandMock.Object,
                    updateFavesCommandMock.Object,
                    updateDatePostedCommandMock.Object
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
            updater.Invoke(targetObject).Wait();

            // Assert
            Assert.IsNotNull(targetObject, "Updated target object should not be null.");

            updateExpressionMock.VerifyGet(e => e.UpdateCommands, Times.Once, "IUpdateExpression.UpdateCommands should be invoked once.");

            updateContentCommandMock.VerifyGet(c => c.UpdateVerb, Times.Never, "UpdateContentCommand.UpdateVerb should not be invoked.");
            updateContentCommandMock.VerifyGet(c => c.FieldName, Times.Once, "UpdateContentCommand.FieldName should be invoked once.");
            updateContentCommandMock.VerifyGet(c => c.Value, Times.Once, "UpdateContentCommand.Value should be invoked once.");

            updateFavesCommandMock.VerifyGet(c => c.UpdateVerb, Times.Never, "UpdateFavesCommand.UpdateVerb should not be invoked.");
            updateFavesCommandMock.VerifyGet(c => c.FieldName, Times.Once, "UpdateFavesCommand.FieldName should be invoked once.");
            updateFavesCommandMock.VerifyGet(c => c.Value, Times.Once, "UpdateFavesCommand.Value should be invoked once.");

            updateDatePostedCommandMock.VerifyGet(c => c.UpdateVerb, Times.Never, "UpdateDatePostedCommand.UpdateVerb should not be invoked.");
            updateDatePostedCommandMock.VerifyGet(c => c.FieldName, Times.Once, "UpdateDatePostedCommand.FieldName should be invoked once.");
            updateDatePostedCommandMock.VerifyGet(c => c.Value, Times.Once, "UpdateDatePostedCommand.Value should be invoked once.");

            targetObjectMock.VerifySet(t => t.Content = contentValue, Times.Once, "ITweet.Content should be set to appropriate value once.");
            targetObjectMock.VerifySet(t => t.DatePosted = datePostedValue, Times.Once, "ITweet.DatePosted should be set to appropriate value once.");
            targetObjectMock.VerifySet(t => t.Faves = favesValue, Times.Once, "ITweet.Faves should be set to appropriate value once.");

            Assert.AreEqual(contentValue, targetContent, "Target object Content should be set correctly.");
            Assert.AreEqual(favesValue, targetFaves, "Target object Faves should be set correctly.");
            Assert.AreEqual(datePostedValue, targetDatePosted, "Target object DatePosted should be set correctly.");
        }

        [Test(Description = @"Updater with valid updateExpression with single Set command with erroneous fieldName on Invoke with valid input object should throw AggregateException with inner InvalidOperationException with message containing ""Property"" and ""is not found"".", Author = "Bozhin Karaivanov", TestOf = typeof(Updater<ITweet>))]
        [Timeout(2000)]
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
                .Returns(new IUpdateCommand[]
                {
                    updateUserNameCommandMock.Object
                });

            var updateExpression = updateExpressionMock.Object;

            var updater = new Updater<ITweet>(updateExpression);

            var targetObjectMock = new Mock<ITweet>();

            var targetObject = targetObjectMock.Object;

            // Act + Assert
            var exception = Assert.Throws<AggregateException>(
                () =>
                {
                    updater.Invoke(targetObject).Wait();
                },
                "AggregateException should be thrown.");

            Assert.AreEqual(1, exception.InnerExceptions.Count, "Number of inner exceptions should be 1.");

            var innerException = exception.InnerExceptions.Single();
            Assert.IsInstanceOf<InvalidOperationException>(innerException, "Inner exception should be instance of InvalidOperationException.");

            StringAssert.Contains("Property", innerException.Message, @"InnerException.Message should contain string ""Property"".");
            StringAssert.Contains("is not found", innerException.Message, @"InnerException.Message should contain string ""is not found"".");

            Assert.IsNotNull(targetObject, "Updated target object should not be null.");

            updateExpressionMock.VerifyGet(e => e.UpdateCommands, Times.Once, "IUpdateExpression.UpdateCommands should be invoked once.");

            updateUserNameCommandMock.VerifyGet(c => c.UpdateVerb, Times.Never, "UpdateContentCommand.UpdateVerb should not be invoked.");
            updateUserNameCommandMock.VerifyGet(c => c.FieldName, Times.Exactly(2), "UpdateContentCommand.FieldName should be invoked twice.");
            updateUserNameCommandMock.VerifyGet(c => c.Value, Times.Never, "UpdateContentCommand.Value should not be invoked.");

            targetObjectMock.VerifySet(t => t.Content = It.IsAny<string>(), Times.Never, "ITweet.Content setter should not be invoked.");
            targetObjectMock.VerifySet(t => t.DatePosted = It.IsAny<DateTime>(), Times.Never, "ITweet.DatePosted setter should not be invoked.");
            targetObjectMock.VerifySet(t => t.Faves = It.IsAny<int>(), Times.Never, "ITweet.Faves setter should not be invoked.");
        }

        [Test(Description = @"Updater with valid updateExpression with single Set command with fieldName of non-settable property on Invoke with valid input object should throw AggregateException with inner InvalidOperationException with message containing ""Set method of property"" and ""is not found"".", Author = "Bozhin Karaivanov", TestOf = typeof(Updater<ITweet>))]
        [Timeout(2000)]
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
                .Returns(new IUpdateCommand[]
                {
                    updateIdCommandMock.Object
                });

            var updateExpression = updateExpressionMock.Object;

            var updater = new Updater<ITweet>(updateExpression);

            var targetObjectMock = new Mock<ITweet>();

            var targetObject = targetObjectMock.Object;

            // Act + Assert
            var exception = Assert.Throws<AggregateException>(
                () =>
                {
                    updater.Invoke(targetObject).Wait();
                },
                "AggregateException should be thrown.");

            Assert.AreEqual(1, exception.InnerExceptions.Count, "Number of inner exceptions should be 1.");

            var innerException = exception.InnerExceptions.Single();
            Assert.IsInstanceOf<InvalidOperationException>(innerException, "Inner exception should be instance of InvalidOperationException.");

            StringAssert.Contains("Set method of property", innerException.Message, @"InnerException.Message should contain string ""Set method of property"".");
            StringAssert.Contains("is not found", innerException.Message, @"InnerException.Message should contain string ""is not found"".");

            Assert.IsNotNull(targetObject, "Updated target object should not be null.");

            updateExpressionMock.VerifyGet(e => e.UpdateCommands, Times.Once, "IUpdateExpression.UpdateCommands should be invoked once.");

            updateIdCommandMock.VerifyGet(c => c.UpdateVerb, Times.Never, "UpdateContentCommand.UpdateVerb should not be invoked.");
            updateIdCommandMock.VerifyGet(c => c.FieldName, Times.Exactly(2), "UpdateContentCommand.FieldName should be invoked twice.");
            updateIdCommandMock.VerifyGet(c => c.Value, Times.Never, "UpdateContentCommand.Value should not be invoked.");

            targetObjectMock.VerifySet(t => t.Content = It.IsAny<string>(), Times.Never, "ITweet.Content setter should not be invoked.");
            targetObjectMock.VerifySet(t => t.DatePosted = It.IsAny<DateTime>(), Times.Never, "ITweet.DatePosted setter should not be invoked.");
            targetObjectMock.VerifySet(t => t.Faves = It.IsAny<int>(), Times.Never, "ITweet.Faves setter should not be invoked.");
        }
    }
}
