namespace ProcessingTools.Bio.Taxonomy.ServiceClient.GlobalNamesResolver.Models.Xml
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "datum", Namespace = "", IsNullable = false)]
    public class HashDataDatum
    {
        [XmlElement("is-known-name")]
        public HashDataDatumIsKnowNname isknownname { get; set; }

        public HashDataDatumResults results { get; set; }

        [XmlElement("supplied-name-string")]
        public string suppliednamestring { get; set; }
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public class HashDataDatumIsKnowNname
    {
        [XmlAttribute]
        public string type { get; set; }

        [XmlText]
        public bool Value { get; set; }
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public class HashDataDatumResults
    {
        [XmlElement("result")]
        public HashDataDatumResultsResult[] result { get; set; }

        [XmlAttribute]
        public string type { get; set; }
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public class HashDataDatumResultsResult
    {
        [XmlElement("canonical-form")]
        public string canonicalform { get; set; }

        [XmlElement("classification-path")]
        public HashDataDatumResultsResultClassificationPath classificationpath { get; set; }

        [XmlElement("classification-path-ids")]
        public string classificationpathids { get; set; }

        [XmlElement("classification-path-ranks")]
        public string classificationpathranks { get; set; }

        [XmlElement("current-name-string")]
        public string currentnamestring { get; set; }

        [XmlElement("current-taxon-id")]
        public uint currenttaxonid { get; set; }

        [XmlElement("data-source-id")]
        public HashDataDatumResultsResultDataSourceId datasourceid { get; set; }

        [XmlElement("data-source-title")]
        public string datasourcetitle { get; set; }

        [XmlElement("edit-distance")]
        public HashDataDatumResultsResultEditDistance editdistance { get; set; }

        [XmlElement("global-id")]
        public string globalid { get; set; }

        [XmlElement("gni-uuid")]
        public string gniuuid { get; set; }

        [XmlElement("local-id")]
        public string localid { get; set; }

        [XmlElement("match-type")]
        public HashDataDatumResultsResultMatchType matchtype { get; set; }

        [XmlElement("name-string")]
        public string namestring { get; set; }

        public string prescore { get; set; }

        [XmlElement("taxon-id")]
        public string taxonid { get; set; }

        public string url { get; set; }
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public class HashDataDatumResultsResultClassificationPath
    {
        [XmlAttribute]
        public bool nil { get; set; }

        [XmlText]
        public string Value { get; set; }
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public class HashDataDatumResultsResultDataSourceId
    {
        [XmlAttribute]
        public string type { get; set; }

        [XmlText]
        public byte Value { get; set; }
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public class HashDataDatumResultsResultEditDistance
    {
        [XmlAttribute]
        public string type { get; set; }

        [XmlText]
        public byte Value { get; set; }
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public class HashDataDatumResultsResultMatchType
    {
        [XmlAttribute]
        public string type { get; set; }

        [XmlText]
        public byte Value { get; set; }
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public class HashDataDatumResultsResultScore
    {
        [XmlAttribute]
        public string type { get; set; }

        [XmlText]
        public decimal Value { get; set; }
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public class HashDataSources
    {
        [XmlAttribute]
        public string type { get; set; }
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public class HashParameters
    {
        [XmlElement("best-match-only")]
        public HashParametersBestMatchOnly bestmatchonly { get; set; }

        [XmlElement("data-sources")]
        public HashParametersDataSources datasources { get; set; }

        [XmlElement("header-only")]
        public HashParametersHeaderOnly headeronly { get; set; }

        [XmlElement("preferred-data-sources")]
        public HashParametersPreferredDataSources preferreddatasources { get; set; }

        [XmlElement("resolve-once")]
        public HashParametersResolveOnce resolveonce { get; set; }

        [XmlElement("with-canonical-ranks")]
        public HashParametersWithCanonicalRanks withcanonicalranks { get; set; }

        [XmlElement("with-context")]
        public HashParametersWithContext withcontext { get; set; }

        [XmlElement("with-vernaculars")]
        public HashParametersWithVernaculars withvernaculars { get; set; }
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public class HashParametersBestMatchOnly
    {
        [XmlAttribute]
        public string type { get; set; }

        [XmlText]
        public bool Value { get; set; }
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public class HashParametersDataSources
    {
        [XmlAttribute]
        public string type { get; set; }
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public class HashParametersHeaderOnly
    {
        [XmlAttribute]
        public string type { get; set; }

        [XmlText]
        public bool Value { get; set; }
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public class HashParametersPreferredDataSources
    {
        [XmlAttribute]
        public string type { get; set; }
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public class HashParametersResolveOnce
    {
        [XmlAttribute]
        public string type { get; set; }

        [XmlText]
        public bool Value { get; set; }
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public class HashParametersWithCanonicalRanks
    {
        [XmlAttribute]
        public string type { get; set; }

        [XmlText]
        public bool Value { get; set; }
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public class HashParametersWithContext
    {
        [XmlAttribute]
        public string type { get; set; }

        [XmlText]
        public bool Value { get; set; }
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public class HashParametersWithVernaculars
    {
        [XmlAttribute]
        public string type { get; set; }

        [XmlText]
        public bool Value { get; set; }
    }
}
