namespace ProcessingTools.Services.Validation.Tests.IntegrationTests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using ProcessingTools.Bio.Taxonomy.ServiceClient.GlobalNamesResolver;
    using ProcessingTools.Cache.Data.Redis.Repositories;
    using ProcessingTools.Clients.Contracts.Bio.Taxonomy;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Data.Common.Redis;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Net;
    using ProcessingTools.Services.Cache;
    using ProcessingTools.Services.Contracts.Cache;
    using ProcessingTools.Services.Validation;

    [TestClass]
    public class TaxaValidationServiceTests
    {
        private IValidationCacheService cacheService;
        private IGlobalNamesResolverDataRequester requester;

        [TestInitialize]
        public void Initialize()
        {
            var repository = new RedisValidationCacheDataRepository(new RedisClientProvider());
            var environmentMock = new Mock<IEnvironment>();
            environmentMock
                .SetupGet(e => e.DateTimeProvider)
                .Returns(() => DateTime.UtcNow);
            this.cacheService = new ValidationCacheService(repository, environmentMock.Object);
            this.requester = new GlobalNamesResolverDataRequester(new NetConnectorFactory());
        }

        [TestMethod]
        public void TaxaValidationServiceTests_WithValidParametersInConstructor_ShouldBuildValidObject()
        {
            var service = new TaxaValidationService(this.cacheService, this.requester);
            Assert.IsNotNull(service, "Service should not be null.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TaxaValidationServiceTests_WithNullConstructor_ShouldThrow()
        {
            new TaxaValidationService(null, this.requester);
        }

        [TestMethod]
        [Timeout(2000)]
        [Ignore]
        public void TaxaValidationServiceTests_ValidateOfThreeItems_SchouldReturnThreeValidatedItems()
        {
            string[] taxa = { "Coleoptera", "Zospeum", "Homo sapiens" };

            var items = taxa.ToArray();

            var service = new TaxaValidationService(this.cacheService, this.requester);
            var result = service.ValidateAsync(items).Result.ToList();

            const int ExpectedNumberOfItems = 3;

            Assert.AreEqual(ExpectedNumberOfItems, result.Count, "The number of returned items should be 3.");

            for (int i = 0; i < ExpectedNumberOfItems; ++i)
            {
                Assert.AreEqual(
                    1,
                    result.Count(r => r.ValidatedObject == items[i]),
                    $"Result should contain Item #{i} only once.");
                Assert.IsTrue(result[i].ValidationStatus == ValidationStatus.Valid, $"Item #{i} should be valid.");
                Assert.IsNull(result[i].ValidationException, $"Item #{i} should have null exception.");
            }
        }

        [TestMethod]
        [Timeout(2000)]
        [Ignore]
        public void TaxaValidationServiceTests_ValidateOfThreeItemsWithOneInvalid_SchouldReturnThreeValidatedItems()
        {
            string[] taxa = { "Coleoptera", "Zospeum", "John Smith" };

            var items = taxa.ToArray();

            var service = new TaxaValidationService(this.cacheService, this.requester);
            var result = service.ValidateAsync(items).Result.ToList();

            const int ExpectedNumberOfItems = 3;

            Assert.AreEqual(ExpectedNumberOfItems, result.Count, "The number of returned items should be 3.");

            foreach (var taxon in taxa)
            {
                var matchingResults = result.Where(r => r.ValidatedObject == taxon);
                Assert.AreEqual(
                    1,
                    matchingResults.Count(),
                    $"Result should contain ‘{taxon}’ only once.");

                var matchingResult = matchingResults.First();

                if (taxon == taxa[2])
                {
                    Assert.IsTrue(matchingResult.ValidationStatus == ValidationStatus.Invalid, $"‘{taxon}’ should be valid.");
                }
                else
                {
                    Assert.IsTrue(matchingResult.ValidationStatus == ValidationStatus.Valid, $"‘{taxon}’ should be valid.");
                }

                Assert.IsNull(matchingResult.ValidationException, $"‘{taxon}’ should have null exception.");
            }
        }
    }
}
