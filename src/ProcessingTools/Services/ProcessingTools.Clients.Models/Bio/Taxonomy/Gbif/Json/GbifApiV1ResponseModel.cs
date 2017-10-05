namespace ProcessingTools.Clients.Models.Bio.Taxonomy.Gbif.Json
{
    using System.Runtime.Serialization;

    [DataContract]
    public class GbifApiV1ResponseModel
    {
        [DataMember(Name = "endOfRecords")]
        public bool EndOfRecords { get; set; }

        [DataMember(Name = "limit")]
        public int Limit { get; set; }

        [DataMember(Name = "offset")]
        public int Offset { get; set; }

        [DataMember(Name = "results")]
        public GbifApiV1ResultModel[] Results { get; set; }
    }
}
