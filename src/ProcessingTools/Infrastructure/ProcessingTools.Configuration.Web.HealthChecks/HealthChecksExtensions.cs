// <copyright file="HealthChecksExtensions.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.Web.HealthChecks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text.Json;
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Diagnostics.HealthChecks;

    /// <summary>
    /// Health checks extensions.
    /// </summary>
    public static class HealthChecksExtensions
    {
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
                    AssemblyName assemblyName = assembly.GetName();

                    var model = new Dictionary<string, object>()
                    {
                        { "version", assemblyName.Version.ToString() as object },
                        { "status", result.Status.ToString() as object },
                    };

                    if (result.Entries.Any())
                    {
                        foreach (var entry in result.Entries)
                        {
                            var value = new Dictionary<string, object>()
                            {
                                { "status", entry.Value.Status.ToString() },
                            };

                            if (!string.IsNullOrEmpty(entry.Value.Description))
                            {
                                value.TryAdd("description", entry.Value.Description);
                            }

                            if (entry.Value.Data != null && entry.Value.Data.Any())
                            {
                                value.TryAdd("data", entry.Value.Data);
                            }

                            if (entry.Value.Exception != null && includeExceptions)
                            {
                                value.TryAdd("exception", entry.Value.Exception.ToString());
                            }

                            model.TryAdd(entry.Key, value);
                        }
                    }

                    string json = JsonSerializer.Serialize(model, model.GetType(), new JsonSerializerOptions
                    {
                        AllowTrailingCommas = false,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    });

                    httpContext.Response.ContentType = "application/json";
                    return httpContext.Response.WriteAsync(json);
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
