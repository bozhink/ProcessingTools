namespace ProcessingTools.Documents.Services.Data.Tests
{
    using System;
    using System.IO;
    using NUnit.Framework;

    /// <summary>
    /// This set-up is required by NUnit3
    /// See https://github.com/nunit/nunit/issues/1072
    /// </summary>
    [SetUpFixture]
    public class NUnitSetUpClass
    {
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var directory = Path.GetDirectoryName(typeof(NUnitSetUpClass).Assembly.Location);
            Environment.CurrentDirectory = directory;
        }
    }
}
