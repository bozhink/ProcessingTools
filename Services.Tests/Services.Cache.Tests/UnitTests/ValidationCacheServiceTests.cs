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
            Assert.IsNotNull(service, "Service should not be null.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidationCacheService_WithNullInDefaultConstructor_ShouldThrow()
        {
            var service = new ValidationCacheService(null);
        }

        [TestMethod]
        public void ValidationCacheService_AddNewEntity_ShouldWork()
        {
            const string Key = "Items 0";

            var service = new ValidationCacheService(this.fakeRepository);
            Assert.IsNotNull(service, "Service should not be null.");

            var now = DateTime.Now;
            var content = $"Content {now.ToLongTimeString()}";
            var entity = new ValidationCacheServiceModel
            {
                Content = content,
                LastUpdate = now,
                Status = ValidationStatus.Undefined
            };

            int numberOfItemsBeforeAdding = service.All(Key).Result.ToList().Count;

            service.Add(Key, entity).Wait();

            int numberOfItemsAfterAdding = service.All(Key).Result.ToList().Count;

            Assert.AreEqual(numberOfItemsBeforeAdding + 1, numberOfItemsAfterAdding, "Number of items after addition should be incremented by 1.");

            var item = service.All(Key).Result.FirstOrDefault(i => i.LastUpdate == now);

            Assert.IsNotNull(item, "Item should be added as a valid object.");

            Assert.AreEqual(numberOfItemsAfterAdding, item.Id, "item.Id should have the maximal value.");

            Assert.AreEqual(ValidationStatus.Undefined, item.Status, "item.Status should be Udefined.");

            Assert.AreEqual(content, item.Content, "item.Content should match");
        }
    }
}