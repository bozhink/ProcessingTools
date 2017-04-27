namespace ProcessingTools.Bio.Taxonomy.ServiceClient.Gbif.Models
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class GbifApiV1ResultModel
    {
        [DataMember(Name = "accordingTo")]
        public string AccordingTo { get; set; }

        [DataMember(Name = "authorship")]
        public string Authorship { get; set; }

        [DataMember(Name = "basionym")]
        public string Basionym { get; set; }

        [DataMember(Name = "basionymKey")]
        public int BasionymKey { get; set; }

        [DataMember(Name = "canonicalName")]
        public string CanonicalName { get; set; }

        [DataMember(Name = "class")]
        public string Class { get; set; }

        [DataMember(Name = "classKey")]
        public int ClassKey { get; set; }

        [DataMember(Name = "constituentKey")]
        public string ConstituentKey { get; set; }

        [DataMember(Name = "datasetKey")]
        public string DatasetKey { get; set; }

        [DataMember(Name = "family")]
        public string Family { get; set; }

        [DataMember(Name = "familyKey")]
        public int FamilyKey { get; set; }

        [DataMember(Name = "genus")]
        public string Genus { get; set; }

        [DataMember(Name = "genusKey")]
        public int GenusKey { get; set; }

        [DataMember(Name = "issues")]
        public string[] Issues { get; set; }

        [DataMember(Name = "key")]
        public int Key { get; set; }

        [DataMember(Name = "kingdom")]
        public string Kingdom { get; set; }

        [DataMember(Name = "kingdomKey")]
        public int KingdomKey { get; set; }

        [DataMember(Name = "lastCrawled")]
        public DateTime LastCrawled { get; set; }

        [DataMember(Name = "lastInterpreted")]
        public DateTime LastInterpreted { get; set; }

        [DataMember(Name = "nameType")]
        public string NameType { get; set; }

        [DataMember(Name = "nomenclaturalStatus")]
        public object[] NomenclaturalStatus { get; set; }

        [DataMember(Name = "nubKey")]
        public int NubKey { get; set; }

        [DataMember(Name = "numDescendants")]
        public int NumDescendants { get; set; }

        [DataMember(Name = "order")]
        public string Order { get; set; }

        [DataMember(Name = "orderKey")]
        public int OrderKey { get; set; }

        [DataMember(Name = "origin")]
        public string Origin { get; set; }

        [DataMember(Name = "parent")]
        public string Parent { get; set; }

        [DataMember(Name = "parentKey")]
        public int ParentKey { get; set; }

        [DataMember(Name = "phylum")]
        public string Phylum { get; set; }

        [DataMember(Name = "phylumKey")]
        public int PhylumKey { get; set; }

        [DataMember(Name = "publishedIn")]
        public string PublishedIn { get; set; }

        [DataMember(Name = "rank")]
        public string Rank { get; set; }

        [DataMember(Name = "references")]
        public string References { get; set; }

        [DataMember(Name = "remarks")]
        public string Remarks { get; set; }

        [DataMember(Name = "scientificName")]
        public string ScientificName { get; set; }

        [DataMember(Name = "sourceTaxonKey")]
        public int SourceTaxonKey { get; set; }

        [DataMember(Name = "species")]
        public string Species { get; set; }

        [DataMember(Name = "speciesKey")]
        public int SpeciesKey { get; set; }

        [DataMember(Name = "synonym")]
        public bool Synonym { get; set; }

        [DataMember(Name = "taxonID")]
        public string TaxonID { get; set; }

        [DataMember(Name = "taxonomicStatus")]
        public string TaxonomicStatus { get; set; }
    }
}
