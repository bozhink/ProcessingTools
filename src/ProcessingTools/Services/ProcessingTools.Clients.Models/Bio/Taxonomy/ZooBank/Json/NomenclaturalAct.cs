// <copyright file="NomenclaturalAct.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.ZooBank.Json
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Nomenclatural Act.
    /// </summary>
    [DataContract]
    public class NomenclaturalAct
    {
        /// <summary>
        /// Gets or sets tnu uuid.
        /// </summary>
        [DataMember(Name = "tnuuuid")]
        public string TnuUuid { get; set; }

        /// <summary>
        /// Gets or sets original reference uuid.
        /// </summary>
        [DataMember(Name = "OriginalReferenceUUID")]
        public string OriginalReferenceUUID { get; set; }

        /// <summary>
        /// Gets or sets protonym uuid.
        /// </summary>
        [DataMember(Name = "protonymuuid")]
        public string ProtonymUuid { get; set; }

        /// <summary>
        /// Gets or sets label.
        /// </summary>
        [DataMember(Name = "label")]
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets value.
        /// </summary>
        [DataMember(Name = "value")]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets lsid.
        /// </summary>
        [DataMember(Name = "lsid")]
        public string Lsid { get; set; }

        /// <summary>
        /// Gets or sets parent name.
        /// </summary>
        [DataMember(Name = "parentname")]
        public string Parentname { get; set; }

        /// <summary>
        /// Gets or sets name string.
        /// </summary>
        [DataMember(Name = "namestring")]
        public string NameString { get; set; }

        /// <summary>
        /// Gets or sets rank group.
        /// </summary>
        [DataMember(Name = "rankgroup")]
        public string RankGroup { get; set; }

        /// <summary>
        /// Gets or sets usage authors.
        /// </summary>
        [DataMember(Name = "usageauthors")]
        public string UsageAuthors { get; set; }

        /// <summary>
        /// Gets or sets taxon name rank id.
        /// </summary>
        [DataMember(Name = "taxonnamerankid")]
        public string TaxonNameRankId { get; set; }

        /// <summary>
        /// Gets or sets parent usage uuid.
        /// </summary>
        [DataMember(Name = "parentusageuuid")]
        public string ParentUsageUuid { get; set; }

        /// <summary>
        /// Gets or sets clean protonym.
        /// </summary>
        [DataMember(Name = "cleanprotonym")]
        public string CleanProtonym { get; set; }

        /// <summary>
        /// Gets or sets nomenclatural code.
        /// </summary>
        [DataMember(Name = "NomenclaturalCode")]
        public string NomenclaturalCode { get; set; }
    }
}
