﻿namespace ProcessingTools.Clients.Models.Bio.Taxonomy.GlobalNamesResolver.Xml
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "current-taxon-id", Namespace = "", IsNullable = false)]
    public class HashDataDatumResultsResultCurrentTaxonId
    {
        [XmlText]
        public string Value { get; set; }
    }
}
