namespace ProcessingTools.Clients.Models.Bio.Taxonomy.GlobalNamesResolver.Xml
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "results", Namespace = "", IsNullable = false)]
    public class HashDataDatumResults
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlElement("result")]
        public HashDataDatumResultsResult[] Result { get; set; }
    }
}
