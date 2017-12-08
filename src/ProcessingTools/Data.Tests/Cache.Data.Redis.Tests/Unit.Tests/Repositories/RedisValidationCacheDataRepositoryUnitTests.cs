namespace ProcessingTools.Cache.Data.Redis.Tests.Unit.Tests.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Cache.Data.Redis.Models;
    using ProcessingTools.Cache.Data.Redis.Repositories;
    using ProcessingTools.Cache.Data.Redis.Tests.Common;
    using ProcessingTools.Contracts.Models.Cache;
    using ProcessingTools.Data.Common.Redis.Contracts;
    using ProcessingTools.Tests.Library;
    using ServiceStack.Redis;

    [TestFixture(Category = "Unit", TestOf = typeof(RedisValidationCacheDataRepository))]
    public class RedisValidationCacheDataRepositoryUnitTests
    {
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisValidationCacheDataRepository), Description = "RedisValidationCacheDataRepository Add valid key and valid value should work.")]
        [Timeout(5000)]
        public async Task RedisValidationCacheDataRepository_AddValidNonPresentKeyAndValidValue_ShouldWork()
        {
            // Arrange
            string key = "some key";

            var value = new ValidationCacheEntity();

            var listMock = new Mock<IRedisList>();

            var clientMock = new Mock<IRedisClient>();
            clientMock
                .Setup(c => c.Lists[It.IsAny<string>()])
                .Returns(listMock.Object);

            var clientProviderMock = new Mock<IRedisClientProvider>();
            clientProviderMock
                .Setup(p => p.Create())
                .Returns(clientMock.Object);

            var repository = new RedisValidationCacheDataRepository(clientProviderMock.Object);

            // Act
            var result = await repository.AddAsync(key, value).ConfigureAwait(false);

            // Asset
            Assert.That(result, Is.EqualTo(true));

            clientProviderMock.Verify(p => p.Create(), Times.Once);
            clientMock.Verify(c => c.Lists[key], Times.Once);
            clientMock.Verify(c => c.Dispose(), Times.Once);
            listMock.Verify(l => l.Add(It.IsAny<string>()), Times.Once);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisValidationCacheDataRepository), Description = "RedisValidationCacheDataRepository Add with invalid key and null value should throw ArgumentNullException.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [Timeout(300)]
        public void RedisValidationCacheDataRepository_AddWithInvalidKeyAndNullValue_ShouldThrowArgumentNullException(string key)
        {
            // Arrange
            var clientProviderMock = new Mock<IRedisClientProvider>();

            var repository = new RedisValidationCacheDataRepository(clientProviderMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.AddAsync(key, null);
            });

            clientProviderMock.Verify(p => p.Create(), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisValidationCacheDataRepository), Description = "RedisValidationCacheDataRepository Add with invalid key and valid value should throw ArgumentNullException with correct ParamName.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [Timeout(300)]
        public void RedisValidationCacheDataRepository_AddWithInvalidKeyAndValidValue_ShouldThrowArgumentNullExceptionWithCorrectParamName(string key)
        {
            // Arrange
            var valueMock = new Mock<IValidationCacheModel>();
            var value = valueMock.Object;

            var clientProviderMock = new Mock<IRedisClientProvider>();

            var repository = new RedisValidationCacheDataRepository(clientProviderMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.AddAsync(key, value);
            });

            Assert.AreEqual(Constants.KeyParamName, exception.ParamName);

            clientProviderMock.Verify(p => p.Create(), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisValidationCacheDataRepository), Description = "RedisValidationCacheDataRepository Add with valid key and null value should throw ArgumentNullException with correct ParamName.")]
        [Timeout(300)]
        public void RedisValidationCacheDataRepository_AddWithValidKeyAndNullValue_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            string key = "some key";

            var clientProviderMock = new Mock<IRedisClientProvider>();

            var repository = new RedisValidationCacheDataRepository(clientProviderMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.AddAsync(key, null);
            });

            Assert.AreEqual(Constants.ValueParamName, exception.ParamName);

            clientProviderMock.Verify(p => p.Create(), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisValidationCacheDataRepository), Description = "RedisValidationCacheDataRepository GetAll valid key and empty list should return empty IEnumerable.")]
        [Timeout(5000)]
        public void RedisValidationCacheDataRepository_GetAllValidKeyAndEmptyList_ShouldReturnEmptyIEnumerable()
        {
            // Arrange
            string key = "some key";

            var list = new List<string>();

            var listMock = new Mock<IRedisList>();
            listMock.Setup(l => l.GetEnumerator())
                .Returns(list.GetEnumerator());

            var clientMock = new Mock<IRedisClient>();
            clientMock
                .Setup(c => c.Lists[It.IsAny<string>()])
                .Returns(listMock.Object);

            var clientProviderMock = new Mock<IRedisClientProvider>();
            clientProviderMock
                .Setup(p => p.Create())
                .Returns(clientMock.Object);

            var repository = new RedisValidationCacheDataRepository(clientProviderMock.Object);

            // Act
            var result = repository.GetAll(key);

            // Act + Asset
            listMock.Verify(l => l.GetEnumerator(), Times.Never);

            Assert.That(result.Count(), Is.EqualTo(0));
            listMock.Verify(l => l.GetEnumerator(), Times.Once);

            var resultValue = result.ToList();
            Assert.IsNotNull(resultValue, "Result List should not be null");
            listMock.Verify(l => l.GetEnumerator(), Times.Exactly(2));

            clientProviderMock.Verify(p => p.Create(), Times.Once);
            clientMock.Verify(c => c.Lists[key], Times.Once);
            clientMock.Verify(c => c.Dispose(), Times.Once);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisValidationCacheDataRepository), Description = "RedisValidationCacheDataRepository GetAll with invalid key should throw ArgumentNullException with correct ParamName.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [Timeout(300)]
        public void RedisValidationCacheDataRepository_GetAllWithInvalidKey_ShouldThrowArgumentNullExceptionWithCorrectParamName(string key)
        {
            // Arrange
            var clientProviderMock = new Mock<IRedisClientProvider>();

            var repository = new RedisValidationCacheDataRepository(clientProviderMock.Object);

            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                repository.GetAll(key);
            });

            Assert.AreEqual(Constants.KeyParamName, exception.ParamName);

            clientProviderMock.Verify(p => p.Create(), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisValidationCacheDataRepository), Description = "RedisValidationCacheDataRepository Get Keys should work.")]
        [Timeout(5000)]
        public void RedisValidationCacheDataRepository_GetKeys_ShouldWork()
        {
            // Arrange
            var listOfKeys = new List<string>();

            var clientMock = new Mock<IRedisClient>();
            clientMock
                .Setup(c => c.GetAllKeys())
                .Returns(listOfKeys);

            var clientProviderMock = new Mock<IRedisClientProvider>();
            clientProviderMock
                .Setup(p => p.Create())
                .Returns(clientMock.Object);

            var repository = new RedisValidationCacheDataRepository(clientProviderMock.Object);

            // Act
            var keys = repository.Keys;

            // Assert
            Assert.AreSame(listOfKeys, keys);

            clientProviderMock.Verify(p => p.Create(), Times.Once);
            clientMock.Verify(c => c.GetAllKeys(), Times.Once);
            clientMock.Verify(c => c.Dispose(), Times.Once);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisValidationCacheDataRepository), Description = "RedisValidationCacheDataRepository Remove valid non-present key should work.")]
        [Timeout(5000)]
        public async Task RedisValidationCacheDataRepository_RemoveValidNonPresentKey_ShouldWork()
        {
            // Arrange
            string key = "some key";

            var clientMock = new Mock<IRedisClient>();
            clientMock
                .Setup(c => c.ContainsKey(It.IsAny<string>()))
                .Returns(false);

            var clientProviderMock = new Mock<IRedisClientProvider>();
            clientProviderMock
                .Setup(p => p.Create())
                .Returns(clientMock.Object);

            var repository = new RedisValidationCacheDataRepository(clientProviderMock.Object);

            // Act
            var result = await repository.RemoveAsync(key).ConfigureAwait(false);

            // Asset
            Assert.That(result, Is.EqualTo(true));

            clientProviderMock.Verify(p => p.Create(), Times.Once);
            clientMock.Verify(c => c.ContainsKey(key), Times.Once);
            clientMock.Verify(c => c.Remove(key), Times.Never);
            clientMock.Verify(c => c.Dispose(), Times.Once);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisValidationCacheDataRepository), Description = "RedisValidationCacheDataRepository Remove valid key and valid value should work.")]
        [Timeout(5000)]
        public async Task RedisValidationCacheDataRepository_RemoveValidNonPresentKeyAndValidValue_ShouldWork()
        {
            // Arrange
            string key = "some key";

            //// JsonStringSerializer does not work with mock? Mock objects are recursive.
            var value = new ValidationCacheEntity();

            var listMock = new Mock<IRedisList>();
            listMock.Setup(l => l.Remove(It.IsAny<string>()))
                .Returns(true);

            var clientMock = new Mock<IRedisClient>();
            clientMock
                .Setup(c => c.Lists[It.IsAny<string>()])
                .Returns(listMock.Object);

            var clientProviderMock = new Mock<IRedisClientProvider>();
            clientProviderMock
                .Setup(p => p.Create())
                .Returns(clientMock.Object);

            var repository = new RedisValidationCacheDataRepository(clientProviderMock.Object);

            // Act
            var result = await repository.RemoveAsync(key, value).ConfigureAwait(false);

            // Asset
            Assert.That(result, Is.EqualTo(true));

            clientProviderMock.Verify(p => p.Create(), Times.Once);
            clientMock.Verify(c => c.Lists[key], Times.Once);
            clientMock.Verify(c => c.Dispose(), Times.Once);
            listMock.Verify(l => l.Remove(It.IsAny<string>()), Times.Once);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisValidationCacheDataRepository), Description = "RedisValidationCacheDataRepository Remove valid yet-present key should work.")]
        [Timeout(5000)]
        public async Task RedisValidationCacheDataRepository_RemoveValidYetPresentKey_ShouldWork()
        {
            // Arrange
            string key = "some key";

            var clientMock = new Mock<IRedisClient>();
            clientMock
                .Setup(c => c.ContainsKey(It.IsAny<string>()))
                .Returns(true);
            clientMock
                .Setup(c => c.Remove(It.IsAny<string>()))
                .Returns(true);

            var clientProviderMock = new Mock<IRedisClientProvider>();
            clientProviderMock
                .Setup(p => p.Create())
                .Returns(clientMock.Object);

            var repository = new RedisValidationCacheDataRepository(clientProviderMock.Object);

            // Act
            var result = await repository.RemoveAsync(key).ConfigureAwait(false);

            // Asset
            Assert.That(result, Is.EqualTo(true));

            clientProviderMock.Verify(p => p.Create(), Times.Once);
            clientMock.Verify(c => c.ContainsKey(key), Times.Once);
            clientMock.Verify(c => c.Remove(key), Times.Once);
            clientMock.Verify(c => c.Dispose(), Times.Once);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisValidationCacheDataRepository), Description = "RedisValidationCacheDataRepository Remove with invalid key should throw ArgumentNullException with correct ParamName.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [Timeout(300)]
        public void RedisValidationCacheDataRepository_RemoveWithInvalidKey_ShouldThrowArgumentNullExceptionWithCorrectParamName(string key)
        {
            // Arrange
            var clientProviderMock = new Mock<IRedisClientProvider>();

            var repository = new RedisValidationCacheDataRepository(clientProviderMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.RemoveAsync(key);
            });

            Assert.AreEqual(Constants.KeyParamName, exception.ParamName);

            clientProviderMock.Verify(p => p.Create(), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisValidationCacheDataRepository), Description = "RedisValidationCacheDataRepository Remove with invalid key and null value should throw ArgumentNullException.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [Timeout(300)]
        public void RedisValidationCacheDataRepository_RemoveWithInvalidKeyAndNullValue_ShouldThrowArgumentNullException(string key)
        {
            // Arrange
            var clientProviderMock = new Mock<IRedisClientProvider>();

            var repository = new RedisValidationCacheDataRepository(clientProviderMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.RemoveAsync(key, null);
            });

            clientProviderMock.Verify(p => p.Create(), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisValidationCacheDataRepository), Description = "RedisValidationCacheDataRepository Remove with invalid key and valid value should throw ArgumentNullException with correct ParamName.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [Timeout(300)]
        public void RedisValidationCacheDataRepository_RemoveWithInvalidKeyAndValidValue_ShouldThrowArgumentNullExceptionWithCorrectParamName(string key)
        {
            // Arrange
            var valueMock = new Mock<IValidationCacheModel>();
            var value = valueMock.Object;

            var clientProviderMock = new Mock<IRedisClientProvider>();

            var repository = new RedisValidationCacheDataRepository(clientProviderMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.RemoveAsync(key, value);
            });

            Assert.AreEqual(Constants.KeyParamName, exception.ParamName);

            clientProviderMock.Verify(p => p.Create(), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisValidationCacheDataRepository), Description = "RedisValidationCacheDataRepository Remove with valid key and null value should throw ArgumentNullException with correct ParamName.")]
        [Timeout(300)]
        public void RedisValidationCacheDataRepository_RemoveWithValidKeyAndNullValue_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            string key = "some key";

            var clientProviderMock = new Mock<IRedisClientProvider>();

            var repository = new RedisValidationCacheDataRepository(clientProviderMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.RemoveAsync(key, null);
            });

            Assert.AreEqual(Constants.ValueParamName, exception.ParamName);

            clientProviderMock.Verify(p => p.Create(), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisValidationCacheDataRepository), Description = "RedisValidationCacheDataRepository SaveChanges should work.")]
        [Timeout(5000)]
        public async Task RedisValidationCacheDataRepository_SaveChanges_ShouldWork()
        {
            // Arrange
            var clientMock = new Mock<IRedisClient>();

            var clientProviderMock = new Mock<IRedisClientProvider>();
            clientProviderMock
                .Setup(p => p.Create())
                .Returns(clientMock.Object);

            var repository = new RedisValidationCacheDataRepository(clientProviderMock.Object);

            // Act
            var result = await repository.SaveChangesAsync().ConfigureAwait(false);

            // Asset
            Assert.That(result, Is.EqualTo(0L));

            clientProviderMock.Verify(p => p.Create(), Times.Once);
            clientMock.Verify(c => c.SaveAsync(), Times.Once);
            clientMock.Verify(c => c.Dispose(), Times.Once);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisValidationCacheDataRepository), Description = "RedisValidationCacheDataRepository with null client provider in constructor should throw ArgumentNullException with correct ParamName.")]
        [Timeout(300)]
        public void RedisValidationCacheDataRepository_WithNullClientProviderInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new RedisValidationCacheDataRepository(null);
            });

            Assert.AreEqual(Constants.ClientProviderFieldName, exception.ParamName);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisValidationCacheDataRepository), Description = "RedisValidationCacheDataRepository with valid client provider in constructor should be initialized correctly.")]
        [Timeout(300)]
        public void RedisValidationCacheDataRepository_WithValidClientProviderInConstructor_ShouldBeInitializedCorrectly()
        {
            // Arrange
            var clientProviderMock = new Mock<IRedisClientProvider>();
            var clientProvider = clientProviderMock.Object;

            // Act + Assert
            var repository = new RedisValidationCacheDataRepository(clientProvider);
            Assert.IsNotNull(repository);

            var providerField = PrivateField.GetInstanceField(
                typeof(RedisValidationCacheDataRepository).BaseType.BaseType,
                repository,
                Constants.ClientProviderFieldName);
            Assert.AreSame(clientProvider, providerField);
        }
    }
}
