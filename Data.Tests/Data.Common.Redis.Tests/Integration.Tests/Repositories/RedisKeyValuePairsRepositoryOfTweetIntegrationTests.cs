namespace ProcessingTools.Data.Common.Redis.Tests.Integration.Tests.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using Models;
    using NUnit.Framework;
    using ProcessingTools.Data.Common.Redis.Repositories;
    using ProcessingTools.Tests.Library;

    [TestFixture(Category = "Integration", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>))]
    public class RedisKeyValuePairsRepositoryOfTweetIntegrationTests
    {
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Add new valid key-value pair and then Get it and Remove it should work.")]
        [Timeout(5000)]
        [Ignore("System-dependent integration test. Needs running Redis server.")]
        public void RedisKeyValuePairsRepositoryOfTweet_AddNewValidKeyValuePairAndThenGetItAndRemoveIt_ShouldWork()
        {
            // Arrange
            var clientProvider = new RedisClientProvider();
            var repository = new RedisKeyValuePairsRepository<ITweet>(clientProvider);
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

            // Act + Assert: SaveChanges
            // Expected internal catch of "ServiceStack.Redis.RedisResponseException : Background save already in progress"
            Assert.That(async () => await repository.SaveChanges(), Is.EqualTo(1L));
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet Get Keys should work.")]
        [Timeout(10000)]
        [Ignore("System-dependent integration test. Needs running Redis server.")]
        public async Task RedisKeyValuePairsRepositoryOfTweet_GetKeys_ShouldWork()
        {
            // Arrange
            const int NumberOfKeys = 10;
            var listOfKeys = Enumerable.Range(0, NumberOfKeys)
                .Select(i => Guid.NewGuid().ToString() + i)
                .ToList();

            var clientProvider = new RedisClientProvider();
            var repository = new RedisKeyValuePairsRepository<ITweet>(clientProvider);

            var value = new Tweet
            {
                Id = 0,
                Content = Guid.NewGuid().ToString(),
                PostedOn = DateTime.UtcNow
            };

            // Act: Add
            foreach (var key in listOfKeys)
            {
                await repository.Add(key, value);
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
                await repository.Remove(key);
            }

            // Act: Get Keys
            var keysAfterRemove = repository.Keys.ToList();

            // Assert: Get Keys
            Assert.AreEqual(0, keysAfterRemove.Count, $"Number of keys after insert should be 0.");
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisKeyValuePairsRepository<ITweet>), Description = "RedisKeyValuePairsRepositoryOfTweet with valid client provider in constructor should be initialized correctly.")]
        [Timeout(500)]
        public void RedisKeyValuePairsRepositoryOfTweet_WithValidClientProviderInConstructor_ShouldBeInitializedCorrectly()
        {
            // Arrange
            var clientProvider = new RedisClientProvider();

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
