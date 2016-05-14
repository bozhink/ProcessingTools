namespace ProcessingTools.Data.Common.Redis.Tests.UnitTests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;
    using Models;

    using ProcessingTools.Data.Common.Redis.Contracts;
    using ProcessingTools.Data.Common.Redis.Repositories;

    [TestClass]
    public class RedisGenericRepositoryUnitTests
    {
        private IRedisClientProvider provider;

        [TestInitialize]
        public void TestInitialize()
        {
            this.provider = new RedisClientProviderMock();
        }

        [TestMethod]
        public void RedisGenericRepository_WithValidParametersInConstructor_ShouldReturnValidObject()
        {
            var repository = new RedisGenericRepository<Tweet>(this.provider);
            Assert.IsNotNull(repository, "Repository should not be null.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RedisGenericRepository_WithNullProviderInConstructor_ShouldThow()
        {
            var repository = new RedisGenericRepository<Tweet>(null);
        }

        [TestMethod]
        public void RedisGenericRepository_WithNullProviderInConstructor_ShouldThowArgumentNullExceptionWithCorrectParameterName()
        {
            try
            {
                var repository = new RedisGenericRepository<Tweet>(null);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("provider", e.ParamName, "ParamName should have value 'provider'.");
            }
        }
    }
}
