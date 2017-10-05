namespace ProcessingTools.Clients.Models.Bio.Taxonomy.GlobalNamesResolver.Xml
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "classification-path", Namespace = "", IsNullable = false)]
    public class HashDataDatumResultsResultClassificationPath
    {
        [XmlText]
        public string Value { get; set; }
    }
}
