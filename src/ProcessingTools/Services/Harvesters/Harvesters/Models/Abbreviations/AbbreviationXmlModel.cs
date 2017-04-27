namespace ProcessingTools.Harvesters.Models.Abbreviations
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;
    using Contracts.Models.Abbreviations;
    using ProcessingTools.Constants.Schema;

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = Namespaces.AbbreviationsNamespace)]
    [XmlRoot(ElementName = "abbreviation", Namespace = Namespaces.AbbreviationsNamespace, IsNullable = false)]
    public class AbbreviationXmlModel : IAbbreviationModel
    {
        [XmlAttribute("content-type")]
        public string ContentType { get; set; }

        [XmlElement("definition")]
        public string Definition { get; set; }

        [XmlElement("value")]
        public string Value { get; set; }

        public override int GetHashCode()
        {
            return (this.ContentType + this.Value + this.Definition).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.GetHashCode() == obj.GetHashCode();
        }
    }
}
