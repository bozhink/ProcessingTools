namespace ProcessingTools.DocumentProvider.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FileProcessorTests
    {
        [TestMethod]
        public void FileProcessor_TestMethod1()
        {
            var fileProcessor = new FileProcessor();
            Console.WriteLine(Environment.GetEnvironmentVariable("TEMP"));
        }
    }
}
