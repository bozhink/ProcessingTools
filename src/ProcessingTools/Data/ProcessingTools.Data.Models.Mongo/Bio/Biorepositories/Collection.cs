// <copyright file="Collection.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Mongo.Bio.Biorepositories
{
    using System;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Contracts.Data.Models.Bio.Biorepositories;

    /// <summary>
    /// Collection.
    /// </summary>
    public class Collection : ICollectionDataModel
    {
        /// <inheritdoc/>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }

        /// <inheritdoc/>
        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public Guid ObjectId { get; set; } = Guid.NewGuid();

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string AccessEligibilityAndRules { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string AccessionStatus { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string CollectionCode { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string CollectionContentType { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string CollectionDescription { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string CollectionName { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string CoolUri { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string InstitutionCode { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string InstitutionName { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string Lsid { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string PreservationType { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string PrimaryContact { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string StatusOfCollection { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string Url { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string UrlForCollection { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string UrlForCollectionSpecimenCatalog { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string UrlForCollectionWebservices { get; set; }

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
