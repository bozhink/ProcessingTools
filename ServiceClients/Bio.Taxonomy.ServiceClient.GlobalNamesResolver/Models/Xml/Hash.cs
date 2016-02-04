namespace ProcessingTools.Bio.Taxonomy.ServiceClient.GlobalNamesResolver.Models.Xml
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "hash", Namespace = "", IsNullable = false)]
    public class Hash
    {
        [XmlElement("data")]
        public HashData Data { get; set; }

        [XmlElement("data-sources")]
        public HashDataSources DataSources { get; set; }

        [XmlElement("id")]
        public string Id { get; set; }

        [XmlElement("message")]
        public string Message { get; set; }

        [XmlElement("parameters")]
        public HashParameters Parameters { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("url")]
        public string Url { get; set; }
    }
}
