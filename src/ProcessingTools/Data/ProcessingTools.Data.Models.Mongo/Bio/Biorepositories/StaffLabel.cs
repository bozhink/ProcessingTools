﻿// <copyright file="StaffLabel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Mongo.Bio.Biorepositories
{
    using System;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Contracts.Data.Models.Bio.Biorepositories;

    /// <summary>
    /// Staff label.
    /// </summary>
    public class StaffLabel : IStaffLabelDataModel
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
        public string CityTown { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string Country { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string PostalZipCode { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string PrimaryInstitution { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string StaffMemberFullName { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string StateProvince { get; set; }

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