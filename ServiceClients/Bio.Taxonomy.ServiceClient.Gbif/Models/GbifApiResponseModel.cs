namespace ProcessingTools.Bio.Taxonomy.ServiceClient.Gbif.Models
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class GbifApiResponseModel : Alternative
    {
        [DataMember(Name = "alternatives")]
        public IEnumerable<Alternative> Alternatives { get; set; }
    }
}