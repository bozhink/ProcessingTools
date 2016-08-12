namespace ProcessingTools.Bio.Environments.Data.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Models;
    using Moq;

    using ProcessingTools.Bio.Environments.Data.Contracts;
    using ProcessingTools.Bio.Environments.Data.Repositories;
    using ProcessingTools.Bio.Environments.Data.Repositories.Contracts;
    using ProcessingTools.Data.Common.Repositories.Contracts;

    [TestClass]
    public class BioEnvironmentsRepositoryProviderTests
    {
        private IBioEnvironmentsDbContextProvider contextProvider;

        [TestInitialize]
        public void Initialize()
        {
            var contextProviderMock = new Mock<IBioEnvironmentsDbContextProvider>();

            contextProviderMock
                .Setup(contextProvider => contextProvider.Create())
                .Returns(new Mock<BioEnvironmentsDbContext>("Fake connection string").Object);

            this.contextProvider = contextProviderMock.Object;
        }

        [TestMethod]
        public void BioEnvironmentsRepositoryProvider_WithValidParametersInConstructor_ShouldReturnValidObject()
        {
            var repositoryProvider = new BioEnvironmentsRepositoryProvider<Tweet>(this.contextProvider);

            Assert.IsNotNull(repositoryProvider, "RepositoryProvider should not be null.");

            Assert.IsInstanceOfType(
                repositoryProvider,
                typeof(IBioEnvironmentsRepositoryProvider<Tweet>),
                $"RepositoryProvider should be a valid {nameof(IBioEnvironmentsRepositoryProvider<Tweet>)} object.");

            Assert.IsInstanceOfType(
                repositoryProvider,
                typeof(IGenericRepositoryProvider<Tweet>),
                $"RepositoryProvider should be a valid {nameof(IGenericRepositoryProvider<Tweet>)} object.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BioEnvironmentsRepositoryProvider_WithNullContextFactoryInConstructor_ShouldThrowArgumentNullException()
        {
            var repositoryProvider = new BioEnvironmentsRepositoryProvider<Tweet>(null);
        }

        [TestMethod]
        public void BioEnvironmentsRepositoryProvider_WithNullContextFactoryInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            try
            {
                var repositoryProvider = new BioEnvironmentsRepositoryProvider<Tweet>(null);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("contextProvider", e.ParamName, "ParamName should be contextProvider.");
            }
        }

        [TestMethod]
        public void BioEnvironmentsRepositoryProvider_Create_ShouldReturnValidObject()
        {
            var repositoryProvider = new BioEnvironmentsRepositoryProvider<Tweet>(this.contextProvider);

            var repository = repositoryProvider.Create();

            Assert.IsNotNull(repository, "Repository should not be null.");

            Assert.IsInstanceOfType(
                repository,
                typeof(BioEnvironmentsRepository<Tweet>),
                $"Repository should be a valid {nameof(BioEnvironmentsRepository<Tweet>)} object.");

            Assert.IsInstanceOfType(
                repository,
                typeof(IGenericRepository<Tweet>),
                $"Repository should be a valid {nameof(IGenericRepository<Tweet>)} object.");
        }
    }
}
