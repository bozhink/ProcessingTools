namespace ProcessingTools.Bio.Taxonomy.ServiceClient.CatalogueOfLife.Models
{
    using System;
    using System.Xml;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public abstract class SingleRecord
    {
        [XmlElement("id")]
        public string Id { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("rank")]
        public string Rank { get; set; }

        [XmlElement("genus")]
        public string Genus { get; set; }

        [XmlElement("subgenus")]
        public string Subgenus { get; set; }

        [XmlElement("species")]
        public string Species { get; set; }

        [XmlElement("infraspecies_marker")]
        public string InfraspeciesMarker { get; set; }

        [XmlElement("infraspecies")]
        public string Infraspecies { get; set; }

        [XmlElement("author")]
        public string Author { get; set; }

        [XmlElement("record_scrutiny_date")]
        public RecordScrutinyDate RecordScrutinyDate { get; set; }

        [XmlElement("online_resource")]
        public string OnlineResource { get; set; }

        [XmlElement("is_extinct")]
        public bool IsExtinct { get; set; }

        [XmlElement("source_database")]
        public string SourceDatabase { get; set; }

        [XmlElement("source_database_url")]
        public string SourceDatabaseUrl { get; set; }

        [XmlElement("bibliographic_citation")]
        public string BibliographicCitation { get; set; }

        [XmlElement("distribution")]
        public string Distribution { get; set; }

        [XmlElement("name_status")]
        public string NameStatus { get; set; }

        [XmlAnyElement("name_html")]
        public XmlNode[] NameHtml { get; set; }

        [XmlElement("url")]
        public string Url { get; set; }

        [XmlArray("references")]
        [XmlArrayItem("reference", typeof(Reference))]
        public Reference[] References { get; set; }
    }
}
