namespace ProcessingTools.Bio.Taxonomy.ServiceClient.GlobalNamesResolver.Models.Xml
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "data-source-title", Namespace = "", IsNullable = false)]
    public class HashDataDatumResultsResultDataSourceTitle
    {
        [XmlText]
        public string Value { get; set; }
    }
}
