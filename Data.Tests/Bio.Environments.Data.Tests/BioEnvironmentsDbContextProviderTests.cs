namespace ProcessingTools.Bio.Environments.Data.Tests
{
    using System;
    using System.Data.Entity;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    using ProcessingTools.Bio.Environments.Data.Contracts;
    using ProcessingTools.Data.Common.Entity.Contracts;

    [TestClass]
    public class BioEnvironmentsDbContextProviderTests
    {
        private IBioEnvironmentsDbContextFactory contextFactory;

        [TestInitialize]
        public void Initialize()
        {
            var contextFactoryMock = new Mock<IBioEnvironmentsDbContextFactory>();

            contextFactoryMock
                .Setup(contextFactory => contextFactory.Create())
                .Returns(new Mock<BioEnvironmentsDbContext>("Fake connection string").Object);

            this.contextFactory = contextFactoryMock.Object;
        }

        [TestMethod]
        public void BioEnvironmentsDbContextProvider_WithValidParametersInConstructor_ShouldReturnValidObject()
        {
            var contextProvider = new BioEnvironmentsDbContextProvider(this.contextFactory);

            Assert.IsNotNull(contextProvider, "ContextProvider should not be null.");

            Assert.IsInstanceOfType(
                contextProvider,
                typeof(IBioEnvironmentsDbContextProvider),
                $"ContextProvider should be a valid {nameof(IBioEnvironmentsDbContextProvider)} object.");

            Assert.IsInstanceOfType(
                contextProvider,
                typeof(IDbContextProvider<BioEnvironmentsDbContext>),
                $"ContextProvider should be a valid {nameof(IDbContextProvider<BioEnvironmentsDbContext>)} object.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BioEnvironmentsDbContextProvider_WithNullContextFactoryInConstructor_ShouldThrowArgumentNullException()
        {
            var contextProvider = new BioEnvironmentsDbContextProvider(null);
        }

        [TestMethod]
        public void BioEnvironmentsDbContextProvider_WithNullContextFactoryInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            try
            {
                var contextProvider = new BioEnvironmentsDbContextProvider(null);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("contextFactory", e.ParamName, "ParamName should be contextFactory.");
            }
        }

        [TestMethod]
        public void BioEnvironmentsDbContextProvider_Create_ShouldReturnValidObject()
        {
            var contextProvider = new BioEnvironmentsDbContextProvider(this.contextFactory);

            var context = contextProvider.Create();

            Assert.IsNotNull(context, "Context should not be null.");

            Assert.IsInstanceOfType(
                context,
                typeof(BioEnvironmentsDbContext),
                $"Context should be a valid {nameof(BioEnvironmentsDbContext)} object.");

            Assert.IsInstanceOfType(
                context,
                typeof(DbContext),
                $"Context should be a valid {nameof(DbContext)} object.");
        }
    }
}
