// <copyright file="AssemblyExtensions.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Dynamic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Assembly extensions.
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Get assemblies.
        /// </summary>
        /// <param name="assemblies">Names of assemblies to be resolved.</param>
        /// <returns>resolved <see cref="Assembly"/> objects.</returns>
        public static IEnumerable<Assembly> GetAssemblies(this IEnumerable<string> assemblies)
        {
            if (assemblies == null || !assemblies.Any())
            {
                return Array.Empty<Assembly>();
            }

            return assemblies.Select(a => Assembly.Load(a));
        }

        /// <summary>
        /// Get exported types.
        /// </summary>
        /// <param name="assemblies">Assemblies to be harvested.</param>
        /// <returns>Exported types from the specified assemblies.</returns>
        public static IEnumerable<Type> GetExportedTypes(this IEnumerable<Assembly> assemblies)
        {
            if (assemblies == null || !assemblies.Any())
            {
                return Array.Empty<Type>();
            }

            var types = new List<Type>();
            foreach (var assembly in assemblies)
            {
                types.AddRange(assembly.GetExportedTypes());
            }

            types.TrimExcess();

            return types;
        }
    }
}
