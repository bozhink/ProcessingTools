namespace ProcessingTools.Globals.Json.Pbdb
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class PbdbAllParents
    {
        [DataMember(Name = "records")]
        public List<PbdbSingleName> Records { get; set; }
    }
}