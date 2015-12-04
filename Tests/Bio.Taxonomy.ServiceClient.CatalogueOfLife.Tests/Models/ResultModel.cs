namespace ProcessingTools.Bio.Taxonomy.ServiceClient.CatalogueOfLife.Tests.Models
{
    using System;
    using System.Xml;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "result")]
    public class ResultModel : AcceptedNameModel
    {
        [XmlElement("accepted_name", typeof(ResultModel))]
        public ResultModel AcceptedName { get; set; }
    }
}