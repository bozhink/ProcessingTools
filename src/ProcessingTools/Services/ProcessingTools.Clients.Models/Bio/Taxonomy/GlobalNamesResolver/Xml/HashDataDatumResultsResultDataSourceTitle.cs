namespace ProcessingTools.Clients.Models.Bio.Taxonomy.GlobalNamesResolver.Xml
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
