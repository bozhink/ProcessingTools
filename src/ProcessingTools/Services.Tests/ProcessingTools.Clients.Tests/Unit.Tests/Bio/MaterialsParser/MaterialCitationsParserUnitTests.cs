// <copyright file="MaterialCitationsParserUnitTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Tests.Unit.Tests.Bio.MaterialsParser
{
    using System;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using ProcessingTools.Clients.Bio.MaterialsParser;
    using ProcessingTools.Contracts;

    /// <summary>
    /// <see cref="MaterialCitationsParser"/> unit tests.
    /// </summary>
    [TestClass]
    public class MaterialCitationsParserUnitTests
    {
        /// <summary>
        /// <see cref="MaterialCitationsParser"/> with valid connector in default constructor should return valid object.
        /// </summary>
        [TestMethod]
        public void MaterialCitationsParser_WithValidConnectorInDefaultConstructor_ShouldReturnValidObject()
        {
            var connectorMock = new Mock<INetConnectorFactory>();
            var parser = new MaterialCitationsParser(connectorMock.Object);
            Assert.IsNotNull(parser, "Parser should not be null object.");
        }

        /// <summary>
        /// <see cref="MaterialCitationsParser"/> with null connector in default constructor should throw <see cref="ArgumentNullException"/>.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MaterialCitationsParser_WithNullConnectorInDefaultConstructor_ShouldThrowArgumentNullException()
        {
            new MaterialCitationsParser(null);
        }

        /// <summary>
        /// <see cref="MaterialCitationsParser"/> with null connector in default constructor should throw <see cref="ArgumentNullException"/> with correct ParamName.
        /// </summary>
        [TestMethod]
        public void MaterialCitationsParser_WithNullConnectorInDefaultConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            try
            {
                new MaterialCitationsParser(null);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("connectorFactory", e.ParamName, "ParamName should be connector.");
            }
        }

        /// <summary>
        /// <see cref="MaterialCitationsParser"/> with valid connector and valid encoding in constructor should return valid object.
        /// </summary>
        [TestMethod]
        public void MaterialCitationsParser_WithValidConnectorAndValidEncodingInConstructor_ShouldReturnValidObject()
        {
            var connectorMock = new Mock<INetConnectorFactory>();
            var parser = new MaterialCitationsParser(connectorMock.Object, Encoding.UTF32);
            Assert.IsNotNull(parser, "Parser should not be null object.");
        }

        /// <summary>
        /// <see cref="MaterialCitationsParser"/> with null connector and valid encoding in constructor should throw <see cref="ArgumentNullException"/>.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MaterialCitationsParser_WithNullConnectorAndValidEncodingInConstructor_ShouldThrowArgumentNullException()
        {
            new MaterialCitationsParser(null, Encoding.UTF32);
        }

        /// <summary>
        /// <see cref="MaterialCitationsParser"/> with null connector and valid encoding in constructor should throw <see cref="ArgumentNullException"/> with correct ParamName.
        /// </summary>
        [TestMethod]
        public void MaterialCitationsParser_WithNullConnectorAndValidEncodingInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            try
            {
                new MaterialCitationsParser(null, Encoding.UTF32);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("connectorFactory", e.ParamName, "ParamName should be connector.");
            }
        }

        /// <summary>
        /// <see cref="MaterialCitationsParser"/> with valid connector and null encoding in constructor should throw <see cref="ArgumentNullException"/>.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MaterialCitationsParser_WithValidConnectorAndNullEncodingInConstructor_ShouldThrowArgumentNullException()
        {
            var connectorMock = new Mock<INetConnectorFactory>();
            new MaterialCitationsParser(connectorMock.Object, null);
        }

        /// <summary>
        /// <see cref="MaterialCitationsParser"/> with valid connector and null encoding in constructor should throw <see cref="ArgumentNullException"/> with correct ParamName.
        /// </summary>
        [TestMethod]
        public void MaterialCitationsParser_WithValidConnectorAndNullEncodingInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            try
            {
                var connectorMock = new Mock<INetConnectorFactory>();
                new MaterialCitationsParser(connectorMock.Object, null);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("encoding", e.ParamName, "ParamName should be encoding.");
            }
        }

        /// <summary>
        /// <see cref="MaterialCitationsParser"/> with null content in invoke should throw <see cref="AggregateException"/>.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        [Timeout(5000)]
        public void MaterialCitationsParser_WithNullContentInInvoke_ShouldThrowAggregateException()
        {
            var connectorMock = new Mock<INetConnectorFactory>();
            var parser = new MaterialCitationsParser(connectorMock.Object, Encoding.UTF8);

            parser.ParseAsync(null).Wait();
        }

        /// <summary>
        /// <see cref="MaterialCitationsParser"/> with null content in invoke should throw <see cref="AggregateException"/> with inner <see cref="ArgumentNullException"/>.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        public void MaterialCitationsParser_WithNullContentInInvoke_ShouldThrowAggregateExceptionWithInnerArgumentNullException()
        {
            var connectorMock = new Mock<INetConnectorFactory>();
            var parser = new MaterialCitationsParser(connectorMock.Object, Encoding.UTF8);

            try
            {
                parser.ParseAsync(null).Wait();
            }
            catch (AggregateException e)
            {
                Assert.AreEqual(1, e.InnerExceptions.Count, "Number of inner exceptions should be 1.");

                Assert.AreEqual(
                    typeof(ArgumentNullException).FullName,
                    e.InnerExceptions.First().GetType().FullName,
                    "Inner exception should be of type ArgumentNullException.");
            }
        }

        /// <summary>
        /// <see cref="MaterialCitationsParser"/> with whitespace content in invoke should throw <see cref="AggregateException"/>.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        [Timeout(5000)]
        public void MaterialCitationsParser_WithWhitespaceContentInInvoke_ShouldThrowAggregateException()
        {
            var connectorMock = new Mock<INetConnectorFactory>();
            var parser = new MaterialCitationsParser(connectorMock.Object, Encoding.UTF8);

            parser.ParseAsync(@"

").Wait();
        }

        /// <summary>
        /// <see cref="MaterialCitationsParser"/> with whitespace content in invoke should throw <see cref="AggregateException"/> with inner <see cref="ArgumentNullException"/>.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        public void MaterialCitationsParser_WithWhitespaceContentInInvoke_ShouldThrowAggregateExceptionWithInnerArgumentNullException()
        {
            var connectorMock = new Mock<INetConnectorFactory>();
            var parser = new MaterialCitationsParser(connectorMock.Object, Encoding.UTF8);

            try
            {
                parser.ParseAsync("  ").Wait();
            }
            catch (AggregateException e)
            {
                Assert.AreEqual(1, e.InnerExceptions.Count, "Number of inner exceptions should be 1.");

                Assert.AreEqual(
                    typeof(ArgumentNullException).FullName,
                    e.InnerExceptions.First().GetType().FullName,
                    "Inner exception should be of type ArgumentNullException.");
            }
        }
    }
}
