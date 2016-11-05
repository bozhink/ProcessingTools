namespace ProcessingTools.Bio.Data.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Models;
    using Moq;

    using ProcessingTools.Bio.Data.Contracts;
    using ProcessingTools.Bio.Data.Repositories;
    using ProcessingTools.Bio.Data.Repositories.Contracts;
    using ProcessingTools.Contracts.Data.Repositories;

    [TestClass]
    public class BioDataRepositoryProviderTests
    {
        private IBioDbContextProvider contextProvider;

        [TestInitialize]
        public void Initialize()
        {
            var contextProviderMock = new Mock<IBioDbContextProvider>();

            contextProviderMock
                .Setup(contextProvider => contextProvider.Create())
                .Returns(new Mock<BioDbContext>("Fake connection string").Object);

            this.contextProvider = contextProviderMock.Object;
        }

        [TestMethod]
        public void BioDataRepositoryProvider_WithValidParametersInConstructor_ShouldReturnValidObject()
        {
            var repositoryProvider = new BioDataRepositoryProvider<Tweet>(this.contextProvider);

            Assert.IsNotNull(repositoryProvider, "RepositoryProvider should not be null.");

            Assert.IsInstanceOfType(
                repositoryProvider,
                typeof(IBioDataRepositoryProvider<Tweet>),
                $"RepositoryProvider should be a valid {nameof(IBioDataRepositoryProvider<Tweet>)} object.");

            Assert.IsInstanceOfType(
                repositoryProvider,
                typeof(ISearchableCountableCrudRepositoryProvider<Tweet>),
                $"RepositoryProvider should be a valid {nameof(ISearchableCountableCrudRepositoryProvider<Tweet>)} object.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BioDataRepositoryProvider_WithNullContextFactoryInConstructor_ShouldThrowArgumentNullException()
        {
            var repositoryProvider = new BioDataRepositoryProvider<Tweet>(null);
        }

        [TestMethod]
        public void BioDataRepositoryProvider_WithNullContextFactoryInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            try
            {
                var repositoryProvider = new BioDataRepositoryProvider<Tweet>(null);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("contextProvider", e.ParamName, "ParamName should be contextProvider.");
            }
        }

        [TestMethod]
        public void BioDataRepositoryProvider_Create_ShouldReturnValidObject()
        {
            var repositoryProvider = new BioDataRepositoryProvider<Tweet>(this.contextProvider);

            var repository = repositoryProvider.Create();

            Assert.IsNotNull(repository, "Repository should not be null.");

            Assert.IsInstanceOfType(
                repository,
                typeof(BioDataRepository<Tweet>),
                $"Repository should be a valid {nameof(BioDataRepository<Tweet>)} object.");

            Assert.IsInstanceOfType(
                repository,
                typeof(ISearchableCountableCrudRepository<Tweet>),
                $"Repository should be a valid {nameof(ISearchableCountableCrudRepository<Tweet>)} object.");
        }
    }
}
