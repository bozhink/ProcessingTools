// <copyright file="CheckSystemResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Application
{
    using Newtonsoft.Json;

    /// <summary>
    /// Check system response model.
    /// </summary>
    public class CheckSystemResponseModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether the system is OK.
        /// </summary>
        [JsonProperty("ok")]
        public bool IsOK { get; set; }

        /// <summary>
        /// Gets or sets the response message.
        /// </summary>
        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
    }
}
