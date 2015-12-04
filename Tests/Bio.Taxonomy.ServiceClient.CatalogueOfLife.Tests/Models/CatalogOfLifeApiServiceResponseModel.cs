namespace ProcessingTools.Bio.Taxonomy.ServiceClient.CatalogueOfLife.Tests.Models
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "results")]
    public class CatalogOfLifeApiServiceResponseModel
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("total_number_of_results")]
        public int TotalNumberOfResults { get; set; }

        [XmlAttribute("number_of_results_returned")]
        public int NumberOfResultsReturned { get; set; }

        [XmlAttribute("start")]
        public int Start { get; set; }

        [XmlAttribute("error_message")]
        public string ErrorMessage { get; set; }

        [XmlAttribute("version")]
        public string Version { get; set; }

        [XmlAttribute("rank")]
        public string Rank { get; set; }

        //[XmlArray]
        [XmlArrayItem("result", typeof(ResultModel))]
        public ResultModel[] Results { get; set; }
    }
}
