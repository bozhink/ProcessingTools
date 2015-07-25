using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ProcessingTools.Base.Json.Pbdb
{
    [DataContract]
    public class PbdbSingleName
    {
        [DataMember]
        public int oid { get; set; }
        [DataMember]
        public int gid { get; set; }
        [DataMember]
        public string typ { get; set; }
        [DataMember]
        public int rnk { get; set; }
        [DataMember]
        public string nam { get; set; }
        [DataMember]
        public string nm2 { get; set; }
        [DataMember]
        public string sta { get; set; }
        [DataMember]
        public int par { get; set; }
        [DataMember]
        public int snr { get; set; }
        [DataMember]
        public List<int> rid { get; set; }
        [DataMember]
        public int ext { get; set; }
    }

    [DataContract]
    public class PbdbAllParents
    {
        [DataMember]
        public List<PbdbSingleName> records { get; set; }
    }
}
