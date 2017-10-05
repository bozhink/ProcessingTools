namespace ProcessingTools.Clients.Models.Bio.Taxonomy.GlobalNamesResolver.Xml
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "data", Namespace = "", IsNullable = false)]
    public class HashData
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlElement("datum")]
        public HashDataDatum[] Datum { get; set; }
    }
}
