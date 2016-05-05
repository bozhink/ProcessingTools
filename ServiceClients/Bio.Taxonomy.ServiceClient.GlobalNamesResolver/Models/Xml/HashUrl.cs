namespace ProcessingTools.Bio.Taxonomy.ServiceClient.GlobalNamesResolver.Models.Xml
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "url", Namespace = "", IsNullable = false)]
    public class HashUrl
    {
        [XmlText]
        public string Value { get; set; }
    }
}