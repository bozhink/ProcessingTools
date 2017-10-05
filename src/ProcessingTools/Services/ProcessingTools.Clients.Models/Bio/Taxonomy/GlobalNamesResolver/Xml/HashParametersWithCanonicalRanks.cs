namespace ProcessingTools.Clients.Models.Bio.Taxonomy.GlobalNamesResolver.Xml
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "with-canonical-ranks", Namespace = "", IsNullable = false)]
    public class HashParametersWithCanonicalRanks
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlText]
        public bool Value { get; set; }
    }
}
