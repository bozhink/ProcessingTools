// <copyright file="JournalMeta.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Documents.Mongo
{
    using System;
    using System.Text.RegularExpressions;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Common.Attributes;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Documents;

    /// <summary>
    /// Journal meta.
    /// </summary>
    [CollectionName("journalsMeta")]
    public class JournalMeta : IJournalMeta, ICreated, IModified
    {
        /// <inheritdoc/>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string AbbreviatedJournalTitle { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string ArchiveNamePattern { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string FileNamePattern { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string IssnEPub { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string IssnPPub { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string JournalId { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string JournalTitle { get; set; }

        /// <inheritdoc/>
        [BsonIgnore]
        public string Permalink => Regex.Replace(this.AbbreviatedJournalTitle, @"\W+", "_").ToLowerInvariant();

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string PublisherName { get; set; }

        /// <inheritdoc/>
        public string CreatedBy { get; set; }

        /// <inheritdoc/>
        public DateTime CreatedOn { get; set; }

        /// <inheritdoc/>
        public string ModifiedBy { get; set; }

        /// <inheritdoc/>
        public DateTime ModifiedOn { get; set; }
    }
}
