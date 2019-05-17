﻿namespace ProcessingTools.Net.Tests.Unit.Tests
{
    using System;
    using NUnit.Framework;
    using ProcessingTools.Contracts;
    using ProcessingTools.Net;

    [TestFixture]
    public class HttpRequesterUnitTests
    {
        [Test]
        public void HttpRequester_ParameterlessConstructor_ShouldReturnValidObject()
        {
            var connector = new HttpRequester();
            Assert.IsNotNull(connector, "Connector should be a valid object.");
            Assert.IsInstanceOf<IHttpRequester>(connector, "Connector should be a instance of {0}.", nameof(IHttpRequester));
        }

        [TestCase("http://localhost")]
        [TestCase("x:\\")]
        [TestCase("x:\\Some\\Directory")]
        [TestCase("x://Some/Directory")]
        [TestCase("file://x/Some/Directory")]
        [TestCase("urn:some-uri.org")]
        [TestCase("uri:some-uri.org")]
        public void HttpRequester_ConstructorWithValidBaseAddress_ShouldReturnValidObject(string baseAddress)
        {
            var connector = new HttpRequester(baseAddress);
            Assert.IsNotNull(connector, "Connector should be a valid object.");
            Assert.IsInstanceOf<IHttpRequester>(connector, "Connector should be a instance of {0}.", nameof(IHttpRequester));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("         ")]
        [TestCase(@"

     ")]
        public void HttpRequester_ConstructorWithNullOrWhiteSpaceBaseAddress_ShouldThrowArgumentNullException(string baseAddress)
        {
            Assert.Catch<ArgumentNullException>(
                () =>
                {
                    new HttpRequester(baseAddress);
                },
                "Constructor With Null BaseAddress should throw {0}",
                nameof(ArgumentNullException));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("         ")]
        [TestCase(@"

     ")]
        public void HttpRequester_ConstructorWithNullOrWhiteSpaceBaseAddress_ShouldThrowArgumentNullExceptionWithCorrectParamName(string baseAddress)
        {
            try
            {
                new HttpRequester(baseAddress);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual(nameof(baseAddress), e.ParamName, "ParamName should be baseAddress");
            }
        }

        [TestCase("localhost")]
        [TestCase("x:localhost")]
        [TestCase("x::localhost")]
        public void HttpRequester_ConstructorWithInvalidBaseAddress_ShouldThrowUriFormatException(string baseAddress)
        {
            Assert.Catch<UriFormatException>(
                () =>
                {
                    new HttpRequester(baseAddress);
                },
                "Constructor should throw UriFormatException");
        }
    }
}
