namespace ProcessingTools.Models.Json.Pbdb
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class PbdbAllParents
    {
        [DataMember(Name = "records")]
        public ICollection<PbdbSingleName> Records { get; set; }
    }
}