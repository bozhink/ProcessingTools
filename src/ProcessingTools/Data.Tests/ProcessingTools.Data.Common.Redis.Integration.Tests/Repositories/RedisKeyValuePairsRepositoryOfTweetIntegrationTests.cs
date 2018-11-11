// <copyright file="RedisKeyValuePairsRepositoryOfTweetIntegrationTests.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Common.Redis.Integration.Tests.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using ProcessingTools.Common.Code.Tests;
    using ProcessingTools.Data.Common.Redis.Integration.Tests.Models;
    using ProcessingTools.Data.Common.Redis.Repositories;
    using ServiceStack.Redis;

    /// <summary>
    /// <see cref="RedisKeyValuePairsRepository{ITweet}"/> integration tests.
    /// </summary>
    [TestFixture(Category = "Integration", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>))]
    public class RedisKeyValuePairsRepositoryOfTweetIntegrationTests
    {
        /// <summary>
        /// RedisKeyValuePairsRepositoryOfTweet Add new valid key-value pair and then Get it and Remove it should work.
        /// </summary>
        /// <returns>Task</returns>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Add new valid key-value pair and then Get it and Remove it should work.")]
        [MaxTime(5000)]
        [Ignore("System-dependent integration test. Needs running Redis server.")]
        public async Task RedisKeyValuePairsRepositoryOfTweet_AddNewValidKeyValuePairAndThenGetItAndRemoveIt_ShouldWork()
        {
            // Arrange
            IRedisClient client = new RedisClient(Constants.ConnectionString);
            var repository = new RedisKeyValuePairsRepository<ITweet>(client);
            var key = Guid.NewGuid().ToString();
            var value = new Tweet
            {
                Id = 0,
                Content = Guid.NewGuid().ToString(),
                PostedOn = DateTime.UtcNow
            };

            // Act: Add
            var added = repository.AddAsync(key, value);

            // Assert: Add
            Assert.That(async () => await added.ConfigureAwait(false), Is.EqualTo(true));

            // Act + Assert: SaveChanges
            Assert.That(async () => await repository.SaveChangesAsync().ConfigureAwait(false), Is.EqualTo(0L));

            // Act: Get
            var valueFromDb = await repository.GetAsync(key).ConfigureAwait(false);

            // Assert: Get
            Assert.AreEqual(value.Id, valueFromDb.Id);
            Assert.AreEqual(value.Content, valueFromDb.Content);
            Assert.AreEqual(value.PostedOn.ToLongDateString(), valueFromDb.PostedOn.ToLongDateString());
            Assert.AreEqual(value.PostedOn.ToLongTimeString(), valueFromDb.PostedOn.ToLongTimeString());

            // Act: Remove
            var removed = repository.RemoveAsync(key);

            // Assert: Remove
            Assert.That(async () => await removed.ConfigureAwait(false), Is.EqualTo(true));

            // Act + Assert: SaveChanges
            // Expected internal catch of "ServiceStack.Redis.RedisResponseException : Background save already in progress"
            Assert.That(async () => await repository.SaveChangesAsync().ConfigureAwait(false), Is.EqualTo(1L));
        }

        /// <summary>
        /// RedisKeyValuePairsRepositoryOfTweet Get Keys should work.
        /// </summary>
        /// <returns>Task</returns>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Get Keys should work.")]
        [MaxTime(10000)]
        [Ignore("System-dependent integration test. Needs running Redis server.")]
        public async Task RedisKeyValuePairsRepositoryOfTweet_GetKeys_ShouldWork()
        {
            // Arrange
            const int NumberOfKeys = 10;
            var listOfKeys = Enumerable.Range(0, NumberOfKeys)
                .Select(i => Guid.NewGuid().ToString() + i)
                .ToList();

            IRedisClient client = new RedisClient(Constants.ConnectionString);
            var repository = new RedisKeyValuePairsRepository<ITweet>(client);

            var value = new Tweet
            {
                Id = 0,
                Content = Guid.NewGuid().ToString(),
                PostedOn = DateTime.UtcNow
            };

            // Act: Add
            foreach (var key in listOfKeys)
            {
                await repository.AddAsync(key, value).ConfigureAwait(false);
            }

            // Act: Get Keys
            var keysAfterAdd = repository.Keys.ToList();

            // Assert: Get Keys
            Assert.AreEqual(NumberOfKeys, keysAfterAdd.Count, $"Number of keys after add should be {NumberOfKeys}.");

            foreach (var key in listOfKeys)
            {
                var keyFromDb = keysAfterAdd.Single(k => k == key);
                Assert.AreEqual(key, keyFromDb);
            }

            // Act: Remove
            foreach (var key in listOfKeys)
            {
                await repository.RemoveAsync(key).ConfigureAwait(false);
            }

            // Act: Get Keys
            var keysAfterRemove = repository.Keys.ToList();

            // Assert: Get Keys
            Assert.AreEqual(0, keysAfterRemove.Count, $"Number of keys after insert should be 0.");
        }

        /// <summary>
        /// RedisKeyValuePairsRepositoryOfTweet with valid client provider in constructor should be initialized correctly.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet with valid client provider in constructor should be initialized correctly.")]
        [MaxTime(500)]
        public void RedisKeyValuePairsRepositoryOfTweet_WithValidClientProviderInConstructor_ShouldBeInitializedCorrectly()
        {
            // Arrange
            IRedisClient client = new RedisClient(Constants.ConnectionString);

            // Act + Assert
            var repository = new RedisKeyValuePairsRepository<ITweet>(client);
            Assert.IsNotNull(repository);

            Type baseType = typeof(RedisKeyValuePairsRepository<ITweet>).BaseType;

            var clientField = PrivateField.GetInstanceField(baseType, repository, Constants.ClientFieldName);
            var clientProperty = PrivateProperty.GetInstanceProperty(baseType, repository, Constants.ClientPropertyName);
            Assert.AreSame(client, clientField ?? clientProperty);
        }
    }
}
