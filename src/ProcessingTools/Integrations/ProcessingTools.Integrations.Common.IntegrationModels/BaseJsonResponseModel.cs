// <copyright file="BaseJsonResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Integrations.Common.IntegrationModels
{
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Base JSON response model.
    /// </summary>
    public class BaseJsonResponseModel
    {
        /// <summary>
        /// Gets or sets the extension data.
        /// </summary>
        [JsonExtensionData]
        public Dictionary<string, JsonElement>? ExtensionData { get; set; }
    }
}
