namespace ProcessingTools.Bio.Environments.Data.Tests
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;
    using Models;
    using Moq;

    using ProcessingTools.Bio.Environments.Data.Contracts;
    using ProcessingTools.Bio.Environments.Data.Repositories;
    using ProcessingTools.Bio.Environments.Data.Repositories.Contracts;
    using ProcessingTools.Contracts.Data.Repositories;

    [TestClass]
    public class BioEnvironmentsRepositoryTests
    {
        private IBioEnvironmentsDbContextProvider contextProvider;

        [TestInitialize]
        public void Initialize()
        {
            var contextMock = new ContextMock();

            var contextProviderMock = new Mock<IBioEnvironmentsDbContextProvider>();
            contextProviderMock
                .Setup(contextProvider => contextProvider.Create())
                .Returns(contextMock.MockObject.Object);

            this.contextProvider = contextProviderMock.Object;
        }

        [TestMethod]
        public void BioEnvironmentsRepository_WithValidParametersInConstructor_ShouldReturnValidObject()
        {
            var repository = new BioEnvironmentsRepository<Tweet>(this.contextProvider);

            Assert.IsNotNull(repository, "Repository should not be null.");

            Assert.IsInstanceOfType(
                repository,
                typeof(IBioEnvironmentsRepository<Tweet>),
                $"Repository should be a valid {nameof(IBioEnvironmentsRepository<Tweet>)} object.");

            Assert.IsInstanceOfType(
                repository,
                typeof(IGenericRepository<Tweet>),
                $"Repository should be a valid {nameof(IGenericRepository<Tweet>)} object.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BioEnvironmentsRepository_WithNullContextFactoryInConstructor_ShouldThrowArgumentNullException()
        {
            var repository = new BioEnvironmentsRepository<Tweet>(null);
        }

        [TestMethod]
        public void BioEnvironmentsRepository_WithNullContextFactoryInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            try
            {
                var repository = new BioEnvironmentsRepository<Tweet>(null);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("contextProvider", e.ParamName, "ParamName should be contextProvider.");
            }
        }

        [TestMethod]
        [Timeout(2000)]
        [ExpectedException(typeof(AggregateException))]
        public void BioEnvironmentsRepository_AddNullEntity_ShouldThrowAggregateException()
        {
            var repository = new BioEnvironmentsRepository<Tweet>(this.contextProvider);
            repository.Add(null).Wait();
        }

        [TestMethod]
        [Timeout(2000)]
        public void BioEnvironmentsRepository_AddNullEntity_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            var repository = new BioEnvironmentsRepository<Tweet>(this.contextProvider);

            try
            {
                repository.Add(null).Wait();
            }
            catch (AggregateException e)
            {
                var argumentNummException = e.InnerExceptions.Single() as ArgumentNullException;

                Assert.AreEqual("entity", argumentNummException.ParamName, "ParamName should be entity.");
            }
        }
    }
}
