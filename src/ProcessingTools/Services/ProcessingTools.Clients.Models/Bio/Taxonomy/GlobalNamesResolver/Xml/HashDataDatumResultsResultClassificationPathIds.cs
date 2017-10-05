namespace ProcessingTools.Clients.Models.Bio.Taxonomy.GlobalNamesResolver.Xml
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "classification-path-ids", Namespace = "", IsNullable = false)]
    public class HashDataDatumResultsResultClassificationPathIds
    {
        [XmlText]
        public string Value { get; set; }
    }
}
