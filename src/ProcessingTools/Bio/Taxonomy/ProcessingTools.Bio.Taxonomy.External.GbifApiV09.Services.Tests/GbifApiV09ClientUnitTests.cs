// <copyright file="GbifApiV09ClientUnitTests.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.External.GbifApiV09.Services.Tests
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// <see cref="GbifApiV09Client"/> unit tests.
    /// </summary>
    [TestFixture(Category = "Unit", TestOf = typeof(GbifApiV09Client))]
    public class GbifApiV09ClientUnitTests
    {
        /// <summary>
        /// <see cref="GbifApiV09Client"/> constructor with null <see cref="IHttpClientFactory"/> should throw <see cref="ArgumentNullException"/>.
        /// </summary>
        [Test(TestOf = typeof(GbifApiV09Client), Description = "GbifApiV09Client constructor with null IHttpClientFactory should throw ArgumentNullException.")]
        public void GbifApiV09Client_Constructor_WithNullHttpClientFactory_ShouldThrowArgumentNullException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<GbifApiV09Client>>();

            // Act + Assert
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new GbifApiV09Client(httpClientFactory: null, logger: loggerMock.Object);
            });

            Assert.AreEqual("httpClientFactory", ex.ParamName);
        }

        /// <summary>
        /// <see cref="GbifApiV09Client"/> constructor with null <see cref="ILogger"/> should throw <see cref="ArgumentNullException"/>.
        /// </summary>
        [Test(TestOf = typeof(GbifApiV09Client), Description = "GbifApiV09Client constructor with null logger should throw ArgumentNullException.")]
        public void GbifApiV09Client_Constructor_WithNullLogger_ShouldThrowArgumentNullException()
        {
            // Arrange
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();

            // Act + Assert
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new GbifApiV09Client(httpClientFactory: httpClientFactoryMock.Object, logger: null);
            });

            Assert.AreEqual("logger", ex.ParamName);
        }

        /// <summary>
        /// <see cref="GbifApiV09Client"/> constructor with null parameters should throw <see cref="ArgumentNullException"/>.
        /// </summary>
        [Test(TestOf = typeof(GbifApiV09Client), Description = "GbifApiV09Client constructor with null parameters should throw ArgumentNullException.")]
        public void GbifApiV09Client_Constructor_WithNullParameters_ShouldThrowArgumentNullException()
        {
            // Arrange + Act + Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new GbifApiV09Client(httpClientFactory: null, logger: null);
            });
        }

        /// <summary>
        /// <see cref="GbifApiV09Client"/> constructor with valid parameters should not throw.
        /// </summary>
        [Test(TestOf = typeof(GbifApiV09Client), Description = "GbifApiV09Client constructor with valid parameters should not throw.")]
        public void GbifApiV09Client_Constructor_WithValidParameters_ShouldNotThrow()
        {
            // Arrange
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var loggerMock = new Mock<ILogger<GbifApiV09Client>>();

            // Act + Assert
            Assert.DoesNotThrow(() =>
            {
                _ = new GbifApiV09Client(httpClientFactory: httpClientFactoryMock.Object, logger: loggerMock.Object);
            });
        }

        /// <summary>
        /// <see cref="GbifApiV09Client"/>.GetDataPerNameAsync with empty name should return null.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <returns>Task.</returns>
        [Test(TestOf = typeof(GbifApiV09Client), Description = "GbifApiV09Client.GetDataPerNameAsync with empty name should return null.")]
        [TestCase(null)]
        [TestCase(@"")]
        [TestCase(@"  ")]
        public async Task GbifApiV09Client_GetDataPerNameAsync_WithEmptyName_ShouldReturnNull(string name)
        {
            // Arrange
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var loggerMock = new Mock<ILogger<GbifApiV09Client>>();
            var traceId = $"test-=={name}==";
            var sut = new GbifApiV09Client(httpClientFactory: httpClientFactoryMock.Object, logger: loggerMock.Object);

            // Act
            var result = await sut.GetDataPerNameAsync(name, traceId).ConfigureAwait(false);

            // Assert
            Assert.IsNull(result);
        }

        /// <summary>
        /// <see cref="GbifApiV09Client"/>.GetDataPerNameAsync with empty name and cancellation token should return null.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="canceled">The canceled state for the token.</param>
        /// <returns>Task.</returns>
        [Test(TestOf = typeof(GbifApiV09Client), Description = "GbifApiV09Client.GetDataPerNameAsync with empty name and cancellation token should return null.")]
        [TestCase(null, false)]
        [TestCase(null, true)]
        [TestCase(@"", false)]
        [TestCase(@"", true)]
        [TestCase(@"  ", false)]
        [TestCase(@"  ", true)]
        public async Task GbifApiV09Client_GetDataPerNameAsync_WithEmptyNameAndCancellationToken_ShouldReturnNull(string name, bool canceled)
        {
            // Arrange
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var loggerMock = new Mock<ILogger<GbifApiV09Client>>();
            var traceId = $"test-=={name}==";
            CancellationToken cancellationToken = new CancellationToken(canceled: canceled);
            var sut = new GbifApiV09Client(httpClientFactory: httpClientFactoryMock.Object, logger: loggerMock.Object);

            // Act
            var result = await sut.GetDataPerNameAsync(name, traceId, cancellationToken).ConfigureAwait(false);

            // Assert
            Assert.IsNull(result);
        }

        /// <summary>
        /// <see cref="GbifApiV09Client"/>.GetDataPerNameAsync with valid name should call CreateClient once.
        /// </summary>
        [Test(TestOf = typeof(GbifApiV09Client), Description = "GbifApiV09Client.GetDataPerNameAsync with valid name should call CreateClient once.")]
        public void GbifApiV09Client_GetDataPerNameAsync_WithValidName_ShouldCallCreateClientOnce()
        {
            // Arrange
            string name = "Coleoptera";
            var traceId = $"test-=={name}==";
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var loggerMock = new Mock<ILogger<GbifApiV09Client>>();
            var sut = new GbifApiV09Client(httpClientFactory: httpClientFactoryMock.Object, logger: loggerMock.Object);

            // Act
            // Here we don't have configured HttpClient, so NullReferenceException is expected behavior.
            Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                _ = await sut.GetDataPerNameAsync(name, traceId).ConfigureAwait(false);
            });

            // Assert
            httpClientFactoryMock.Verify(m => m.CreateClient(nameof(GbifApiV09Client)), Times.Once);
        }

        /// <summary>
        /// <see cref="GbifApiV09Client"/>.GetDataPerNameAsync with valid name and empty BaseAddress should return null.
        /// </summary>
        /// <returns>Task.</returns>
        [Test(TestOf = typeof(GbifApiV09Client), Description = "GbifApiV09Client.GetDataPerNameAsync with valid name and empty BaseAddress should return null.")]
        public async Task GbifApiV09Client_GetDataPerNameAsync_WithValidNameAndEmptyBaseAddress_ShouldReturnNull()
        {
            // Arrange
            string name = "Coleoptera";
            var traceId = $"test-=={name}==";
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(f => f.CreateClient(nameof(GbifApiV09Client))).Returns(new HttpClient());
            var loggerMock = new Mock<ILogger<GbifApiV09Client>>();
            var sut = new GbifApiV09Client(httpClientFactory: httpClientFactoryMock.Object, logger: loggerMock.Object);

            // Act
            var result = await sut.GetDataPerNameAsync(name, traceId).ConfigureAwait(false);

            // Assert
            Assert.IsNull(result);
            httpClientFactoryMock.Verify(m => m.CreateClient(nameof(GbifApiV09Client)), Times.Once);
        }

        /// <summary>
        /// <see cref="GbifApiV09Client"/>.GetDataPerNameAsync with valid name and invalid BaseAddress should return null.
        /// </summary>
        /// <returns>Task.</returns>
        [Test(TestOf = typeof(GbifApiV09Client), Description = "GbifApiV09Client.GetDataPerNameAsync with valid name and invalid BaseAddress should return null.")]
        public async Task GbifApiV09Client_GetDataPerNameAsync_WithValidNameAndInvalidBaseAddress_ShouldReturnNull()
        {
            // Arrange
            string name = "Coleoptera";
            var traceId = $"test-=={name}==";
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(f => f.CreateClient(nameof(GbifApiV09Client))).Returns(new HttpClient() { BaseAddress = new Uri("https://loclhost:1/") });
            var loggerMock = new Mock<ILogger<GbifApiV09Client>>();
            var sut = new GbifApiV09Client(httpClientFactory: httpClientFactoryMock.Object, logger: loggerMock.Object);

            // Act
            var result = await sut.GetDataPerNameAsync(name, traceId).ConfigureAwait(false);

            // Assert
            Assert.IsNull(result);
            httpClientFactoryMock.Verify(m => m.CreateClient(nameof(GbifApiV09Client)), Times.Once);
            loggerMock.Verify();
        }
    }
}
