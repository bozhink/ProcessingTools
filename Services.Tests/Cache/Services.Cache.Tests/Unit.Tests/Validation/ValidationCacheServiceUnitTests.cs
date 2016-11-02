namespace ProcessingTools.Services.Cache.Tests.Unit.Tests.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Cache.Data.Common.Contracts.Models;
    using ProcessingTools.Cache.Data.Common.Contracts.Repositories;
    using ProcessingTools.Services.Cache.Models.Validation;
    using ProcessingTools.Services.Cache.Validation;
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
            var valueMock = new Mock<IValidationCacheServiceModel>();

            var repositoryMock = new Mock<IValidationCacheDataRepository>();
            repositoryMock.Setup(r => r.Add(key, It.IsAny<IValidationCacheEntity>()))
                .Returns(Task.FromResult<object>(true));

            var service = new ValidationCacheService(repositoryMock.Object);

            // Act
            var result = await service.Add(key, valueMock.Object);

            // Asset
            Assert.That(result, Is.EqualTo(true));

            repositoryMock.Verify(r => r.Add(key, valueMock.Object), Times.Once);
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

            var service = new ValidationCacheService(repositoryMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return service.Add(key, null);
            });

            repositoryMock.Verify(p => p.Add(It.IsAny<string>(), It.IsAny<IValidationCacheEntity>()), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ValidationCacheService), Description = "ValidationCacheService Add with invalid key and valid value should throw ArgumentNullException with correct ParamName.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [Timeout(300)]
        public void ValidationCacheService_AddWithInvalidKeyAndValidValue_ShouldThrowArgumentNullExceptionWithCorrectParamName(string key)
        {
            // Arrange
            var valueMock = new Mock<IValidationCacheServiceModel>();

            var repositoryMock = new Mock<IValidationCacheDataRepository>();

            var service = new ValidationCacheService(repositoryMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return service.Add(key, valueMock.Object);
            });

            Assert.AreEqual(Constants.KeyParamName, exception.ParamName);

            repositoryMock.Verify(p => p.Add(It.IsAny<string>(), It.IsAny<IValidationCacheEntity>()), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ValidationCacheService), Description = "ValidationCacheService Add with valid key and null value should throw ArgumentNullException with correct ParamName.")]
        [Timeout(300)]
        public void ValidationCacheService_AddWithValidKeyAndNullValue_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            string key = "some key";

            var repositoryMock = new Mock<IValidationCacheDataRepository>();

            var service = new ValidationCacheService(repositoryMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return service.Add(key, null);
            });

            Assert.AreEqual(Constants.ValueParamName, exception.ParamName);

            repositoryMock.Verify(p => p.Add(It.IsAny<string>(), It.IsAny<IValidationCacheEntity>()), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ValidationCacheService), Description = "ValidationCacheService GetAll valid key and empty list should return empty IEnumerable.")]
        [Timeout(1000)]
        public void ValidationCacheService_GetAllValidKeyAndEmptyList_ShouldReturnEmptyIEnumerable()
        {
            // Arrange
            string key = "some key";

            var list = new List<IValidationCacheEntity>();

            var repositoryMock = new Mock<IValidationCacheDataRepository>();
            repositoryMock.Setup(r => r.GetAll(key))
                .Returns(list);

            var service = new ValidationCacheService(repositoryMock.Object);

            // Act
            var result = service.GetAll(key);

            // Asset
            Assert.That(result.Count(), Is.EqualTo(0));

            repositoryMock.Verify(r => r.GetAll(key), Times.Once);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ValidationCacheService), Description = "ValidationCacheService GetAll with invalid key should throw ArgumentNullException with correct ParamName.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [Timeout(300)]
        public void ValidationCacheService_GetAllWithInvalidKey_ShouldThrowArgumentNullExceptionWithCorrectParamName(string key)
        {
            // Arrange
            var repositoryMock = new Mock<IValidationCacheDataRepository>();

            var service = new ValidationCacheService(repositoryMock.Object);

            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                service.GetAll(key);
            });

            Assert.AreEqual(Constants.KeyParamName, exception.ParamName);

            repositoryMock.Verify(r => r.GetAll(key), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ValidationCacheService), Description = "ValidationCacheService Remove with invalid key and null value should throw ArgumentNullException.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [Timeout(300)]
        public void ValidationCacheService_RemoveWithInvalidKeyAndNullValue_ShouldThrowArgumentNullException(string key)
        {
            // Arrange
            var repositoryMock = new Mock<IValidationCacheDataRepository>();

            var service = new ValidationCacheService(repositoryMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return service.Remove(key, null);
            });

            repositoryMock.Verify(p => p.Remove(It.IsAny<string>(), It.IsAny<IValidationCacheEntity>()), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ValidationCacheService), Description = "ValidationCacheService Remove with invalid key and valid value should throw ArgumentNullException with correct ParamName.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [Timeout(300)]
        public void ValidationCacheService_RemoveWithInvalidKeyAndValidValue_ShouldThrowArgumentNullExceptionWithCorrectParamName(string key)
        {
            // Arrange
            var valueMock = new Mock<IValidationCacheServiceModel>();

            var repositoryMock = new Mock<IValidationCacheDataRepository>();

            var service = new ValidationCacheService(repositoryMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return service.Remove(key, valueMock.Object);
            });

            Assert.AreEqual(Constants.KeyParamName, exception.ParamName);

            repositoryMock.Verify(p => p.Remove(It.IsAny<string>(), It.IsAny<IValidationCacheEntity>()), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ValidationCacheService), Description = "ValidationCacheService Remove with valid key and null value should throw ArgumentNullException with correct ParamName.")]
        [Timeout(300)]
        public void ValidationCacheService_RemoveWithValidKeyAndNullValue_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            string key = "some key";

            var repositoryMock = new Mock<IValidationCacheDataRepository>();

            var service = new ValidationCacheService(repositoryMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return service.Remove(key, null);
            });

            Assert.AreEqual(Constants.ValueParamName, exception.ParamName);

            repositoryMock.Verify(p => p.Remove(It.IsAny<string>(), It.IsAny<IValidationCacheEntity>()), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ValidationCacheService), Description = "ValidationCacheService Remove with valid key and valid value should work.")]
        [Timeout(5000)]
        public async Task ValidationCacheService_RemoveWithValidNonPresentKeyAndValidValue_ShouldWork()
        {
            // Arrange
            string key = "some key";
            var valueMock = new Mock<IValidationCacheServiceModel>();

            var repositoryMock = new Mock<IValidationCacheDataRepository>();
            repositoryMock.Setup(r => r.Remove(key, It.IsAny<IValidationCacheEntity>()))
                .Returns(Task.FromResult<object>(true));

            var service = new ValidationCacheService(repositoryMock.Object);

            // Act
            var result = await service.Remove(key, valueMock.Object);

            // Asset
            Assert.That(result, Is.EqualTo(true));

            repositoryMock.Verify(r => r.Remove(key, valueMock.Object), Times.Once);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ValidationCacheService), Description = "ValidationCacheService with null repository in constructor should throw ArgumentNullException with correct ParamName.")]
        [Timeout(300)]
        public void ValidationCacheService_WithNullRepositoryInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new ValidationCacheService(null);
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

            // Act
            var service = new ValidationCacheService(repository);

            // Assert
            Assert.IsNotNull(service);

            var repositoryFieldValue = PrivateField.GetInstanceField<ValidationCacheService>(service, Constants.RepositoryParamName);
            Assert.AreSame(repository, repositoryFieldValue);
        }
    }
}
