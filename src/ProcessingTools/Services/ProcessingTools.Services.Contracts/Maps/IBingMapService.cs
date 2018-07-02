// <copyright file="IBingMapService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Maps
{
    using System.Threading.Tasks;

    /// <summary>
    /// Bing Maps service.
    /// </summary>
    public interface IBingMapService
    {
        /// <summary>
        /// Gets longitude and latitude by address.
        /// </summary>
        /// <param name="address">Address to be requested.</param>
        /// <returns>Pair of coordinates.</returns>
        Task<string[]> GetLongitudeAndLatitudeByAddressAsync(string address);
    }
}
