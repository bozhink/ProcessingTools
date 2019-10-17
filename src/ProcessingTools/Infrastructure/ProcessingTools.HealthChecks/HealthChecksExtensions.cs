// <copyright file="HealthChecksExtensions.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.HealthChecks
{
    using System.Linq;
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Health checks extensions.
    /// </summary>
    public static class HealthChecksExtensions
    {
        /// <summary>
        /// Get results to JSON.
        /// </summary>
        /// <param name="result"><see cref="HealthReport"/> to be evaluated.</param>
        /// <param name="includeExceptions">Include exceptions.</param>
        /// <returns>Serialized result as JSON.</returns>
        public static JProperty GetResultsToJSON(this HealthReport result, bool includeExceptions)
        {
            if (result != null && result.Entries.Any())
            {
                return new JProperty("results", new JObject(result.Entries.Select(pair =>
                {
                    var value = new JObject
                    {
                        new JProperty("status", pair.Value.Status.ToString()),
                    };

                    if (!string.IsNullOrEmpty(pair.Value.Description))
                    {
                        value.Add(new JProperty("description", pair.Value.Description));
                    }

                    if (pair.Value.Data != null && pair.Value.Data.Any())
                    {
                        value.Add(new JProperty("data", new JObject(pair.Value.Data.Select(p => new JProperty(p.Key, p.Value)))));
                    }

                    if (pair.Value.Exception != null && includeExceptions)
                    {
                        value.Add(new JProperty("exception", pair.Value.Exception.ToString()));
                    }

                    return new JProperty(pair.Key, value);
                })));
            }

            return default;
        }
    }
}
