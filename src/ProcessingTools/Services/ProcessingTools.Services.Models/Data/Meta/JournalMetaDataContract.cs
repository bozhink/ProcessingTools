// <copyright file="JournalMetaDataContract.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Data.Meta
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Journal meta.
    /// </summary>
    [DataContract]
    public class JournalMetaDataContract
    {
        /// <summary>
        /// Gets or sets abbreviated journal title.
        /// </summary>
        [DataMember(Name = "abbreviatedJournalTitle")]
        public string AbbreviatedJournalTitle { get; set; }

        /// <summary>
        /// Gets or sets file name pattern.
        /// </summary>
        [DataMember(Name = "fileNamePattern")]
        public string FileNamePattern { get; set; }

        /// <summary>
        /// Gets or sets electronic publication ISSN.
        /// </summary>
        [DataMember(Name = "issnEPub")]
        public string IssnEPub { get; set; }

        /// <summary>
        /// Gets or sets print publication ISSN.
        /// </summary>
        [DataMember(Name = "issnPPub")]
        public string IssnPPub { get; set; }

        /// <summary>
        /// Gets or sets journal id.
        /// </summary>
        [DataMember(Name = "journalId")]
        public string JournalId { get; set; }

        /// <summary>
        /// Gets or sets journal title.
        /// </summary>
        [DataMember(Name = "journalTitle")]
        public string JournalTitle { get; set; }

        /// <summary>
        /// Gets or sets publisher name.
        /// </summary>
        [DataMember(Name = "publisherName")]
        public string PublisherName { get; set; }
    }
}
