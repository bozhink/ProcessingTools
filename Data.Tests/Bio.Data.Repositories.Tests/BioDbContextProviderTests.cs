namespace ProcessingTools.Bio.Data.Repositories.Tests
{
    using System;
    using System.Data.Entity;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    using ProcessingTools.Bio.Data.Contracts;
    using ProcessingTools.Bio.Data.Repositories.Contracts;
    using ProcessingTools.Data.Common.Entity.Contracts;

    [TestClass]
    public class BioDbContextProviderTests
    {
        private IBioDbContextFactory contextFactory;

        [TestInitialize]
        public void Initialize()
        {
            var contextFactoryMock = new Mock<IBioDbContextFactory>();

            contextFactoryMock
                .Setup(contextFactory => contextFactory.Create())
                .Returns(new Mock<BioDbContext>("Fake connection string").Object);

            this.contextFactory = contextFactoryMock.Object;
        }

        [TestMethod]
        public void BioDbContextProvider_WithValidParametersInConstructor_ShouldReturnValidObject()
        {
            var contextProvider = new BioDbContextProvider(this.contextFactory);

            Assert.IsNotNull(contextProvider, "ContextProvider should not be null.");

            Assert.IsInstanceOfType(
                contextProvider,
                typeof(IBioDbContextProvider),
                $"ContextProvider should be a valid {nameof(IBioDbContextProvider)} object.");

            Assert.IsInstanceOfType(
                contextProvider, 
                typeof(IDbContextProvider<BioDbContext>),
                $"ContextProvider should be a valid {nameof(IDbContextProvider<BioDbContext>)} object.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BioDbContextProvider_WithNullContextFactoryInConstructor_ShouldThrowArgumentNullException()
        {
            var contextProvider = new BioDbContextProvider(null);
        }

        [TestMethod]
        public void BioDbContextProvider_WithNullContextFactoryInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            try
            {
                var contextProvider = new BioDbContextProvider(null);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("contextFactory", e.ParamName, "ParamName should be contextFactory.");
            }
        }

        [TestMethod]
        public void BioDbContextProvider_Create_ShouldReturnValidObject()
        {
            var contextProvider = new BioDbContextProvider(this.contextFactory);

            var context = contextProvider.Create();

            Assert.IsNotNull(context, "Context should not be null.");

            Assert.IsInstanceOfType(
                context,
                typeof(BioDbContext),
                $"Context should be a valid {nameof(BioDbContext)} object.");

            Assert.IsInstanceOfType(
                context,
                typeof(DbContext),
                $"Context should be a valid {nameof(DbContext)} object.");
        }
    }
}
