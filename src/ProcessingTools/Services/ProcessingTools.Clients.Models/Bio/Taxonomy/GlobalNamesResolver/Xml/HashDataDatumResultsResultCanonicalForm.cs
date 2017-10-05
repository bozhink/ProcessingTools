namespace ProcessingTools.Clients.Models.Bio.Taxonomy.GlobalNamesResolver.Xml
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "canonical-form", Namespace = "", IsNullable = false)]
    public class HashDataDatumResultsResultCanonicalForm
    {
        [XmlText]
        public string Value { get; set; }
    }
}
