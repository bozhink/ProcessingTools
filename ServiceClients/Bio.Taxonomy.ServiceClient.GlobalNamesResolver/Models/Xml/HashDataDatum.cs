namespace ProcessingTools.Bio.Taxonomy.ServiceClient.GlobalNamesResolver.Models.Xml
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "datum", Namespace = "", IsNullable = false)]
    public class HashDataDatum
    {
        [XmlElement("supplied-name-string")]
        public HashDataDatumSuppliedNameString SuppliedNameString { get; set; }

        [XmlElement("is-known-name")]
        public HashDataDatumIsKnownName IsKnownName { get; set; }

        [XmlElement("results")]
        public HashDataDatumResults Results { get; set; }
    }
}
