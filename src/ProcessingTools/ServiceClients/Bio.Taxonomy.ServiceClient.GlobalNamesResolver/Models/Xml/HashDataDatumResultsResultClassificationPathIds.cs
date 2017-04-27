namespace ProcessingTools.Bio.Taxonomy.ServiceClient.GlobalNamesResolver.Models.Xml
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
