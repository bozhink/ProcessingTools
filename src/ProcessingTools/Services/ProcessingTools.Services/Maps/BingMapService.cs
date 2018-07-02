// <copyright file="BingMapService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Maps
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Services.Contracts.Maps;

    /// <summary>
    /// Bing Maps service.
    /// </summary>
    public class BingMapService : IBingMapService
    {
        private static readonly string BingMapApiURL = "http://dev.virtualearth.net/REST/v1/Locations/US/{0}?output=json&key=" + AppSettings.BingMapKey;

        /// <summary>
        /// Get longitude and latitude based on address.
        /// Reference URL: https://msdn.microsoft.com/en-us/library/ff701711.aspx.
        /// </summary>
        /// <param name="address">Address to get longitude and latitude.</param>
        /// <returns>Task</returns>
        public async Task<string[]> GetLongitudeAndLatitudeByAddressAsync(string address)
        {
            var result = new List<string>(2);
            using (var client = new HttpClient())
            {
                var uri = string.Format(BingMapApiURL, address);
                try
                {
                    var response = await client.GetAsync(uri);
                    response.EnsureSuccessStatusCode();
                    JObject json = (JObject)JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                    var coordinates = json["resourceSets"][0]["resources"][0]["point"]["coordinates"];
                    result.Add(coordinates[0].ToString());
                    result.Add(coordinates[1].ToString());
                }
                catch
                {
                    // TODO: Add logger.
                }
            }

            return result.ToArray();
        }
    }
}
