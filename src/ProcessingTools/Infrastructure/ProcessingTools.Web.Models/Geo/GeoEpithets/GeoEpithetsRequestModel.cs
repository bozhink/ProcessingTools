// <copyright file="GeoEpithetsRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Geo.GeoEpithets
{
    using System;
    using System.Linq;
    using ProcessingTools.Models.Contracts.Geo;

    /// <summary>
    /// GeoEpithets request model
    /// </summary>
    public class GeoEpithetsRequestModel
    {
        /// <summary>
        /// Gets or sets the geo-names to be imported - one per row.
        /// </summary>
        public string Names { get; set; }

        /// <summary>
        /// Maps provided names to array of <see cref="IGeoEpithet"/>.
        /// </summary>
        /// <returns>Processed array of <see cref="IGeoEpithet"/></returns>
        public IGeoEpithet[] ToArray()
        {
            if (string.IsNullOrWhiteSpace(this.Names))
            {
                return Array.Empty<IGeoEpithet>();
            }

            return this.Names.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => new GeoEpithetRequestModel
                {
                    Name = x.Trim(new[] { '\r' })
                })
                .ToArray();
        }
    }
}
