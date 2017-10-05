namespace ProcessingTools.Clients.Models.Bio.Taxonomy.GlobalNamesResolver.Xml
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "id", Namespace = "", IsNullable = false)]
    public class HashId
    {
        [XmlText]
        public string Value { get; set; }
    }
}
