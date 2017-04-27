namespace ProcessingTools.Bio.Taxonomy.ServiceClient.CatalogueOfLife.Models
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "result")]
    public class Result : AcceptedName
    {
        [XmlElement("accepted_name", typeof(Result))]
        public Result AcceptedName { get; set; }
    }
}