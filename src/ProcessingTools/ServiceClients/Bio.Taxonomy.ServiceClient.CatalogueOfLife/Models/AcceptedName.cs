namespace ProcessingTools.Bio.Taxonomy.ServiceClient.CatalogueOfLife.Models
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "accepted_name")]
    public class AcceptedName : SingleRecord
    {
        [XmlArray("classification")]
        [XmlArrayItem("taxon", typeof(Taxon))]
        public Taxon[] Classification { get; set; }

        [XmlArray("child_taxa")]
        [XmlArrayItem("taxon", typeof(Taxon))]
        public Taxon[] ChildTaxa { get; set; }

        [XmlArray("synonyms")]
        [XmlArrayItem("taxon", typeof(SingleRecord))]
        public SingleRecord[] Synonyms { get; set; }

        [XmlArray("common_names")]
        [XmlArrayItem("taxon", typeof(CommonName))]
        public CommonName[] CommonNames { get; set; }
    }
}