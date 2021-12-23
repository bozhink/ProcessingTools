// <copyright file="VersionHealthCheck.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.Web.HealthChecks
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Diagnostics.HealthChecks;

    /// <summary>
    /// Version health check.
    /// </summary>
    public class VersionHealthCheck : IHealthCheck
    {
        /// <summary>
        /// Name of the health check.
        /// </summary>
        public const string HealthCheckName = "version";

        /// <inheritdoc/>
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var data = assemblies.OrderBy(a => a.FullName)
                .Select(a => a.GetName())
                .ToDictionary<AssemblyName, string, object>(a => a.Name, a => new AssemblyVersionModel
                {
                    Version = a.Version.ToString(),
                    Name = a.Name,
                    FullName = a.FullName,
                });

            return Task.FromResult(HealthCheckResult.Healthy(description: "Versions", data: data));
        }

        /// <summary>
        /// Assembly version model for output.
        /// </summary>
        private class AssemblyVersionModel
        {
            /// <summary>
            /// Gets or sets the version of the assembly.
            /// </summary>
            public string Version { get; set; }

            /// <summary>
            /// Gets or sets the name of the assembly.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the full name of the assembly.
            /// </summary>
            public string FullName { get; set; }
        }
    }
}
