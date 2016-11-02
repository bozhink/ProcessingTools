namespace ProcessingTools.Data.Common.Redis.Tests.Integration.Tests.Repositories
{
    using System;
    using System.Linq;
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
            Assert.That(async () => await repository.SaveChanges(), Is.EqualTo(0L));

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
    }
}
