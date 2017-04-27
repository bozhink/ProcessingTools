namespace ProcessingTools.Net.Tests.Models
{
    using System.Xml.Serialization;

    [XmlType(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/ProcessingTools.TestWebApiServer.Models")]
    [XmlRoot(Namespace = "http://schemas.datacontract.org/2004/07/ProcessingTools.TestWebApiServer.Models", IsNullable = false)]
    public partial class ArrayOfProduct
    {
        [XmlElement("Product")]
        public Product[] Products { get; set; }
    }
}
