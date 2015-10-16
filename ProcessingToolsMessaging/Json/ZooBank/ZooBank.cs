namespace ProcessingTools.Globals.Json.ZooBank
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class Author
    {
        [DataMember]
        public string familyname { get; set; }

        [DataMember]
        public string givenname { get; set; }

        [DataMember]
        public string gnubuuid { get; set; }
    }

    [DataContract]
    public class NomenclaturalAct
    {
        [DataMember]
        public string tnuuuid { get; set; }

        [DataMember]
        public string OriginalReferenceUUID { get; set; }

        [DataMember]
        public string protonymuuid { get; set; }

        [DataMember]
        public string label { get; set; }

        [DataMember]
        public string value { get; set; }

        [DataMember]
        public string lsid { get; set; }

        [DataMember]
        public string parentname { get; set; }

        [DataMember]
        public string namestring { get; set; }

        [DataMember]
        public string rankgroup { get; set; }

        [DataMember]
        public string usageauthors { get; set; }

        [DataMember]
        public string taxonnamerankid { get; set; }

        [DataMember]
        public string parentusageuuid { get; set; }

        [DataMember]
        public string cleanprotonym { get; set; }

        [DataMember]
        public string NomenclaturalCode { get; set; }
    }

    [DataContract]
    public class ZooBankRegistration
    {
        [DataMember]
        public string referenceuuid { get; set; }

        [DataMember]
        public string label { get; set; }

        [DataMember]
        public string value { get; set; }

        [DataMember]
        public string authorlist { get; set; }

        [DataMember]
        public string year { get; set; }

        [DataMember]
        public string title { get; set; }

        [DataMember]
        public string citationdetails { get; set; }

        [DataMember]
        public string volume { get; set; }

        [DataMember]
        public string number { get; set; }

        [DataMember]
        public string edition { get; set; }

        [DataMember]
        public string publisher { get; set; }

        [DataMember]
        public string placepublished { get; set; }

        [DataMember]
        public string pagination { get; set; }

        [DataMember]
        public string startpage { get; set; }

        [DataMember]
        public string endpage { get; set; }

        [DataMember]
        public string language { get; set; }

        [DataMember]
        public string languageid { get; set; }

        [DataMember]
        public string referencetype { get; set; }

        [DataMember]
        public string lsid { get; set; }

        [DataMember]
        public string parentreferenceid { get; set; }

        [DataMember]
        public string parentreference { get; set; }

        [DataMember]
        public List<List<Author>> authors { get; set; }

        [DataMember]
        public List<NomenclaturalAct> NomenclaturalActs { get; set; }
    }
}