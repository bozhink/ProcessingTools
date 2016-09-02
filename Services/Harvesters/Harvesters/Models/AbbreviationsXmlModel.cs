namespace ProcessingTools.Harvesters.Models
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "abbreviations")]
    public class AbbreviationsXmlModel
    {
        [XmlElement("abbreviation")]
        public AbbreviationXmlModel[] Abbreviations { get; set; }
    }
}