namespace ProcessingTools.Bio.Data.Tests
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using ProcessingTools.Bio.Data.Common.Constants;
    using ProcessingTools.Bio.Data.Contracts;
    using ProcessingTools.Bio.Data.Factories;
    using ProcessingTools.Common.Providers;

    [TestClass]
    public class BioDbContextFactoryTests
    {
        private const int NumberOfGetSetIterations = 1000;
        private const int MinimalLengthOfConnectionString = 5;
        private const int MaximalLengthOfConnectionString = 100;

        private RandomProvider randomProvider;

        [TestInitialize]
        public void Initialize()
        {
            this.randomProvider = new RandomProvider();
        }

        [TestMethod]
        public void BioDbContextFactory_WithDefaultContrustor_ShouldReturnValidObject()
        {
            var factory = new BioDbContextFactory();

            Assert.IsNotNull(factory, "Factory should not be null.");

            Assert.IsInstanceOfType(factory, typeof(IBioDbContextFactory), $"Factory should be a valid {nameof(IBioDbContextFactory)} object.");

            Assert.IsInstanceOfType(factory, typeof(IDbContextFactory<BioDbContext>), $"Factory should be a valid {nameof(IDbContextFactory<BioDbContext>)} object.");
        }

        [TestMethod]
        public void BioDbContextFactory_DefaultValueOfConnectionString_ShouldBeConnectionConstantsBioDbContextConnectionKey()
        {
            var factory = new BioDbContextFactory();

            Assert.IsFalse(
                string.IsNullOrWhiteSpace(factory.ConnectionString),
                $"Default value of {nameof(factory.ConnectionString)} should not be null or whitespace.");

            Assert.AreEqual(
                ConnectionConstants.BioDbContextConnectionKey,
                factory.ConnectionString,
                $"Default value of {nameof(factory.ConnectionString)} should be ConnectionConstants.BioDbContextConnectionKey.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BioDbContextFactory_SetNullToConnectionString_ShouldThrowArgumentNullException()
        {
            var factory = new BioDbContextFactory();
            factory.ConnectionString = null;
        }

        [TestMethod]
        public void BioDbContextFactory_SetNullToConnectionString_ShouldThrowArgumentNullExceptionWithCorrectParameterName()
        {
            var factory = new BioDbContextFactory();

            try
            {
                factory.ConnectionString = null;
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual(nameof(factory.ConnectionString), e.ParamName, $"ParamName should be {nameof(factory.ConnectionString)}");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BioDbContextFactory_SetEmptyToConnectionString_ShouldThrowArgumentNullException()
        {
            var factory = new BioDbContextFactory();
            factory.ConnectionString = string.Empty;
        }

        [TestMethod]
        public void BioDbContextFactory_SetEmptyToConnectionString_ShouldThrowArgumentNullExceptionWithCorrectParameterName()
        {
            var factory = new BioDbContextFactory();

            try
            {
                factory.ConnectionString = string.Empty;
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual(nameof(factory.ConnectionString), e.ParamName, $"ParamName should be {nameof(factory.ConnectionString)}");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BioDbContextFactory_SetWhitespaceToConnectionString_ShouldThrowArgumentNullException()
        {
            var factory = new BioDbContextFactory();
            factory.ConnectionString = "   ";
        }

        [TestMethod]
        public void BioDbContextFactory_SetWhitespaceToConnectionString_ShouldThrowArgumentNullExceptionWithCorrectParameterName()
        {
            var factory = new BioDbContextFactory();

            try
            {
                factory.ConnectionString = "   ";
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual(nameof(factory.ConnectionString), e.ParamName, $"ParamName should be {nameof(factory.ConnectionString)}");
            }
        }

        [TestMethod]
        public void BioDbContextFactory_SetValidConnectionString_ShouldWork()
        {
            var factory = new BioDbContextFactory();

            for (int i = 0; i < NumberOfGetSetIterations; ++i)
            {
                string fakeConnectionString = this.randomProvider.GetRandomString(MinimalLengthOfConnectionString, MaximalLengthOfConnectionString);

                factory.ConnectionString = fakeConnectionString;

                Assert.AreEqual(fakeConnectionString, factory.ConnectionString, "Connection string should match.");
            }
        }

        [TestMethod]
        public void BioDbContextFactory_Create_ShouldReturnValidObject()
        {
            var factory = new BioDbContextFactory();
            factory.ConnectionString = "Fake connection string.";

            var context = factory.Create();

            Assert.IsNotNull(context, "Context should not be null.");

            Assert.IsInstanceOfType(context, typeof(BioDbContext), $"Context should be a valid {nameof(BioDbContext)} object.");

            Assert.IsInstanceOfType(context, typeof(DbContext), $"Context should be a valid {nameof(DbContext)} object.");
        }
    }
}
