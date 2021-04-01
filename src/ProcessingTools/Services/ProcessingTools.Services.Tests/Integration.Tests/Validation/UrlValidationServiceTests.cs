// <copyright file="UrlValidationServiceTests.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Tests.Integration.Tests.Validation
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Contracts.DataAccess.Cache;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services.Cache;
    using ProcessingTools.Services.Cache;
    using ProcessingTools.Services.Validation;

    /// <summary>
    /// URL validation service tests.
    /// </summary>
    [TestClass]
    public class UrlValidationServiceTests
    {
        private IValidationCacheService cacheService;

        /// <summary>
        /// Test initialize.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            var daoMock = new Mock<IValidationCacheDataAccessObject>();
            var applicationContextMock = new Mock<IApplicationContext>();
            applicationContextMock
                .SetupGet(e => e.DateTimeProvider)
                .Returns(() => DateTime.UtcNow);

            this.cacheService = new ValidationCacheService(daoMock.Object, applicationContextMock.Object);
        }

        /// <summary>
        /// URL validation service with default constructor should build valid object.
        /// </summary>
        [TestMethod]
        public void UrlValidationService_WithDefaultConstructor_ShouldBuildValidObject()
        {
            var service = new UrlValidationService(this.cacheService);
            Assert.IsNotNull(service, "Service should not be null.");
        }

        /// <summary>
        /// URL validation service with null constructor should throw.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UrlValidationService_WithNullConstructor_ShouldThrow()
        {
            new UrlValidationService(null);
        }

        /// <summary>
        /// URL validation service validate of two items without base address should return two validated items.
        /// </summary>
        [TestMethod]
        [Timeout(2000)]
        [Ignore] // Integration test
        public void UrlValidationService_ValidateOfTwoItems_WithoutBaseAddress_ShouldReturnTwoValidatedItems()
        {
            int i = 0;
            var items = (new int[2]).Select(item => $"https://www.google.com/search?q={++i}").ToArray();

            var service = new UrlValidationService(this.cacheService);
            var result = service.ValidateAsync(items.ToArray()).Result
                .OrderBy(u => u.ValidatedObject)
                .ToList();

            Assert.AreEqual(2, result.Count, "The number of returned items should be 2.");

            Assert.AreEqual(items[0], result[0].ValidatedObject, "First item: addresses should match.");
            Assert.IsTrue(result[0].ValidationStatus == ValidationStatus.Valid, "First item should be valid.");
            Assert.IsNull(result[0].ValidationException, "First item should have null exception.");

            Assert.AreEqual(items[1], result[1].ValidatedObject, "Second item: addresses should match.");
            Assert.IsTrue(result[1].ValidationStatus == ValidationStatus.Valid, "Second item should be valid.");
            Assert.IsNull(result[1].ValidationException, "Second item should have null exception.");
        }
    }
}
