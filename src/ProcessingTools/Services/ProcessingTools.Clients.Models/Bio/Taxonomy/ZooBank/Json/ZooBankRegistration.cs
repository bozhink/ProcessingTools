// <copyright file="ZooBankRegistration.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.ZooBank.Json
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// ZooBank record.
    /// </summary>
    [DataContract]
    public class ZooBankRegistration
    {
        /// <summary>
        /// Gets or sets reference uuid.
        /// </summary>
        [DataMember(Name = "referenceuuid")]
        public string ReferenceUuid { get; set; }

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
        /// Gets or sets author list.
        /// </summary>
        [DataMember(Name = "authorlist")]
        public string AuthorList { get; set; }

        /// <summary>
        /// Gets or sets year.
        /// </summary>
        [DataMember(Name = "year")]
        public string Year { get; set; }

        /// <summary>
        /// Gets or sets title.
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets citation details.
        /// </summary>
        [DataMember(Name = "citationdetails")]
        public string CitationDetails { get; set; }

        /// <summary>
        /// Gets or sets volume.
        /// </summary>
        [DataMember(Name = "volume")]
        public string Volume { get; set; }

        /// <summary>
        /// Gets or sets number.
        /// </summary>
        [DataMember(Name = "number")]
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets edition.
        /// </summary>
        [DataMember(Name = "edition")]
        public string Edition { get; set; }

        /// <summary>
        /// Gets or sets publisher.
        /// </summary>
        [DataMember(Name = "publisher")]
        public string Publisher { get; set; }

        /// <summary>
        /// Gets or sets place published.
        /// </summary>
        [DataMember(Name = "placepublished")]
        public string PlacePublished { get; set; }

        /// <summary>
        /// Gets or sets pagination.
        /// </summary>
        [DataMember(Name = "pagination")]
        public string Pagination { get; set; }

        /// <summary>
        /// Gets or sets start page.
        /// </summary>
        [DataMember(Name = "startpage")]
        public string StartPage { get; set; }

        /// <summary>
        /// Gets or sets end page.
        /// </summary>
        [DataMember(Name = "endpage")]
        public string EndPage { get; set; }

        /// <summary>
        /// Gets or sets language.
        /// </summary>
        [DataMember(Name = "language")]
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets language id.
        /// </summary>
        [DataMember(Name = "languageid")]
        public string LanguageId { get; set; }

        /// <summary>
        /// Gets or sets reference type.
        /// </summary>
        [DataMember(Name = "referencetype")]
        public string ReferenceType { get; set; }

        /// <summary>
        /// Gets or sets lsid.
        /// </summary>
        [DataMember(Name = "lsid")]
        public string Lsid { get; set; }

        /// <summary>
        /// Gets or sets parent reference id.
        /// </summary>
        [DataMember(Name = "parentreferenceid")]
        public string ParentReferenceId { get; set; }

        /// <summary>
        /// Gets or sets parent reference.
        /// </summary>
        [DataMember(Name = "parentreference")]
        public string ParentReference { get; set; }

        /// <summary>
        /// Gets or sets authors.
        /// </summary>
        [DataMember(Name = "authors")]
        public ICollection<ICollection<Author>> Authors { get; set; }

        /// <summary>
        /// Gets or sets nomenclatural acts.
        /// </summary>
        [DataMember(Name = "NomenclaturalActs")]
        public ICollection<NomenclaturalAct> NomenclaturalActs { get; set; }
    }
}
