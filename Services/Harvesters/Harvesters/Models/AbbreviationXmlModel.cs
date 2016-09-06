namespace ProcessingTools.Harvesters.Models
{
    using System;
    using System.Xml.Serialization;

    using Contracts;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "abbreviation")]
    public class AbbreviationXmlModel : IAbbreviationModel
    {
        [XmlAttribute("content-type")]
        public string ContentType { get; set; }

        [XmlElement("definition")]
        public string Definition { get; set; }

        [XmlElement("value")]
        public string Value { get; set; }
    }
}
