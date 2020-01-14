// <copyright file="HealthChecksExtensions.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.HealthChecks
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using Newtonsoft.Json;
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

        /// <summary>
        /// Get health check options excluding detailed version information.
        /// </summary>
        /// <param name="assembly">Calling assembly.</param>
        /// <param name="includeExceptions">Include exceptions.</param>
        /// <param name="predicate">Predicate for filtering of health checks.</param>
        /// <returns>Instance of <see cref="HealthCheckOptions"/>.</returns>
        public static HealthCheckOptions GetHealthCheckOptions(Assembly assembly, bool includeExceptions, Func<HealthCheckRegistration, bool> predicate)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return new HealthCheckOptions
            {
                AllowCachingResponses = false,
                Predicate = predicate,
                ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Degraded] = StatusCodes.Status200OK,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
                },
                ResponseWriter = (httpContext, result) =>
                {
                    httpContext.Response.ContentType = "application/json";

                    var json = new JObject
                    {
                        new JProperty("version", assembly.GetName().Version.ToString()),
                        new JProperty("status", result.Status.ToString()),
                    };

                    if (result.Entries.Any())
                    {
                        json.Add(result.GetResultsToJSON(includeExceptions));
                    }

                    return httpContext.Response.WriteAsync(json.ToString(Formatting.None));
                },
            };
        }

        /// <summary>
        /// Get health check options.
        /// </summary>
        /// <param name="assembly">Calling assembly.</param>
        /// <param name="includeExceptions">Include exceptions.</param>
        /// <returns>Instance of <see cref="HealthCheckOptions"/>.</returns>
        public static HealthCheckOptions GetHealthCheckOptions(Assembly assembly, bool includeExceptions) => GetHealthCheckOptions(assembly: assembly, includeExceptions: includeExceptions, predicate: r => true);

        /// <summary>
        /// Get health check options excluding detailed version information.
        /// </summary>
        /// <param name="assembly">Calling assembly.</param>
        /// <param name="includeExceptions">Include exceptions.</param>
        /// <returns>Instance of <see cref="HealthCheckOptions"/>.</returns>
        public static HealthCheckOptions GetHealthCheckOptionsExcludingVersion(Assembly assembly, bool includeExceptions) => GetHealthCheckOptions(assembly: assembly, includeExceptions: includeExceptions, predicate: r => r.Name != VersionHealthCheck.HealthCheckName);

        /// <summary>
        /// Get health check options for detailed version information.
        /// </summary>
        /// <param name="assembly">Calling assembly.</param>
        /// <param name="includeExceptions">Include exceptions.</param>
        /// <returns>Instance of <see cref="HealthCheckOptions"/>.</returns>
        public static HealthCheckOptions GetHealthCheckOptionsForVersion(Assembly assembly, bool includeExceptions) => GetHealthCheckOptions(assembly: assembly, includeExceptions: includeExceptions, predicate: r => r.Name == VersionHealthCheck.HealthCheckName);
    }
}
