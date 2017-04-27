namespace ProcessingTools.Net.Tests.Models
{
    using System.Xml.Serialization;

    [XmlType(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/ProcessingTools.TestWebApiServer.Models")]
    [XmlRoot(Namespace = "http://schemas.datacontract.org/2004/07/ProcessingTools.TestWebApiServer.Models", IsNullable = false)]
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }
    }
}
