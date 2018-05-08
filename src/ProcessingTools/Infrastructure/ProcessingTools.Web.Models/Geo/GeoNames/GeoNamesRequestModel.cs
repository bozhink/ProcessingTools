// <copyright file="GeoNamesRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Geo.GeoNames
{
    using System;
    using System.Linq;
    using ProcessingTools.Models.Contracts.Geo;

    /// <summary>
    /// GeoNames request model
    /// </summary>
    public class GeoNamesRequestModel
    {
        /// <summary>
        /// Gets or sets the geo-names to be imported - one per row.
        /// </summary>
        public string Names { get; set; }

        /// <summary>
        /// Maps provided names to array of <see cref="IGeoName"/>.
        /// </summary>
        /// <returns>Processed array of <see cref="IGeoName"/></returns>
        public IGeoName[] ToArray()
        {
            if (string.IsNullOrWhiteSpace(this.Names))
            {
                return Array.Empty<IGeoName>();
            }

            return this.Names.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => new GeoNameRequestModel
                {
                    Name = x.Trim(new[] { '\r' })
                })
                .ToArray();
        }
    }
}
