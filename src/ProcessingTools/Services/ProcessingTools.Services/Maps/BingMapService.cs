// <copyright file="BingMapService.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Maps
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using ProcessingTools.Contracts.Services.Maps;

    /// <summary>
    /// Bing Maps service.
    /// </summary>
    public class BingMapService : IBingMapService
    {
        private readonly HttpClient httpClient;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BingMapService"/> class.
        /// </summary>
        /// <param name="httpClient">Instance of <see cref="HttpClient"/>.</param>
        /// <param name="logger">Logger.</param>
        public BingMapService(HttpClient httpClient, ILogger<BingMapService> logger)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get longitude and latitude based on address.
        /// Reference URL: https://msdn.microsoft.com/en-us/library/ff701711.aspx.
        /// </summary>
        /// <param name="address">Address to get longitude and latitude.</param>
        /// <param name="serviceKey">Key for the Bing Maps service.</param>
        /// <returns>Pair of coordinates.</returns>
        public Task<string[]> GetLongitudeAndLatitudeByAddressAsync(string address, string serviceKey)
        {
            if (string.IsNullOrEmpty(serviceKey))
            {
                throw new ArgumentNullException(nameof(serviceKey));
            }

            if (string.IsNullOrEmpty(address))
            {
                return Task.FromResult(Array.Empty<string>());
            }

            return this.GetLongitudeAndLatitudeByAddressInternalAsync(address, serviceKey);
        }

        private async Task<string[]> GetLongitudeAndLatitudeByAddressInternalAsync(string address, string serviceKey)
        {
            Uri baseAddress = new Uri($"http://dev.virtualearth.net");

            string[] result = new string[2];

            try
            {
                string requestAddress = WebUtility.UrlEncode(address);

                var requestUri = new Uri(baseAddress, $"/REST/v1/Locations/US/{requestAddress}?output=json&key={serviceKey}");

                var response = await this.httpClient.GetAsync(requestUri).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                JObject json = (JObject)JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                var coordinates = json["resourceSets"][0]["resources"][0]["point"]["coordinates"];

                result[0] = coordinates[0].ToString();
                result[1] = coordinates[1].ToString();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, string.Empty);
            }

            return result;
        }
    }
}
