namespace ProcessingTools.Clients.Models.Bio.Taxonomy.GlobalNamesResolver.Xml
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "data-source-id", Namespace = "", IsNullable = false)]
    public class HashDataDatumResultsResultDataSourceId
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlText]
        public int Value { get; set; }
    }
}
