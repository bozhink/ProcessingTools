
namespace ProcessingTools.Bio.Taxonomy.ServiceClient.CatalogueOfLife.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Xml.Serialization;
    using Models;
    using System.IO;
    [TestClass]
    public class ModelTests
    {
        [TestMethod]
        public void CoLModel_Test_Deserialization()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ResponseModel));
            ResponseModel response = null;

            using (var stream = new FileStream(@"DataFiles\\Coleoptera-example-response.xml", FileMode.Open))
            {
                response = serializer.Deserialize(stream) as ResponseModel;
            }

            Console.WriteLine(response.name);
        }
    }
}
