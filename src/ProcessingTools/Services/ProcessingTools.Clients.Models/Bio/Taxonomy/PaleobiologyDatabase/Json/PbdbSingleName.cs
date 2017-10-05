namespace ProcessingTools.Clients.Models.Bio.Taxonomy.PaleobiologyDatabase.Json
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class PbdbSingleName
    {
        [DataMember(Name = "oid")]
        public int ObjectId { get; set; }

        [DataMember(Name = "gid")]
        public int GroupId { get; set; }

        [DataMember(Name = "typ")]
        public string Type { get; set; }

        [DataMember(Name = "rnk")]
        public int Rank { get; set; }

        [DataMember(Name = "nam")]
        public string Name { get; set; }

        [DataMember(Name = "nm2")]
        public string Name2 { get; set; }

        [DataMember(Name = "sta")]
        public string Sta { get; set; }

        [DataMember(Name = "par")]
        public int Par { get; set; }

        [DataMember(Name = "snr")]
        public int Snr { get; set; }

        [DataMember(Name = "rid")]
        public List<int> Rid { get; set; }

        [DataMember(Name = "ext")]
        public int Ext { get; set; }
    }
}