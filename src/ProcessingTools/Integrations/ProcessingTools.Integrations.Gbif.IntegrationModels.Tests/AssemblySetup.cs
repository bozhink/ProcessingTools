// <copyright file="AssemblySetup.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Integrations.Gbif.IntegrationModels.Tests
{
    using System.IO;
    using NUnit.Framework;

    /// <summary>
    /// Assembly setup.
    /// </summary>
    [SetUpFixture]
    public static class AssemblySetup
    {
        /// <summary>
        /// One-time set-up.
        /// </summary>
        [OneTimeSetUp]
        public static void OneTimeSetUp()
        {
            string location = typeof(AssemblySetup).Assembly.Location;
            string? currentAssemblyPath = Path.GetDirectoryName(location);

            if (!string.IsNullOrEmpty(currentAssemblyPath))
            {
                Directory.SetCurrentDirectory(currentAssemblyPath);
            }
        }
    }
}
