namespace ProcessingTools.Bio.Taxonomy.ServiceClient.CatalogueOfLife.Tests.Models
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "accepted_name")]
    public class AcceptedNameModel : SingleRecordModel
    {
        [XmlArray("classification")]
        [XmlArrayItem("taxon", typeof(TaxonModel))]
        public TaxonModel[] Classification { get; set; }

        [XmlArray("child_taxa")]
        [XmlArrayItem("taxon", typeof(TaxonModel))]
        public TaxonModel[] ChildTaxa { get; set; }

        [XmlArray("synonyms")]
        [XmlArrayItem("taxon", typeof(SingleRecordModel))]
        public SingleRecordModel[] Synonyms { get; set; }

        [XmlArray("common_names")]
        [XmlArrayItem("taxon", typeof(CommonNameModel))]
        public CommonNameModel[] CommonNames { get; set; }
    }
}