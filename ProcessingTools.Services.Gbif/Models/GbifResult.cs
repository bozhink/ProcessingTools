namespace ProcessingTools.Services.Gbif.Models
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class GbifResult
    {
        [DataMember(Name = "usageKey")]
        public int UsageKey { get; set; }

        [DataMember(Name = "scientificName")]
        public string ScientificName { get; set; }

        [DataMember(Name = "canonicalName")]
        public string CanonicalName { get; set; }

        [DataMember(Name = "rank")]
        public string Rank { get; set; }

        [DataMember(Name = "synonym")]
        public bool Synonym { get; set; }

        [DataMember(Name = "confidence")]
        public int Confidence { get; set; }

        [DataMember(Name = "note")]
        public string Note { get; set; }

        [DataMember(Name = "matchType")]
        public string MatchType { get; set; }

        [DataMember(Name = "alternatives")]
        public IEnumerable<Alternative> Alternatives { get; set; }

        [DataMember(Name = "kingdom")]
        public string Kingdom { get; set; }

        [DataMember(Name = "phylum")]
        public string Phylum { get; set; }

        [DataMember(Name = "order")]
        public string Order { get; set; }

        [DataMember(Name = "family")]
        public string Family { get; set; }

        [DataMember(Name = "genus")]
        public string Genus { get; set; }

        [DataMember(Name = "kingdomKey")]
        public int KingdomKey { get; set; }

        [DataMember(Name = "phylumKey")]
        public int PhylumKey { get; set; }

        [DataMember(Name = "classKey")]
        public int ClassKey { get; set; }

        [DataMember(Name = "orderKey")]
        public int OrderKey { get; set; }

        [DataMember(Name = "familyKey")]
        public int FamilyKey { get; set; }

        [DataMember(Name = "genusKey")]
        public int GenusKey { get; set; }

        [DataMember(Name = "class")]
        public string Class { get; set; }
    }
}