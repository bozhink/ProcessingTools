namespace ProcessingTools.Bio.Taxonomy.ServiceClient.CatalogueOfLife.Tests.Models
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "record_scrutiny_date")]
    public partial class RecordScrutinyDateModel
    {
        [XmlElement("scrutiny")]
        public string Scrutiny { get; set; }
    }
}
