namespace ProcessingTools.Bio.Taxonomy.ServiceClient.GlobalNamesResolver.Models.Xml
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "hash", Namespace = "", IsNullable = false)]
    public class Hash
    {
        [XmlElement("id")]
        public HashId Id { get; set; }

        [XmlElement("url")]
        public HashUrl Url { get; set; }

        [XmlElement("data-sources")]
        public HashDataSources DataSources { get; set; }

        [XmlElement("data")]
        public HashData Data { get; set; }

        [XmlElement("status")]
        public HashStatus Status { get; set; }

        [XmlElement("message")]
        public HashMessage Message { get; set; }

        [XmlElement("parameters")]
        public HashParameters Parameters { get; set; }
    }
}
