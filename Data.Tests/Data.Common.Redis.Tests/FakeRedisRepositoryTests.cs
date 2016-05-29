namespace ProcessingTools.Data.Common.Redis.Tests
{
    using System;
    using System.Linq;

    using Fakes.Models;
    using Fakes.Repositories;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FakeRedisRepositoryTests
    {
        [TestMethod]
        [Ignore]
        public void FakeRedisRepository_WithDefaultConstructor_ShouldReturnValidObject()
        {
            var repository = new FakeRedisRepository();
            Assert.IsNotNull(repository, "Repository schould not be null.");
        }

        [TestMethod]
        [Timeout(2000)]
        [Ignore]
        public void FakeRedisRepository_AddEntityDeleteEntity_ShouldWork()
        {
            string key = Guid.NewGuid().ToString();

            var repository = new FakeRedisRepository();
            Assert.IsNotNull(repository, "Repository schould not be null.");

            repository.Delete(key).Wait();
            repository.SaveChanges(key).Wait();

            var now = DateTime.Now;
            var value = $"{key} {now.ToLongTimeString()}";
            var entity = new SimpleTimeRecordModel
            {
                LastUpdate = now,
                Value = value
            };

            repository.Add(key, entity).Wait();
            repository.SaveChanges(key).Wait();

            var items = repository.All(key).Result.ToList();

            Assert.AreEqual(1, items.Count, "Number of items should be 1.");

            var item = items.FirstOrDefault();

            Assert.AreEqual(1, item.Id, "item.Id should be 1.");
            Assert.AreEqual(now.ToLongTimeString(), item.LastUpdate.ToLongTimeString(), "item.LastUpdate should match.");
            Assert.AreEqual(value, item.Value, "item.Value schould match.");

            repository.Delete(key, entity).Wait();
            repository.SaveChanges(key).Wait();

            Assert.AreEqual(0, repository.All(key).Result.ToList().Count, "Number of items after deletion should be 0.");
        }

        [TestMethod]
        [Timeout(2000)]
        [Ignore]
        public void FakeRedisRepository_AddEntityDeleteByIdEntity_ShouldWork()
        {
            string key = Guid.NewGuid().ToString();

            var repository = new FakeRedisRepository();
            Assert.IsNotNull(repository, "Repository schould not be null.");

            repository.Delete(key).Wait();
            repository.SaveChanges(key).Wait();

            var now = DateTime.Now;
            var value = $"{key} {now.ToLongTimeString()}";
            var entity = new SimpleTimeRecordModel
            {
                LastUpdate = now,
                Value = value
            };

            repository.Add(key, entity).Wait();

            Assert.IsTrue(entity.Id > 0, "entity.Id should be grater than 0.");

            var items = repository.All(key).Result.ToList();
            repository.SaveChanges(key).Wait();

            Assert.AreEqual(1, items.Count, "Number of items should be 1.");

            var item = items.FirstOrDefault();

            Assert.AreEqual(1, item.Id, "item.Id should be 1.");
            Assert.AreEqual(now.ToLongTimeString(), item.LastUpdate.ToLongTimeString(), "item.LastUpdate should match.");
            Assert.AreEqual(value, item.Value, "item.Value schould match.");

            repository.Delete(key, entity.Id).Wait();
            repository.SaveChanges(key).Wait();

            Assert.AreEqual(0, repository.All(key).Result.ToList().Count, "Number of items after deletion should be 0.");
        }

        [TestMethod]
        [Timeout(2000)]
        [Ignore]
        public void FakeRedisRepository_AddMultipleEntitiesInSameKey_ShouldWork()
        {
            const int NumberOfItems = 100;

            var repository = new FakeRedisRepository();
            Assert.IsNotNull(repository, "Repository schould not be null.");

            string key = Guid.NewGuid().ToString();

            repository.Delete(key).Wait();
            repository.SaveChanges(key).Wait();

            for (int i = 0; i < NumberOfItems; ++i)
            {
                var now = DateTime.Now;
                var value = $"{key} {i} {now.ToLongTimeString()}";
                var entity = new SimpleTimeRecordModel
                {
                    LastUpdate = now,
                    Value = value
                };

                repository.Add(key, entity).Wait();
                repository.SaveChanges(key).Wait();
            }

            var items = repository.All(key).Result.ToList();
            Assert.AreEqual(NumberOfItems, items.Count, $"Number of items should be {NumberOfItems}.");

            repository.Delete(key);
        }
    }
}