namespace ProcessingTools.Services.Cache.Tests.Unit.Tests.Validation
{
    using System;
    using System.Threading.Tasks;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Common.Code.Tests;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Contracts.Cache;
    using ProcessingTools.Data.Models.Contracts.Cache;
    using ProcessingTools.Models.Contracts.Cache;
    using ProcessingTools.Services.Cache.Tests.Common;

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

            var daoMock = new Mock<IValidationCacheDataAccessObject>();
            daoMock
                .Setup(r => r.AddAsync(key, It.IsAny<IValidationCacheModel>()))
                .Returns(Task.FromResult<object>(true));

            var applicationContextMock = new Mock<IApplicationContext>();

            var sut = new ValidationCacheService(daoMock.Object, applicationContextMock.Object);

            // Act
            var result = await sut.AddAsync(key, valueMock.Object).ConfigureAwait(false);

            // Asset
            Assert.That(result, Is.EqualTo(true));

            daoMock.Verify(r => r.AddAsync(key, It.IsAny<IValidationCacheModel>()), Times.Once);
            applicationContextMock.VerifyGet(e => e.DateTimeProvider.Invoke(), Times.Once);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ValidationCacheService), Description = "ValidationCacheService Add with invalid key and null value should throw ArgumentNullException.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [Timeout(1000)]
        public void ValidationCacheService_AddWithInvalidKeyAndNullValue_ShouldThrowArgumentNullException(string key)
        {
            // Arrange
            var daoMock = new Mock<IValidationCacheDataAccessObject>();
            var applicationContextMock = new Mock<IApplicationContext>();
            var sut = new ValidationCacheService(daoMock.Object, applicationContextMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return sut.AddAsync(key, null);
            });

            daoMock.Verify(p => p.AddAsync(It.IsAny<string>(), It.IsAny<IValidationCacheModel>()), Times.Never);
            applicationContextMock.VerifyGet(e => e.DateTimeProvider.Invoke(), Times.Never);
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
            var daoMock = new Mock<IValidationCacheDataAccessObject>();
            var applicationContextMock = new Mock<IApplicationContext>();
            var sut = new ValidationCacheService(daoMock.Object, applicationContextMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return sut.AddAsync(key, valueMock.Object);
            });

            Assert.AreEqual(Constants.KeyParamName, exception.ParamName);

            daoMock.Verify(p => p.AddAsync(It.IsAny<string>(), It.IsAny<IValidationCacheModel>()), Times.Never);
            applicationContextMock.VerifyGet(e => e.DateTimeProvider.Invoke(), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ValidationCacheService), Description = "ValidationCacheService Add with valid key and null value should throw ArgumentNullException with correct ParamName.")]
        [Timeout(300)]
        public void ValidationCacheService_AddWithValidKeyAndNullValue_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            string key = "some key";
            var daoMock = new Mock<IValidationCacheDataAccessObject>();
            var applicationContextMock = new Mock<IApplicationContext>();
            var sut = new ValidationCacheService(daoMock.Object, applicationContextMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return sut.AddAsync(key, null);
            });

            Assert.AreEqual(Constants.ValueParamName, exception.ParamName);

            daoMock.Verify(p => p.AddAsync(It.IsAny<string>(), It.IsAny<IValidationCacheModel>()), Times.Never);
            applicationContextMock.VerifyGet(e => e.DateTimeProvider.Invoke(), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ValidationCacheService), Description = "ValidationCacheService Get valid key from cache in which is not present should return null.")]
        [Timeout(1000)]
        public async Task ValidationCacheService_GetAllValidKeyFromCacheInWhichIsNotPresent_ShouldReturnEmptyIEnumerable()
        {
            // Arrange
            string key = "some key";

            var daoMock = new Mock<IValidationCacheDataAccessObject>();
            daoMock
                .Setup(r => r.GetLastForKeyAsync(key))
                .Returns(value: Task.FromResult<IValidationCacheDataModel>(null));

            var applicationContextMock = new Mock<IApplicationContext>();

            var sut = new ValidationCacheService(daoMock.Object, applicationContextMock.Object);

            // Act
            var result = await sut.GetAsync(key).ConfigureAwait(false);

            // Asset
            Assert.IsNull(result);

            daoMock.Verify(r => r.GetLastForKeyAsync(key), Times.Once);
            applicationContextMock.VerifyGet(e => e.DateTimeProvider.Invoke(), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ValidationCacheService), Description = "ValidationCacheService Get valid key with empty list should return null.")]
        [Timeout(1000)]
        public async Task ValidationCacheService_GetAllValidKeyWithEmptyList_ShouldReturnEmptyIEnumerable()
        {
            // Arrange
            string key = "some key";

            var valueMock = new Mock<IValidationCacheDataModel>();

            var daoMock = new Mock<IValidationCacheDataAccessObject>();
            daoMock
                .Setup(r => r.GetLastForKeyAsync(key))
                .Returns(Task.FromResult(valueMock.Object));

            var applicationContextMock = new Mock<IApplicationContext>();

            var sut = new ValidationCacheService(daoMock.Object, applicationContextMock.Object);

            // Act
            var result = await sut.GetAsync(key).ConfigureAwait(false);

            // Asset
            Assert.IsNull(result);

            daoMock.Verify(r => r.GetLastForKeyAsync(key), Times.Once);
            applicationContextMock.VerifyGet(e => e.DateTimeProvider.Invoke(), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ValidationCacheService), Description = "ValidationCacheService Get with invalid key should throw ArgumentNullException with correct ParamName.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [Timeout(300)]
        public void ValidationCacheService_GetAllWithInvalidKey_ShouldThrowArgumentNullExceptionWithCorrectParamName(string key)
        {
            // Arrange
            var daoMock = new Mock<IValidationCacheDataAccessObject>();
            var applicationContextMock = new Mock<IApplicationContext>();
            var sut = new ValidationCacheService(daoMock.Object, applicationContextMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return sut.GetAsync(key);
            });

            Assert.AreEqual(Constants.KeyParamName, exception.ParamName);

            daoMock.Verify(r => r.GetLastForKeyAsync(key), Times.Never);
            applicationContextMock.VerifyGet(e => e.DateTimeProvider.Invoke(), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ValidationCacheService), Description = "ValidationCacheService Get valid key with list with single value should return a new copy of it.")]
        [Timeout(1000)]
        public async Task ValidationCacheService_GetValidKeyWithListWithSingleValue_ShouldReturnANewCopyIt()
        {
            // Arrange
            string key = "some key";
            var valueMock = new Mock<IValidationCacheDataModel>();

            var daoMock = new Mock<IValidationCacheDataAccessObject>();
            daoMock
                .Setup(r => r.GetLastForKeyAsync(key))
                .Returns(Task.FromResult(valueMock.Object));

            var applicationContextMock = new Mock<IApplicationContext>();

            var sut = new ValidationCacheService(daoMock.Object, applicationContextMock.Object);

            // Act
            var result = await sut.GetAsync(key).ConfigureAwait(false);

            // Asset
            Assert.IsNotNull(result);
            Assert.AreNotSame(valueMock.Object, result);

            Assert.AreEqual(valueMock.Object.Content, result.Content, "Content");
            Assert.AreEqual(valueMock.Object.LastUpdate, result.LastUpdate, "LastUpdate");
            Assert.AreEqual(valueMock.Object.Status, result.Status, "Status");

            daoMock.Verify(r => r.GetLastForKeyAsync(key), Times.Once);
            applicationContextMock.VerifyGet(e => e.DateTimeProvider.Invoke(), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ValidationCacheService), Description = "ValidationCacheService with null DAO and null applicationContext in constructor should throw ArgumentNullException.")]
        [Timeout(300)]
        public void ValidationCacheService_WithNullDataAccessObjectAndNullApplicationContextInConstructor_ShouldThrowArgumentNullException()
        {
            // Act + Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new ValidationCacheService(null, null);
            });
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ValidationCacheService), Description = "ValidationCacheService with null DAO and valid applicationContext in constructor should throw ArgumentNullException with correct ParamName.")]
        [Timeout(300)]
        public void ValidationCacheService_WithNullDataAccessObjectAndValidApplicationContextInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var applicationContextMock = new Mock<IApplicationContext>();

            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new ValidationCacheService(null, applicationContextMock.Object);
            });

            Assert.AreEqual(Constants.DataAccessObjectParamName, exception.ParamName);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ValidationCacheService), Description = "ValidationCacheService with valid parameters in constructor should be initialized correctly.")]
        [Timeout(1000)]
        public void ValidationCacheService_WithValidParametersInConstructor_ShouldBeInitializedCorrectly()
        {
            // Arrange
            var daoMock = new Mock<IValidationCacheDataAccessObject>();

            var applicationContextMock = new Mock<IApplicationContext>();

            // Act
            var service = new ValidationCacheService(daoMock.Object, applicationContextMock.Object);

            // Assert
            Assert.IsNotNull(service);

            var daoFieldValue = PrivateField.GetInstanceField<ValidationCacheService>(service, Constants.DataAccessObjectParamName);
            Assert.AreSame(daoMock.Object, daoFieldValue, "DAO field should be set correctly.");

            var applicationContextFieldValue = PrivateField.GetInstanceField<ValidationCacheService>(service, Constants.ApplicationContextParamName);
            Assert.AreEqual(applicationContextMock.Object, applicationContextFieldValue, "ApplicationContext field should be set correctly.");
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ValidationCacheService), Description = "ValidationCacheService with valid DAO and null applicationContext in constructor should throw ArgumentNullException with correct ParamName.")]
        [Timeout(300)]
        public void ValidationCacheService_WithValidDataAccessObjectAndNullApplicationContextInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var daoMock = new Mock<IValidationCacheDataAccessObject>();

            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new ValidationCacheService(daoMock.Object, null);
            });

            Assert.AreEqual(Constants.ApplicationContextParamName, exception.ParamName);
        }
    }
}
