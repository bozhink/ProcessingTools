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
        [Ignore]
        public void FakeRedisRepository_AddEntityDeleteEntity_ShouldWork()
        {
            const string Key = "Items 0";

            var repository = new FakeRedisRepository();
            Assert.IsNotNull(repository, "Repository schould not be null.");

            var now = DateTime.Now;
            var value = $"{Key} {now.ToLongTimeString()}";
            var entity = new SimpleTimeRecordModel
            {
                LastUpdate = now,
                Value = value
            };

            repository.Delete(Key);

            repository.Add(Key, entity).Wait();

            var items = repository.All(Key).Result.ToList();

            repository.Delete(Key, entity).Wait();

            Console.WriteLine("After deletion mark.");

            Assert.AreEqual(1, items.Count, "Number of items should be 1.");

            var item = items.FirstOrDefault();

            Assert.AreEqual(1, item.Id, "item.Id should be 1.");
            Assert.AreEqual(now.ToLongTimeString(), item.LastUpdate.ToLongTimeString(), "item.LastUpdate should match.");
            Assert.AreEqual(value, item.Value, "item.Value schould match.");

            Assert.AreEqual(0, repository.All(Key).Result.ToList().Count, "Number of items after deletion should be 0.");
        }

        [TestMethod]
        [Ignore]
        public void FakeRedisRepository_AddEntityDeleteByIdEntity_ShouldWork()
        {
            const string Key = "Items 1";

            var repository = new FakeRedisRepository();
            Assert.IsNotNull(repository, "Repository schould not be null.");

            var now = DateTime.Now;
            var value = $"{Key} {now.ToLongTimeString()}";
            var entity = new SimpleTimeRecordModel
            {
                LastUpdate = now,
                Value = value
            };

            repository.Delete(Key);

            repository.Add(Key, entity).Wait();

            var items = repository.All(Key).Result.ToList();

            repository.Delete(Key, entity.Id).Wait();

            Console.WriteLine("After deletion mark.");

            Assert.AreEqual(1, items.Count, "Number of items should be 1.");

            var item = items.FirstOrDefault();

            Assert.AreEqual(1, item.Id, "item.Id should be 1.");
            Assert.AreEqual(now.ToLongTimeString(), item.LastUpdate.ToLongTimeString(), "item.LastUpdate should match.");
            Assert.AreEqual(value, item.Value, "item.Value schould match.");

            Assert.AreEqual(0, repository.All(Key).Result.ToList().Count, "Number of items after deletion should be 0.");
        }

        [TestMethod]
        [Ignore]
        public void FakeRedisRepository_AddMultipleEntitiesInSameKey_ShouldWork()
        {
            const string Key = "Items 2";
            const int NumberOfItems = 100;

            var repository = new FakeRedisRepository();
            Assert.IsNotNull(repository, "Repository schould not be null.");

            repository.Delete(Key);

            for (int i = 0; i < NumberOfItems; ++i)
            {
                var now = DateTime.Now;
                var value = $"{Key} {i} {now.ToLongTimeString()}";
                var entity = new SimpleTimeRecordModel
                {
                    LastUpdate = now,
                    Value = value
                };

                repository.Add(Key, entity).Wait();
            }

            var items = repository.All(Key).Result.ToList();

            repository.Delete(Key);

            Assert.AreEqual(NumberOfItems, items.Count, $"Number of items should be {NumberOfItems}.");
        }
    }
}