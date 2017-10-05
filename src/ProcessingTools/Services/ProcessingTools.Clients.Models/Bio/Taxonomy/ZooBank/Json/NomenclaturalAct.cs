namespace ProcessingTools.Clients.Models.Bio.Taxonomy.ZooBank.Json
{
    using System.Runtime.Serialization;

    [DataContract]
    public class NomenclaturalAct
    {
        [DataMember(Name = "tnuuuid")]
        public string TnuUuid { get; set; }

        [DataMember(Name = "OriginalReferenceUUID")]
        public string OriginalReferenceUUID { get; set; }

        [DataMember(Name = "protonymuuid")]
        public string ProtonymUuid { get; set; }

        [DataMember(Name = "label")]
        public string Label { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }

        [DataMember(Name = "lsid")]
        public string Lsid { get; set; }

        [DataMember(Name = "parentname")]
        public string Parentname { get; set; }

        [DataMember(Name = "namestring")]
        public string NameString { get; set; }

        [DataMember(Name = "rankgroup")]
        public string RankGroup { get; set; }

        [DataMember(Name = "usageauthors")]
        public string UsageAuthors { get; set; }

        [DataMember(Name = "taxonnamerankid")]
        public string TaxonNameRankId { get; set; }

        [DataMember(Name = "parentusageuuid")]
        public string ParentUsageUuid { get; set; }

        [DataMember(Name = "cleanprotonym")]
        public string CleanProtonym { get; set; }

        [DataMember(Name = "NomenclaturalCode")]
        public string NomenclaturalCode { get; set; }
    }
}