namespace ProcessingTools.Services.Validation.Tests.IntegrationTests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;
    using ProcessingTools.Cache.Data.Redis.Repositories;
    using ProcessingTools.Common.Providers;
    using ProcessingTools.Data.Common.Redis;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Services.Cache.Contracts.Validation;
    using ProcessingTools.Services.Cache.Validation;

    [TestClass]
    public class UrlValidationServiceTests
    {
        private IValidationCacheService cacheService;

        [TestInitialize]
        public void Initialize()
        {
            var repository = new RedisValidationCacheDataRepository(new RedisClientProvider());
            var dateTimeProvider = new DateTimeProvider();
            this.cacheService = new ValidationCacheService(repository, dateTimeProvider);
        }

        [TestMethod]
        public void UrlValidationService_WithDefaultConstructor_ShouldBuildValidObject()
        {
            var service = new UrlValidationService(this.cacheService);
            Assert.IsNotNull(service, "Service should not be null.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UrlValidationService_WithNullConstructor_ShouldThrow()
        {
            var service = new UrlValidationService(null);
        }

        [TestMethod]
        [Timeout(2000)]
        [Ignore]
        public void UrlValidationService_ValidateOfTwoItems_WithoutBaseAddress_SchouldReturnTwoValidatedItems()
        {
            int i = 0;
            var items = (new int[2]).Select(item => new UrlServiceModel
            {
                Address = $"https://www.google.com/search?q={++i}"
            }).ToList();

            var service = new UrlValidationService(this.cacheService);
            var result = service.Validate(items.ToArray()).Result
                .OrderBy(u => u.ValidatedObject.Address)
                .ToList();

            Assert.AreEqual(2, result.Count, "The number of returned items should be 2.");

            Assert.AreEqual(items[0].Address, result[0].ValidatedObject.Address, "First item: addresses should match.");
            Assert.AreEqual(items[0].BaseAddress, result[0].ValidatedObject.BaseAddress, "First item: base addresses should match.");
            Assert.IsTrue(result[0].ValidationStatus == ValidationStatus.Valid, "First item should be valid.");
            Assert.IsNull(result[0].ValidationException, "First item should have null exception.");

            Assert.AreEqual(items[1].Address, result[1].ValidatedObject.Address, "Second item: addresses should match.");
            Assert.AreEqual(items[1].BaseAddress, result[1].ValidatedObject.BaseAddress, "Second item: base addresses should match.");
            Assert.IsTrue(result[1].ValidationStatus == ValidationStatus.Valid, "Second item should be valid.");
            Assert.IsNull(result[1].ValidationException, "Second item should have null exception.");
        }
    }
}
