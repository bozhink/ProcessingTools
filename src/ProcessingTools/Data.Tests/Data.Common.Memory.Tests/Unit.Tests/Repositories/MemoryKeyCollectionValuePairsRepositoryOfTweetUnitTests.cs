namespace ProcessingTools.Data.Common.Memory.Tests.Unit.Tests.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using Models;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Common.Code.Tests;
    using ProcessingTools.Data.Common.Memory.Contracts;
    using ProcessingTools.Data.Common.Memory.Repositories;

    [TestFixture(Category = "Unit", TestOf = typeof(MemoryKeyCollectionValuePairsRepository<ITweet>))]
    public class MemoryKeyCollectionValuePairsRepositoryOfTweetUnitTests
    {
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(MemoryKeyCollectionValuePairsRepository<ITweet>), Description = "MemoryKeyCollectionValuePairsRepositoryOfTweet Add valid key and valid value should work.")]
        [Timeout(1000)]
        public async Task MemoryKeyCollectionValuePairsRepositoryOfTweet_AddValidNonPresentKeyAndValidValue_ShouldWork()
        {
            // Arrange
            string key = "some key";
            var valueMock = new Mock<ITweet>();
            var returnValueMock = new Mock<ICollection<ITweet>>();

            var dataStoreMock = new Mock<IMemoryStringKeyCollectionValueDataStore<ITweet>>();
            dataStoreMock
                .Setup(s => s.AddOrUpdate(key, It.IsAny<Func<string, ICollection<ITweet>>>(), It.IsAny<Func<string, ICollection<ITweet>, ICollection<ITweet>>>()))
                .Returns(returnValueMock.Object);

            var repository = new MemoryKeyCollectionValuePairsRepository<ITweet>(dataStoreMock.Object);

            // Act
            var result = await repository.AddAsync(key, valueMock.Object).ConfigureAwait(false);

            // Asset
            Assert.AreSame(result, returnValueMock.Object);

            dataStoreMock.Verify(
                s => s.AddOrUpdate(It.IsAny<string>(), It.IsAny<Func<string, ICollection<ITweet>>>(), It.IsAny<Func<string, ICollection<ITweet>, ICollection<ITweet>>>()),
                Times.Once);

            dataStoreMock.Verify(
                s => s.AddOrUpdate(key, It.IsAny<Func<string, ICollection<ITweet>>>(), It.IsAny<Func<string, ICollection<ITweet>, ICollection<ITweet>>>()),
                Times.Once);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(MemoryKeyCollectionValuePairsRepository<ITweet>), Description = "MemoryKeyCollectionValuePairsRepositoryOfTweet Add with invalid key and null value should throw ArgumentNullException.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [Timeout(300)]
        public void MemoryKeyCollectionValuePairsRepositoryOfTweet_AddWithInvalidKeyAndNullValue_ShouldThrowArgumentNullException(string key)
        {
            // Arrange
            var dataStoreMock = new Mock<IMemoryStringKeyCollectionValueDataStore<ITweet>>();

            var repository = new MemoryKeyCollectionValuePairsRepository<ITweet>(dataStoreMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.AddAsync(key, null);
            });

            dataStoreMock.Verify(
                s => s.AddOrUpdate(key, It.IsAny<Func<string, ICollection<ITweet>>>(), It.IsAny<Func<string, ICollection<ITweet>, ICollection<ITweet>>>()),
                Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(MemoryKeyCollectionValuePairsRepository<ITweet>), Description = "MemoryKeyCollectionValuePairsRepositoryOfTweet Add with invalid key and valid value should throw ArgumentNullException with correct ParamName.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [Timeout(300)]
        public void MemoryKeyCollectionValuePairsRepositoryOfTweet_AddWithInvalidKeyAndValidValue_ShouldThrowArgumentNullExceptionWithCorrectParamName(string key)
        {
            // Arrange
            var valueMock = new Mock<ITweet>();

            var dataStoreMock = new Mock<IMemoryStringKeyCollectionValueDataStore<ITweet>>();

            var repository = new MemoryKeyCollectionValuePairsRepository<ITweet>(dataStoreMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.AddAsync(key, valueMock.Object);
            });

            Assert.AreEqual(Constants.KeyParamName, exception.ParamName);

            dataStoreMock.Verify(
                s => s.AddOrUpdate(key, It.IsAny<Func<string, ICollection<ITweet>>>(), It.IsAny<Func<string, ICollection<ITweet>, ICollection<ITweet>>>()),
                Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(MemoryKeyCollectionValuePairsRepository<ITweet>), Description = "MemoryKeyCollectionValuePairsRepositoryOfTweet Add with valid key and null value should throw ArgumentNullException with correct ParamName.")]
        [Timeout(300)]
        public void MemoryKeyCollectionValuePairsRepositoryOfTweet_AddWithValidKeyAndNullValue_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            string key = "some key";

            var dataStoreMock = new Mock<IMemoryStringKeyCollectionValueDataStore<ITweet>>();

            var repository = new MemoryKeyCollectionValuePairsRepository<ITweet>(dataStoreMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.AddAsync(key, null);
            });

            Assert.AreEqual(Constants.ValueParamName, exception.ParamName);

            dataStoreMock.Verify(
                s => s.AddOrUpdate(key, It.IsAny<Func<string, ICollection<ITweet>>>(), It.IsAny<Func<string, ICollection<ITweet>, ICollection<ITweet>>>()),
                Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(MemoryKeyCollectionValuePairsRepository<ITweet>), Description = "MemoryKeyCollectionValuePairsRepositoryOfTweet GetAll valid key and empty list should return empty IEnumerable.")]
        [Timeout(1000)]
        public void MemoryKeyCollectionValuePairsRepositoryOfTweet_GetAllValidKeyAndEmptyList_ShouldReturnEmptyIEnumerable()
        {
            // Arrange
            string key = "some key";

            var list = new List<ITweet>();

            var dataStoreMock = new Mock<IMemoryStringKeyCollectionValueDataStore<ITweet>>();
            dataStoreMock
                .Setup(s => s[It.IsAny<string>()])
                .Returns(list);

            var repository = new MemoryKeyCollectionValuePairsRepository<ITweet>(dataStoreMock.Object);

            // Act
            var result = repository.GetAll(key);

            // Act + Asset
            Assert.That(result.Count(), Is.EqualTo(0));

            dataStoreMock.Verify(s => s[It.IsAny<string>()], Times.Once);
            dataStoreMock.Verify(s => s[key], Times.Once);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(MemoryKeyCollectionValuePairsRepository<ITweet>), Description = "MemoryKeyCollectionValuePairsRepositoryOfTweet GetAll with invalid key should throw ArgumentNullException with correct ParamName.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [Timeout(300)]
        public void MemoryKeyCollectionValuePairsRepositoryOfTweet_GetAllWithInvalidKey_ShouldThrowArgumentNullExceptionWithCorrectParamName(string key)
        {
            // Arrange
            var dataStoreMock = new Mock<IMemoryStringKeyCollectionValueDataStore<ITweet>>();

            var repository = new MemoryKeyCollectionValuePairsRepository<ITweet>(dataStoreMock.Object);

            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                repository.GetAll(key);
            });

            Assert.AreEqual(Constants.KeyParamName, exception.ParamName);

            dataStoreMock.Verify(s => s[It.IsAny<string>()], Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(MemoryKeyCollectionValuePairsRepository<ITweet>), Description = "MemoryKeyCollectionValuePairsRepositoryOfTweet Get Keys should work.")]
        [Timeout(1000)]
        public void MemoryKeyCollectionValuePairsRepositoryOfTweet_GetKeys_ShouldWork()
        {
            // Arrange
            var listOfKeys = new List<string>();

            var dataStoreMock = new Mock<IMemoryStringKeyCollectionValueDataStore<ITweet>>();
            dataStoreMock
                .SetupGet(s => s.Keys)
                .Returns(listOfKeys);

            var repository = new MemoryKeyCollectionValuePairsRepository<ITweet>(dataStoreMock.Object);

            // Act
            var keys = repository.Keys;

            // Assert
            Assert.AreSame(listOfKeys, keys);

            dataStoreMock.Verify(s => s.Keys, Times.Once);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(MemoryKeyCollectionValuePairsRepository<ITweet>), Description = "MemoryKeyCollectionValuePairsRepositoryOfTweet Remove valid key and valid value should work.")]
        [Timeout(1000)]
        public async Task MemoryKeyCollectionValuePairsRepositoryOfTweet_RemoveValidKeyAndValidValue_ShouldWork()
        {
            // Arrange
            string key = "some key";
            var valueMock = new Mock<ITweet>();

            var dataStoreMock = new Mock<IMemoryStringKeyCollectionValueDataStore<ITweet>>();

            var repository = new MemoryKeyCollectionValuePairsRepository<ITweet>(dataStoreMock.Object);

            // Act
            var result = await repository.RemoveAsync(key, valueMock.Object).ConfigureAwait(false);

            // Asset
            Assert.That(result, Is.EqualTo(true));

            dataStoreMock.Verify(
               s => s.AddOrUpdate(It.IsAny<string>(), It.IsAny<Func<string, ICollection<ITweet>>>(), It.IsAny<Func<string, ICollection<ITweet>, ICollection<ITweet>>>()),
               Times.Once);

            dataStoreMock.Verify(
                s => s.AddOrUpdate(key, It.IsAny<Func<string, ICollection<ITweet>>>(), It.IsAny<Func<string, ICollection<ITweet>, ICollection<ITweet>>>()),
                Times.Once);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(MemoryKeyCollectionValuePairsRepository<ITweet>), Description = "MemoryKeyCollectionValuePairsRepositoryOfTweet Remove valid non-present key should work.")]
        [Timeout(1000)]
        public async Task MemoryKeyCollectionValuePairsRepositoryOfTweet_RemoveValidNonPresentKey_ShouldWork()
        {
            // Arrange
            string key = "some key";

            var dataStoreMock = new Mock<IMemoryStringKeyCollectionValueDataStore<ITweet>>();
            dataStoreMock
                .Setup(s => s.Remove(key))
                .Returns(true);

            var repository = new MemoryKeyCollectionValuePairsRepository<ITweet>(dataStoreMock.Object);

            // Act
            var result = await repository.RemoveAsync(key).ConfigureAwait(false);

            // Asset
            Assert.That(result, Is.EqualTo(true));

            dataStoreMock.Verify(s => s.Remove(It.IsAny<string>()), Times.Once);
            dataStoreMock.Verify(s => s.Remove(key), Times.Once);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(MemoryKeyCollectionValuePairsRepository<ITweet>), Description = "MemoryKeyCollectionValuePairsRepositoryOfTweet Remove valid yet-present key should work.")]
        [Timeout(1000)]
        public async Task MemoryKeyCollectionValuePairsRepositoryOfTweet_RemoveValidYetPresentKey_ShouldWork()
        {
            // Arrange
            string key = "some key";

            var dataStoreMock = new Mock<IMemoryStringKeyCollectionValueDataStore<ITweet>>();
            dataStoreMock
                .Setup(s => s.Remove(key))
                .Returns(true);

            var repository = new MemoryKeyCollectionValuePairsRepository<ITweet>(dataStoreMock.Object);

            // Act
            var result = await repository.RemoveAsync(key).ConfigureAwait(false);

            // Asset
            Assert.That(result, Is.EqualTo(true));

            dataStoreMock.Verify(s => s.Remove(It.IsAny<string>()), Times.Once);
            dataStoreMock.Verify(s => s.Remove(key), Times.Once);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(MemoryKeyCollectionValuePairsRepository<ITweet>), Description = "MemoryKeyCollectionValuePairsRepositoryOfTweet Remove with invalid key should throw ArgumentNullException with correct ParamName.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [Timeout(300)]
        public void MemoryKeyCollectionValuePairsRepositoryOfTweet_RemoveWithInvalidKey_ShouldThrowArgumentNullExceptionWithCorrectParamName(string key)
        {
            // Arrange
            var dataStoreMock = new Mock<IMemoryStringKeyCollectionValueDataStore<ITweet>>();

            var repository = new MemoryKeyCollectionValuePairsRepository<ITweet>(dataStoreMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.RemoveAsync(key);
            });

            Assert.AreEqual(Constants.KeyParamName, exception.ParamName);

            dataStoreMock.Verify(s => s.Remove(It.IsAny<string>()), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(MemoryKeyCollectionValuePairsRepository<ITweet>), Description = "MemoryKeyCollectionValuePairsRepositoryOfTweet Remove with invalid key and null value should throw ArgumentNullException.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [Timeout(300)]
        public void MemoryKeyCollectionValuePairsRepositoryOfTweet_RemoveWithInvalidKeyAndNullValue_ShouldThrowArgumentNullException(string key)
        {
            // Arrange
            var dataStoreMock = new Mock<IMemoryStringKeyCollectionValueDataStore<ITweet>>();

            var repository = new MemoryKeyCollectionValuePairsRepository<ITweet>(dataStoreMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.RemoveAsync(key, null);
            });

            dataStoreMock.Verify(s => s.Remove(It.IsAny<string>()), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(MemoryKeyCollectionValuePairsRepository<ITweet>), Description = "MemoryKeyCollectionValuePairsRepositoryOfTweet Remove with invalid key and valid value should throw ArgumentNullException with correct ParamName.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [Timeout(300)]
        public void MemoryKeyCollectionValuePairsRepositoryOfTweet_RemoveWithInvalidKeyAndValidValue_ShouldThrowArgumentNullExceptionWithCorrectParamName(string key)
        {
            // Arrange
            var valueMock = new Mock<ITweet>();
            var value = valueMock.Object;

            var dataStoreMock = new Mock<IMemoryStringKeyCollectionValueDataStore<ITweet>>();

            var repository = new MemoryKeyCollectionValuePairsRepository<ITweet>(dataStoreMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.RemoveAsync(key, value);
            });

            Assert.AreEqual(Constants.KeyParamName, exception.ParamName);

            dataStoreMock.Verify(s => s.Remove(It.IsAny<string>()), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(MemoryKeyCollectionValuePairsRepository<ITweet>), Description = "MemoryKeyCollectionValuePairsRepositoryOfTweet Remove with valid key and null value should throw ArgumentNullException with correct ParamName.")]
        [Timeout(300)]
        public void MemoryKeyCollectionValuePairsRepositoryOfTweet_RemoveWithValidKeyAndNullValue_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            string key = "some key";

            var dataStoreMock = new Mock<IMemoryStringKeyCollectionValueDataStore<ITweet>>();

            var repository = new MemoryKeyCollectionValuePairsRepository<ITweet>(dataStoreMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.RemoveAsync(key, null);
            });

            Assert.AreEqual(Constants.ValueParamName, exception.ParamName);

            dataStoreMock.Verify(s => s.Remove(It.IsAny<string>()), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(MemoryKeyCollectionValuePairsRepository<ITweet>), Description = "MemoryKeyCollectionValuePairsRepositoryOfTweet SaveChanges should work.")]
        [Timeout(1000)]
        public async Task MemoryKeyCollectionValuePairsRepositoryOfTweet_SaveChanges_ShouldWork()
        {
            // Arrange
            var dataStoreMock = new Mock<IMemoryStringKeyCollectionValueDataStore<ITweet>>();

            var repository = new MemoryKeyCollectionValuePairsRepository<ITweet>(dataStoreMock.Object);

            // Act
            var result = await repository.SaveChangesAsync().ConfigureAwait(false);

            // Asset
            Assert.That(result, Is.EqualTo(0L));

            dataStoreMock.Verify();
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(MemoryKeyCollectionValuePairsRepository<ITweet>), Description = "MemoryKeyCollectionValuePairsRepositoryOfTweet with null dataStore in constructor should throw ArgumentNullException with correct ParamName.")]
        [Timeout(300)]
        public void MemoryKeyCollectionValuePairsRepositoryOfTweet_WithNullDataStoreInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new MemoryKeyCollectionValuePairsRepository<ITweet>(null);
            });

            Assert.AreEqual(Constants.DataStoreParamName, exception.ParamName);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(MemoryKeyCollectionValuePairsRepository<ITweet>), Description = "MemoryKeyCollectionValuePairsRepositoryOfTweet with valid dataStore in constructor should be initialized correctly.")]
        [Timeout(300)]
        public void MemoryKeyCollectionValuePairsRepositoryOfTweet_WithValidDataStoreInConstructor_ShouldBeInitializedCorrectly()
        {
            // Arrange
            var dataStoreMock = new Mock<IMemoryStringKeyCollectionValueDataStore<ITweet>>();
            var dataStore = dataStoreMock.Object;

            // Act + Assert
            var repository = new MemoryKeyCollectionValuePairsRepository<ITweet>(dataStore);
            Assert.IsNotNull(repository);

            var dataStoreFieldValue = PrivateField.GetInstanceField<MemoryKeyCollectionValuePairsRepository<ITweet>>(
                repository,
                Constants.DataStoreParamName);
            Assert.AreSame(dataStore, dataStoreFieldValue);
        }
    }
}
