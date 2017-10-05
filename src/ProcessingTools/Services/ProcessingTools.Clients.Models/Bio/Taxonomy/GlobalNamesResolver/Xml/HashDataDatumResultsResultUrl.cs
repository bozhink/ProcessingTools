namespace ProcessingTools.Clients.Models.Bio.Taxonomy.GlobalNamesResolver.Xml
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "url", Namespace = "", IsNullable = false)]
    public class HashDataDatumResultsResultUrl
    {
        [XmlText]
        public string Value { get; set; }
    }
}
