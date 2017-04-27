namespace ProcessingTools.Bio.Taxonomy.ServiceClient.GlobalNamesResolver.Models.Xml
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "match-type", Namespace = "", IsNullable = false)]
    public class HashDataDatumResultsResultMatchType
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlText]
        public byte Value { get; set; }
    }
}
