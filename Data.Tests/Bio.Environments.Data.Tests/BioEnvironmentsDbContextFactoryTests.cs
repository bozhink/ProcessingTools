namespace ProcessingTools.Bio.Environments.Data.Tests
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using ProcessingTools.Bio.Environments.Data.Common.Constants;
    using ProcessingTools.Bio.Environments.Data.Contracts;
    using ProcessingTools.Bio.Environments.Data.Factories;
    using ProcessingTools.Common.Providers;

    [TestClass]
    public class BioEnvironmentsDbContextFactoryTests
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
        public void BioEnvironmentsDbContextFactory_WithDefaultContrustor_ShouldReturnValidObject()
        {
            var factory = new BioEnvironmentsDbContextFactory();

            Assert.IsNotNull(factory, "Factory should not be null.");

            Assert.IsInstanceOfType(factory, typeof(IBioEnvironmentsDbContextFactory), $"Factory should be a valid {nameof(IBioEnvironmentsDbContextFactory)} object.");

            Assert.IsInstanceOfType(factory, typeof(IDbContextFactory<BioEnvironmentsDbContext>), $"Factory should be a valid {nameof(IDbContextFactory<BioEnvironmentsDbContext>)} object.");
        }

        [TestMethod]
        public void BioEnvironmentsDbContextFactory_DefaultValueOfConnectionString_ShouldBeConnectionConstantsBioEnvironmentsDbContextConnectionKey()
        {
            var factory = new BioEnvironmentsDbContextFactory();

            Assert.IsFalse(
                string.IsNullOrWhiteSpace(factory.ConnectionString),
                $"Default value of {nameof(factory.ConnectionString)} should not be null or whitespace.");

            Assert.AreEqual(
                ConnectionConstants.BioEnvironmentsDatabaseConnectionKey,
                factory.ConnectionString,
                $"Default value of {nameof(factory.ConnectionString)} should be ConnectionConstants.BioEnvironmentsDbContextConnectionKey.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BioEnvironmentsDbContextFactory_SetNullToConnectionString_ShouldThrowArgumentNullException()
        {
            var factory = new BioEnvironmentsDbContextFactory();
            factory.ConnectionString = null;
        }

        [TestMethod]
        public void BioEnvironmentsDbContextFactory_SetNullToConnectionString_ShouldThrowArgumentNullExceptionWithCorrectParameterName()
        {
            var factory = new BioEnvironmentsDbContextFactory();

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
        public void BioEnvironmentsDbContextFactory_SetEmptyToConnectionString_ShouldThrowArgumentNullException()
        {
            var factory = new BioEnvironmentsDbContextFactory();
            factory.ConnectionString = string.Empty;
        }

        [TestMethod]
        public void BioEnvironmentsDbContextFactory_SetEmptyToConnectionString_ShouldThrowArgumentNullExceptionWithCorrectParameterName()
        {
            var factory = new BioEnvironmentsDbContextFactory();

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
        public void BioEnvironmentsDbContextFactory_SetWhitespaceToConnectionString_ShouldThrowArgumentNullException()
        {
            var factory = new BioEnvironmentsDbContextFactory();
            factory.ConnectionString = "   ";
        }

        [TestMethod]
        public void BioEnvironmentsDbContextFactory_SetWhitespaceToConnectionString_ShouldThrowArgumentNullExceptionWithCorrectParameterName()
        {
            var factory = new BioEnvironmentsDbContextFactory();

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
        public void BioEnvironmentsDbContextFactory_SetValidConnectionString_ShouldWork()
        {
            var factory = new BioEnvironmentsDbContextFactory();

            for (int i = 0; i < NumberOfGetSetIterations; ++i)
            {
                string fakeConnectionString = this.randomProvider.GetRandomString(MinimalLengthOfConnectionString, MaximalLengthOfConnectionString);

                factory.ConnectionString = fakeConnectionString;

                Assert.AreEqual(fakeConnectionString, factory.ConnectionString, "Connection string should match.");
            }
        }

        [TestMethod]
        public void BioEnvironmentsDbContextFactory_Create_ShouldReturnValidObject()
        {
            var factory = new BioEnvironmentsDbContextFactory();
            factory.ConnectionString = "Fake connection string.";

            var context = factory.Create();

            Assert.IsNotNull(context, "Context should not be null.");

            Assert.IsInstanceOfType(context, typeof(BioEnvironmentsDbContext), $"Context should be a valid {nameof(BioEnvironmentsDbContext)} object.");

            Assert.IsInstanceOfType(context, typeof(DbContext), $"Context should be a valid {nameof(DbContext)} object.");
        }
    }
}
