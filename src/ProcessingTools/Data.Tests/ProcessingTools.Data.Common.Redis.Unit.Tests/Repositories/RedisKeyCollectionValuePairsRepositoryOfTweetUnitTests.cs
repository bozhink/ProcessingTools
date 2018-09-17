// <copyright file="RedisKeyCollectionValuePairsRepositoryOfTweetUnitTests.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Common.Redis.Unit.Tests.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Common.Code.Tests;
    using ProcessingTools.Data.Common.Redis.Repositories;
    using ProcessingTools.Data.Common.Redis.Unit.Tests.Models;
    using ServiceStack.Redis;

    /// <summary>
    /// <see cref="RedisKeyCollectionValuePairsRepository{ITweet}"/> unit tests.
    /// </summary>
    [TestFixture(Category = "Unit", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>))]
    public class RedisKeyCollectionValuePairsRepositoryOfTweetUnitTests
    {
        /// <summary>
        /// RedisKeyCollectionValuePairsRepositoryOfTweet Add valid key and valid value should work.
        /// </summary>
        /// <returns>Task</returns>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet Add valid key and valid value should work.")]
        [MaxTime(5000)]
        public async Task RedisKeyCollectionValuePairsRepositoryOfTweet_AddValidNonPresentKeyAndValidValue_ShouldWork()
        {
            // Arrange
            string key = "some key";

            //// JsonStringSerializer does not work with mock? var valueMock = new Mock<ITweet>();
            var value = new Tweet();

            var listMock = new Mock<IRedisList>();
            var clientMock = new Mock<IRedisClient>();
            clientMock.Setup(c => c.Lists[It.IsAny<string>()]).Returns(listMock.Object);

            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(clientMock.Object);

            // Act
            var result = await repository.AddAsync(key, value).ConfigureAwait(false);

            // Asset
            Assert.That(result, Is.EqualTo(true));

            listMock.Verify(l => l.Add(It.IsAny<string>()), Times.Once);
            listMock.Verify(l => l.Remove(It.IsAny<string>()), Times.Never);
            clientMock.Verify(c => c.GetAllKeys(), Times.Never);
            clientMock.Verify(c => c.Lists[key], Times.Once);
            clientMock.Verify(c => c.Remove(It.IsAny<string>()), Times.Never);
            clientMock.Verify(c => c.Dispose(), Times.Never);
        }

        /// <summary>
        /// RedisKeyCollectionValuePairsRepositoryOfTweet Add with invalid key and null value should throw ArgumentNullException.
        /// </summary>
        /// <param name="key">Key parameter value.</param>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet Add with invalid key and null value should throw ArgumentNullException.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [MaxTime(3000)]
        public void RedisKeyCollectionValuePairsRepositoryOfTweet_AddWithInvalidKeyAndNullValue_ShouldThrowArgumentNullException(string key)
        {
            // Arrange
            var listMock = new Mock<IRedisList>();
            var clientMock = new Mock<IRedisClient>();
            clientMock.Setup(c => c.Lists[It.IsAny<string>()]).Returns(listMock.Object);

            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(clientMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.AddAsync(key, null);
            });

            listMock.Verify(l => l.Add(It.IsAny<string>()), Times.Never);
            listMock.Verify(l => l.Remove(It.IsAny<string>()), Times.Never);
            clientMock.Verify(c => c.GetAllKeys(), Times.Never);
            clientMock.Verify(c => c.Lists[key], Times.Never);
            clientMock.Verify(c => c.Remove(It.IsAny<string>()), Times.Never);
            clientMock.Verify(c => c.Dispose(), Times.Never);
        }

        /// <summary>
        /// RedisKeyCollectionValuePairsRepositoryOfTweet Add with invalid key and valid value should throw ArgumentNullException with correct ParamName.
        /// </summary>
        /// <param name="key">Key parameter value.</param>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet Add with invalid key and valid value should throw ArgumentNullException with correct ParamName.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [MaxTime(3000)]
        public void RedisKeyCollectionValuePairsRepositoryOfTweet_AddWithInvalidKeyAndValidValue_ShouldThrowArgumentNullExceptionWithCorrectParamName(string key)
        {
            // Arrange
            //// JsonStringSerializer does not work with mock? var valueMock = new Mock<ITweet>();
            var value = new Tweet();

            var listMock = new Mock<IRedisList>();
            var clientMock = new Mock<IRedisClient>();
            clientMock.Setup(c => c.Lists[It.IsAny<string>()]).Returns(listMock.Object);

            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(clientMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.AddAsync(key, value);
            });

            Assert.AreEqual(Constants.KeyParamName, exception.ParamName);

            listMock.Verify(l => l.Add(It.IsAny<string>()), Times.Never);
            listMock.Verify(l => l.Remove(It.IsAny<string>()), Times.Never);
            clientMock.Verify(c => c.GetAllKeys(), Times.Never);
            clientMock.Verify(c => c.Lists[key], Times.Never);
            clientMock.Verify(c => c.Remove(It.IsAny<string>()), Times.Never);
            clientMock.Verify(c => c.Dispose(), Times.Never);
        }

        /// <summary>
        /// RedisKeyCollectionValuePairsRepositoryOfTweet Add with valid key and null value should throw ArgumentNullException with correct ParamName.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet Add with valid key and null value should throw ArgumentNullException with correct ParamName.")]
        [MaxTime(3000)]
        public void RedisKeyCollectionValuePairsRepositoryOfTweet_AddWithValidKeyAndNullValue_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            string key = "some key";

            var listMock = new Mock<IRedisList>();
            var clientMock = new Mock<IRedisClient>();
            clientMock.Setup(c => c.Lists[It.IsAny<string>()]).Returns(listMock.Object);

            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(clientMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.AddAsync(key, null);
            });

            Assert.AreEqual(Constants.ValueParamName, exception.ParamName);

            listMock.Verify(l => l.Add(It.IsAny<string>()), Times.Never);
            listMock.Verify(l => l.Remove(It.IsAny<string>()), Times.Never);
            clientMock.Verify(c => c.GetAllKeys(), Times.Never);
            clientMock.Verify(c => c.Lists[key], Times.Never);
            clientMock.Verify(c => c.Remove(It.IsAny<string>()), Times.Never);
            clientMock.Verify(c => c.Dispose(), Times.Never);
        }

        /// <summary>
        /// RedisKeyCollectionValuePairsRepositoryOfTweet GetAll valid key and empty list should return empty IEnumerable.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet GetAll valid key and empty list should return empty IEnumerable.")]
        [MaxTime(5000)]
        public void RedisKeyCollectionValuePairsRepositoryOfTweet_GetAllValidKeyAndEmptyList_ShouldReturnEmptyIEnumerable()
        {
            // Arrange
#pragma warning disable S4158 // Empty collections should not be accessed or iterated
            string key = "some key";

            var list = new List<string>();

            var listMock = new Mock<IRedisList>();
            listMock.Setup(l => l.GetEnumerator()).Returns(list.GetEnumerator());
            var clientMock = new Mock<IRedisClient>();
            clientMock.Setup(c => c.Lists[It.IsAny<string>()]).Returns(listMock.Object);

            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(clientMock.Object);
#pragma warning restore S4158 // Empty collections should not be accessed or iterated

            // Act
            var result = repository.GetAll(key);

            // Act + Asset
            listMock.Verify(l => l.GetEnumerator(), Times.Never);

            Assert.That(result.Count(), Is.EqualTo(0));

            var resultList = result.ToList();
            Assert.IsNotNull(resultList, "List should not be null");

            listMock.Verify(l => l.Add(It.IsAny<string>()), Times.Never);
            listMock.Verify(l => l.Remove(It.IsAny<string>()), Times.Never);
            clientMock.Verify(c => c.GetAllKeys(), Times.Never);
            clientMock.Verify(c => c.Lists[key], Times.Once);
            clientMock.Verify(c => c.Remove(It.IsAny<string>()), Times.Never);
            clientMock.Verify(c => c.Dispose(), Times.Never);
        }

        /// <summary>
        /// RedisKeyCollectionValuePairsRepositoryOfTweet GetAll with invalid key should throw ArgumentNullException with correct ParamName.
        /// </summary>
        /// <param name="key">Key parameter value.</param>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet GetAll with invalid key should throw ArgumentNullException with correct ParamName.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [MaxTime(3000)]
        public void RedisKeyCollectionValuePairsRepositoryOfTweet_GetAllWithInvalidKey_ShouldThrowArgumentNullExceptionWithCorrectParamName(string key)
        {
            // Arrange
            var listMock = new Mock<IRedisList>();
            var clientMock = new Mock<IRedisClient>();
            clientMock.Setup(c => c.Lists[It.IsAny<string>()]).Returns(listMock.Object);

            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(clientMock.Object);

            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                repository.GetAll(key);
            });

            Assert.AreEqual(Constants.KeyParamName, exception.ParamName);

            listMock.Verify(l => l.Add(It.IsAny<string>()), Times.Never);
            listMock.Verify(l => l.Remove(It.IsAny<string>()), Times.Never);
            clientMock.Verify(c => c.GetAllKeys(), Times.Never);
            clientMock.Verify(c => c.Lists[key], Times.Never);
            clientMock.Verify(c => c.Remove(It.IsAny<string>()), Times.Never);
            clientMock.Verify(c => c.Dispose(), Times.Never);
        }

        /// <summary>
        /// RedisKeyCollectionValuePairsRepositoryOfTweet Get Keys should work.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet Get Keys should work.")]
        [MaxTime(5000)]
        public void RedisKeyCollectionValuePairsRepositoryOfTweet_GetKeys_ShouldWork()
        {
            // Arrange
            var listOfKeys = new List<string>();

            var listMock = new Mock<IRedisList>();
            var clientMock = new Mock<IRedisClient>();
            clientMock.Setup(c => c.Lists[It.IsAny<string>()]).Returns(listMock.Object);
            clientMock.Setup(c => c.GetAllKeys()).Returns(listOfKeys);

            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(clientMock.Object);

            // Act
            var keys = repository.Keys;

            // Assert
            Assert.AreSame(listOfKeys, keys);

            listMock.Verify(l => l.Add(It.IsAny<string>()), Times.Never);
            listMock.Verify(l => l.Remove(It.IsAny<string>()), Times.Never);
            clientMock.Verify(c => c.GetAllKeys(), Times.Once);
            clientMock.Verify(c => c.Lists[It.IsAny<string>()], Times.Never);
            clientMock.Verify(c => c.Remove(It.IsAny<string>()), Times.Never);
            clientMock.Verify(c => c.Dispose(), Times.Never);
        }

        /// <summary>
        /// RedisKeyCollectionValuePairsRepositoryOfTweet Remove valid non-present key should work.
        /// </summary>
        /// <returns>Task</returns>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet Remove valid non-present key should work.")]
        [MaxTime(5000)]
        public async Task RedisKeyCollectionValuePairsRepositoryOfTweet_RemoveValidNonPresentKey_ShouldWork()
        {
            // Arrange
            string key = "some key";

            var listMock = new Mock<IRedisList>();
            var clientMock = new Mock<IRedisClient>();
            clientMock.Setup(c => c.Lists[It.IsAny<string>()]).Returns(listMock.Object);
            clientMock.Setup(c => c.ContainsKey(It.IsAny<string>())).Returns(false);

            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(clientMock.Object);

            // Act
            var result = await repository.RemoveAsync(key).ConfigureAwait(false);

            // Asset
            Assert.That(result, Is.EqualTo(true));

            listMock.Verify(l => l.Add(It.IsAny<string>()), Times.Never);
            listMock.Verify(l => l.Remove(It.IsAny<string>()), Times.Never);
            clientMock.Verify(c => c.ContainsKey(key), Times.Once);
            clientMock.Verify(c => c.Remove(key), Times.Never);
            clientMock.Verify(c => c.GetAllKeys(), Times.Never);
            clientMock.Verify(c => c.Lists[It.IsAny<string>()], Times.Never);
            clientMock.Verify(c => c.Remove(It.IsAny<string>()), Times.Never);
            clientMock.Verify(c => c.Dispose(), Times.Never);
        }

        /// <summary>
        /// RedisKeyCollectionValuePairsRepositoryOfTweet Remove valid key and valid value should work.
        /// </summary>
        /// <returns>Task</returns>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet Remove valid key and valid value should work.")]
        [MaxTime(5000)]
        public async Task RedisKeyCollectionValuePairsRepositoryOfTweet_RemoveValidKeyAndValidValue_ShouldWork()
        {
            // Arrange
            string key = "some key";

            //// JsonStringSerializer does not work with mock? var valueMock = new Mock<ITweet>();
            var value = new Tweet();

            var listMock = new Mock<IRedisList>();
            listMock.Setup(l => l.Remove(It.IsAny<string>())).Returns(true);
            var clientMock = new Mock<IRedisClient>();
            clientMock.Setup(c => c.Lists[It.IsAny<string>()]).Returns(listMock.Object);

            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(clientMock.Object);

            // Act
            var result = await repository.RemoveAsync(key, value).ConfigureAwait(false);

            // Asset
            Assert.That(result, Is.EqualTo(true));

            listMock.Verify(l => l.Add(It.IsAny<string>()), Times.Never);
            listMock.Verify(l => l.Remove(It.IsAny<string>()), Times.Once);
            clientMock.Verify(c => c.Lists[key], Times.Once);
            clientMock.Verify(c => c.Dispose(), Times.Never);
        }

        /// <summary>
        /// RedisKeyCollectionValuePairsRepositoryOfTweet Remove valid yet-present key should work.
        /// </summary>
        /// <returns>Task</returns>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet Remove valid yet-present key should work.")]
        [MaxTime(5000)]
        public async Task RedisKeyCollectionValuePairsRepositoryOfTweet_RemoveValidYetPresentKey_ShouldWork()
        {
            // Arrange
            string key = "some key";

            var clientMock = new Mock<IRedisClient>();
            clientMock.Setup(c => c.ContainsKey(It.IsAny<string>())).Returns(true);
            clientMock.Setup(c => c.Remove(It.IsAny<string>())).Returns(true);

            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(clientMock.Object);

            // Act
            var result = await repository.RemoveAsync(key).ConfigureAwait(false);

            // Asset
            Assert.That(result, Is.EqualTo(true));

            clientMock.Verify(c => c.ContainsKey(key), Times.Once);
            clientMock.Verify(c => c.Remove(key), Times.Once);
            clientMock.Verify(c => c.Dispose(), Times.Never);
        }

        /// <summary>
        /// RedisKeyCollectionValuePairsRepositoryOfTweet Remove with invalid key should throw ArgumentNullException with correct ParamName.
        /// </summary>
        /// <param name="key">Key parameter value.</param>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet Remove with invalid key should throw ArgumentNullException with correct ParamName.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [MaxTime(3000)]
        public void RedisKeyCollectionValuePairsRepositoryOfTweet_RemoveWithInvalidKey_ShouldThrowArgumentNullExceptionWithCorrectParamName(string key)
        {
            // Arrange
            var clientMock = new Mock<IRedisClient>();

            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(clientMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.RemoveAsync(key);
            });

            Assert.AreEqual(Constants.KeyParamName, exception.ParamName);

            clientMock.Verify(c => c.Lists[key], Times.Never);
            clientMock.Verify(c => c.Remove(It.IsAny<string>()), Times.Never);
            clientMock.Verify(c => c.Dispose(), Times.Never);
        }

        /// <summary>
        /// RedisKeyCollectionValuePairsRepositoryOfTweet Remove with invalid key and null value should throw ArgumentNullException.
        /// </summary>
        /// <param name="key">Key parameter value.</param>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet Remove with invalid key and null value should throw ArgumentNullException.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [MaxTime(3000)]
        public void RedisKeyCollectionValuePairsRepositoryOfTweet_RemoveWithInvalidKeyAndNullValue_ShouldThrowArgumentNullException(string key)
        {
            // Arrange
            var clientMock = new Mock<IRedisClient>();

            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(clientMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.RemoveAsync(key, null);
            });

            clientMock.Verify(c => c.Lists[key], Times.Never);
            clientMock.Verify(c => c.Remove(It.IsAny<string>()), Times.Never);
            clientMock.Verify(c => c.Dispose(), Times.Never);
        }

        /// <summary>
        /// RedisKeyCollectionValuePairsRepositoryOfTweet Remove with invalid key and valid value should throw ArgumentNullException with correct ParamName.
        /// </summary>
        /// <param name="key">Key parameter value.</param>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet Remove with invalid key and valid value should throw ArgumentNullException with correct ParamName.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [MaxTime(3000)]
        public void RedisKeyCollectionValuePairsRepositoryOfTweet_RemoveWithInvalidKeyAndValidValue_ShouldThrowArgumentNullExceptionWithCorrectParamName(string key)
        {
            // Arrange
            //// JsonStringSerializer does not work with mock? var valueMock = new Mock<ITweet>();
            var value = new Tweet();

            var clientMock = new Mock<IRedisClient>();

            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(clientMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.RemoveAsync(key, value);
            });

            Assert.AreEqual(Constants.KeyParamName, exception.ParamName);

            clientMock.Verify(c => c.Lists[key], Times.Never);
            clientMock.Verify(c => c.Remove(It.IsAny<string>()), Times.Never);
            clientMock.Verify(c => c.Dispose(), Times.Never);
        }

        /// <summary>
        /// RedisKeyCollectionValuePairsRepositoryOfTweet Remove with valid key and null value should throw ArgumentNullException with correct ParamName.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet Remove with valid key and null value should throw ArgumentNullException with correct ParamName.")]
        [MaxTime(3000)]
        public void RedisKeyCollectionValuePairsRepositoryOfTweet_RemoveWithValidKeyAndNullValue_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            string key = "some key";

            var clientMock = new Mock<IRedisClient>();

            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(clientMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.RemoveAsync(key, null);
            });

            Assert.AreEqual(Constants.ValueParamName, exception.ParamName);

            clientMock.Verify(c => c.Lists[key], Times.Never);
            clientMock.Verify(c => c.Remove(It.IsAny<string>()), Times.Never);
            clientMock.Verify(c => c.Dispose(), Times.Never);
        }

        /// <summary>
        /// RedisKeyCollectionValuePairsRepositoryOfTweet SaveChanges should work.
        /// </summary>
        /// <returns>Task</returns>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet SaveChanges should work.")]
        [MaxTime(5000)]
        public async Task RedisKeyCollectionValuePairsRepositoryOfTweet_SaveChanges_ShouldWork()
        {
            // Arrange
            var clientMock = new Mock<IRedisClient>();

            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(clientMock.Object);

            // Act
            var result = await repository.SaveChangesAsync().ConfigureAwait(false);

            // Asset
            Assert.That(result, Is.EqualTo(0L));

            clientMock.Verify(c => c.SaveAsync(), Times.Once);
            clientMock.Verify(c => c.Lists[It.IsAny<string>()], Times.Never);
            clientMock.Verify(c => c.Remove(It.IsAny<string>()), Times.Never);
            clientMock.Verify(c => c.Dispose(), Times.Never);
        }

        /// <summary>
        /// RedisKeyCollectionValuePairsRepositoryOfTweet with null client provider in constructor should throw ArgumentNullException with correct ParamName.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet with null client provider in constructor should throw ArgumentNullException with correct ParamName.")]
        [MaxTime(3000)]
        public void RedisKeyCollectionValuePairsRepositoryOfTweet_WithNullClientProviderInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new RedisKeyCollectionValuePairsRepository<ITweet>(null);
            });

            Assert.AreEqual(Constants.ClientFieldName, exception.ParamName);
        }

        /// <summary>
        /// RedisKeyCollectionValuePairsRepositoryOfTweet with valid client provider in constructor should be initialized correctly.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet with valid client provider in constructor should be initialized correctly.")]
        [MaxTime(3000)]
        public void RedisKeyCollectionValuePairsRepositoryOfTweet_WithValidClientProviderInConstructor_ShouldBeInitializedCorrectly()
        {
            // Arrange
            var clientMock = new Mock<IRedisClient>();

            // Act + Assert
            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(clientMock.Object);
            Assert.IsNotNull(repository);

            Type baseType = typeof(RedisKeyCollectionValuePairsRepository<ITweet>).BaseType;

            var clientField = PrivateField.GetInstanceField(baseType, repository, Constants.ClientFieldName);
            var clientProperty = PrivateProperty.GetInstanceProperty(baseType, repository, Constants.ClientPropertyName);
            Assert.AreSame(clientMock.Object, clientField ?? clientProperty);
        }
    }
}
