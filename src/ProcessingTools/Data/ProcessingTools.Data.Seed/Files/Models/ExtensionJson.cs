// <copyright file="ExtensionJson.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Seed.Files.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// Extension JSON model.
    /// </summary>
    public class ExtensionJson
    {
        /// <summary>
        /// Gets or sets the extension.
        /// </summary>
        [JsonProperty("extension")]
        public string Extension { get; set; }

        /// <summary>
        /// Gets or sets the mimetype.
        /// </summary>
        [JsonProperty("mimetype")]
        public string Mimetype { get; set; }

        /// <summary>
        /// Gets or sets the mimesubtype.
        /// </summary>
        [JsonProperty("mimesubtype")]
        public string Mimesubtype { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}