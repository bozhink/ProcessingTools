﻿// <copyright file="AssemblySetup.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace ProcessingTools.Data.Seed.Integration.Tests
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
