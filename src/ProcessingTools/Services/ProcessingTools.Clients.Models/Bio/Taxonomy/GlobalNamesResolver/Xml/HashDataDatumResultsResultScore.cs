namespace ProcessingTools.Clients.Models.Bio.Taxonomy.GlobalNamesResolver.Xml
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "score", Namespace = "", IsNullable = false)]
    public class HashDataDatumResultsResultScore
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlText]
        public double Value { get; set; }
    }
}
