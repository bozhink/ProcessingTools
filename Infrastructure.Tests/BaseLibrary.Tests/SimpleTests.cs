namespace ProcessingTools.BaseLibrary.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using ProcessingTools.Bio.Taxonomy.ServiceClient.Aphia;

    [TestClass]
    public class SimpleTests
    {
        [TestMethod]
        [Ignore]
        public void TestAphiaService()
        {
            var server = new AphiaNameService();
            var records = server.getAphiaRecords("Anodontiglanis", true, true, false, 0);

            Console.WriteLine(records?.Length);
            if (records != null)
            {
                foreach (AphiaRecord record in records)
                {
                    Console.WriteLine(record?.rank);
                    Console.WriteLine(record?.scientificname);
                    Console.WriteLine(record?.valid_name);
                }
            }
        }
    }
}
