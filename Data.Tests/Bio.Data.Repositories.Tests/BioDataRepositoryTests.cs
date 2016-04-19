namespace ProcessingTools.Bio.Data.Repositories.Tests
{
    using System;
    using System.Data.Entity;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    using ProcessingTools.Bio.Data.Repositories.Contracts;
    using ProcessingTools.Bio.Data.Repositories.Tests.Models;
    using ProcessingTools.Bio.Data.Repositories.Tests.Mocks;
    using ProcessingTools.Data.Common.Entity.Repositories;
    using ProcessingTools.Data.Common.Repositories.Contracts;

    [TestClass]
    public class BioDataRepositoryTests
    {
        private IBioDbContextProvider contextProvider;

        [TestInitialize]
        public void Initialize()
        {
            var contextMock = new ContextMock();

            var contextProviderMock = new Mock<IBioDbContextProvider>();
            contextProviderMock
                .Setup(contextProvider => contextProvider.Create())
                .Returns(contextMock.MockObject.Object);

            this.contextProvider = contextProviderMock.Object;
        }

        [TestMethod]
        public void BioDataRepository_WithValidParametersInConstructor_ShouldReturnValidObject()
        {
            var repository = new BioDataRepository<Tweet>(this.contextProvider);

            Assert.IsNotNull(repository, "Repository should not be null.");

            Assert.IsInstanceOfType(
                repository,
                typeof(IBioDataRepository<Tweet>),
                $"Repository should be a valid {nameof(IBioDataRepository<Tweet>)} object.");

            Assert.IsInstanceOfType(
                repository,
                typeof(IGenericRepository<Tweet>),
                $"Repository should be a valid {nameof(IGenericRepository<Tweet>)} object.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BioDataRepository_WithNullContextFactoryInConstructor_ShouldThrowArgumentNullException()
        {
            var repository = new BioDataRepository<Tweet>(null);
        }

        [TestMethod]
        public void BioDataRepository_WithNullContextFactoryInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            try
            {
                var repository = new BioDataRepository<Tweet>(null);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("contextProvider", e.ParamName, "ParamName should be contextProvider.");
            }
        }

        [TestMethod]
        [Timeout(2000)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BioDataRepository_AddNullEntity_ShouldThrowArgumentNullException()
        {
            var repository = new BioDataRepository<Tweet>(this.contextProvider);
            repository.Add(null).Wait();
        }

        [TestMethod]
        [Timeout(2000)]
        public void BioDataRepository_AddNullEntity_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            var repository = new BioDataRepository<Tweet>(this.contextProvider);

            try
            {
                repository.Add(null).Wait();
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("entity", e.ParamName, "ParamName should be entity.");
            }
        }

        //////[TestMethod]
        //////[Timeout(2000)]
        //////public void BioDataRepository_AddValidEntity_ShouldWork()
        //////{
        //////    var repository = new BioDataRepository<Tweet>(this.contextProvider);
        //////    repository.Add(new Tweet
        //////    {
        //////        Id = 1,
        //////        Content = "Content"
        //////    });
        //////}

        //////[TestMethod]
        //////[Timeout(2000)]
        //////public void BioDataRepository_All_ShouldReturnAllItemsAsQueryable()
        //////{
        //////    var repository = new BioDataRepository<Tweet>(this.contextProvider);

        //////    repository.All().Wait();
        //////    var result = repository.All().Result;

        //////    Assert.IsNotNull(result, "All() should not return Task of null.");
        //////}
    }
}
