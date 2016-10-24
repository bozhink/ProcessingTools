namespace ProcessingTools.Harvesters.Models.Abbreviations
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    using ProcessingTools.Constants.Schema;

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = Namespaces.AbbreviationsNamespace)]
    [XmlRoot(ElementName = "abbreviations", Namespace = Namespaces.AbbreviationsNamespace, IsNullable = false)]
    public class AbbreviationsXmlModel
    {
        [XmlElement("abbreviation")]
        public AbbreviationXmlModel[] Abbreviations { get; set; }
    }
}
