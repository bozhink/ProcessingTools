// <copyright file="VersionHealthCheck.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.HealthChecks
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Version health check.
    /// </summary>
    public class VersionHealthCheck : IHealthCheck
    {
        /// <inheritdoc/>
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var data = assemblies.OrderBy(a => a.FullName)
                .Select(a => a.GetName())
                .ToDictionary<AssemblyName, string, object>(a => a.Name, a => new JObject
                {
                    new JProperty("version", a.Version.ToString()),
                    new JProperty("name", a.Name),
                    new JProperty("fullName", a.FullName),
                });

            return Task.FromResult(HealthCheckResult.Healthy(description: "Versions", data: data));
        }
    }
}
