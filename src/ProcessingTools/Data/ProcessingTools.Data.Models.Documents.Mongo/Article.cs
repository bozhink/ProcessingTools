// <copyright file="Article.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Documents.Mongo
{
    using System;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Attributes;
    using ProcessingTools.Data.Models.Contracts.Documents.Articles;
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Article
    /// </summary>
    [CollectionName("articles")]
    public class Article : IArticleDataModel, IArticleDetailsDataModel, ICreated, IModified
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Article"/> class.
        /// </summary>
        public Article()
        {
            this.ObjectId = Guid.NewGuid();
        }

        /// <inheritdoc/>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <inheritdoc/>
        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public Guid ObjectId { get; set; }

        /// <inheritdoc/>
        [BsonRequired]
        public string Title { get; set; }

        /// <inheritdoc/>
        public string SubTitle { get; set; }

        /// <inheritdoc/>
        public string Doi { get; set; }

        /// <inheritdoc/>
        public string JournalId { get; set; }

        /// <summary>
        /// Gets or sets the article's journal.
        /// </summary>
        [BsonIgnore]
        public IArticleJournalDataModel Journal { get; set; }

        /// <inheritdoc/>
        public DateTime? PublishedOn { get; set; }

        /// <inheritdoc/>
        public DateTime? AcceptedOn { get; set; }

        /// <inheritdoc/>
        public DateTime? ReceivedOn { get; set; }

        /// <inheritdoc/>
        public string VolumeSeries { get; set; }

        /// <inheritdoc/>
        public string Volume { get; set; }

        /// <inheritdoc/>
        public string Issue { get; set; }

        /// <inheritdoc/>
        public string IssuePart { get; set; }

        /// <inheritdoc/>
        public string ELocationId { get; set; }

        /// <inheritdoc/>
        public string FirstPage { get; set; }

        /// <inheritdoc/>
        public string LastPage { get; set; }

        /// <inheritdoc/>
        public int NumberOfPages { get; set; }

        /// <inheritdoc/>
        public bool IsFinalized { get; set; }

        /// <inheritdoc/>
        public bool IsDeployed { get; set; }

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
