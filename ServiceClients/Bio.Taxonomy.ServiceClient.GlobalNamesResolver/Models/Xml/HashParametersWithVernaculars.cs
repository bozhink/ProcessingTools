namespace ProcessingTools.Bio.Taxonomy.ServiceClient.GlobalNamesResolver.Models.Xml
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "with-vernaculars", Namespace = "", IsNullable = false)]
    public class HashParametersWithVernaculars
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlText]
        public bool Value { get; set; }
    }
}
