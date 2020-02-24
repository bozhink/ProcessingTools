// <copyright file="BingMapService.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Maps
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using ProcessingTools.Common.Constants.Configuration;
    using ProcessingTools.Contracts.Services.Maps;

    /// <summary>
    /// Bing Maps service.
    /// </summary>
    public class BingMapService : IBingMapService
    {
        private static readonly string BingMapApiURL = "http://dev.virtualearth.net/REST/v1/Locations/US/{0}?output=json&key=" + AppSettings.BingMapsKey;

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
        /// <returns>Task.</returns>
        public async Task<string[]> GetLongitudeAndLatitudeByAddressAsync(string address)
        {
            var result = new List<string>(2);

            var uri = new Uri(string.Format(BingMapApiURL, address));
            try
            {
                var response = await this.httpClient.GetAsync(uri).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                JObject json = (JObject)JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                var coordinates = json["resourceSets"][0]["resources"][0]["point"]["coordinates"];
                result.Add(coordinates[0].ToString());
                result.Add(coordinates[1].ToString());
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error in Bing Maps service request.");
            }

            return result.ToArray();
        }
    }
}
