namespace ProcessingTools.Clients.Models.Bio.Taxonomy.GlobalNamesResolver.Xml
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "global-id", Namespace = "", IsNullable = false)]
    public class HashDataDatumResultsResultGlobalId
    {
        [XmlText]
        public string Value { get; set; }
    }
}
