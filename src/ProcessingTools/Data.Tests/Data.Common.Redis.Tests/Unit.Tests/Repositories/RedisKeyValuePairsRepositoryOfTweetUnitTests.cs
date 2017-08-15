namespace ProcessingTools.Data.Common.Redis.Tests.Unit.Tests.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Data.Common.Redis.Contracts;
    using ProcessingTools.Data.Common.Redis.Repositories;
    using ProcessingTools.Data.Common.Redis.Tests.Common;
    using ProcessingTools.Data.Common.Redis.Tests.Models;
    using ProcessingTools.Tests.Library;
    using ServiceStack.Redis;

    [TestFixture(Category = "Unit", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>))]
    public class RedisKeyValuePairsRepositoryOfTweetUnitTests
    {
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Add valid non-present key and valid value should work.")]
        [Timeout(1000)]
        public async Task RedisKeyValuePairsRepositoryOfTweet_AddValidNonPresentKeyAndValidValue_ShouldWork()
        {
            // Arrange
            string key = "some key";

            var valueMock = new Mock<ITweet>();
            var value = valueMock.Object;

            var clientMock = new Mock<IRedisClient>();
            clientMock
                .Setup(c => c.ContainsKey(It.IsAny<string>()))
                .Returns(false);
            clientMock
                .Setup(c => c.Add(It.IsAny<string>(), It.IsAny<ITweet>()))
                .Returns(true);

            var clientProviderMock = new Mock<IRedisClientProvider>();
            clientProviderMock
                .Setup(p => p.Create())
                .Returns(clientMock.Object);

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientProviderMock.Object);

            // Act
            var result = await repository.Add(key, value);

            // Asset
            Assert.That(result, Is.EqualTo(true));

            clientProviderMock.Verify(p => p.Create(), Times.Once);
            clientMock.Verify(c => c.ContainsKey(key), Times.Once);
            clientMock.Verify(c => c.Add(key, value), Times.Once);
            clientMock.Verify(c => c.Dispose(), Times.Once);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Add valid yet-present key and valid value should throw KeyExistsException.")]
        [Timeout(1000)]
        public void RedisKeyValuePairsRepositoryOfTweet_AddValidYetPresentKeyAndValidValue_ShouldThowKeyExistsException()
        {
            // Arrange
            string key = "some key";

            var valueMock = new Mock<ITweet>();
            var value = valueMock.Object;

            var clientMock = new Mock<IRedisClient>();
            clientMock
                .Setup(c => c.ContainsKey(It.IsAny<string>()))
                .Returns(true);

            var clientProviderMock = new Mock<IRedisClientProvider>();
            clientProviderMock
                .Setup(p => p.Create())
                .Returns(clientMock.Object);

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientProviderMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<KeyExistsException>(() =>
            {
                return repository.Add(key, value);
            });

            clientProviderMock.Verify(p => p.Create(), Times.Once);
            clientMock.Verify(c => c.ContainsKey(key), Times.Once);
            clientMock.Verify(c => c.Add(key, value), Times.Never);
            clientMock.Verify(c => c.Dispose(), Times.Once);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Add with invalid key and null value should throw ArgumentNullException.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [Timeout(300)]
        public void RedisKeyValuePairsRepositoryOfTweet_AddWithInvalidKeyAndNullValue_ShouldThrowArgumentNullException(string key)
        {
            // Arrange
            var clientProviderMock = new Mock<IRedisClientProvider>();

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientProviderMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.Add(key, null);
            });

            clientProviderMock.Verify(p => p.Create(), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Add with invalid key and valid value should throw ArgumentNullException with correct ParamName.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [Timeout(300)]
        public void RedisKeyValuePairsRepositoryOfTweet_AddWithInvalidKeyAndValidValue_ShouldThrowArgumentNullExceptionWithCorrectParamName(string key)
        {
            // Arrange
            var valueMock = new Mock<ITweet>();
            var value = valueMock.Object;

            var clientProviderMock = new Mock<IRedisClientProvider>();

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientProviderMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.Add(key, value);
            });

            Assert.AreEqual(Constants.KeyParamName, exception.ParamName);

            clientProviderMock.Verify(p => p.Create(), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Add with valid key and null value should throw ArgumentNullException with correct ParamName.")]
        [Timeout(300)]
        public void RedisKeyValuePairsRepositoryOfTweet_AddWithValidKeyAndNullValue_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            string key = "some key";

            var clientProviderMock = new Mock<IRedisClientProvider>();

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientProviderMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.Add(key, null);
            });

            Assert.AreEqual(Constants.ValueParamName, exception.ParamName);

            clientProviderMock.Verify(p => p.Create(), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Get Keys should work.")]
        [Timeout(1000)]
        public void RedisKeyValuePairsRepositoryOfTweet_GetKeys_ShouldWork()
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

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientProviderMock.Object);

            // Act
            var keys = repository.Keys;

            // Assert
            Assert.AreSame(listOfKeys, keys);

            clientProviderMock.Verify(p => p.Create(), Times.Once);
            clientMock.Verify(c => c.GetAllKeys(), Times.Once);
            clientMock.Verify(c => c.Dispose(), Times.Once);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Get valid non-present key should throw KeyExistsException.")]
        [Timeout(1000)]
        public void RedisKeyValuePairsRepositoryOfTweet_GetValidNonPresentKey_ShouldThowKeyExistsException()
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

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientProviderMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ProcessingTools.Common.Exceptions.KeyNotFoundException>(() =>
            {
                return repository.Get(key);
            });

            clientProviderMock.Verify(p => p.Create(), Times.Once);
            clientMock.Verify(c => c.ContainsKey(key), Times.Once);
            clientMock.Verify(c => c.Get<ITweet>(key), Times.Never);
            clientMock.Verify(c => c.Dispose(), Times.Once);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Get valid yet-present key should work.")]
        [Timeout(1000)]
        public async Task RedisKeyValuePairsRepositoryOfTweet_GetValidYetPresentKey_ShouldWork()
        {
            // Arrange
            string key = "some key";

            var valueMock = new Mock<ITweet>();
            var value = valueMock.Object;

            var clientMock = new Mock<IRedisClient>();
            clientMock
                .Setup(c => c.ContainsKey(It.IsAny<string>()))
                .Returns(true);
            clientMock
                .Setup(c => c.Get<ITweet>(It.IsAny<string>()))
                .Returns(value);

            var clientProviderMock = new Mock<IRedisClientProvider>();
            clientProviderMock
                .Setup(p => p.Create())
                .Returns(clientMock.Object);

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientProviderMock.Object);

            // Act
            var result = await repository.Get(key);

            // Asset
            Assert.AreSame(value, result);

            clientProviderMock.Verify(p => p.Create(), Times.Once);
            clientMock.Verify(c => c.ContainsKey(key), Times.Once);
            clientMock.Verify(c => c.Get<ITweet>(key), Times.Once);
            clientMock.Verify(c => c.Dispose(), Times.Once);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Get with invalid key should throw ArgumentNullException with correct ParamName.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [Timeout(300)]
        public void RedisKeyValuePairsRepositoryOfTweet_GetWithInvalidKey_ShouldThrowArgumentNullExceptionWithCorrectParamName(string key)
        {
            // Arrange
            var clientProviderMock = new Mock<IRedisClientProvider>();

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientProviderMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.Get(key);
            });

            Assert.AreEqual(Constants.KeyParamName, exception.ParamName);

            clientProviderMock.Verify(p => p.Create(), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Remove valid non-present key should work.")]
        [Timeout(1000)]
        public async Task RedisKeyValuePairsRepositoryOfTweet_RemoveValidNonPresentKey_ShouldWork()
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

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientProviderMock.Object);

            // Act
            var result = await repository.Remove(key);

            // Asset
            Assert.That(result, Is.EqualTo(true));

            clientProviderMock.Verify(p => p.Create(), Times.Once);
            clientMock.Verify(c => c.ContainsKey(key), Times.Once);
            clientMock.Verify(c => c.Remove(key), Times.Never);
            clientMock.Verify(c => c.Dispose(), Times.Once);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Remove valid yet-present key should work.")]
        [Timeout(1000)]
        public async Task RedisKeyValuePairsRepositoryOfTweet_RemoveValidYetPresentKey_ShouldWork()
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

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientProviderMock.Object);

            // Act
            var result = await repository.Remove(key);

            // Asset
            Assert.That(result, Is.EqualTo(true));

            clientProviderMock.Verify(p => p.Create(), Times.Once);
            clientMock.Verify(c => c.ContainsKey(key), Times.Once);
            clientMock.Verify(c => c.Remove(key), Times.Once);
            clientMock.Verify(c => c.Dispose(), Times.Once);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Remove with invalid key should throw ArgumentNullException with correct ParamName.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [Timeout(300)]
        public void RedisKeyValuePairsRepositoryOfTweet_RemoveWithInvalidKey_ShouldThrowArgumentNullExceptionWithCorrectParamName(string key)
        {
            // Arrange
            var clientProviderMock = new Mock<IRedisClientProvider>();

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientProviderMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.Remove(key);
            });

            Assert.AreEqual(Constants.KeyParamName, exception.ParamName);

            clientProviderMock.Verify(p => p.Create(), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet SaveChanges should work.")]
        [Timeout(1000)]
        public async Task RedisKeyValuePairsRepositoryOfTweet_SaveChanges_ShouldWork()
        {
            // Arrange
            var clientMock = new Mock<IRedisClient>();

            var clientProviderMock = new Mock<IRedisClientProvider>();
            clientProviderMock
                .Setup(p => p.Create())
                .Returns(clientMock.Object);

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientProviderMock.Object);

            // Act
            var result = await repository.SaveChangesAsync();

            // Asset
            Assert.That(result, Is.EqualTo(0L));

            clientProviderMock.Verify(p => p.Create(), Times.Once);
            clientMock.Verify(c => c.SaveAsync(), Times.Once);
            clientMock.Verify(c => c.Dispose(), Times.Once);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Update valid non-present key and valid value should throw KeyExistsException.")]
        [Timeout(1000)]
        public void RedisKeyValuePairsRepositoryOfTweet_UpdateValidNonPresentKeyAndValidValue_ShouldThowKeyExistsException()
        {
            // Arrange
            string key = "some key";

            var valueMock = new Mock<ITweet>();
            var value = valueMock.Object;

            var clientMock = new Mock<IRedisClient>();
            clientMock
                .Setup(c => c.ContainsKey(It.IsAny<string>()))
                .Returns(false);

            var clientProviderMock = new Mock<IRedisClientProvider>();
            clientProviderMock
                .Setup(p => p.Create())
                .Returns(clientMock.Object);

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientProviderMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ProcessingTools.Common.Exceptions.KeyNotFoundException>(() =>
            {
                return repository.Update(key, value);
            });

            clientProviderMock.Verify(p => p.Create(), Times.Once);
            clientMock.Verify(c => c.ContainsKey(key), Times.Once);
            clientMock.Verify(c => c.Replace(key, value), Times.Never);
            clientMock.Verify(c => c.Dispose(), Times.Once);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Update valid yet-present key and valid value should work.")]
        [Timeout(1000)]
        public async Task RedisKeyValuePairsRepositoryOfTweet_UpdateValidYetPresentKeyAndValidValue_ShouldWork()
        {
            // Arrange
            string key = "some key";

            var valueMock = new Mock<ITweet>();
            var value = valueMock.Object;

            var clientMock = new Mock<IRedisClient>();
            clientMock
                .Setup(c => c.ContainsKey(It.IsAny<string>()))
                .Returns(true);
            clientMock
                .Setup(c => c.Replace(It.IsAny<string>(), It.IsAny<ITweet>()))
                .Returns(true);

            var clientProviderMock = new Mock<IRedisClientProvider>();
            clientProviderMock
                .Setup(p => p.Create())
                .Returns(clientMock.Object);

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientProviderMock.Object);

            // Act
            var result = await repository.Update(key, value);

            // Asset
            Assert.That(result, Is.EqualTo(true));

            clientProviderMock.Verify(p => p.Create(), Times.Once);
            clientMock.Verify(c => c.ContainsKey(key), Times.Once);
            clientMock.Verify(c => c.Replace(key, value), Times.Once);
            clientMock.Verify(c => c.Dispose(), Times.Once);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Update with invalid key and null value should throw ArgumentNullException.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [Timeout(300)]
        public void RedisKeyValuePairsRepositoryOfTweet_UpdateWithInvalidKeyAndNullValue_ShouldThrowArgumentNullException(string key)
        {
            // Arrange
            var clientProviderMock = new Mock<IRedisClientProvider>();

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientProviderMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.Update(key, null);
            });

            clientProviderMock.Verify(p => p.Create(), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Update with invalid key and valid value should throw ArgumentNullException with correct ParamName.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [Timeout(300)]
        public void RedisKeyValuePairsRepositoryOfTweet_UpdateWithInvalidKeyAndValidValue_ShouldThrowArgumentNullExceptionWithCorrectParamName(string key)
        {
            // Arrange
            var valueMock = new Mock<ITweet>();
            var value = valueMock.Object;

            var clientProviderMock = new Mock<IRedisClientProvider>();

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientProviderMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.Update(key, value);
            });

            Assert.AreEqual(Constants.KeyParamName, exception.ParamName);

            clientProviderMock.Verify(p => p.Create(), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Update with valid key and null value should throw ArgumentNullException with correct ParamName.")]
        [Timeout(300)]
        public void RedisKeyValuePairsRepositoryOfTweet_UpdateWithValidKeyAndNullValue_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            string key = "some key";

            var clientProviderMock = new Mock<IRedisClientProvider>();

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientProviderMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.Update(key, null);
            });

            Assert.AreEqual(Constants.ValueParamName, exception.ParamName);

            clientProviderMock.Verify(p => p.Create(), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Upsert valid non-present key and valid value should work.")]
        [Timeout(1000)]
        public async Task RedisKeyValuePairsRepositoryOfTweet_UpsertValidNonPresentKeyAndValidValue_ShouldWork()
        {
            // Arrange
            string key = "some key";

            var valueMock = new Mock<ITweet>();
            var value = valueMock.Object;

            var clientMock = new Mock<IRedisClient>();
            clientMock
                .Setup(c => c.ContainsKey(It.IsAny<string>()))
                .Returns(false);
            clientMock
                .Setup(c => c.Set(It.IsAny<string>(), It.IsAny<ITweet>()))
                .Returns(true);

            var clientProviderMock = new Mock<IRedisClientProvider>();
            clientProviderMock
                .Setup(p => p.Create())
                .Returns(clientMock.Object);

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientProviderMock.Object);

            // Act
            var result = await repository.Upsert(key, value);

            // Asset
            Assert.That(result, Is.EqualTo(true));

            clientProviderMock.Verify(p => p.Create(), Times.Once);
            clientMock.Verify(c => c.ContainsKey(key), Times.Never);
            clientMock.Verify(c => c.Set(key, value), Times.Once);
            clientMock.Verify(c => c.Dispose(), Times.Once);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Upsert valid yet-present key and valid value should work.")]
        [Timeout(1000)]
        public async Task RedisKeyValuePairsRepositoryOfTweet_UpsertValidYetPresentKeyAndValidValue_ShouldWork()
        {
            // Arrange
            string key = "some key";

            var valueMock = new Mock<ITweet>();
            var value = valueMock.Object;

            var clientMock = new Mock<IRedisClient>();
            clientMock
                .Setup(c => c.ContainsKey(It.IsAny<string>()))
                .Returns(true);
            clientMock
                .Setup(c => c.Set(It.IsAny<string>(), It.IsAny<ITweet>()))
                .Returns(true);

            var clientProviderMock = new Mock<IRedisClientProvider>();
            clientProviderMock
                .Setup(p => p.Create())
                .Returns(clientMock.Object);

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientProviderMock.Object);

            // Act
            var result = await repository.Upsert(key, value);

            // Asset
            Assert.That(result, Is.EqualTo(true));

            clientProviderMock.Verify(p => p.Create(), Times.Once);
            clientMock.Verify(c => c.ContainsKey(key), Times.Never);
            clientMock.Verify(c => c.Set(key, value), Times.Once);
            clientMock.Verify(c => c.Dispose(), Times.Once);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Upsert with invalid key and null value should throw ArgumentNullException.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [Timeout(300)]
        public void RedisKeyValuePairsRepositoryOfTweet_UpsertWithInvalidKeyAndNullValue_ShouldThrowArgumentNullException(string key)
        {
            // Arrange
            var clientProviderMock = new Mock<IRedisClientProvider>();

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientProviderMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.Upsert(key, null);
            });

            clientProviderMock.Verify(p => p.Create(), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Upsert with invalid key and valid value should throw ArgumentNullException with correct ParamName.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [Timeout(300)]
        public void RedisKeyValuePairsRepositoryOfTweet_UpsertWithInvalidKeyAndValidValue_ShouldThrowArgumentNullExceptionWithCorrectParamName(string key)
        {
            // Arrange
            var valueMock = new Mock<ITweet>();
            var value = valueMock.Object;

            var clientProviderMock = new Mock<IRedisClientProvider>();

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientProviderMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.Upsert(key, value);
            });

            Assert.AreEqual(Constants.KeyParamName, exception.ParamName);

            clientProviderMock.Verify(p => p.Create(), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Upsert with valid key and null value should throw ArgumentNullException with correct ParamName.")]
        [Timeout(300)]
        public void RedisKeyValuePairsRepositoryOfTweet_UpsertWithValidKeyAndNullValue_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            string key = "some key";

            var clientProviderMock = new Mock<IRedisClientProvider>();

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientProviderMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.Upsert(key, null);
            });

            Assert.AreEqual(Constants.ValueParamName, exception.ParamName);

            clientProviderMock.Verify(p => p.Create(), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet with null client provider in constructor should throw ArgumentNullException with correct ParamName.")]
        [Timeout(300)]
        public void RedisKeyValuePairsRepositoryOfTweet_WithNullClientProviderInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new RedisKeyValuePairsRepository<ITweet>(null);
            });

            Assert.AreEqual(Constants.ClientProviderFieldName, exception.ParamName);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet with valid client provider in constructor should be initialized correctly.")]
        [Timeout(300)]
        public void RedisKeyValuePairsRepositoryOfTweet_WithValidClientProviderInConstructor_ShouldBeInitializedCorrectly()
        {
            // Arrange
            var clientProviderMock = new Mock<IRedisClientProvider>();
            var clientProvider = clientProviderMock.Object;

            // Act + Assert
            var repository = new RedisKeyValuePairsRepository<ITweet>(clientProvider);
            Assert.IsNotNull(repository);

            var providerField = PrivateField.GetInstanceField(
                typeof(RedisKeyValuePairsRepository<ITweet>).BaseType,
                repository,
                Constants.ClientProviderFieldName);
            Assert.AreSame(clientProvider, providerField);
        }
    }
}
