// <copyright file="RedisKeyValuePairsRepositoryOfTweetUnitTests.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Common.Redis.Unit.Tests.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Common.Code.Tests;
    using ProcessingTools.Data.Common.Redis.Repositories;
    using ProcessingTools.Data.Common.Redis.Unit.Tests.Models;
    using ServiceStack.Redis;

    /// <summary>
    /// <see cref="RedisKeyValuePairsRepository{ITweet}"/> unit tests.
    /// </summary>
    [TestFixture(Category = "Unit", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>))]
    public class RedisKeyValuePairsRepositoryOfTweetUnitTests
    {
        /// <summary>
        /// RedisKeyValuePairsRepositoryOfTweet Add valid non-present key and valid value should work.
        /// </summary>
        /// <returns>Task</returns>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Add valid non-present key and valid value should work.")]
        [MaxTime(1000)]
        public async Task RedisKeyValuePairsRepositoryOfTweet_AddValidNonPresentKeyAndValidValue_ShouldWork()
        {
            // Arrange
            string key = "some key";

            var valueMock = new Mock<ITweet>();
            var value = valueMock.Object;

            var clientMock = new Mock<IRedisClient>();
            clientMock.Setup(c => c.ContainsKey(It.IsAny<string>())).Returns(false);
            clientMock.Setup(c => c.Add(It.IsAny<string>(), It.IsAny<ITweet>())).Returns(true);

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientMock.Object);

            // Act
            var result = await repository.AddAsync(key, value).ConfigureAwait(false);

            // Asset
            Assert.That(result, Is.EqualTo(true));

            clientMock.Verify(c => c.ContainsKey(key), Times.Once);
            clientMock.Verify(c => c.Add(key, value), Times.Once);
            clientMock.Verify(c => c.Dispose(), Times.Never);
        }

        /// <summary>
        /// RedisKeyValuePairsRepositoryOfTweet Add valid yet-present key and valid value should throw KeyExistsException.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Add valid yet-present key and valid value should throw KeyExistsException.")]
        [MaxTime(1000)]
        public void RedisKeyValuePairsRepositoryOfTweet_AddValidYetPresentKeyAndValidValue_ShouldThowKeyExistsException()
        {
            // Arrange
            string key = "some key";

            var valueMock = new Mock<ITweet>();
            var value = valueMock.Object;

            var clientMock = new Mock<IRedisClient>();
            clientMock.Setup(c => c.ContainsKey(It.IsAny<string>())).Returns(true);

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ProcessingTools.Common.Exceptions.KeyExistsException>(() =>
            {
                return repository.AddAsync(key, value);
            });

            clientMock.Verify(c => c.ContainsKey(key), Times.Once);
            clientMock.Verify(c => c.Add(key, value), Times.Never);
            clientMock.Verify(c => c.Dispose(), Times.Never);
        }

        /// <summary>
        /// RedisKeyValuePairsRepositoryOfTweet Add with invalid key and null value should throw ArgumentNullException.
        /// </summary>
        /// <param name="key">Key parameter value.</param>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Add with invalid key and null value should throw ArgumentNullException.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [MaxTime(3000)]
        public void RedisKeyValuePairsRepositoryOfTweet_AddWithInvalidKeyAndNullValue_ShouldThrowArgumentNullException(string key)
        {
            // Arrange
            var clientMock = new Mock<IRedisClient>();

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.AddAsync(key, null);
            });

            clientMock.Verify(c => c.ContainsKey(key), Times.Never);
            clientMock.Verify(c => c.Add(key, It.IsAny<ITweet>()), Times.Never);
            clientMock.Verify(c => c.Dispose(), Times.Never);
        }

        /// <summary>
        /// RedisKeyValuePairsRepositoryOfTweet Add with invalid key and valid value should throw ArgumentNullException with correct ParamName.
        /// </summary>
        /// <param name="key">Key parameter value.</param>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Add with invalid key and valid value should throw ArgumentNullException with correct ParamName.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [MaxTime(3000)]
        public void RedisKeyValuePairsRepositoryOfTweet_AddWithInvalidKeyAndValidValue_ShouldThrowArgumentNullExceptionWithCorrectParamName(string key)
        {
            // Arrange
            var valueMock = new Mock<ITweet>();
            var value = valueMock.Object;

            var clientMock = new Mock<IRedisClient>();

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.AddAsync(key, value);
            });

            Assert.AreEqual(Constants.KeyParamName, exception.ParamName);

            clientMock.Verify(c => c.ContainsKey(key), Times.Never);
            clientMock.Verify(c => c.Add(key, It.IsAny<ITweet>()), Times.Never);
            clientMock.Verify(c => c.Dispose(), Times.Never);
        }

        /// <summary>
        /// RedisKeyValuePairsRepositoryOfTweet Add with valid key and null value should throw ArgumentNullException with correct ParamName.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Add with valid key and null value should throw ArgumentNullException with correct ParamName.")]
        [MaxTime(3000)]
        public void RedisKeyValuePairsRepositoryOfTweet_AddWithValidKeyAndNullValue_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            string key = "some key";

            var clientMock = new Mock<IRedisClient>();

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.AddAsync(key, null);
            });

            Assert.AreEqual(Constants.ValueParamName, exception.ParamName);

            clientMock.Verify(c => c.ContainsKey(key), Times.Never);
            clientMock.Verify(c => c.Add(key, It.IsAny<ITweet>()), Times.Never);
            clientMock.Verify(c => c.Dispose(), Times.Never);
        }

        /// <summary>
        /// RedisKeyValuePairsRepositoryOfTweet Get Keys should work.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Get Keys should work.")]
        [MaxTime(1000)]
        public void RedisKeyValuePairsRepositoryOfTweet_GetKeys_ShouldWork()
        {
            // Arrange
            var listOfKeys = new List<string>();

            var clientMock = new Mock<IRedisClient>();
            clientMock.Setup(c => c.GetAllKeys()).Returns(listOfKeys);

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientMock.Object);

            // Act
            var keys = repository.Keys;

            // Assert
            Assert.AreSame(listOfKeys, keys);

            clientMock.Verify(c => c.GetAllKeys(), Times.Once);
            clientMock.Verify(c => c.Dispose(), Times.Never);
        }

        /// <summary>
        /// RedisKeyValuePairsRepositoryOfTweet Get valid non-present key should throw KeyExistsException.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Get valid non-present key should throw KeyExistsException.")]
        [MaxTime(1000)]
        public void RedisKeyValuePairsRepositoryOfTweet_GetValidNonPresentKey_ShouldThowKeyExistsException()
        {
            // Arrange
            string key = "some key";

            var clientMock = new Mock<IRedisClient>();
            clientMock.Setup(c => c.ContainsKey(It.IsAny<string>())).Returns(false);

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ProcessingTools.Common.Exceptions.KeyNotFoundException>(() =>
            {
                return repository.GetAsync(key);
            });

            clientMock.Verify(c => c.ContainsKey(key), Times.Once);
            clientMock.Verify(c => c.Get<ITweet>(key), Times.Never);
            clientMock.Verify(c => c.Dispose(), Times.Never);
        }

        /// <summary>
        /// RedisKeyValuePairsRepositoryOfTweet Get valid yet-present key should work.
        /// </summary>
        /// <returns>Task</returns>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Get valid yet-present key should work.")]
        [MaxTime(1000)]
        public async Task RedisKeyValuePairsRepositoryOfTweet_GetValidYetPresentKey_ShouldWork()
        {
            // Arrange
            string key = "some key";

            var valueMock = new Mock<ITweet>();
            var value = valueMock.Object;

            var clientMock = new Mock<IRedisClient>();
            clientMock.Setup(c => c.ContainsKey(It.IsAny<string>())).Returns(true);
            clientMock.Setup(c => c.Get<ITweet>(It.IsAny<string>())).Returns(value);

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientMock.Object);

            // Act
            var result = await repository.GetAsync(key).ConfigureAwait(false);

            // Asset
            Assert.AreSame(value, result);

            clientMock.Verify(c => c.ContainsKey(key), Times.Once);
            clientMock.Verify(c => c.Get<ITweet>(key), Times.Once);
            clientMock.Verify(c => c.Dispose(), Times.Never);
        }

        /// <summary>
        /// RedisKeyValuePairsRepositoryOfTweet Get with invalid key should throw ArgumentNullException with correct ParamName.
        /// </summary>
        /// <param name="key">Key parameter value.</param>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Get with invalid key should throw ArgumentNullException with correct ParamName.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [MaxTime(3000)]
        public void RedisKeyValuePairsRepositoryOfTweet_GetWithInvalidKey_ShouldThrowArgumentNullExceptionWithCorrectParamName(string key)
        {
            // Arrange
            var clientMock = new Mock<IRedisClient>();

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.GetAsync(key);
            });

            Assert.AreEqual(Constants.KeyParamName, exception.ParamName);

            clientMock.Verify(c => c.ContainsKey(key), Times.Never);
            clientMock.Verify(c => c.Get<ITweet>(key), Times.Never);
            clientMock.Verify(c => c.Dispose(), Times.Never);
        }

        /// <summary>
        /// RedisKeyValuePairsRepositoryOfTweet Remove valid non-present key should work.
        /// </summary>
        /// <returns>Task</returns>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Remove valid non-present key should work.")]
        [MaxTime(1000)]
        public async Task RedisKeyValuePairsRepositoryOfTweet_RemoveValidNonPresentKey_ShouldWork()
        {
            // Arrange
            string key = "some key";

            var clientMock = new Mock<IRedisClient>();
            clientMock.Setup(c => c.ContainsKey(It.IsAny<string>())).Returns(false);

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientMock.Object);

            // Act
            var result = await repository.RemoveAsync(key).ConfigureAwait(false);

            // Asset
            Assert.That(result, Is.EqualTo(true));

            clientMock.Verify(c => c.ContainsKey(key), Times.Once);
            clientMock.Verify(c => c.Remove(key), Times.Never);
            clientMock.Verify(c => c.Dispose(), Times.Never);
        }

        /// <summary>
        /// RedisKeyValuePairsRepositoryOfTweet Remove valid yet-present key should work.
        /// </summary>
        /// <returns>Task</returns>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Remove valid yet-present key should work.")]
        [MaxTime(1000)]
        public async Task RedisKeyValuePairsRepositoryOfTweet_RemoveValidYetPresentKey_ShouldWork()
        {
            // Arrange
            string key = "some key";

            var clientMock = new Mock<IRedisClient>();
            clientMock.Setup(c => c.ContainsKey(It.IsAny<string>())).Returns(true);
            clientMock.Setup(c => c.Remove(It.IsAny<string>())).Returns(true);

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientMock.Object);

            // Act
            var result = await repository.RemoveAsync(key).ConfigureAwait(false);

            // Asset
            Assert.That(result, Is.EqualTo(true));

            clientMock.Verify(c => c.ContainsKey(key), Times.Once);
            clientMock.Verify(c => c.Remove(key), Times.Once);
            clientMock.Verify(c => c.Dispose(), Times.Never);
        }

        /// <summary>
        /// RedisKeyValuePairsRepositoryOfTweet Remove with invalid key should throw ArgumentNullException with correct ParamName.
        /// </summary>
        /// <param name="key">Key parameter value.</param>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Remove with invalid key should throw ArgumentNullException with correct ParamName.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [MaxTime(3000)]
        public void RedisKeyValuePairsRepositoryOfTweet_RemoveWithInvalidKey_ShouldThrowArgumentNullExceptionWithCorrectParamName(string key)
        {
            // Arrange
            var clientMock = new Mock<IRedisClient>();

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.RemoveAsync(key);
            });

            Assert.AreEqual(Constants.KeyParamName, exception.ParamName);

            clientMock.Verify(c => c.ContainsKey(key), Times.Never);
            clientMock.Verify(c => c.Remove(key), Times.Never);
            clientMock.Verify(c => c.Dispose(), Times.Never);
        }

        /// <summary>
        /// RedisKeyValuePairsRepositoryOfTweet SaveChanges should work.
        /// </summary>
        /// <returns>Task</returns>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet SaveChanges should work.")]
        [MaxTime(1000)]
        public async Task RedisKeyValuePairsRepositoryOfTweet_SaveChanges_ShouldWork()
        {
            // Arrange
            var clientMock = new Mock<IRedisClient>();

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientMock.Object);

            // Act
            var result = await repository.SaveChangesAsync().ConfigureAwait(false);

            // Asset
            Assert.That(result, Is.EqualTo(0L));

            clientMock.Verify(c => c.SaveAsync(), Times.Once);
            clientMock.Verify(c => c.Dispose(), Times.Never);
        }

        /// <summary>
        /// RedisKeyValuePairsRepositoryOfTweet Update valid non-present key and valid value should throw KeyExistsException.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Update valid non-present key and valid value should throw KeyExistsException.")]
        [MaxTime(1000)]
        public void RedisKeyValuePairsRepositoryOfTweet_UpdateValidNonPresentKeyAndValidValue_ShouldThowKeyExistsException()
        {
            // Arrange
            string key = "some key";

            var valueMock = new Mock<ITweet>();
            var value = valueMock.Object;

            var clientMock = new Mock<IRedisClient>();
            clientMock.Setup(c => c.ContainsKey(It.IsAny<string>())).Returns(false);

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ProcessingTools.Common.Exceptions.KeyNotFoundException>(() =>
            {
                return repository.UpdateAsync(key, value);
            });

            clientMock.Verify(c => c.ContainsKey(key), Times.Once);
            clientMock.Verify(c => c.Replace(key, value), Times.Never);
            clientMock.Verify(c => c.Dispose(), Times.Never);
        }

        /// <summary>
        /// RedisKeyValuePairsRepositoryOfTweet Update valid yet-present key and valid value should work.
        /// </summary>
        /// <returns>Task</returns>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Update valid yet-present key and valid value should work.")]
        [MaxTime(1000)]
        public async Task RedisKeyValuePairsRepositoryOfTweet_UpdateValidYetPresentKeyAndValidValue_ShouldWork()
        {
            // Arrange
            string key = "some key";

            var valueMock = new Mock<ITweet>();
            var value = valueMock.Object;

            var clientMock = new Mock<IRedisClient>();
            clientMock.Setup(c => c.ContainsKey(It.IsAny<string>())).Returns(true);
            clientMock.Setup(c => c.Replace(It.IsAny<string>(), It.IsAny<ITweet>())).Returns(true);

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientMock.Object);

            // Act
            var result = await repository.UpdateAsync(key, value).ConfigureAwait(false);

            // Asset
            Assert.That(result, Is.EqualTo(true));

            clientMock.Verify(c => c.ContainsKey(key), Times.Once);
            clientMock.Verify(c => c.Replace(key, value), Times.Once);
            clientMock.Verify(c => c.Dispose(), Times.Never);
        }

        /// <summary>
        /// RedisKeyValuePairsRepositoryOfTweet Update with invalid key and null value should throw ArgumentNullException.
        /// </summary>
        /// <param name="key">Key parameter value.</param>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Update with invalid key and null value should throw ArgumentNullException.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [MaxTime(3000)]
        public void RedisKeyValuePairsRepositoryOfTweet_UpdateWithInvalidKeyAndNullValue_ShouldThrowArgumentNullException(string key)
        {
            // Arrange
            var clientMock = new Mock<IRedisClient>();

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.UpdateAsync(key, null);
            });

            clientMock.Verify(c => c.ContainsKey(key), Times.Never);
            clientMock.Verify(c => c.Replace(key, It.IsAny<ITweet>()), Times.Never);
            clientMock.Verify(c => c.Dispose(), Times.Never);
        }

        /// <summary>
        /// RedisKeyValuePairsRepositoryOfTweet Update with invalid key and valid value should throw ArgumentNullException with correct ParamName.
        /// </summary>
        /// <param name="key">Key parameter value.</param>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Update with invalid key and valid value should throw ArgumentNullException with correct ParamName.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [MaxTime(3000)]
        public void RedisKeyValuePairsRepositoryOfTweet_UpdateWithInvalidKeyAndValidValue_ShouldThrowArgumentNullExceptionWithCorrectParamName(string key)
        {
            // Arrange
            var valueMock = new Mock<ITweet>();
            var value = valueMock.Object;

            var clientMock = new Mock<IRedisClient>();

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.UpdateAsync(key, value);
            });

            Assert.AreEqual(Constants.KeyParamName, exception.ParamName);

            clientMock.Verify(c => c.ContainsKey(key), Times.Never);
            clientMock.Verify(c => c.Replace(key, It.IsAny<ITweet>()), Times.Never);
            clientMock.Verify(c => c.Dispose(), Times.Never);
        }

        /// <summary>
        /// RedisKeyValuePairsRepositoryOfTweet Update with valid key and null value should throw ArgumentNullException with correct ParamName.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Update with valid key and null value should throw ArgumentNullException with correct ParamName.")]
        [MaxTime(3000)]
        public void RedisKeyValuePairsRepositoryOfTweet_UpdateWithValidKeyAndNullValue_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            string key = "some key";

            var clientMock = new Mock<IRedisClient>();

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.UpdateAsync(key, null);
            });

            Assert.AreEqual(Constants.ValueParamName, exception.ParamName);

            clientMock.Verify(c => c.ContainsKey(key), Times.Never);
            clientMock.Verify(c => c.Replace(key, It.IsAny<ITweet>()), Times.Never);
            clientMock.Verify(c => c.Dispose(), Times.Never);
        }

        /// <summary>
        /// RedisKeyValuePairsRepositoryOfTweet Upsert valid non-present key and valid value should work.
        /// </summary>
        /// <returns>Task</returns>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Upsert valid non-present key and valid value should work.")]
        [MaxTime(1000)]
        public async Task RedisKeyValuePairsRepositoryOfTweet_UpsertValidNonPresentKeyAndValidValue_ShouldWork()
        {
            // Arrange
            string key = "some key";

            var valueMock = new Mock<ITweet>();
            var value = valueMock.Object;

            var clientMock = new Mock<IRedisClient>();
            clientMock.Setup(c => c.ContainsKey(It.IsAny<string>())).Returns(false);
            clientMock.Setup(c => c.Set(It.IsAny<string>(), It.IsAny<ITweet>())).Returns(true);

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientMock.Object);

            // Act
            var result = await repository.UpsertAsync(key, value).ConfigureAwait(false);

            // Asset
            Assert.That(result, Is.EqualTo(true));

            clientMock.Verify(c => c.ContainsKey(key), Times.Never);
            clientMock.Verify(c => c.Set(key, value), Times.Once);
            clientMock.Verify(c => c.Dispose(), Times.Never);
        }

        /// <summary>
        /// RedisKeyValuePairsRepositoryOfTweet Upsert valid yet-present key and valid value should work.
        /// </summary>
        /// <returns>Task</returns>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Upsert valid yet-present key and valid value should work.")]
        [MaxTime(1000)]
        public async Task RedisKeyValuePairsRepositoryOfTweet_UpsertValidYetPresentKeyAndValidValue_ShouldWork()
        {
            // Arrange
            string key = "some key";

            var valueMock = new Mock<ITweet>();
            var value = valueMock.Object;

            var clientMock = new Mock<IRedisClient>();
            clientMock.Setup(c => c.ContainsKey(It.IsAny<string>())).Returns(true);
            clientMock.Setup(c => c.Set(It.IsAny<string>(), It.IsAny<ITweet>())).Returns(true);

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientMock.Object);

            // Act
            var result = await repository.UpsertAsync(key, value).ConfigureAwait(false);

            // Asset
            Assert.That(result, Is.EqualTo(true));

            clientMock.Verify(c => c.ContainsKey(key), Times.Never);
            clientMock.Verify(c => c.Set(key, value), Times.Once);
            clientMock.Verify(c => c.Dispose(), Times.Never);
        }

        /// <summary>
        /// RedisKeyValuePairsRepositoryOfTweet Upsert with invalid key and null value should throw ArgumentNullException.
        /// </summary>
        /// <param name="key">Key parameter value.</param>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Upsert with invalid key and null value should throw ArgumentNullException.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [MaxTime(3000)]
        public void RedisKeyValuePairsRepositoryOfTweet_UpsertWithInvalidKeyAndNullValue_ShouldThrowArgumentNullException(string key)
        {
            // Arrange
            var clientMock = new Mock<IRedisClient>();

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.UpsertAsync(key, null);
            });

            clientMock.Verify(c => c.ContainsKey(key), Times.Never);
            clientMock.Verify(c => c.Set(key, It.IsAny<ITweet>()), Times.Never);
            clientMock.Verify(c => c.Dispose(), Times.Never);
        }

        /// <summary>
        /// RedisKeyValuePairsRepositoryOfTweet Upsert with invalid key and valid value should throw ArgumentNullException with correct ParamName.
        /// </summary>
        /// <param name="key">Key parameter value.</param>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Upsert with invalid key and valid value should throw ArgumentNullException with correct ParamName.")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        [MaxTime(3000)]
        public void RedisKeyValuePairsRepositoryOfTweet_UpsertWithInvalidKeyAndValidValue_ShouldThrowArgumentNullExceptionWithCorrectParamName(string key)
        {
            // Arrange
            var valueMock = new Mock<ITweet>();
            var value = valueMock.Object;

            var clientMock = new Mock<IRedisClient>();

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.UpsertAsync(key, value);
            });

            Assert.AreEqual(Constants.KeyParamName, exception.ParamName);

            clientMock.Verify(c => c.ContainsKey(key), Times.Never);
            clientMock.Verify(c => c.Set(key, It.IsAny<ITweet>()), Times.Never);
            clientMock.Verify(c => c.Dispose(), Times.Never);
        }

        /// <summary>
        /// RedisKeyValuePairsRepositoryOfTweet Upsert with valid key and null value should throw ArgumentNullException with correct ParamName.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Upsert with valid key and null value should throw ArgumentNullException with correct ParamName.")]
        [MaxTime(3000)]
        public void RedisKeyValuePairsRepositoryOfTweet_UpsertWithValidKeyAndNullValue_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            string key = "some key";

            var clientMock = new Mock<IRedisClient>();

            var repository = new RedisKeyValuePairsRepository<ITweet>(clientMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return repository.UpsertAsync(key, null);
            });

            Assert.AreEqual(Constants.ValueParamName, exception.ParamName);

            clientMock.Verify(c => c.ContainsKey(key), Times.Never);
            clientMock.Verify(c => c.Set(key, It.IsAny<ITweet>()), Times.Never);
            clientMock.Verify(c => c.Dispose(), Times.Never);
        }

        /// <summary>
        /// RedisKeyValuePairsRepositoryOfTweet with null client provider in constructor should throw ArgumentNullException with correct ParamName.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet with null client provider in constructor should throw ArgumentNullException with correct ParamName.")]
        [MaxTime(3000)]
        public void RedisKeyValuePairsRepositoryOfTweet_WithNullClientProviderInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new RedisKeyValuePairsRepository<ITweet>(null);
            });

            Assert.AreEqual(Constants.ClientFieldName, exception.ParamName);
        }

        /// <summary>
        /// RedisKeyValuePairsRepositoryOfTweet with valid client provider in constructor should be initialized correctly.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet with valid client provider in constructor should be initialized correctly.")]
        [MaxTime(3000)]
        public void RedisKeyValuePairsRepositoryOfTweet_WithValidClientProviderInConstructor_ShouldBeInitializedCorrectly()
        {
            // Arrange
            var clientMock = new Mock<IRedisClient>();

            // Act + Assert
            var repository = new RedisKeyValuePairsRepository<ITweet>(clientMock.Object);
            Assert.IsNotNull(repository);

            Type baseType = typeof(RedisKeyValuePairsRepository<ITweet>).BaseType;

            var clientField = PrivateField.GetInstanceField(baseType, repository, Constants.ClientFieldName);
            var clientProperty = PrivateProperty.GetInstanceProperty(baseType, repository, Constants.ClientPropertyName);
            Assert.AreSame(clientMock.Object, clientField ?? clientProperty);
        }
    }
}
