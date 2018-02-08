namespace ProcessingTools.Services.Cache.Tests.Unit.Tests.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Data.Contracts.Cache;
    using ProcessingTools.Models.Contracts.Cache;
    using ProcessingTools.Services.Cache.Tests.Common;
    using ProcessingTools.Tests.Library;

    [TestFixture(Category = "Unit", TestOf = typeof(ValidationCacheService))]
    public class ValidationCacheServiceUnitTests
    {
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ValidationCacheService), Description = "ValidationCacheService Add valid key and valid value should work.")]
        [Timeout(5000)]
        public async Task ValidationCacheService_AddValidNonPresentKeyAndValidValue_ShouldWork()
        {
            // Arrange
            string key = "some key";
            var valueMock = new Mock<IValidationCacheModel>();

            var repositoryMock = new Mock<IValidationCacheDataRepository>();
            repositoryMock
                .Setup(r => r.AddAsync(key, It.IsAny<IValidationCacheModel>()))
                .Returns(Task.FromResult<object>(true));

            var environmentMock = new Mock<IEnvironment>();

            var service = new ValidationCacheService(repositoryMock.Object, environmentMock.Object);

            // Act
            var result = await service.AddAsync(key, valueMock.Object).ConfigureAwait(false);

            // Asset
            Assert.That(result, Is.EqualTo(true));

            repositoryMock.Verify(r => r.AddAsync(key, It.IsAny<IValidationCacheModel>()), Times.Once);
            environmentMock.VerifyGet(e => e.DateTimeProvider.Invoke(), Times.Once);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ValidationCacheService), Description = "ValidationCacheService Add with invalid key and null value should throw ArgumentNullException.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [Timeout(1000)]
        public void ValidationCacheService_AddWithInvalidKeyAndNullValue_ShouldThrowArgumentNullException(string key)
        {
            // Arrange
            var repositoryMock = new Mock<IValidationCacheDataRepository>();
            var environmentMock = new Mock<IEnvironment>();
            var service = new ValidationCacheService(repositoryMock.Object, environmentMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return service.AddAsync(key, null);
            });

            repositoryMock.Verify(p => p.AddAsync(It.IsAny<string>(), It.IsAny<IValidationCacheModel>()), Times.Never);
            environmentMock.VerifyGet(e => e.DateTimeProvider.Invoke(), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ValidationCacheService), Description = "ValidationCacheService Add with invalid key and valid value should throw ArgumentNullException with correct ParamName.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [Timeout(300)]
        public void ValidationCacheService_AddWithInvalidKeyAndValidValue_ShouldThrowArgumentNullExceptionWithCorrectParamName(string key)
        {
            // Arrange
            var valueMock = new Mock<IValidationCacheModel>();
            var repositoryMock = new Mock<IValidationCacheDataRepository>();
            var environmentMock = new Mock<IEnvironment>();
            var service = new ValidationCacheService(repositoryMock.Object, environmentMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return service.AddAsync(key, valueMock.Object);
            });

            Assert.AreEqual(Constants.KeyParamName, exception.ParamName);

            repositoryMock.Verify(p => p.AddAsync(It.IsAny<string>(), It.IsAny<IValidationCacheModel>()), Times.Never);
            environmentMock.VerifyGet(e => e.DateTimeProvider.Invoke(), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ValidationCacheService), Description = "ValidationCacheService Add with valid key and null value should throw ArgumentNullException with correct ParamName.")]
        [Timeout(300)]
        public void ValidationCacheService_AddWithValidKeyAndNullValue_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            string key = "some key";
            var repositoryMock = new Mock<IValidationCacheDataRepository>();
            var environmentMock = new Mock<IEnvironment>();
            var service = new ValidationCacheService(repositoryMock.Object, environmentMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return service.AddAsync(key, null);
            });

            Assert.AreEqual(Constants.ValueParamName, exception.ParamName);

            repositoryMock.Verify(p => p.AddAsync(It.IsAny<string>(), It.IsAny<IValidationCacheModel>()), Times.Never);
            environmentMock.VerifyGet(e => e.DateTimeProvider.Invoke(), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ValidationCacheService), Description = "ValidationCacheService Get valid key from cache in which is not present should return null.")]
        [Timeout(1000)]
        public async Task ValidationCacheService_GetAllValidKeyFromCacheInWhichIsNotPresent_ShouldReturnEmptyIEnumerable()
        {
            // Arrange
            string key = "some key";

            var repositoryMock = new Mock<IValidationCacheDataRepository>();
            repositoryMock
                .Setup(r => r.GetAll(key))
                .Returns(value: null);

            var environmentMock = new Mock<IEnvironment>();

            var service = new ValidationCacheService(repositoryMock.Object, environmentMock.Object);

            // Act
            var result = await service.GetAsync(key).ConfigureAwait(false);

            // Asset
            Assert.IsNull(result);

            repositoryMock.Verify(r => r.GetAll(key), Times.Once);
            environmentMock.VerifyGet(e => e.DateTimeProvider.Invoke(), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ValidationCacheService), Description = "ValidationCacheService Get valid key with empty list should return null.")]
        [Timeout(1000)]
        public async Task ValidationCacheService_GetAllValidKeyWithEmptyList_ShouldReturnEmptyIEnumerable()
        {
            // Arrange
            string key = "some key";

            var list = new List<IValidationCacheModel>();

            var repositoryMock = new Mock<IValidationCacheDataRepository>();
            repositoryMock
                .Setup(r => r.GetAll(key))
                .Returns(list);

            var environmentMock = new Mock<IEnvironment>();

            var service = new ValidationCacheService(repositoryMock.Object, environmentMock.Object);

            // Act
            var result = await service.GetAsync(key).ConfigureAwait(false);

            // Asset
            Assert.IsNull(result);

            repositoryMock.Verify(r => r.GetAll(key), Times.Once);
            environmentMock.VerifyGet(e => e.DateTimeProvider.Invoke(), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ValidationCacheService), Description = "ValidationCacheService Get with invalid key should throw ArgumentNullException with correct ParamName.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [Timeout(300)]
        public void ValidationCacheService_GetAllWithInvalidKey_ShouldThrowArgumentNullExceptionWithCorrectParamName(string key)
        {
            // Arrange
            var repositoryMock = new Mock<IValidationCacheDataRepository>();
            var environmentMock = new Mock<IEnvironment>();
            var service = new ValidationCacheService(repositoryMock.Object, environmentMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return service.GetAsync(key);
            });

            Assert.AreEqual(Constants.KeyParamName, exception.ParamName);

            repositoryMock.Verify(r => r.GetAll(key), Times.Never);
            environmentMock.VerifyGet(e => e.DateTimeProvider.Invoke(), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ValidationCacheService), Description = "ValidationCacheService Get valid key with list with single value should return a new copy of it.")]
        [Timeout(1000)]
        public async Task ValidationCacheService_GetValidKeyWithListWithSingleValue_ShouldReturnANewCopyIt()
        {
            // Arrange
            string key = "some key";
            var valueMock = new Mock<IValidationCacheModel>();
            var value = valueMock.Object;
            var list = new[] { value };

            var repositoryMock = new Mock<IValidationCacheDataRepository>();
            repositoryMock
                .Setup(r => r.GetAll(key))
                .Returns(list);

            var environmentMock = new Mock<IEnvironment>();

            var service = new ValidationCacheService(repositoryMock.Object, environmentMock.Object);

            // Act
            var result = await service.GetAsync(key).ConfigureAwait(false);

            // Asset
            Assert.IsNotNull(result);
            Assert.AreNotSame(value, result);

            Assert.AreEqual(value.Content, result.Content);
            Assert.AreEqual(value.LastUpdate, result.LastUpdate);
            Assert.AreEqual(value.Status, result.Status);

            repositoryMock.Verify(r => r.GetAll(key), Times.Once);
            environmentMock.VerifyGet(e => e.DateTimeProvider.Invoke(), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ValidationCacheService), Description = "ValidationCacheService Get valid key with list with two values should return a new copy of the last updated one.")]
        [Timeout(1000)]
        public async Task ValidationCacheService_GetValidKeyWithListWithTwoValues_ShouldReturnANewCopyOfTheLastUpdatedOne()
        {
            // Arrange
            string key = "some key";
            var now = DateTime.Now;

            var value1Mock = new Mock<IValidationCacheModel>();
            value1Mock
                .SetupGet(v => v.LastUpdate)
                .Returns(now);

            var value2Mock = new Mock<IValidationCacheModel>();
            value2Mock
                .SetupGet(v => v.LastUpdate)
                .Returns(now + TimeSpan.FromHours(1));

            var expectedValue = value2Mock.Object;

            var list = new[] { value1Mock.Object, value2Mock.Object };

            var repositoryMock = new Mock<IValidationCacheDataRepository>();
            repositoryMock
                .Setup(r => r.GetAll(key))
                .Returns(list);

            var environmentMock = new Mock<IEnvironment>();

            var service = new ValidationCacheService(repositoryMock.Object, environmentMock.Object);

            // Act
            var result = await service.GetAsync(key).ConfigureAwait(false);

            // Asset
            Assert.IsNotNull(result);
            Assert.AreNotSame(value1Mock.Object, result);
            Assert.AreNotSame(value2Mock.Object, result);

            Assert.AreEqual(expectedValue.Content, result.Content);
            Assert.AreEqual(expectedValue.LastUpdate, result.LastUpdate);
            Assert.AreEqual(expectedValue.Status, result.Status);

            repositoryMock.Verify(r => r.GetAll(key), Times.Once);
            environmentMock.VerifyGet(e => e.DateTimeProvider.Invoke(), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ValidationCacheService), Description = "ValidationCacheService with null repository and null dateTimeProvider in constructor should throw ArgumentNullException.")]
        [Timeout(300)]
        public void ValidationCacheService_WithNullRepositoryAndNullDateTimeProviderInConstructor_ShouldThrowArgumentNullException()
        {
            // Act + Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new ValidationCacheService(null, null);
            });
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ValidationCacheService), Description = "ValidationCacheService with null repository and valid dateTimeProvider in constructor should throw ArgumentNullException with correct ParamName.")]
        [Timeout(300)]
        public void ValidationCacheService_WithNullRepositoryAndValidDateTimeProviderInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var environmentMock = new Mock<IEnvironment>();

            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new ValidationCacheService(null, environmentMock.Object);
            });

            Assert.AreEqual(Constants.RepositoryParamName, exception.ParamName);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ValidationCacheService), Description = "ValidationCacheService with valid parameters in constructor should be initialized correctly.")]
        [Timeout(1000)]
        public void ValidationCacheService_WithValidParametersInConstructor_ShouldBeInitializedCorrectly()
        {
            // Arrange
            var repositoryMock = new Mock<IValidationCacheDataRepository>();
            var repository = repositoryMock.Object;

            var environmentMock = new Mock<IEnvironment>();
            var dateTimeProvider = environmentMock.Object;

            // Act
            var service = new ValidationCacheService(repository, dateTimeProvider);

            // Assert
            Assert.IsNotNull(service);

            var repositoryFieldValue = PrivateField.GetInstanceField<ValidationCacheService>(service, Constants.RepositoryParamName);
            Assert.AreSame(repository, repositoryFieldValue, "Repository field should be set correctly.");

            var dateTimeProviderFieldValue = PrivateField.GetInstanceField<ValidationCacheService>(service, Constants.DateTimeProviderParamName);
            Assert.AreEqual(dateTimeProvider, dateTimeProviderFieldValue, "DateTimeProvider field should be set correctly.");
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ValidationCacheService), Description = "ValidationCacheService with valid repository and null dateTimeProvider in constructor should throw ArgumentNullException with correct ParamName.")]
        [Timeout(300)]
        public void ValidationCacheService_WithValidRepositoryAndNullDateTimeProviderInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var repositoryMock = new Mock<IValidationCacheDataRepository>();

            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new ValidationCacheService(repositoryMock.Object, null);
            });

            Assert.AreEqual(Constants.DateTimeProviderParamName, exception.ParamName);
        }
    }
}
