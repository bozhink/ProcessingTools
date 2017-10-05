namespace ProcessingTools.Clients.Models.Bio.Taxonomy.GlobalNamesResolver.Xml
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "gni-uuid", Namespace = "", IsNullable = false)]
    public class HashDataDatumResultsResultGniUuid
    {
        [XmlText]
        public string Value { get; set; }
    }
}
