namespace ProcessingTools.Clients.Models.Bio.Taxonomy.GlobalNamesResolver.Xml
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "prescore", Namespace = "", IsNullable = false)]
    public class HashDataDatumResultsResultPrescore
    {
        [XmlText]
        public string Value { get; set; }
    }
}
