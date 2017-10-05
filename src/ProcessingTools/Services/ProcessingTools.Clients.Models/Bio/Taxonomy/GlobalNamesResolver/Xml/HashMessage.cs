namespace ProcessingTools.Clients.Models.Bio.Taxonomy.GlobalNamesResolver.Xml
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "message", Namespace = "", IsNullable = false)]
    public class HashMessage
    {
        [XmlText]
        public string Value { get; set; }
    }
}