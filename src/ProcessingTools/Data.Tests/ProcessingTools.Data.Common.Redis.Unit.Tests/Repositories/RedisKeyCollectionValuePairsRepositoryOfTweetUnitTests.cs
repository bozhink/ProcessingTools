////namespace ProcessingTools.Data.Common.Redis.Tests.Unit.Tests.Repositories
////{
////    using System;
////    using System.Collections.Generic;
////    using System.Linq;
////    using System.Threading.Tasks;
////    using Common;
////    using Models;
////    using Moq;
////    using NUnit.Framework;
////    using ProcessingTools.Data.Common.Redis.Contracts;
////    using ProcessingTools.Data.Common.Redis.Repositories;
////    using ProcessingTools.Tests.Library;
////    using ServiceStack.Redis;

////    [TestFixture(Category = "Unit", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>))]
////    public class RedisKeyCollectionValuePairsRepositoryOfTweetUnitTests
////    {
////        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet Add valid key and valid value should work.")]
////        [Timeout(5000)]
////        public async Task RedisKeyCollectionValuePairsRepositoryOfTweet_AddValidNonPresentKeyAndValidValue_ShouldWork()
////        {
////            // Arrange
////            string key = "some key";

////            //// JsonStringSerializer does not work with mock? var valueMock = new Mock<ITweet>();
////            var value = new Tweet();

////            var listMock = new Mock<IRedisList>();

////            var clientMock = new Mock<IRedisClient>();
////            clientMock
////                .Setup(c => c.Lists[It.IsAny<string>()])
////                .Returns(listMock.Object);

////            var clientProviderMock = new Mock<IRedisClientProvider>();
////            clientProviderMock
////                .Setup(p => p.Create())
////                .Returns(clientMock.Object);

////            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(clientProviderMock.Object);

////            // Act
////            var result = await repository.AddAsync(key, value).ConfigureAwait(false);

////            // Asset
////            Assert.That(result, Is.EqualTo(true));

////            clientProviderMock.Verify(p => p.Create(), Times.Once);
////            clientMock.Verify(c => c.Lists[key], Times.Once);
////            clientMock.Verify(c => c.Dispose(), Times.Once);
////            listMock.Verify(l => l.Add(It.IsAny<string>()), Times.Once);
////        }

////        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet Add with invalid key and null value should throw ArgumentNullException.")]
////        [TestCase(null)]
////        [TestCase("")]
////        [TestCase("         ")]
////        [Timeout(300)]
////        public void RedisKeyCollectionValuePairsRepositoryOfTweet_AddWithInvalidKeyAndNullValue_ShouldThrowArgumentNullException(string key)
////        {
////            // Arrange
////            var clientProviderMock = new Mock<IRedisClientProvider>();

////            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(clientProviderMock.Object);

////            // Act + Assert
////            Assert.ThrowsAsync<ArgumentNullException>(() =>
////            {
////                return repository.AddAsync(key, null);
////            });

////            clientProviderMock.Verify(p => p.Create(), Times.Never);
////        }

////        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet Add with invalid key and valid value should throw ArgumentNullException with correct ParamName.")]
////        [TestCase(null)]
////        [TestCase("")]
////        [TestCase("         ")]
////        [Timeout(300)]
////        public void RedisKeyCollectionValuePairsRepositoryOfTweet_AddWithInvalidKeyAndValidValue_ShouldThrowArgumentNullExceptionWithCorrectParamName(string key)
////        {
////            // Arrange
////            var valueMock = new Mock<ITweet>();
////            var value = valueMock.Object;

////            var clientProviderMock = new Mock<IRedisClientProvider>();

////            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(clientProviderMock.Object);

////            // Act + Assert
////            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
////            {
////                return repository.AddAsync(key, value);
////            });

////            Assert.AreEqual(Constants.KeyParamName, exception.ParamName);

////            clientProviderMock.Verify(p => p.Create(), Times.Never);
////        }

////        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet Add with valid key and null value should throw ArgumentNullException with correct ParamName.")]
////        [Timeout(300)]
////        public void RedisKeyCollectionValuePairsRepositoryOfTweet_AddWithValidKeyAndNullValue_ShouldThrowArgumentNullExceptionWithCorrectParamName()
////        {
////            // Arrange
////            string key = "some key";

////            var clientProviderMock = new Mock<IRedisClientProvider>();

////            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(clientProviderMock.Object);

