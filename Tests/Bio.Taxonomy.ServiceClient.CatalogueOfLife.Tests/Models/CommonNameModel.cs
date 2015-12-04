namespace ProcessingTools.Bio.Taxonomy.ServiceClient.CatalogueOfLife.Tests.Models
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "common_name")]
    public class CommonNameModel
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("language")]
        public string Language { get; set; }

        [XmlElement("country")]
        public string Country { get; set; }

        [XmlArray("references")]
        [XmlArrayItem("reference", typeof(ReferenceModel))]
        public ReferenceModel[] References { get; set; }
    }
}
