namespace ProcessingTools.Clients.Models.Bio.Taxonomy.Gbif.Json
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class GbifApiV09ResponseModel : Alternative
    {
        [DataMember(Name = "alternatives")]
        public IEnumerable<Alternative> Alternatives { get; set; }
    }
}