////            // Act + Assert
////            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
////            {
////                return repository.AddAsync(key, null);
////            });

////            Assert.AreEqual(Constants.ValueParamName, exception.ParamName);

////            clientProviderMock.Verify(p => p.Create(), Times.Never);
////        }

////        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet GetAll valid key and empty list should return empty IEnumerable.")]
////        [Timeout(5000)]
////        public void RedisKeyCollectionValuePairsRepositoryOfTweet_GetAllValidKeyAndEmptyList_ShouldReturnEmptyIEnumerable()
////        {
////            // Arrange
////#pragma warning disable S4158 // Empty collections should not be accessed or iterated
////            string key = "some key";

////            var list = new List<string>();

////            var listMock = new Mock<IRedisList>();
////            listMock.Setup(l => l.GetEnumerator())
////                .Returns(list.GetEnumerator());

////            var clientMock = new Mock<IRedisClient>();
////            clientMock
////                .Setup(c => c.Lists[It.IsAny<string>()])
////                .Returns(listMock.Object);

////            var clientProviderMock = new Mock<IRedisClientProvider>();
////            clientProviderMock
////                .Setup(p => p.Create())
////                .Returns(clientMock.Object);

////            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(clientProviderMock.Object);
////#pragma warning restore S4158 // Empty collections should not be accessed or iterated

////            // Act
////            var result = repository.GetAll(key);

////            // Act + Asset
////            listMock.Verify(l => l.GetEnumerator(), Times.Never);

////            Assert.That(result.Count(), Is.EqualTo(0));
////            listMock.Verify(l => l.GetEnumerator(), Times.Once);

////            var resultList = result.ToList();
////            Assert.IsNotNull(resultList, "List should not be null");
////            listMock.Verify(l => l.GetEnumerator(), Times.Exactly(2));

////            clientProviderMock.Verify(p => p.Create(), Times.Once);
////            clientMock.Verify(c => c.Lists[key], Times.Once);
////            clientMock.Verify(c => c.Dispose(), Times.Once);
////        }

////        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet GetAll with invalid key should throw ArgumentNullException with correct ParamName.")]
////        [TestCase(null)]
////        [TestCase("")]
////        [TestCase("         ")]
////        [Timeout(300)]
////        public void RedisKeyCollectionValuePairsRepositoryOfTweet_GetAllWithInvalidKey_ShouldThrowArgumentNullExceptionWithCorrectParamName(string key)
////        {
////            // Arrange
////            var clientProviderMock = new Mock<IRedisClientProvider>();

////            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(clientProviderMock.Object);

////            // Act + Assert
////            var exception = Assert.Throws<ArgumentNullException>(() =>
////            {
////                repository.GetAll(key);
////            });

////            Assert.AreEqual(Constants.KeyParamName, exception.ParamName);

////            clientProviderMock.Verify(p => p.Create(), Times.Never);
////        }

////        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet Get Keys should work.")]
////        [Timeout(5000)]
////        public void RedisKeyCollectionValuePairsRepositoryOfTweet_GetKeys_ShouldWork()
////        {
////            // Arrange
////            var listOfKeys = new List<string>();

////            var clientMock = new Mock<IRedisClient>();
////            clientMock
////                .Setup(c => c.GetAllKeys())
////                .Returns(listOfKeys);

////            var clientProviderMock = new Mock<IRedisClientProvider>();
////            clientProviderMock
////                .Setup(p => p.Create())
////                .Returns(clientMock.Object);

////            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(clientProviderMock.Object);

////            // Act
////            var keys = repository.Keys;

////            // Assert
////            Assert.AreSame(listOfKeys, keys);

////            clientProviderMock.Verify(p => p.Create(), Times.Once);
////            clientMock.Verify(c => c.GetAllKeys(), Times.Once);
////            clientMock.Verify(c => c.Dispose(), Times.Once);
////        }

////        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet Remove valid non-present key should work.")]
////        [Timeout(5000)]
////        public async Task RedisKeyCollectionValuePairsRepositoryOfTweet_RemoveValidNonPresentKey_ShouldWork()
////        {
////            // Arrange
////            string key = "some key";

////            var clientMock = new Mock<IRedisClient>();
////            clientMock
////                .Setup(c => c.ContainsKey(It.IsAny<string>()))
////                .Returns(false);

