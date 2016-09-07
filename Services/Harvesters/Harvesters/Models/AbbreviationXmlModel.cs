namespace ProcessingTools.Harvesters.Models
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    using Contracts;
    using ProcessingTools.Common.Constants;

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = Namespaces.InternalAbbreviationsNamespace)]
    [XmlRoot(ElementName = "abbreviation", Namespace = Namespaces.InternalAbbreviationsNamespace, IsNullable = false)]
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
