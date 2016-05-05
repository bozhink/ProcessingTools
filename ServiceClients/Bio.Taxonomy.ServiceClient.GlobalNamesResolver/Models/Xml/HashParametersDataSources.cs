namespace ProcessingTools.Bio.Taxonomy.ServiceClient.GlobalNamesResolver.Models.Xml
{
    using System;
    using System.Xml.Serialization;

    // TODO: <data-sources type="array" />
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "data-sources", Namespace = "", IsNullable = false)]
    public class HashParametersDataSources
    {
        [XmlAttribute("type")]
        public string Type { get; set; }
    }
}
