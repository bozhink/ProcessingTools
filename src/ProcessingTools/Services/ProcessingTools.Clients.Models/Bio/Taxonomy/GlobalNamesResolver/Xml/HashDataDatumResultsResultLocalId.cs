namespace ProcessingTools.Clients.Models.Bio.Taxonomy.GlobalNamesResolver.Xml
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "local-id", Namespace = "", IsNullable = false)]
    public class HashDataDatumResultsResultLocalId
    {
        [XmlText]
        public string Value { get; set; }
    }
}
