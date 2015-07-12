using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Base.Json.Gbif
{
	[DataContract]
	public class GbifResult
	{
		[DataMember]
		public int usageKey { get; set; }
		[DataMember]
		public string scientificName { get; set; }
		[DataMember]
		public string canonicalName { get; set; }
		[DataMember]
		public string rank { get; set; }
		[DataMember]
		public bool synonym { get; set; }
		[DataMember]
		public int confidence { get; set; }
		[DataMember]
		public string note { get; set; }
		[DataMember]
		public string matchType { get; set; }
		[DataMember]
		public List<Alternative> alternatives { get; set; }
		[DataMember]
		public string kingdom { get; set; }
		[DataMember]
		public string phylum { get; set; }
		[DataMember]
		public string order { get; set; }
		[DataMember]
		public string family { get; set; }
		[DataMember]
		public string genus { get; set; }
		[DataMember]
		public int kingdomKey { get; set; }
		[DataMember]
		public int phylumKey { get; set; }
		[DataMember]
		public int classKey { get; set; }
		[DataMember]
		public int orderKey { get; set; }
		[DataMember]
		public int familyKey { get; set; }
		[DataMember]
		public int genusKey { get; set; }
		[DataMember]
		public string @class { get; set; }
	}

	[DataContract]
	public class Alternative
	{
		[DataMember]
		public int usageKey { get; set; }
		[DataMember]
		public string scientificName { get; set; }
		[DataMember]
		public string canonicalName { get; set; }
		[DataMember]
		public string rank { get; set; }
		[DataMember]
		public bool synonym { get; set; }
		[DataMember]
		public int confidence { get; set; }
		[DataMember]
		public string note { get; set; }
		[DataMember]
		public string matchType { get; set; }
		[DataMember]
		public string kingdom { get; set; }
		[DataMember]
		public string phylum { get; set; }
		[DataMember]
		public string order { get; set; }
		[DataMember]
		public string family { get; set; }
		[DataMember]
		public string genus { get; set; }
		[DataMember]
		public int kingdomKey { get; set; }
		[DataMember]
		public int phylumKey { get; set; }
		[DataMember]
		public int classKey { get; set; }
		[DataMember]
		public int orderKey { get; set; }
		[DataMember]
		public int familyKey { get; set; }
		[DataMember]
		public int genusKey { get; set; }
		[DataMember]
		public string Class { get; set; }
	}
}
