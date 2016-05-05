namespace ProcessingTools.Harvesters.Models
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "abbreviations")]
    public class AbbreviationsModel
    {
        [XmlElement("abbreviation")]
        public AbbreviationModel[] Abbreviations { get; set; }
    }
}