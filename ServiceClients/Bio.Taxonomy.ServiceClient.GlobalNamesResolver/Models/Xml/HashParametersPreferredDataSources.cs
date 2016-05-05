namespace ProcessingTools.Bio.Taxonomy.ServiceClient.GlobalNamesResolver.Models.Xml
{
    using System;
    using System.Xml.Serialization;

    // TODO: <preferred-data-sources type="array" />
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "preferred-data-sources", Namespace = "", IsNullable = false)]
    public class HashParametersPreferredDataSources
    {
        [XmlAttribute("type")]
        public string Type { get; set; }
    }
}