////            var clientProviderMock = new Mock<IRedisClientProvider>();
////            clientProviderMock
////                .Setup(p => p.Create())
////                .Returns(clientMock.Object);

////            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(clientProviderMock.Object);

////            // Act
////            var result = await repository.RemoveAsync(key).ConfigureAwait(false);

////            // Asset
////            Assert.That(result, Is.EqualTo(true));

////            clientProviderMock.Verify(p => p.Create(), Times.Once);
////            clientMock.Verify(c => c.ContainsKey(key), Times.Once);
////            clientMock.Verify(c => c.Remove(key), Times.Never);
////            clientMock.Verify(c => c.Dispose(), Times.Once);
////        }

////        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet Remove valid key and valid value should work.")]
////        [Timeout(5000)]
////        public async Task RedisKeyCollectionValuePairsRepositoryOfTweet_RemoveValidKeyAndValidValue_ShouldWork()
////        {
////            // Arrange
////            string key = "some key";

////            //// JsonStringSerializer does not work with mock? var valueMock = new Mock<ITweet>();
////            var value = new Tweet();

////            var listMock = new Mock<IRedisList>();
////            listMock.Setup(l => l.Remove(It.IsAny<string>()))
////                .Returns(true);

////            var clientMock = new Mock<IRedisClient>();
////            clientMock
////                .Setup(c => c.Lists[It.IsAny<string>()])
////                .Returns(listMock.Object);

////            var clientProviderMock = new Mock<IRedisClientProvider>();
////            clientProviderMock
////                .Setup(p => p.Create())
////                .Returns(clientMock.Object);

////            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(clientProviderMock.Object);

////            // Act
////            var result = await repository.RemoveAsync(key, value).ConfigureAwait(false);

////            // Asset
////            Assert.That(result, Is.EqualTo(true));

////            clientProviderMock.Verify(p => p.Create(), Times.Once);
////            clientMock.Verify(c => c.Lists[key], Times.Once);
////            clientMock.Verify(c => c.Dispose(), Times.Once);
////            listMock.Verify(l => l.Remove(It.IsAny<string>()), Times.Once);
////        }

////        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet Remove valid yet-present key should work.")]
////        [Timeout(5000)]
////        public async Task RedisKeyCollectionValuePairsRepositoryOfTweet_RemoveValidYetPresentKey_ShouldWork()
////        {
////            // Arrange
////            string key = "some key";

////            var clientMock = new Mock<IRedisClient>();
////            clientMock
////                .Setup(c => c.ContainsKey(It.IsAny<string>()))
////                .Returns(true);
////            clientMock
////                .Setup(c => c.Remove(It.IsAny<string>()))
////                .Returns(true);

////            var clientProviderMock = new Mock<IRedisClientProvider>();
////            clientProviderMock
////                .Setup(p => p.Create())
////                .Returns(clientMock.Object);

////            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(clientProviderMock.Object);

////            // Act
////            var result = await repository.RemoveAsync(key).ConfigureAwait(false);

////            // Asset
////            Assert.That(result, Is.EqualTo(true));

////            clientProviderMock.Verify(p => p.Create(), Times.Once);
////            clientMock.Verify(c => c.ContainsKey(key), Times.Once);
////            clientMock.Verify(c => c.Remove(key), Times.Once);
////            clientMock.Verify(c => c.Dispose(), Times.Once);
////        }

////        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet Remove with invalid key should throw ArgumentNullException with correct ParamName.")]
////        [TestCase(null)]
////        [TestCase("")]
////        [TestCase("         ")]
////        [Timeout(300)]
////        public void RedisKeyCollectionValuePairsRepositoryOfTweet_RemoveWithInvalidKey_ShouldThrowArgumentNullExceptionWithCorrectParamName(string key)
////        {
////            // Arrange
////            var clientProviderMock = new Mock<IRedisClientProvider>();

////            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(clientProviderMock.Object);

////            // Act + Assert
////            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
////            {
////                return repository.RemoveAsync(key);
////            });

////            Assert.AreEqual(Constants.KeyParamName, exception.ParamName);

////            clientProviderMock.Verify(p => p.Create(), Times.Never);
////        }

////        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet Remove with invalid key and null value should throw ArgumentNullException.")]
////        [TestCase(null)]
////        [TestCase("")]
////        [TestCase("         ")]
////        [Timeout(300)]
////        public void RedisKeyCollectionValuePairsRepositoryOfTweet_RemoveWithInvalidKeyAndNullValue_ShouldThrowArgumentNullException(string key)
////        {
////            // Arrange
////            var clientProviderMock = new Mock<IRedisClientProvider>();

