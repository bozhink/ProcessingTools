namespace ProcessingTools.Clients.Models.Bio.Taxonomy.GlobalNamesResolver.Xml
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "parameters", Namespace = "", IsNullable = false)]
    public class HashParameters
    {
        [XmlElement("with-context")]
        public HashParametersWithContext WithContext { get; set; }

        [XmlElement("header-only")]
        public HashParametersHeaderOnly HeaderOnly { get; set; }

        [XmlElement("with-canonical-ranks")]
        public HashParametersWithCanonicalRanks WithCanonicalRanks { get; set; }

        [XmlElement("with-vernaculars")]
        public HashParametersWithVernaculars WithVernaculars { get; set; }

        [XmlElement("best-match-only")]
        public HashParametersBestMatchOnly BestMatchOnly { get; set; }

        [XmlElement("data-sources")]
        public HashParametersDataSources DataSources { get; set; }

        [XmlElement("preferred-data-sources")]
        public HashParametersPreferredDataSources PreferredDataSources { get; set; }

        [XmlElement("resolve-once")]
        public HashParametersResolveOnce ResolveOnce { get; set; }
    }
}
