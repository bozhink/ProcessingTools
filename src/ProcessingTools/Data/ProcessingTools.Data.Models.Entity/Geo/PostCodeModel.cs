// <copyright file="PostCodeModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Geo
{
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// Post code model.
    /// </summary>
    public class PostCodeModel : IPostCode
    {
        /// <summary>
        /// Gets or sets the ID of the city.
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// Gets or sets the value of the post code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the ID of the post code.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the post code type.
        /// </summary>
        public PostCodeType Type { get; set; }
    }
}
