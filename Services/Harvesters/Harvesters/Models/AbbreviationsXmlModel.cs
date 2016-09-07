namespace ProcessingTools.Harvesters.Models
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    using ProcessingTools.Common.Constants;

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = Namespaces.InternalAbbreviationsNamespace)]
    [XmlRoot(ElementName = "abbreviations", Namespace = Namespaces.InternalAbbreviationsNamespace, IsNullable = false)]
    public class AbbreviationsXmlModel
    {
        [XmlElement("abbreviation")]
        public AbbreviationXmlModel[] Abbreviations { get; set; }
    }
}
