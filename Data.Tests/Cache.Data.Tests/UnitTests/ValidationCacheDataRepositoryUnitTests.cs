namespace ProcessingTools.Cache.Data.Tests.UnitTests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    using ProcessingTools.Cache.Data.Repositories;
    using ProcessingTools.Data.Common.Redis.Contracts;

    [TestClass]
    public class ValidationCacheDataRepositoryUnitTests
    {
        private IRedisClientProvider provider;

        [TestInitialize]
        public void TestInitialize()
        {
            var providerMock = new Mock<IRedisClientProvider>();
            this.provider = providerMock.Object;
        }

        [TestMethod]
        public void ValidationCacheDataRepository_WithValidParametersInConstructor_ShouldReturnValidObject()
        {
            var repository = new ValidationCacheDataRepository(this.provider);
            Assert.IsNotNull(repository, "Repository should not be null.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidationCacheDataRepository_WithNullProviderInConstructor_ShouldThow()
        {
            var repository = new ValidationCacheDataRepository(null);
        }

        [TestMethod]
        public void ValidationCacheDataRepository_WithNullProviderInConstructor_ShouldThowArgumentNullExceptionWithCorrectParameterName()
        {
            try
            {
                var repository = new ValidationCacheDataRepository(null);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("provider", e.ParamName, "ParamName should have value 'provider'.");
            }
        }
    }
}
