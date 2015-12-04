namespace ProcessingTools.Bio.Taxonomy.ServiceClient.CatalogueOfLife.Tests.Models
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "taxon")]
    public class TaxonModel
    {
        [XmlElement("id")]
        public string Id { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("rank")]
        public string Rank { get; set; }

        [XmlElement("name_html")]
        public string NameHtml { get; set; }

        [XmlElement("url")]
        public string Url { get; set; }

        [XmlElement("is_extinct")]
        public bool IsExtinct { get; set; }
    }
}