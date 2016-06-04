﻿namespace ProcessingTools.Services.Validation.Tests.IntegrationTests
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;
    using ProcessingTools.Cache.Data;
    using ProcessingTools.Cache.Data.Repositories;
    using ProcessingTools.Contracts.Types;
    using ProcessingTools.Services.Cache;
    using ProcessingTools.Services.Cache.Contracts;

    [TestClass]
    public class TaxaValidationServiceTests
    {
        private IValidationCacheService cacheService;

        [TestInitialize]
        public void Initialize()
        {
            var repository = new ValidationCacheDataRepository(new RedisClientProvider());
            this.cacheService = new ValidationCacheService(repository);
        }

        [TestMethod]
        public void TaxaValidationServiceTests_WithValidParametersInConstructor_ShouldBuildValidObject()
        {
            var service = new TaxaValidationService(this.cacheService);
            Assert.IsNotNull(service, "Service should not be null.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TaxaValidationServiceTests_WithNullConstructor_ShouldThrow()
        {
            var service = new TaxaValidationService(null);
        }

        [TestMethod]
        [Timeout(2000)]
        [Ignore]
        public void TaxaValidationServiceTests_ValidateOfThreeItems_SchouldReturnThreeValidatedItems()
        {
            string[] taxa = { "Coleoptera", "Zospeum", "Homo sapiens" };

            var items = taxa.Select(t => new TaxonNameServiceModel
            {
                Name = t
            })
            .ToArray();

            var service = new TaxaValidationService(this.cacheService);
            var result = service.Validate(items).Result.ToList();

            const int ExpectedNumberOfItems = 3;

            Assert.AreEqual(ExpectedNumberOfItems, result.Count, "The number of returned items should be 3.");

            for (int i = 0; i < ExpectedNumberOfItems; ++i)
            {
                Assert.AreEqual(
                    1,
                    result.Where(r => r.ValidatedObject.Name == items[i].Name).Count(),
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

            var items = taxa.Select(t => new TaxonNameServiceModel
            {
                Name = t
            })
            .ToArray();

            var service = new TaxaValidationService(this.cacheService);
            var result = service.Validate(items).Result.ToList();

            const int ExpectedNumberOfItems = 3;

            Assert.AreEqual(ExpectedNumberOfItems, result.Count, "The number of returned items should be 3.");

            foreach (var taxon in taxa)
            {
                var matchingResults = result.Where(r => r.ValidatedObject.Name == taxon);
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
