namespace ProcessingTools.Data.Common.Memory.Tests.Integration.Tests
{
    using System;
    using System.Linq;
    using Models;
    using NUnit.Framework;
    using ProcessingTools.Data.Common.Memory;

    [TestFixture(Category = "Integration", TestOf = typeof(MemoryKeyValueDataStore<IKeyModel, IValueModel>))]
    public class MemoryKeyValueDataStoreIntegrationTests
    {
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(MemoryKeyValueDataStore<IKeyModel, IValueModel>), Description = "MemoryKeyValueDataStore Add Get Remove of one key should work.")]
        [Timeout(5000)]
        public void MemoryKeyValueDataStore_AddGetRemoveOfOneKey_ShouldWork()
        {
            // Arrange
            var db = new MemoryKeyValueDataStore<IKeyModel, IValueModel>();
            var key = new KeyModel
            {
                Id = 7432982
            };

            var value = new ValueModel
            {
                Id = 234234,
                Content = Guid.NewGuid().ToString()
            };

            // Act: AddOrUpdate
            var result = db.AddOrUpdate(key, k => value, (k, v) => value);

            // Assert: AddOrUpdate
            Assert.AreSame(value, result, "Returned after addition value should be same as the added.");

            // Act: Keys
            var keysAfterAdd = db.Keys.ToList();

            // Assert: Keys
            Assert.IsTrue(keysAfterAdd.Contains(key), "Added key should be present in the Keys list.");

            // Act: Get
            var valueFromDb = db[key];

            // Assert: Get
            Assert.AreSame(value, valueFromDb, "Indexer should return the added object.");

            // Act: Remove
            var removeResult = db.Remove(key);

            // Assert: Remove
            Assert.IsTrue(removeResult, "Key should be removed.");

            // Act: Keys
            var keysAfterRemove = db.Keys.ToList();

            // Assert: Keys
            Assert.IsFalse(keysAfterRemove.Contains(key), "Removed key should not be present in the Keys list.");
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(MemoryKeyValueDataStore<IKeyModel, IValueModel>), Description = "MemoryKeyValueDataStore Add Update Get Remove of one key should work.")]
        [Timeout(5000)]
        public void MemoryKeyValueDataStore_AddUpdateGetRemoveOfOneKey_ShouldWork()
        {
            // Arrange
            var db = new MemoryKeyValueDataStore<IKeyModel, IValueModel>();
            var key = new KeyModel
            {
                Id = DateTime.Now.Millisecond
            };

            var value1 = new ValueModel
            {
                Id = 234234 + DateTime.Now.Millisecond,
                Content = Guid.NewGuid().ToString()
            };

            var value2 = new ValueModel
            {
                Id = 5354 + DateTime.Now.Millisecond,
                Content = Guid.NewGuid().ToString()
            };

            // Act: AddOrUpdate
            var addResult = db.AddOrUpdate(key, k => value1, (k, v) => value1);

            // Assert: AddOrUpdate
            Assert.AreSame(value1, addResult, "Returned after addition value should be same as the added.");

            // Act: AddOrUpdate
            var updateResult = db.AddOrUpdate(key, k => value2, (k, v) => value2);

            // Assert: AddOrUpdate
            Assert.AreSame(value2, updateResult, "Returned after update value should be same as the updated.");

            // Act: Keys
            var keysAfterAdd = db.Keys.ToList();

            // Assert: Keys
            Assert.IsTrue(keysAfterAdd.Contains(key), "Added key should be present in the Keys list.");

            // Act: Get
            var valueFromDb = db[key];

            // Assert: Get
            Assert.AreSame(value2, valueFromDb, "Indexer should return the added object.");

            // Act: Remove
            var removeResult = db.Remove(key);

            // Assert: Remove
            Assert.IsTrue(removeResult, "Key should be removed.");

            // Act: Keys
            var keysAfterRemove = db.Keys.ToList();

            // Assert: Keys
            Assert.IsFalse(keysAfterRemove.Contains(key), "Removed key should not be present in the Keys list.");
        }
    }
}
