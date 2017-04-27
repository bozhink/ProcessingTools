namespace ProcessingTools.Bio.Taxonomy.ServiceClient.GlobalNamesResolver.Models.Xml
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
