namespace ProcessingTools.Bio.Taxonomy.ServiceClient.GlobalNamesResolver.Models.Xml
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "current-name-string", Namespace = "", IsNullable = false)]
    public class HashDataDatumResultsResultCurrentNameString
    {
        [XmlText]
        public string Value { get; set; }
    }
}
