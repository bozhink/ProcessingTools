namespace ProcessingTools.Bio.Taxonomy.ServiceClient.GlobalNamesResolver.Models.Xml
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "edit-distance", Namespace = "", IsNullable = false)]
    public class HashDataDatumResultsResultEditDistance
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlText]
        public int Value { get; set; }
    }
}