////            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(clientProviderMock.Object);

////            // Act + Assert
////            Assert.ThrowsAsync<ArgumentNullException>(() =>
////            {
////                return repository.RemoveAsync(key, null);
////            });

////            clientProviderMock.Verify(p => p.Create(), Times.Never);
////        }

////        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet Remove with invalid key and valid value should throw ArgumentNullException with correct ParamName.")]
////        [TestCase(null)]
////        [TestCase("")]
////        [TestCase("         ")]
////        [Timeout(300)]
////        public void RedisKeyCollectionValuePairsRepositoryOfTweet_RemoveWithInvalidKeyAndValidValue_ShouldThrowArgumentNullExceptionWithCorrectParamName(string key)
////        {
////            // Arrange
////            var valueMock = new Mock<ITweet>();
////            var value = valueMock.Object;

////            var clientProviderMock = new Mock<IRedisClientProvider>();

////            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(clientProviderMock.Object);

////            // Act + Assert
////            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
////            {
////                return repository.RemoveAsync(key, value);
////            });

////            Assert.AreEqual(Constants.KeyParamName, exception.ParamName);

////            clientProviderMock.Verify(p => p.Create(), Times.Never);
////        }

////        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet Remove with valid key and null value should throw ArgumentNullException with correct ParamName.")]
////        [Timeout(300)]
////        public void RedisKeyCollectionValuePairsRepositoryOfTweet_RemoveWithValidKeyAndNullValue_ShouldThrowArgumentNullExceptionWithCorrectParamName()
////        {
////            // Arrange
////            string key = "some key";

////            var clientProviderMock = new Mock<IRedisClientProvider>();

////            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(clientProviderMock.Object);

////            // Act + Assert
////            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
////            {
////                return repository.RemoveAsync(key, null);
////            });

////            Assert.AreEqual(Constants.ValueParamName, exception.ParamName);

////            clientProviderMock.Verify(p => p.Create(), Times.Never);
////        }

////        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet SaveChanges should work.")]
////        [Timeout(5000)]
////        public async Task RedisKeyCollectionValuePairsRepositoryOfTweet_SaveChanges_ShouldWork()
////        {
////            // Arrange
////            var clientMock = new Mock<IRedisClient>();

////            var clientProviderMock = new Mock<IRedisClientProvider>();
////            clientProviderMock
////                .Setup(p => p.Create())
////                .Returns(clientMock.Object);

////            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(clientProviderMock.Object);

////            // Act
////            var result = await repository.SaveChangesAsync().ConfigureAwait(false);

////            // Asset
////            Assert.That(result, Is.EqualTo(0L));

////            clientProviderMock.Verify(p => p.Create(), Times.Once);
////            clientMock.Verify(c => c.SaveAsync(), Times.Once);
////            clientMock.Verify(c => c.Dispose(), Times.Once);
////        }

////        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet with null client provider in constructor should throw ArgumentNullException with correct ParamName.")]
////        [Timeout(300)]
////        public void RedisKeyCollectionValuePairsRepositoryOfTweet_WithNullClientProviderInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
////        {
////            // Act + Assert
////            var exception = Assert.Throws<ArgumentNullException>(() =>
////            {
////                new RedisKeyCollectionValuePairsRepository<ITweet>(null);
////            });

////            Assert.AreEqual(Constants.ClientProviderFieldName, exception.ParamName);
////        }

////        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet with valid client provider in constructor should be initialized correctly.")]
////        [Timeout(300)]
////        public void RedisKeyCollectionValuePairsRepositoryOfTweet_WithValidClientProviderInConstructor_ShouldBeInitializedCorrectly()
////        {
////            // Arrange
////            var clientProviderMock = new Mock<IRedisClientProvider>();
////            var clientProvider = clientProviderMock.Object;

////            // Act + Assert
////            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(clientProvider);
////            Assert.IsNotNull(repository);

////            var providerField = PrivateField.GetInstanceField(
////                typeof(RedisKeyCollectionValuePairsRepository<ITweet>).BaseType,
////                repository,
////                Constants.ClientProviderFieldName);
////            Assert.AreSame(clientProvider, providerField);
////        }
////    }
////}
