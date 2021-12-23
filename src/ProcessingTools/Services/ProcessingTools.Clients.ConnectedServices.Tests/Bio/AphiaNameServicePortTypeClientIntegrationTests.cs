// <copyright file="AphiaNameServicePortTypeClientIntegrationTests.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.ConnectedServices.Integration.Tests.Bio
{
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using ProcessingTools.Clients.ConnectedServices.Bio.AphiaServiceReference;
    using ProcessingTools.Clients.ConnectedServices.Interceptors;

    /// <summary>
    /// <see cref="AphiaNameServicePortTypeClient"/> integration tests.
    /// </summary>
    [TestFixture]
    public class AphiaNameServicePortTypeClientIntegrationTests
    {
        /// <summary>
        /// <see cref="AphiaNameServicePortTypeClient"/> GetAphiaRecords with valid parameters should work.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Test]
        [Timeout(20000)]
        [Ignore(reason: "Net dependent integration test")] // Net dependent integration test
        public async Task AphiaNameServicePortTypeClient_GetAphiaRecords_WithValidParameters_ShouldWork()
        {
            // Arrange
            string scientificname = "Anodontiglanis";
            string rank = "genus";

            getAphiaRecordsRequest request = new getAphiaRecordsRequest(scientificname: scientificname, like: true, fuzzy: true, marine_only: false, offset: 0);
            getAphiaRecordsResponse response = null;

            Binding binding = new BasicHttpBinding
            {
                MaxBufferSize = int.MaxValue,
                ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max,
                MaxReceivedMessageSize = int.MaxValue,
                AllowCookies = true,
            };

            EndpointAddress endpoint = new EndpointAddress("http://www.marinespecies.org/aphia.php?p=soap");

            InterceptRequestAndResponseEndpointBehavior endpointBehavior = new InterceptRequestAndResponseEndpointBehavior();

            // Act
            try
            {
                using (AphiaNameServicePortTypeClient client = new AphiaNameServicePortTypeClient(binding, endpoint))
                {
                    client.Endpoint.EndpointBehaviors.Add(endpointBehavior);

                    await client.OpenAsync().ConfigureAwait(false);

                    Task<getAphiaRecordsResponse> task = client.getAphiaRecordsAsync(request);
                    response = await task.ConfigureAwait(false);

                    await client.CloseAsync().ConfigureAwait(false);
                }
            }
            finally
            {
                TestContext.WriteLine(endpointBehavior.RequestContent);
                TestContext.WriteLine(endpointBehavior.ResponseContent);
            }

            // Assert
            Assert.IsNotNull(response);

            // Act
            AphiaRecord[] records = response.@return;

            // Assert
            Assert.IsNotNull(records);
            Assert.IsTrue(records.Any());

            // Act
            AphiaRecord genusRecord = records.FirstOrDefault();

            // Assert
            Assert.IsNotNull(genusRecord);
            Assert.IsTrue(string.Compare(rank, genusRecord.rank, System.StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.AreEqual(scientificname, genusRecord.scientificname);
            Assert.AreEqual(scientificname, genusRecord.valid_name);
        }
    }
}
