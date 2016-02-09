namespace ProcessingTools.Bio.Taxonomy.ServiceClient.GlobalNamesResolver.Models.Xml
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "supplied-name-string", Namespace = "", IsNullable = false)]
    public class HashDataDatumSuppliedNameString
    {
        [XmlText]
        public string Value { get; set; }
    }
}