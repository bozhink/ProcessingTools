namespace ProcessingTools.Clients.Models.Bio.Taxonomy.GlobalNamesResolver.Xml
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "name-string", Namespace = "", IsNullable = false)]
    public class HashDataDatumResultsResultNameString
    {
        [XmlText]
        public string Value { get; set; }
    }
}
