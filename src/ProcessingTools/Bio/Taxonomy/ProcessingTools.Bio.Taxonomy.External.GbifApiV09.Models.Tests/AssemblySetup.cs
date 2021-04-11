// <copyright file="AssemblySetup.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.External.GbifApiV09.Models.Tests
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
            Directory.SetCurrentDirectory(Path.GetDirectoryName(typeof(AssemblySetup).Assembly.Location));
        }
    }
}
