namespace ProcessingTools.Clients.Models.Bio.Taxonomy.GlobalNamesResolver.Xml
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "status", Namespace = "", IsNullable = false)]
    public class HashStatus
    {
        [XmlText]
        public string Value { get; set; }
    }
}