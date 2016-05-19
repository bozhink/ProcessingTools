namespace ProcessingTools.Bio.Taxonomy.ServiceClient.GlobalNamesResolver.Models.Xml
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "classification-path-ranks", Namespace = "", IsNullable = false)]
    public class HashDataDatumResultsResultClassificationPathRanks
    {
        [XmlText]
        public string Value { get; set; }
    }
}
