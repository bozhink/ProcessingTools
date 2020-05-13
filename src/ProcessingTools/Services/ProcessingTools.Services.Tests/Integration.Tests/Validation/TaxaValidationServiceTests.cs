// <copyright file="TaxaValidationServiceTests.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Tests.Integration.Tests.Validation
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using ProcessingTools.Clients.Bio.Taxonomy.GlobalNamesResolver;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;
    using ProcessingTools.Contracts.Services.Cache;
    using ProcessingTools.Services.Net;
    using ProcessingTools.Services.Validation;

    /// <summary>
    /// <see cref="TaxaValidationService"/> tests.
    /// </summary>
    [TestClass]
    public class TaxaValidationServiceTests
    {
        private IValidationCacheService cacheService;
        private IGlobalNamesResolverDataRequester requester;

        /// <summary>
        /// Test initialize.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            ////var repository = new RedisValidationCacheDataRepository(new RedisClientProvider());
            var applicationContextMock = new Mock<IApplicationContext>();
            applicationContextMock
                .SetupGet(e => e.DateTimeProvider)
                .Returns(() => DateTime.UtcNow);
            ////this.cacheService = new ValidationCacheService(repository, applicationContextMock.Object); // TODO: IValidationCacheService
            this.requester = new GlobalNamesResolverDataRequester(new HttpRequester());
        }

        /// <summary>
        /// <see cref="TaxaValidationService"/> with valid parameters in constructor should build valid object.
        /// </summary>
        [TestMethod]
        public void TaxaValidationService_WithValidParametersInConstructor_ShouldBuildValidObject()
        {
            var service = new TaxaValidationService(this.cacheService, this.requester);
            Assert.IsNotNull(service, "Service should not be null.");
        }

        /// <summary>
        /// <see cref="TaxaValidationService"/> with null constructor should throw.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TaxaValidationService_WithNullConstructor_ShouldThrow()
        {
            new TaxaValidationService(null, this.requester);
        }

        /// <summary>
        /// <see cref="TaxaValidationService"/> validate of three items should return three validated items.
        /// </summary>
        [TestMethod]
        [Timeout(2000)]
        [Ignore] // Integration test
        public void TaxaValidationService_ValidateOfThreeItems_ShouldReturnThreeValidatedItems()
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

        /// <summary>
        /// <see cref="TaxaValidationService"/> validate of three items with one invalid should return three validated items.
        /// </summary>
        [TestMethod]
        [Timeout(2000)]
        [Ignore] // Integration test
        public void TaxaValidationService_ValidateOfThreeItemsWithOneInvalid_ShouldReturnThreeValidatedItems()
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
