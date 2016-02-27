namespace ProcessingTools.Services.Cache.Tests.UnitTests
{
    using System;
    using System.Linq;

    using Fakes;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;
    using Moq;
    using ProcessingTools.Cache.Data.Repositories.Contracts;
    using ProcessingTools.Contracts.Types;

    [TestClass]
    public class ValidationCacheServiceTests
    {
        private const string ServiceShouldNotBeNullMessage = "Service should not be null.";
        private const string StatusShouldMatchMessage = "Status should match.";
        private const string ContentShouldMatchMessage = "Content should match.";
        private const string IdShouldMatchMessage = "Id should match.";
        private const string IdShouldBeUnchagedMessage = "Id should be unchaged.";
        private const string IdShouldHaveMaximalValueMessage = "Id should have the maximal value.";
        private const string NumberOfItemsAfterAdditionMessage = "Number of items after addition should be incremented by 1.";
        private const string NumberOfItemsAfterDeletionMessage = "Number of items after deletion should be equal to the initial number of items.";
        private const string AddedItemShouldBeValidObjectMessage = "Item should be added as a valid object.";
        private const string ItemShouldBeNullMessage = "Item should be null.";

        private static readonly Random Random = new Random();

        private IValidationCacheDataRepository mockedRepository;
        private IValidationCacheDataRepository fakeRepository;

        [TestInitialize]
        public void Initialize()
        {
            var repositoryMock = new Mock<IValidationCacheDataRepository>();
            this.mockedRepository = repositoryMock.Object;

            this.fakeRepository = new FakeValidationCacheDataRepository();
        }

        [TestMethod]
        public void ValidationCacheService_WithValidDefaultConstructor_ShouldReturnValidObject()
        {
            var service = new ValidationCacheService(this.mockedRepository);
            Assert.IsNotNull(service, ServiceShouldNotBeNullMessage);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidationCacheService_WithNullInDefaultConstructor_ShouldThrow()
        {
            var service = new ValidationCacheService(null);
        }

        [TestMethod]
        public void ValidationCacheService_AddNewItemWithUndefinedStatus_ShouldWork()
        {
            string key = Guid.NewGuid().ToString();

            var service = new ValidationCacheService(this.fakeRepository);
            Assert.IsNotNull(service, ServiceShouldNotBeNullMessage);

            int initialNumberOfItems = service.All(key).Result.ToList().Count;

            int id = Random.Next();
            var now = DateTime.Now;
            var content = $"Content {now.ToLongTimeString()}";
            var item = new ValidationCacheServiceModel
            {
                Id = id,
                Content = content,
                LastUpdate = now,
                Status = ValidationStatus.Undefined
            };

            service.Add(key, item).Wait();
            Assert.AreEqual(id, item.Id, IdShouldBeUnchagedMessage);

            int numberOfItemsAfterAddition = service.All(key).Result.ToList().Count;

            Assert.AreEqual(initialNumberOfItems + 1, numberOfItemsAfterAddition, NumberOfItemsAfterAdditionMessage);

            var itemAfterAddition = service.All(key).Result.FirstOrDefault(i => i.LastUpdate == now);
            Assert.IsNotNull(itemAfterAddition, AddedItemShouldBeValidObjectMessage);
            Assert.AreEqual(numberOfItemsAfterAddition, itemAfterAddition.Id, IdShouldHaveMaximalValueMessage);
            Assert.AreEqual(ValidationStatus.Undefined, itemAfterAddition.Status, StatusShouldMatchMessage);
            Assert.AreEqual(content, itemAfterAddition.Content, ContentShouldMatchMessage);
        }

        [TestMethod]
        public void ValidationCacheService_AddNewItemWithValidStatus_ShouldWork()
        {
            string key = Guid.NewGuid().ToString();

            var service = new ValidationCacheService(this.fakeRepository);
            Assert.IsNotNull(service, ServiceShouldNotBeNullMessage);

            int initialNumberOfItems = service.All(key).Result.ToList().Count;

            int id = Random.Next();
            var now = DateTime.Now;
            var content = $"Content {now.ToLongTimeString()}";
            var item = new ValidationCacheServiceModel
            {
                Id = id,
                Content = content,
                LastUpdate = now,
                Status = ValidationStatus.Valid
            };

            service.Add(key, item).Wait();
            Assert.AreEqual(id, item.Id, IdShouldBeUnchagedMessage);

            int numberOfItemsAfterAddition = service.All(key).Result.ToList().Count;

            Assert.AreEqual(initialNumberOfItems + 1, numberOfItemsAfterAddition, NumberOfItemsAfterAdditionMessage);

            var itemAfterAddition = service.All(key).Result.FirstOrDefault(i => i.LastUpdate == now);
            Assert.IsNotNull(itemAfterAddition, AddedItemShouldBeValidObjectMessage);
            Assert.AreEqual(numberOfItemsAfterAddition, itemAfterAddition.Id, IdShouldHaveMaximalValueMessage);
            Assert.AreEqual(ValidationStatus.Valid, itemAfterAddition.Status, StatusShouldMatchMessage);
            Assert.AreEqual(content, itemAfterAddition.Content, ContentShouldMatchMessage);
        }

        [TestMethod]
        public void ValidationCacheService_AddNewItemWithInValidStatus_ShouldWork()
        {
            string key = Guid.NewGuid().ToString();

            var service = new ValidationCacheService(this.fakeRepository);
            Assert.IsNotNull(service, ServiceShouldNotBeNullMessage);

            int initialNumberOfItems = service.All(key).Result.ToList().Count;

            int id = Random.Next();
            var now = DateTime.Now;
            var content = $"Content {now.ToLongTimeString()}";
            var item = new ValidationCacheServiceModel
            {
                Id = id,
                Content = content,
                LastUpdate = now,
                Status = ValidationStatus.Invalid
            };

            service.Add(key, item).Wait();
            Assert.AreEqual(id, item.Id, IdShouldBeUnchagedMessage);

            int numberOfItemsAfterAddition = service.All(key).Result.ToList().Count;

            Assert.AreEqual(initialNumberOfItems + 1, numberOfItemsAfterAddition, NumberOfItemsAfterAdditionMessage);

            var itemAfterAddition = service.All(key).Result.FirstOrDefault(i => i.LastUpdate == now);
            Assert.IsNotNull(itemAfterAddition, AddedItemShouldBeValidObjectMessage);
            Assert.AreEqual(numberOfItemsAfterAddition, itemAfterAddition.Id, IdShouldHaveMaximalValueMessage);
            Assert.AreEqual(ValidationStatus.Invalid, itemAfterAddition.Status, StatusShouldMatchMessage);
            Assert.AreEqual(content, itemAfterAddition.Content, ContentShouldMatchMessage);
        }

        [TestMethod]
        public void ValidationCacheService_AddNewMultipleItems_ShouldWork()
        {
            const int NumberOfItems = 100;
            string key = Guid.NewGuid().ToString();

            var service = new ValidationCacheService(this.fakeRepository);
            Assert.IsNotNull(service, ServiceShouldNotBeNullMessage);

            int initialNumberOfItems = service.All(key).Result.ToList().Count;

            for (int i = 0; i < NumberOfItems; ++i)
            {
                int id = Random.Next();
                var now = DateTime.Now;
                var content = $"Content {now.ToLongTimeString()}";
                var item = new ValidationCacheServiceModel
                {
                    Id = id,
                    Content = content,
                    LastUpdate = now,
                    Status = ValidationStatus.Undefined
                };

                service.Add(key, item).Wait();

                Assert.AreEqual(id, item.Id, IdShouldBeUnchagedMessage);
            }

            int numberOfItemsAfterAddition = service.All(key).Result.ToList().Count;

            Assert.AreEqual(initialNumberOfItems + NumberOfItems, numberOfItemsAfterAddition, $"Number of items after addition should be incremented by {NumberOfItems}.");
        }

        [TestMethod]
        public void ValidationCacheService_AddItemDeleteItem_ShouldWork()
        {
            string key = Guid.NewGuid().ToString();

            var service = new ValidationCacheService(this.fakeRepository);
            Assert.IsNotNull(service, ServiceShouldNotBeNullMessage);

            int initialNumberOfItems = service.All(key).Result.ToList().Count;

            int id = Random.Next();
            var now = DateTime.Now;
            var content = $"Content {now.ToLongTimeString()}";
            var item = new ValidationCacheServiceModel
            {
                Id = id,
                Content = content,
                LastUpdate = now,
                Status = ValidationStatus.Undefined
            };

            service.Add(key, item).Wait();

            Assert.AreEqual(id, item.Id, IdShouldBeUnchagedMessage);

            int numberOfItemsAfterAddition = service.All(key).Result.ToList().Count;

            Assert.AreEqual(initialNumberOfItems + 1, numberOfItemsAfterAddition, NumberOfItemsAfterAdditionMessage);

            var itemAfterAddition = service.All(key).Result.FirstOrDefault(i => i.LastUpdate == now);
            Assert.IsNotNull(itemAfterAddition, AddedItemShouldBeValidObjectMessage);

            // Here we delete the item from the database, because added item’s Id might not be equal to 
            // the Id of the cooresponding entity in the database.
            service.Delete(key, itemAfterAddition).Wait();

            int numberOfItemsAfterDeletion = service.All(key).Result.ToList().Count;
            Assert.AreEqual(initialNumberOfItems, numberOfItemsAfterDeletion, NumberOfItemsAfterDeletionMessage);

            var itemAfterDeletion = service.All(key).Result.FirstOrDefault(i => i.LastUpdate == now);
            Assert.IsNull(itemAfterDeletion, ItemShouldBeNullMessage);
        }

        [TestMethod]
        public void ValidationCacheService_AddItemDeleteItemById_ShouldWork()
        {
            string key = Guid.NewGuid().ToString();

            var service = new ValidationCacheService(this.fakeRepository);
            Assert.IsNotNull(service, ServiceShouldNotBeNullMessage);

            int initialNumberOfItems = service.All(key).Result.ToList().Count;

            int id = Random.Next();
            var now = DateTime.Now;
            var content = $"Content {now.ToLongTimeString()}";
            var item = new ValidationCacheServiceModel
            {
                Id = id,
                Content = content,
                LastUpdate = now,
                Status = ValidationStatus.Valid
            };

            service.Add(key, item).Wait();

            Assert.AreEqual(id, item.Id, IdShouldBeUnchagedMessage);

            int numberOfItemsAfterAddition = service.All(key).Result.ToList().Count;

            Assert.AreEqual(initialNumberOfItems + 1, numberOfItemsAfterAddition, NumberOfItemsAfterAdditionMessage);

            var itemAfterAddition = service.All(key).Result.FirstOrDefault(i => i.LastUpdate == now);
            Assert.IsNotNull(itemAfterAddition, AddedItemShouldBeValidObjectMessage);

            // Here we delete the item from the database, because added item’s Id might not be equal to 
            // the Id of the cooresponding entity in the database.
            service.Delete(key, itemAfterAddition.Id).Wait();

            int numberOfItemsAfterDeletion = service.All(key).Result.ToList().Count;
            Assert.AreEqual(initialNumberOfItems, numberOfItemsAfterDeletion, NumberOfItemsAfterDeletionMessage);

            var itemAfterDeletion = service.All(key).Result.FirstOrDefault(i => i.LastUpdate == now);
            Assert.IsNull(itemAfterDeletion, ItemShouldBeNullMessage);
        }

        [TestMethod]
        public void ValidationCacheService_AddItemDeleteContext_ShouldWork()
        {
            string key = Guid.NewGuid().ToString();

            var service = new ValidationCacheService(this.fakeRepository);
            Assert.IsNotNull(service, ServiceShouldNotBeNullMessage);

            int initialNumberOfItems = service.All(key).Result.ToList().Count;

            int id = Random.Next();
            var now = DateTime.Now;
            var content = $"Content {now.ToLongTimeString()}";
            var item = new ValidationCacheServiceModel
            {
                Id = id,
                Content = content,
                LastUpdate = now,
                Status = ValidationStatus.Invalid
            };

            service.Add(key, item).Wait();
            Assert.AreEqual(id, item.Id, IdShouldBeUnchagedMessage);

            int numberOfItemsAfterAddition = service.All(key).Result.ToList().Count;

            Assert.AreEqual(initialNumberOfItems + 1, numberOfItemsAfterAddition, NumberOfItemsAfterAdditionMessage);

            var itemAfterAddition = service.All(key).Result.FirstOrDefault(i => i.LastUpdate == now);
            Assert.IsNotNull(itemAfterAddition, AddedItemShouldBeValidObjectMessage);

            service.Delete(key).Wait();

            int numberOfItemsAfterDeletion = service.All(key).Result.ToList().Count;
            Assert.AreEqual(0, numberOfItemsAfterDeletion, "Number of items after deletion should be equal to 0.");

            var itemAfterDeletion = service.All(key).Result.FirstOrDefault(i => i.LastUpdate == now);
            Assert.IsNull(itemAfterDeletion, ItemShouldBeNullMessage);
        }

        [TestMethod]
        public void ValidationCacheService_DeleteEmptyContext_ShouldWork()
        {
            string key = Guid.NewGuid().ToString();

            var service = new ValidationCacheService(this.fakeRepository);
            Assert.IsNotNull(service, ServiceShouldNotBeNullMessage);

            service.Delete(key).Wait();

            int numberOfItemsAfterDeletion = service.All(key).Result.ToList().Count;
            Assert.AreEqual(0, numberOfItemsAfterDeletion, "Number of items after deletion should be equal to 0.");
        }

        [TestMethod]
        public void ValidationCacheService_AddItemUpdateItem_ShouldWork()
        {
            string key = Guid.NewGuid().ToString();

            var service = new ValidationCacheService(this.fakeRepository);
            Assert.IsNotNull(service, ServiceShouldNotBeNullMessage);

            int initialNumberOfItems = service.All(key).Result.ToList().Count;

            int id = Random.Next();
            var now = DateTime.Now;
            var content = $"Content {now.ToLongTimeString()}";
            var item = new ValidationCacheServiceModel
            {
                Id = id,
                Content = content,
                LastUpdate = now,
                Status = ValidationStatus.Valid
            };

            service.Add(key, item).Wait();
            Assert.AreEqual(id, item.Id, IdShouldBeUnchagedMessage);

            int numberOfItemsAfterAddition = service.All(key).Result.ToList().Count;
            Assert.AreEqual(initialNumberOfItems + 1, numberOfItemsAfterAddition, NumberOfItemsAfterAdditionMessage);

            var itemAfterAddition = service.All(key).Result.FirstOrDefault(i => i.LastUpdate == now);
            Assert.IsNotNull(itemAfterAddition, AddedItemShouldBeValidObjectMessage);
            Assert.AreEqual(ValidationStatus.Valid, itemAfterAddition.Status, StatusShouldMatchMessage);
            Assert.AreEqual(content, itemAfterAddition.Content, ContentShouldMatchMessage);

            // Here we update the item from the database, because added item’s Id might not be equal to 
            // the Id of the cooresponding entity in the database.
            itemAfterAddition.Status = ValidationStatus.Invalid;
            service.Update(key, itemAfterAddition).Wait();

            int numberOfItemsAfterUpdate = service.All(key).Result.ToList().Count;
            Assert.AreEqual(numberOfItemsAfterAddition, numberOfItemsAfterUpdate, "Number of items after update should be equal to the number of items after addition.");

            var itemAfterUpdate = service.All(key).Result.FirstOrDefault(i => i.LastUpdate == now);
            Assert.IsNotNull(itemAfterUpdate, "Item should not be null.");
            Assert.AreEqual(itemAfterAddition.Id, itemAfterUpdate.Id, IdShouldBeUnchagedMessage);
            Assert.AreEqual(ValidationStatus.Invalid, itemAfterUpdate.Status, StatusShouldMatchMessage);
            Assert.AreEqual(content, itemAfterUpdate.Content, ContentShouldMatchMessage);
        }
    }
}