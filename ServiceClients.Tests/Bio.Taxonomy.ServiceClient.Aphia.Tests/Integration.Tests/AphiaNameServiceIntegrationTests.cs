namespace ProcessingTools.Bio.Taxonomy.ServiceClient.Aphia.Tests.Integration.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using ProcessingTools.Bio.Taxonomy.ServiceClient.Aphia;

    [TestClass]
    public class AphiaNameServiceIntegrationTests
    {
        [TestMethod]
        [Timeout(20000)]
        [Ignore]
        public void AphiaService_GetAphiaRecordsWithValidParameters_ShouldWork()
        {
            var service = new AphiaNameService();
            var records = service.getAphiaRecords("Anodontiglanis", true, true, false, 0);

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
