namespace ProcessingTools.Cache.Data.Redis.Tests.Integration.Tests.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using ProcessingTools.Cache.Data.Redis.Repositories;
    using ProcessingTools.Cache.Data.Redis.Tests.Common;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Data.Models.Cache.Redis;
    using ProcessingTools.Models.Contracts.Cache;
    using ProcessingTools.Tests.Library;

    [TestFixture(Category = "Integration", TestOf = typeof(RedisValidationCacheDataRepository))]
    public class RedisValidationCacheDataRepositoryIntegrationTests
    {
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisValidationCacheDataRepository), Description = "RedisValidationCacheDataRepository Add and Remove key-value pair should work.")]
        [Timeout(10000)]
        [Ignore("System-dependent integration test. Needs running Redis server.")]
        public async Task RedisValidationCacheDataRepository_AddAndRemoveKeyValuePair_ShouldWork()
        {
            // Arrange
            var clientProvider = new RedisClientProvider();
            var repository = new RedisValidationCacheDataRepository(clientProvider);
            var key = Guid.NewGuid().ToString();

            var now = DateTime.Now;
            var value = new ValidationCacheEntity
            {
                Content = $"{key} {now.ToLongTimeString()}",
                LastUpdate = now,
                Status = ValidationStatus.Valid
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
            Assert.AreEqual(ValidationStatus.Valid, valueFromDb.Status);
            Assert.AreEqual(now.ToLongTimeString(), valueFromDb.LastUpdate.ToLongTimeString());
            Assert.AreEqual(value.Content, valueFromDb.Content);

            // Act: Remove
            await repository.RemoveAsync(key).ConfigureAwait(false);
            Assert.That(async () => await repository.SaveChangesAsync().ConfigureAwait(false), Is.EqualTo(0L).After(2000));

            // Assert: Remove
            Assert.AreEqual(0, repository.GetAll(key).ToList().Count, "All values in the list should be removed.");
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisValidationCacheDataRepository), Description = "RedisValidationCacheDataRepository Add multiple values in the same key and Remove one should work.")]
        [Timeout(10000)]
        [Ignore("System-dependent integration test. Needs running Redis server.")]
        public async Task RedisValidationCacheDataRepository_AddMultipleValuesInTheSameKeyAndRemoveOne_ShouldWork()
        {
            // Arrange
            const int NumberOfItems = 100;
            var clientProvider = new RedisClientProvider();
            var repository = new RedisValidationCacheDataRepository(clientProvider);
            var key = Guid.NewGuid().ToString();

            var values = new List<IValidationCacheModel>(NumberOfItems);

            // Act: Clean-up database
            await repository.RemoveAsync(key).ConfigureAwait(false);
            await repository.SaveChangesAsync().ConfigureAwait(false);

            // Act: Add
            for (int i = 0; i < NumberOfItems; ++i)
            {
                var now = DateTime.Now + TimeSpan.FromHours(i);
                var content = $"{key} {i} {now.ToLongTimeString()}";
                var value = new ValidationCacheEntity
                {
                    Content = content,
                    LastUpdate = now,
                    Status = ValidationStatus.Valid
                };

                values.Add(value);
                await repository.AddAsync(key, value).ConfigureAwait(false);
            }

            var valueToBeDeleted = new ValidationCacheEntity
            {
                Content = "To be removed",
                LastUpdate = DateTime.Now,
                Status = ValidationStatus.Undefined
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

            Assert.IsFalse(valuesFromDb.Any(v => v.Status != ValidationStatus.Valid));

            foreach (var value in values)
            {
                var valueFromDb = valuesFromDb.Single(v => v.Content == value.Content);
                Assert.IsNotNull(valueFromDb);

                Assert.AreEqual(value.Status, valueFromDb.Status);
                Assert.AreEqual(value.LastUpdate.ToLongTimeString(), valueFromDb.LastUpdate.ToLongTimeString());
                Assert.AreEqual(value.Content, valueFromDb.Content);
            }

            // Act: Remove
            await repository.RemoveAsync(key).ConfigureAwait(false);
            Assert.That(async () => await repository.SaveChangesAsync().ConfigureAwait(false), Is.EqualTo(0L).After(2000));

            // Assert: Remove
            Assert.AreEqual(0, repository.GetAll(key).ToList().Count, "All values in the list should be removed.");
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisValidationCacheDataRepository), Description = "RedisValidationCacheDataRepository Add multiple values in the same key and Remove them should work.")]
        [Timeout(10000)]
        [Ignore("System-dependent integration test. Needs running Redis server.")]
        public async Task RedisValidationCacheDataRepository_AddMultipleValuesInTheSameKeyAndRemoveThem_ShouldWork()
        {
            // Arrange
            const int NumberOfItems = 100;
            var clientProvider = new RedisClientProvider();
            var repository = new RedisValidationCacheDataRepository(clientProvider);
            var key = Guid.NewGuid().ToString();

            var values = new List<IValidationCacheModel>(NumberOfItems);

            // Act: Clean-up database
            await repository.RemoveAsync(key).ConfigureAwait(false);
            await repository.SaveChangesAsync().ConfigureAwait(false);

            // Act: Add
            for (int i = 0; i < NumberOfItems; ++i)
            {
                var now = DateTime.Now + TimeSpan.FromHours(i);
                var content = $"{key} {i} {now.ToLongTimeString()}";
                var value = new ValidationCacheEntity
                {
                    Content = content,
                    LastUpdate = now,
                    Status = (ValidationStatus)(i % 3)
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
                var valueFromDb = valuesFromDb.Single(v => v.Content == value.Content);
                Assert.IsNotNull(valueFromDb);

                Assert.AreEqual(value.Status, valueFromDb.Status);
                Assert.AreEqual(value.LastUpdate.ToLongTimeString(), valueFromDb.LastUpdate.ToLongTimeString());
                Assert.AreEqual(value.Content, valueFromDb.Content);
            }

            // Act: Remove
            await repository.RemoveAsync(key).ConfigureAwait(false);
            Assert.That(async () => await repository.SaveChangesAsync().ConfigureAwait(false), Is.EqualTo(0L).After(2000));

            // Assert: Remove
            Assert.AreEqual(0, repository.GetAll(key).ToList().Count, "All values in the list should be removed.");
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisValidationCacheDataRepository), Description = "RedisValidationCacheDataRepository Add new valid key-value pair and then GetAll and Remove it should work.")]
        [Timeout(10000)]
        [Ignore("System-dependent integration test. Needs running Redis server.")]
        public void RedisValidationCacheDataRepository_AddNewValidKeyValuePairAndThenGetAllAndRemoveIt_ShouldWork()
        {
            // Arrange
            var clientProvider = new RedisClientProvider();
            var repository = new RedisValidationCacheDataRepository(clientProvider);
            var key = Guid.NewGuid().ToString();
            var value = new ValidationCacheEntity
            {
                Content = Guid.NewGuid().ToString(),
                LastUpdate = DateTime.UtcNow,
                Status = ValidationStatus.Invalid
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

            Assert.AreEqual(value.Status, valueFromDb.Status);
            Assert.AreEqual(value.Content, valueFromDb.Content);
            Assert.AreEqual(value.LastUpdate.ToLongDateString(), valueFromDb.LastUpdate.ToLongDateString());
            Assert.AreEqual(value.LastUpdate.ToLongTimeString(), valueFromDb.LastUpdate.ToLongTimeString());

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

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisValidationCacheDataRepository), Description = "RedisValidationCacheDataRepository Get Keys should work.")]
        [Timeout(10000)]
        [Ignore("System-dependent integration test. Needs running Redis server.")]
        public async Task RedisValidationCacheDataRepository_GetKeys_ShouldWork()
        {
            // Arrange
            const int NumberOfKeys = 10;
            var listOfKeys = Enumerable.Range(0, NumberOfKeys)
                .Select(i => Guid.NewGuid().ToString() + i)
                .ToList();

            var clientProvider = new RedisClientProvider();
            var repository = new RedisValidationCacheDataRepository(clientProvider);

            var value = new ValidationCacheEntity
            {
                Content = Guid.NewGuid().ToString(),
                LastUpdate = DateTime.UtcNow,
                Status = ValidationStatus.Valid
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

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(RedisValidationCacheDataRepository), Description = "RedisValidationCacheDataRepository with valid client provider in constructor should be initialized correctly.")]
        [Timeout(500)]
        public void RedisValidationCacheDataRepository_WithValidClientProviderInConstructor_ShouldBeInitializedCorrectly()
        {
            // Arrange
            var clientProvider = new RedisClientProvider();

            // Act + Assert
            var repository = new RedisValidationCacheDataRepository(clientProvider);
            Assert.IsNotNull(repository);

            var providerField = PrivateField.GetInstanceField(
                typeof(RedisValidationCacheDataRepository).BaseType.BaseType,
                repository,
                Constants.ClientProviderFieldName);
            Assert.AreSame(clientProvider, providerField);
        }
    }
}
