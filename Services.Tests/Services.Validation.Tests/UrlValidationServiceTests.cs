namespace ProcessingTools.Services.Validation.Tests
{
    using System.Linq;

    using Common.Types;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;
    using Services;

    [TestClass]
    public class UrlValidationServiceTests
    {
        [TestMethod]
        public void UrlValidationService_WithDefaultConstructor_ShouldBuildValidObject()
        {
            var service = new UrlValidationService();
            Assert.IsNotNull(service, "Service should not be null.");
        }

        [TestMethod]
        [Ignore]
        public void UrlValidationService_ValidateOfTwoItems_WithoutBaseAddress_SchouldReturnTwoValidatedItems()
        {
            int i = 0;
            var items = (new int[2]).Select(item => new UrlModel
            {
                Address = $"https://www.google.com/search?q={++i}"
            }).ToList();

            var service = new UrlValidationService();
            var result = service.Validate(items).Result.ToList();

            Assert.AreEqual(2, result.Count, "The number of returned items should be 2.");

            Assert.AreEqual(items[0].Address, result[0].ValidatedObject.Address, "First item: addresses should match.");
            Assert.AreEqual(items[0].BaseAddress, result[0].ValidatedObject.BaseAddress, "First item: base addresses should match.");
            Assert.IsTrue(result[0].ValidationStatus == ValidationStatus.Valid, "First item schould be valid.");
            Assert.IsNull(result[0].ValidationException, "First item should have null exception.");

            Assert.AreEqual(items[1].Address, result[1].ValidatedObject.Address, "Second item: addresses should match.");
            Assert.AreEqual(items[1].BaseAddress, result[1].ValidatedObject.BaseAddress, "Second item: base addresses should match.");
            Assert.IsTrue(result[1].ValidationStatus == ValidationStatus.Valid, "Second item schould be valid.");
            Assert.IsNull(result[1].ValidationException, "Second item should have null exception.");
        }
    }
}
