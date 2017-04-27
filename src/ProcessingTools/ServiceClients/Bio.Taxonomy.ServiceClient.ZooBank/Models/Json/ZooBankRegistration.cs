namespace ProcessingTools.Bio.Taxonomy.ServiceClient.ZooBank.Models.Json
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class ZooBankRegistration
    {
        [DataMember(Name = "referenceuuid")]
        public string ReferenceUuid { get; set; }

        [DataMember(Name = "label")]
        public string Label { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }

        [DataMember(Name = "authorlist")]
        public string AuthorList { get; set; }

        [DataMember(Name = "year")]
        public string Year { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "citationdetails")]
        public string CitationDetails { get; set; }

        [DataMember(Name = "volume")]
        public string Volume { get; set; }

        [DataMember(Name = "number")]
        public string Number { get; set; }

        [DataMember(Name = "edition")]
        public string Edition { get; set; }

        [DataMember(Name = "publisher")]
        public string Publisher { get; set; }

        [DataMember(Name = "placepublished")]
        public string PlacePublished { get; set; }

        [DataMember(Name = "pagination")]
        public string Pagination { get; set; }

        [DataMember(Name = "startpage")]
        public string StartPage { get; set; }

        [DataMember(Name = "endpage")]
        public string EndPage { get; set; }

        [DataMember(Name = "language")]
        public string Language { get; set; }

        [DataMember(Name = "languageid")]
        public string LanguageId { get; set; }

        [DataMember(Name = "referencetype")]
        public string ReferenceType { get; set; }

        [DataMember(Name = "lsid")]
        public string Lsid { get; set; }

        [DataMember(Name = "parentreferenceid")]
        public string ParentReferenceId { get; set; }

        [DataMember(Name = "parentreference")]
        public string ParentReference { get; set; }

        [DataMember(Name = "authors")]
        public ICollection<ICollection<Author>> Authors { get; set; }

        [DataMember(Name = "NomenclaturalActs")]
        public ICollection<NomenclaturalAct> NomenclaturalActs { get; set; }
    }
}