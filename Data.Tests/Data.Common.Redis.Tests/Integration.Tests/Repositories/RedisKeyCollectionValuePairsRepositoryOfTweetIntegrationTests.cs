namespace ProcessingTools.Data.Common.Redis.Tests.Integration.Tests.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using Models;
    using NUnit.Framework;
    using ProcessingTools.Data.Common.Redis.Repositories;
    using ProcessingTools.Tests.Library;

    [TestFixture(Category = "Integration", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>))]
    public class RedisKeyCollectionValuePairsRepositoryOfTweetIntegrationTests
    {
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet with valid client provider in constructor should be initialized correctly.")]
        [Timeout(500)]
        public void RedisKeyCollectionValuePairsRepositoryOfTweet_WithValidClientProviderInConstructor_ShouldBeInitializedCorrectly()
        {
            // Arrange
            var clientProvider = new RedisClientProvider();

            // Act + Assert
            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(clientProvider);
            Assert.IsNotNull(repository);

            var providerField = PrivateField.GetInstanceField(
                typeof(RedisKeyCollectionValuePairsRepository<ITweet>).BaseType,
                repository,
                Constants.ClientProviderFieldName);
            Assert.AreSame(clientProvider, providerField);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet Add new valid key-value pair and then GetAll and Remove it should work.")]
        [Timeout(10000)]
        [Ignore("System-dependent integration test. Needs running Redis server.")]
        public void RedisKeyCollectionValuePairsRepositoryOfTweet_AddNewValidKeyValuePairAndThenGetAllAndRemoveIt_ShouldWork()
        {
            // Arrange
            var clientProvider = new RedisClientProvider();
            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(clientProvider);
            var key = Guid.NewGuid().ToString();
            var value = new Tweet
            {
                Id = 0,
                Content = Guid.NewGuid().ToString(),
                PostedOn = DateTime.UtcNow
            };

            // Act: Add
            var added = repository.Add(key, value);

            // Assert: Add
            Assert.That(async () => await added, Is.EqualTo(true));

            // Act + Assert: SaveChanges
            Assert.That(async () => await repository.SaveChanges(), Is.EqualTo(0L).After(2000));

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
            var removedValue = repository.Remove(key, value);

            // Assert: Remove value
            Assert.That(async () => await removedValue, Is.EqualTo(true));

            // Act: Remove
            var removed = repository.Remove(key);

            // Assert: Remove
            Assert.That(async () => await removed, Is.EqualTo(true));

            // Act + Assert: SaveChanges
            // Expected internal catch of "ServiceStack.Redis.RedisResponseException : Background save already in progress"
            Assert.That(async () => await repository.SaveChanges(), Is.EqualTo(1L));
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet Add and Remove key-value pair should work.")]
        [Timeout(10000)]
        [Ignore("System-dependent integration test. Needs running Redis server.")]
        public async Task RedisKeyCollectionValuePairsRepositoryOfTweet_AddAndRemoveKeyValuePair_ShouldWork()
        {
            // Arrange
            var clientProvider = new RedisClientProvider();
            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(clientProvider);
            var key = Guid.NewGuid().ToString();

            var now = DateTime.Now;
            var value = new Tweet
            {
                Id = 1,
                PostedOn = now,
                Content = $"{key} {now.ToLongTimeString()}"
            };

            // Act: Clean-up database
            await repository.Remove(key);
            await repository.SaveChanges();

            // Act: Add
            await repository.Add(key, value);
            await repository.SaveChanges();

            // Act + Assert: Retrieve data
            var valuesFromDb = repository.GetAll(key).ToList();
            Assert.AreEqual(1, valuesFromDb.Count);

            var valueFromDb = valuesFromDb.Single();
            Assert.AreEqual(1, valueFromDb.Id);
            Assert.AreEqual(now.ToLongTimeString(), valueFromDb.PostedOn.ToLongTimeString());
            Assert.AreEqual(value.Content, valueFromDb.Content);

            // Act: Remove
            await repository.Remove(key);
            Assert.That(async () => await repository.SaveChanges(), Is.EqualTo(0L).After(2000));

            // Assert: Remove
            Assert.AreEqual(0, repository.GetAll(key).ToList().Count, "All values in the list should be removed.");
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet Add multiple values in the same key and Remove them should work.")]
        [Timeout(10000)]
        [Ignore("System-dependent integration test. Needs running Redis server.")]
        public async Task RedisKeyCollectionValuePairsRepositoryOfTweet_AddMultipleValuesInTheSameKeyAndRemoveThem_ShouldWork()
        {
            // Arrange
            const int NumberOfItems = 1000;
            var clientProvider = new RedisClientProvider();
            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(clientProvider);
            var key = Guid.NewGuid().ToString();

            var values = new List<ITweet>(NumberOfItems);

            // Act: Clean-up database
            await repository.Remove(key);
            await repository.SaveChanges();

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
                await repository.Add(key, value);
            }

            values.TrimExcess();
            await repository.SaveChanges();

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
            await repository.Remove(key);
            Assert.That(async () => await repository.SaveChanges(), Is.EqualTo(0L).After(2000));

            // Assert: Remove
            Assert.AreEqual(0, repository.GetAll(key).ToList().Count, "All values in the list should be removed.");
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyCollectionValuePairsRepository<ITweet>), Description = "RedisKeyCollectionValuePairsRepositoryOfTweet Add multiple values in the same key and Remove one should work.")]
        [Timeout(10000)]
        [Ignore("System-dependent integration test. Needs running Redis server.")]
        public async Task RedisKeyCollectionValuePairsRepositoryOfTweet_AddMultipleValuesInTheSameKeyAndRemoveOne_ShouldWork()
        {
            // Arrange
            const int NumberOfItems = 1000;
            var clientProvider = new RedisClientProvider();
            var repository = new RedisKeyCollectionValuePairsRepository<ITweet>(clientProvider);
            var key = Guid.NewGuid().ToString();

            var values = new List<ITweet>(NumberOfItems);

            // Act: Clean-up database
            await repository.Remove(key);
            await repository.SaveChanges();

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
                await repository.Add(key, value);
            }

            var valueToBeDeleted = new Tweet
            {
                Id = -1,
                Content = "To be removed",
                PostedOn = DateTime.Now
            };

            await repository.Add(key, valueToBeDeleted);

            values.TrimExcess();
            await repository.SaveChanges();

            // Assert: Get
            Assert.AreEqual(NumberOfItems, values.Count);
            Assert.AreEqual(NumberOfItems + 1, repository.GetAll(key).ToList().Count);

            // Act: Remove value
            var removed = await repository.Remove(key, valueToBeDeleted);

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
            await repository.Remove(key);
            Assert.That(async () => await repository.SaveChanges(), Is.EqualTo(0L).After(2000));

            // Assert: Remove
            Assert.AreEqual(0, repository.GetAll(key).ToList().Count, "All values in the list should be removed.");
        }
    }
}
