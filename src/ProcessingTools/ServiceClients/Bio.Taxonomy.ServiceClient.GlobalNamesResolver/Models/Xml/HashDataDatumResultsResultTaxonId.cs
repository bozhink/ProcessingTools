namespace ProcessingTools.Bio.Taxonomy.ServiceClient.GlobalNamesResolver.Models.Xml
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "taxon-id", Namespace = "", IsNullable = false)]
    public class HashDataDatumResultsResultTaxonId
    {
        [XmlText]
        public string Value { get; set; }
    }
}
