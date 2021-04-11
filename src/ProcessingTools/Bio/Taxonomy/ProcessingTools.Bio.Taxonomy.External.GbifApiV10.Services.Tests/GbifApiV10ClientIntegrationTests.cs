// <copyright file="GbifApiV10ClientIntegrationTests.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.External.GbifApiV10.Services.Tests
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// <see cref="GbifApiV10Client"/> unit tests.
    /// </summary>
    [TestFixture(Category = "Integration ", TestOf = typeof(GbifApiV10Client))]
    public class GbifApiV10ClientIntegrationTests
    {
        /// <summary>
        /// <see cref="GbifApiV10Client"/>.GetDataPerNameAsync with valid name and valid BaseAddress should return valid result.
        /// </summary>
        /// <returns>Task.</returns>
        [Test(TestOf = typeof(GbifApiV10Client), Description = "GbifApiV10Client.GetDataPerNameAsync with valid name and valid BaseAddress should valid result.")]
        public async Task GbifApiV10Client_GetDataPerNameAsync_WithValidNameAndValidBaseAddress_ShouldReturnValidResult()
        {
            // Arrange
            string name = "Coleoptera";
            var traceId = $"test-=={name}==";
            Uri baseAddress = new Uri("https://api.gbif.org/");
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(f => f.CreateClient(nameof(GbifApiV10Client))).Returns(new HttpClient() { BaseAddress = baseAddress });
            var loggerMock = new Mock<ILogger<GbifApiV10Client>>();
            var sut = new GbifApiV10Client(httpClientFactory: httpClientFactoryMock.Object, logger: loggerMock.Object);

            // Act
            var result = await sut.GetDataPerNameAsync(name, traceId).ConfigureAwait(false);

            // Assert
            Assert.IsNotNull(result);
            httpClientFactoryMock.Verify(m => m.CreateClient(nameof(GbifApiV10Client)), Times.Once);
            loggerMock.Verify();
        }

        /// <summary>
        /// <see cref="GbifApiV10Client"/>.GetDataPerNameAsync with valid name and valid BaseAddress and cancellation token false should return valid result.
        /// </summary>
        /// <returns>Task.</returns>
        [Test(TestOf = typeof(GbifApiV10Client), Description = "GbifApiV10Client.GetDataPerNameAsync with valid name and valid BaseAddress and cancellation token false should valid result.")]
        public async Task GbifApiV10Client_GetDataPerNameAsync_WithValidNameAndValidBaseAddressAndCancellationTokenFalse_ShouldReturnValidResult()
        {
            // Arrange
            string name = "Coleoptera";
            var traceId = $"test-=={name}==";
            CancellationToken cancellationToken = new CancellationToken(false);
            Uri baseAddress = new Uri("https://api.gbif.org/");
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(f => f.CreateClient(nameof(GbifApiV10Client))).Returns(new HttpClient() { BaseAddress = baseAddress });
            var loggerMock = new Mock<ILogger<GbifApiV10Client>>();
            var sut = new GbifApiV10Client(httpClientFactory: httpClientFactoryMock.Object, logger: loggerMock.Object);

            // Act
            var result = await sut.GetDataPerNameAsync(name, traceId, cancellationToken).ConfigureAwait(false);

            // Assert
            Assert.IsNotNull(result);
            httpClientFactoryMock.Verify(m => m.CreateClient(nameof(GbifApiV10Client)), Times.Once);
            loggerMock.Verify();
        }

        /// <summary>
        /// <see cref="GbifApiV10Client"/>.GetDataPerNameAsync with valid name and valid BaseAddress and cancellation token true should return null.
        /// </summary>
        /// <returns>Task.</returns>
        [Test(TestOf = typeof(GbifApiV10Client), Description = "GbifApiV10Client.GetDataPerNameAsync with valid name and valid BaseAddress and cancellation token true should null.")]
        public async Task GbifApiV10Client_GetDataPerNameAsync_WithValidNameAndValidBaseAddressAndCancellationTokenTrue_ShouldReturnNull()
        {
            // Arrange
            string name = "Coleoptera";
            var traceId = $"test-=={name}==";
            CancellationToken cancellationToken = new CancellationToken(true);
            Uri baseAddress = new Uri("https://api.gbif.org/");
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(f => f.CreateClient(nameof(GbifApiV10Client))).Returns(new HttpClient() { BaseAddress = baseAddress });
            var loggerMock = new Mock<ILogger<GbifApiV10Client>>();
            var sut = new GbifApiV10Client(httpClientFactory: httpClientFactoryMock.Object, logger: loggerMock.Object);

            // Act
            var result = await sut.GetDataPerNameAsync(name, traceId, cancellationToken).ConfigureAwait(false);

            // Assert
            Assert.IsNull(result);
            httpClientFactoryMock.Verify(m => m.CreateClient(nameof(GbifApiV10Client)), Times.Once);
            loggerMock.Verify();
        }
    }
}
