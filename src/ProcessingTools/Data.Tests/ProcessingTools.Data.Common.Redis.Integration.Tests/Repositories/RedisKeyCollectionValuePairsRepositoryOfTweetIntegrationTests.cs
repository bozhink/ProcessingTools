// <copyright file="RedisKeyCollectionValuePairsRepositoryOfTweetIntegrationTests.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Common.Redis.Integration.Tests.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using ProcessingTools.Common.Code.Tests;
    using ProcessingTools.Data.Common.Redis.Integration.Tests.Models;
    using ProcessingTools.Data.Common.Redis.Repositories;
    using ServiceStack.Redis;

    /// <summary>
    /// <see cref="RedisKeyCollectionValuePairsRepository{ITweet}"/> integration tests.
    /// </summary>
    [TestFixture(Category = "Integration", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>))]
    public class RedisKeyCollectionValuePairsRepositoryOfTweetIntegrationTests
    {
        /// <summary>
        /// RedisKeyCollectionValuePairsRepositoryOfTweet Add and Remove key-value pair should work.
        /// </summary>
        /// <returns>Task</returns>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet Add and Remove key-value pair should work.")]
        [MaxTime(10000)]
        [Ignore("System-dependent integration test. Needs running Redis server.")]
        public async Task RedisKeyCollectionValuePairsRepositoryOfTweet_AddAndRemoveKeyValuePair_ShouldWork()
        {
            // Arrange
            IRedisClient client = new RedisClient(Constants.ConnectionString);
            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(client);
            var key = Guid.NewGuid().ToString();

            var now = DateTime.Now;
            var value = new Tweet
            {
                Id = 1,
                PostedOn = now,
                Content = $"{key} {now.ToLongTimeString()}"
            };

            // Act: Clean-up database
            await repository.RemoveAsync(key).ConfigureAwait(false);
            await repository.SaveChangesAsync().ConfigureAwait(false);

            // Act: Add
            await repository.AddAsync(key, value).ConfigureAwait(false);
            await repository.SaveChangesAsync().ConfigureAwait(false);

            // Act + Assert: Retrieve data
            var valuesFromDb = repository.GetAll(key).ToList();
            Assert.AreEqual(1, valuesFromDb.Count);

            var valueFromDb = valuesFromDb.Single();
            Assert.AreEqual(1, valueFromDb.Id);
            Assert.AreEqual(now.ToLongTimeString(), valueFromDb.PostedOn.ToLongTimeString());
            Assert.AreEqual(value.Content, valueFromDb.Content);

            // Act: Remove
            await repository.RemoveAsync(key).ConfigureAwait(false);
            Assert.That(async () => await repository.SaveChangesAsync().ConfigureAwait(false), Is.EqualTo(0L).After(2000));

            // Assert: Remove
            Assert.AreEqual(0, repository.GetAll(key).ToList().Count, "All values in the list should be removed.");
        }

        /// <summary>
        /// RedisKeyCollectionValuePairsRepositoryOfTweet Add multiple values in the same key and Remove one should work.
        /// </summary>
        /// <returns>Task</returns>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet Add multiple values in the same key and Remove one should work.")]
        [MaxTime(10000)]
        [Ignore("System-dependent integration test. Needs running Redis server.")]
        public async Task RedisKeyCollectionValuePairsRepositoryOfTweet_AddMultipleValuesInTheSameKeyAndRemoveOne_ShouldWork()
        {
            // Arrange
            const int NumberOfItems = 1000;
            IRedisClient client = new RedisClient(Constants.ConnectionString);
            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(client);
            var key = Guid.NewGuid().ToString();

            var values = new List<ITweet>(NumberOfItems);

            // Act: Clean-up database
            await repository.RemoveAsync(key).ConfigureAwait(false);
            await repository.SaveChangesAsync().ConfigureAwait(false);

            // Act: Add
            for (int i = 0; i < NumberOfItems; ++i)
            {
                var now = DateTime.Now + TimeSpan.FromHours(i);
                var content = $"{key} {i} {now.ToLongTimeString()}";
                var value = new Tweet
                {
                    Id = i,
                    PostedOn = now,
                    Content = content
                };

                values.Add(value);
                await repository.AddAsync(key, value).ConfigureAwait(false);
            }

            var valueToBeDeleted = new Tweet
            {
                Id = -1,
                Content = "To be removed",
                PostedOn = DateTime.Now
            };

            await repository.AddAsync(key, valueToBeDeleted).ConfigureAwait(false);

            values.TrimExcess();
            await repository.SaveChangesAsync().ConfigureAwait(false);

            // Assert: Get
            Assert.AreEqual(NumberOfItems, values.Count);
            Assert.AreEqual(NumberOfItems + 1, repository.GetAll(key).ToList().Count);

            // Act: Remove value
            var removed = await repository.RemoveAsync(key, valueToBeDeleted).ConfigureAwait(false);

            // Assert: Remove value
            Assert.That(removed, Is.EqualTo(true));

            // Act: Get
            var valuesFromDb = repository.GetAll(key).ToList();

            // Assert: Get
            Assert.AreEqual(NumberOfItems, values.Count);
            Assert.AreEqual(NumberOfItems, valuesFromDb.Count);

            Assert.IsFalse(valuesFromDb.Any(v => v.Id < 0));

            foreach (var value in values)
            {
                var valueFromDb = valuesFromDb.Single(v => v.Id == value.Id);
                Assert.IsNotNull(valueFromDb);

                Assert.AreEqual(value.Id, valueFromDb.Id);
                Assert.AreEqual(value.PostedOn.ToLongTimeString(), valueFromDb.PostedOn.ToLongTimeString());
                Assert.AreEqual(value.Content, valueFromDb.Content);
            }

            // Act: Remove
            await repository.RemoveAsync(key).ConfigureAwait(false);
            Assert.That(async () => await repository.SaveChangesAsync().ConfigureAwait(false), Is.EqualTo(0L).After(2000));

            // Assert: Remove
            Assert.AreEqual(0, repository.GetAll(key).ToList().Count, "All values in the list should be removed.");
        }

        /// <summary>
        /// RedisKeyCollectionValuePairsRepositoryOfTweet Add multiple values in the same key and Remove them should work.
        /// </summary>
        /// <returns>Task</returns>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet Add multiple values in the same key and Remove them should work.")]
        [MaxTime(10000)]
        [Ignore("System-dependent integration test. Needs running Redis server.")]
        public async Task RedisKeyCollectionValuePairsRepositoryOfTweet_AddMultipleValuesInTheSameKeyAndRemoveThem_ShouldWork()
        {
            // Arrange
            const int NumberOfItems = 1000;
            IRedisClient client = new RedisClient(Constants.ConnectionString);
            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(client);
            var key = Guid.NewGuid().ToString();

            var values = new List<ITweet>(NumberOfItems);

            // Act: Clean-up database
            await repository.RemoveAsync(key).ConfigureAwait(false);
            await repository.SaveChangesAsync().ConfigureAwait(false);

            // Act: Add
            for (int i = 0; i < NumberOfItems; ++i)
            {
                var now = DateTime.Now + TimeSpan.FromHours(i);
                var content = $"{key} {i} {now.ToLongTimeString()}";
                var value = new Tweet
                {
                    Id = i,
                    PostedOn = now,
                    Content = content
                };

                values.Add(value);
                await repository.AddAsync(key, value).ConfigureAwait(false);
            }

            values.TrimExcess();
            await repository.SaveChangesAsync().ConfigureAwait(false);

            // Act: Get
            var valuesFromDb = repository.GetAll(key).ToList();

            // Assert: Get
            Assert.AreEqual(NumberOfItems, values.Count);
            Assert.AreEqual(NumberOfItems, valuesFromDb.Count);

            foreach (var value in values)
            {
                var valueFromDb = valuesFromDb.Single(v => v.Id == value.Id);
                Assert.IsNotNull(valueFromDb);

                Assert.AreEqual(value.Id, valueFromDb.Id);
                Assert.AreEqual(value.PostedOn.ToLongTimeString(), valueFromDb.PostedOn.ToLongTimeString());
                Assert.AreEqual(value.Content, valueFromDb.Content);
            }

            // Act: Remove
            await repository.RemoveAsync(key).ConfigureAwait(false);
            Assert.That(async () => await repository.SaveChangesAsync().ConfigureAwait(false), Is.EqualTo(0L).After(2000));

            // Assert: Remove
            Assert.AreEqual(0, repository.GetAll(key).ToList().Count, "All values in the list should be removed.");
        }

        /// <summary>
        /// RedisKeyCollectionValuePairsRepositoryOfTweet Add new valid key-value pair and then GetAll and Remove it should work.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet Add new valid key-value pair and then GetAll and Remove it should work.")]
        [MaxTime(10000)]
        [Ignore("System-dependent integration test. Needs running Redis server.")]
        public void RedisKeyCollectionValuePairsRepositoryOfTweet_AddNewValidKeyValuePairAndThenGetAllAndRemoveIt_ShouldWork()
        {
            // Arrange
            IRedisClient client = new RedisClient(Constants.ConnectionString);
            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(client);
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
            Assert.That(async () => await repository.SaveChangesAsync().ConfigureAwait(false), Is.EqualTo(0L).After(2000));

            // Act: Get
            var valuesFromDb = repository.GetAll(key);
            var valueFromDb = valuesFromDb.Single();

            // Assert: Get
            Assert.AreEqual(1, valuesFromDb.Count());

            Assert.IsNotNull(valueFromDb);

            Assert.AreEqual(value.Id, valueFromDb.Id);
            Assert.AreEqual(value.Content, valueFromDb.Content);
            Assert.AreEqual(value.PostedOn.ToLongDateString(), valueFromDb.PostedOn.ToLongDateString());
            Assert.AreEqual(value.PostedOn.ToLongTimeString(), valueFromDb.PostedOn.ToLongTimeString());

            // Act: Remove value
            var removedValue = repository.RemoveAsync(key, value);

            // Assert: Remove value
            Assert.That(async () => await removedValue.ConfigureAwait(false), Is.EqualTo(true));

            // Act: Remove
            var removed = repository.RemoveAsync(key);

            // Assert: Remove
            Assert.That(async () => await removed.ConfigureAwait(false), Is.EqualTo(true));

            // Act + Assert: SaveChanges
            // Expected internal catch of "ServiceStack.Redis.RedisResponseException : Background save already in progress"
            Assert.That(async () => await repository.SaveChangesAsync().ConfigureAwait(false), Is.EqualTo(1L));
        }

        /// <summary>
        /// RedisKeyCollectionValuePairsRepositoryOfTweet Get Keys should work.
        /// </summary>
        /// <returns>Task</returns>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet Get Keys should work.")]
        [MaxTime(10000)]
        [Ignore("System-dependent integration test. Needs running Redis server.")]
        public async Task RedisKeyCollectionValuePairsRepositoryOfTweet_GetKeys_ShouldWork()
        {
            // Arrange
            const int NumberOfKeys = 10;
            var listOfKeys = Enumerable.Range(0, NumberOfKeys).Select(i => Guid.NewGuid().ToString() + i).ToList();

            IRedisClient client = new RedisClient(Constants.ConnectionString);
            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(client);

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
        /// RedisKeyCollectionValuePairsRepositoryOfTweet with valid client provider in constructor should be initialized correctly.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet with valid client provider in constructor should be initialized correctly.")]
        [MaxTime(500)]
        public void RedisKeyCollectionValuePairsRepositoryOfTweet_WithValidClientProviderInConstructor_ShouldBeInitializedCorrectly()
        {
            // Arrange
            IRedisClient client = new RedisClient(Constants.ConnectionString);

            // Act + Assert
            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(client);
            Assert.IsNotNull(repository);

            Type baseType = typeof(RedisKeyCollectionValuePairsRepository<ITweet>).BaseType;

            var clientField = PrivateField.GetInstanceField(baseType, repository, Constants.ClientFieldName);
            var clientProperty = PrivateProperty.GetInstanceProperty(baseType, repository, Constants.ClientPropertyName);
            Assert.AreSame(client, clientField ?? clientProperty);
        }
    }
}
