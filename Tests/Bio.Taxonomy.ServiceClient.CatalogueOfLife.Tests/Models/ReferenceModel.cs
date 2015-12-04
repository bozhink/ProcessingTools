namespace ProcessingTools.Bio.Taxonomy.ServiceClient.CatalogueOfLife.Tests.Models
{
    using System;
    using System.Xml;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "reference")]
    public partial class ReferenceModel
    {
        [XmlElement("author")]
        public string Author { get; set; }

        [XmlElement("year")]
        public string Year { get; set; }

        [XmlAnyElement("title")]
        public XmlNode[] Title { get; set; }

        [XmlAnyElement("source")]
        public XmlNode[] Source { get; set; }
    }
}
