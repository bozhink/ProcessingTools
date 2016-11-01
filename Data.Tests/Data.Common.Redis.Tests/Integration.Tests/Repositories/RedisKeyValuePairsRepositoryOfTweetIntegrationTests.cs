namespace ProcessingTools.Data.Common.Redis.Tests.Integration.Tests.Repositories
{
    using System;
    using Common;
    using Models;
    using NUnit.Framework;
    using ProcessingTools.Data.Common.Redis.Repositories;
    using ProcessingTools.Tests.Library;

    [TestFixture(Category = "Integration", TestOf = typeof(RedisKeyValuePairsRepository<Tweet>))]
    public class RedisKeyValuePairsRepositoryOfTweetIntegrationTests
    {
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<Tweet>), Description = "RedisKeyValuePairsRepositoryOfTweet with valid client provider in constructor should be initialized correctly.")]
        [Timeout(500)]
        public void RedisKeyValuePairsRepositoryOfTweet_WithValidClientProviderInConstructor_ShouldBeInitializedCorrectly()
        {
            // Arrange
            var clientProvider = new RedisClientProvider();

            // Act + Assert
            var repository = new RedisKeyValuePairsRepository<Tweet>(clientProvider);
            Assert.IsNotNull(repository);

            var providerField = PrivateField.GetInstanceField<RedisKeyValuePairsRepository<Tweet>>(repository, Constants.ProviderFieldName);
            Assert.AreSame(clientProvider, providerField);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<Tweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Add new valid key-value pair and then Get it and Remove it should work.")]
        [Timeout(5000)]
        [Ignore("System-dependent integration test. Needs running Redis server.")]
        public void RedisKeyValuePairsRepositoryOfTweet_AddNewValidKeyValuePairAndThenGetItAndRemoveIt_ShouldWork()
        {
            // Arrange
            var clientProvider = new RedisClientProvider();
            var repository = new RedisKeyValuePairsRepository<Tweet>(clientProvider);
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

            // Act: Get
            var valueFromDb = repository.Get(key).Result;

            // Assert: Get
            Assert.AreEqual(value.Id, valueFromDb.Id);
            Assert.AreEqual(value.Content, valueFromDb.Content);
            Assert.AreEqual(value.PostedOn.ToLongDateString(), valueFromDb.PostedOn.ToLongDateString());
            Assert.AreEqual(value.PostedOn.ToLongTimeString(), valueFromDb.PostedOn.ToLongTimeString());

            // Act: Remove
            var removed = repository.Remove(key);

            // Assert: Remove
            Assert.That(async () => await removed, Is.EqualTo(true));
        }
    }
}
